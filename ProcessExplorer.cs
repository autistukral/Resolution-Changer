using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Text.Json; // For JSON serialization/deserialization
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Resolution_Changer
{
    public partial class ProcessExplorer : Form
    {
        private const string RegistryKeyPath = @"Software\AutistukralResolutionChanger";
        private const string RegistryValueApps = "AppsList";

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

        // Code to make the application top bar colored by the windows
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        public List<string> ProcessesPaths = new List<string>();

        public ProcessExplorer()
        {
            InitializeComponent();
            LoadProcesses();
        }

        private void ProcessExplorer_Load(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private Dictionary<string, Icon> LoadProcesses()
        {
            // Clear existing items
            listView_processExplorer.Items.Clear();
            imageListProcesses.Images.Clear();

            // Use a HashSet to track process names
            var processNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Dictionary to store process icons
            var processIcons = new Dictionary<string, Icon>();

            // get all processes
            var processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                try
                {
                    // Get the process's executable path
                    var path = process.MainModule?.FileName;

                    if (!string.IsNullOrEmpty(path)
                        && !path.StartsWith(@"C:\Windows", StringComparison.OrdinalIgnoreCase)
                        && processNames.Add(process.ProcessName)) // Add returns false if the name already exists
                    {
                        // Get the process icon
                        Icon icon = Icon.ExtractAssociatedIcon(path);
                        if (icon != null)
                        {
                            // Add the icon to the ImageList
                            imageListProcesses.Images.Add(process.ProcessName, icon);
                            processIcons[process.ProcessName] = icon;
                        }

                        // Add process name and ID to the ListView
                        var item = new ListViewItem(process.ProcessName);
                        item.SubItems.Add(process.Id.ToString());
                        item.Tag = process; // Store the process object for later use


                        // set the icons
                        item.ImageKey = process.ProcessName;

                        // add the item to listview
                        listView_processExplorer.Items.Add(item);
                    }
                }
                catch
                {
                    // ignore what cannot be processed
                }
            }

            return processIcons;
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
            LoadProcesses();
        }
    }
}
