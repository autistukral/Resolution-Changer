using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Text.Json;
using System.Drawing.Imaging;

namespace Resolution_Changer
{
    public partial class ProcessExplorer : Form
    {
        private const string RegistryKeyPath = @"Software\AutistukralResolutionChanger";
        private const string RegistryValueApps = "AppsList";

        private const int GCL_HICON = -14;
        private const int GCL_HICONSM = -34;
        private const uint SHGFI_ICON = 0x000000100;
        private const uint SHGFI_LARGEICON = 0x000000000;

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes,
        ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);


        // Code to make the application top bar colored by the windows
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        private string GetIconSaveDirectory()
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appFolder = Path.Combine(localAppData, "AutistukralResolutionChanger");

            // ensure the directory exists
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            return appFolder;
        }

        public List<string> ProcessesPaths = new List<string>();

        public ProcessExplorer()
        {
            InitializeComponent();

            LoadProcesses(listView_processExplorer, imageListProcesses);
        }

        private void ProcessExplorer_Load(object sender, EventArgs e)
        {
            LoadProcesses(listView_processExplorer, imageListProcesses);
        }

        public Dictionary<string, Icon> LoadProcesses(ListView listView, ImageList imageList)
        {
            // Clear existing items
            //listView_processExplorer.Items.Clear();
            listView.Items.Clear();
            //imageListProcesses.Images.Clear();
            imageList.Images.Clear();

            // Use a HashSet to track process names
            var processNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Dictionary to store process icons
            var processIcons = new Dictionary<string, Icon>();

            // Enumerate all processes
            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    string processName = process.ProcessName;
                    if (!processNames.Add(processName)) continue;

                    string exePath = null;
                    try { exePath = process.MainModule?.FileName; } catch { }

                    Icon icon = ExtractIconFromPath(exePath) ?? ExtractIconFromWindow(process);

                    if (icon == null) icon = SystemIcons.Application;

                    imageList.Images.Add(processName, icon);
                    processIcons[processName] = icon;

                    var item = new ListViewItem(processName) { ImageKey = processName };
                    listView.Items.Add(item);
                }
                catch
                {
                    // Ignore inaccessible processes
                }
            }

            return processIcons;
        }

        private Icon ExtractIconFromPath(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;

            var shinfo = new SHFILEINFO();
            IntPtr hImg = SHGetFileInfo(path, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo),
                SHGFI_ICON | SHGFI_LARGEICON);

            return hImg != IntPtr.Zero ? Icon.FromHandle(shinfo.hIcon) : null;
        }

        private Icon ExtractIconFromWindow(Process process)
        {
            IntPtr hWnd = GetProcessMainWindow(process);
            if (hWnd == IntPtr.Zero) return null;

            IntPtr iconHandle = GetClassLongPtr(hWnd, GCL_HICON);
            if (iconHandle == IntPtr.Zero)
            {
                iconHandle = GetClassLongPtr(hWnd, GCL_HICONSM);
            }

            return iconHandle != IntPtr.Zero ? Icon.FromHandle(iconHandle) : null;
        }

        private IntPtr GetProcessMainWindow(Process process)
        {
            IntPtr windowHandle = IntPtr.Zero;

            EnumWindows((hWnd, lParam) =>
            {
                GetWindowThreadProcessId(hWnd, out int windowProcessId);
                if (windowProcessId == process.Id)
                {
                    windowHandle = hWnd;
                    return false; // Stop enumeration
                }
                return true;
            }, IntPtr.Zero);

            return windowHandle;
        }


        private void SaveSelectedItemToRegistry(string selectedItem)
        {
            // read existing list from reg
            List<string> procList = ReadListFromRegistry();

            // check dups
            if (!procList.Contains(selectedItem))
            {
                procList.Add(selectedItem);



                SaveListToRegistry(procList);
            }
        }

        private List<string> ReadListFromRegistry()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
            {
                if (key != null)
                {
                    string serializedList = key.GetValue(RegistryValueApps)?.ToString();
                    if (!string.IsNullOrEmpty(serializedList))
                    {
                        return JsonSerializer.Deserialize<List<string>>(serializedList);
                    }
                }
            }

            return new List<string>(); // Return an empty list if no value is found
        }

        private void SaveListToRegistry(List<string> procList)
        {
            string serializedList = JsonSerializer.Serialize(procList);

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath))
            {
                key?.SetValue(RegistryValueApps, serializedList);
            }
        }

        static int GetNextValue(List<string> items)
        {
            // Get values
            var values = items
                .Select(item => item.Split('@', '#').LastOrDefault())
                .Where(value => int.TryParse(value, out _))
                .Select(int.Parse)
                .ToList();

            // check for lowest missing value
            for (int i = 1; i <= 4; i++)
            {
                if (!values.Contains(i))
                    return i;
            }

            // if all values are present return 4
            return 4;
        }

        private void SaveIcons(Dictionary<string, Icon> processIcons)
        {
            string saveDirectory = GetIconSaveDirectory();

            foreach (var kvp in processIcons)
            {
                string processName = kvp.Key;
                Icon icon = kvp.Value;

                // save each icon as an PNG file
                string filePath = Path.Combine(saveDirectory, $"{processName}.png");
                if (!File.Exists(filePath))
                {
                    using (var iconBitmap = icon.ToBitmap())
                    {
                        iconBitmap.Save(filePath, ImageFormat.Png);
                    }
                }
            }
        }

        private void btn_addProcessToList_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure an item is selected
                if (listView_processExplorer.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select a process.");
                    return;
                }

                // read existing list from reg
                List<string> procList = ReadListFromRegistry();

                if (procList.Count >= 4)
                {
                    MessageBox.Show("You can add a maximum of 4 apps.");
                    return;
                }

                // Get selected process
                string selectedApp = listView_processExplorer.SelectedItems[0].Text;

                // Get icons from the previously loaded dictionary
                if (!imageListProcesses.Images.ContainsKey(selectedApp))
                {
                    MessageBox.Show("Could not retrieve the icon for the selected app.");
                    return;
                }

                // Convert the Image to an Icon
                var processIcons = new Dictionary<string, Icon>();
                using (var stream = new MemoryStream())
                {
                    imageListProcesses.Images[selectedApp].Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Position = 0;
                    using (var bitmap = new Bitmap(stream))
                    {
                        processIcons[selectedApp] = Icon.FromHandle(bitmap.GetHicon());
                    }
                };

                SaveIcons(processIcons);

                // Generate the name for registry entry
                string nameForRegistry = selectedApp + "@1" + $"#{GetNextValue(procList)}";

                // Save the process name with tag to the registry
                SaveSelectedItemToRegistry(nameForRegistry);

                label_addedProcess.Text = "Added " + selectedApp + " to the list";
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_refreshList_Click(object sender, EventArgs e)
        {
            LoadProcesses(listView_processExplorer, imageListProcesses);
        }
    }
}
