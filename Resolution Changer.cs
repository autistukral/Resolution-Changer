using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Text.Json;
using System.Management;

namespace Resolution_Changer
{
    public partial class ResolutionChanger : Form
    {
        private const string RegistryKeyPath = @"Software\AutistukralResolutionChanger";
        private const string RegistryValueName1 = "Resolution1";
        private const string RegistryValueName2 = "Resolution2";
        private const string RegistryValueName3 = "Resolution3";
        private const string RegistryValueApps = "AppsList";
        private const string appName = "Autistukral Resolution Changer";
        string fileVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;

        int primWidth = Screen.PrimaryScreen.Bounds.Width; // Width of primary monitor
        int primHeight = Screen.PrimaryScreen.Bounds.Height; // Height of primary monitor

        private List<string> targetProcesses; // List of process names to monitor
        private HashSet<string> activeProcesses; // Tracks currently running target processes

        private System.Windows.Forms.Timer updateTimer;

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

        public class MenuColorTable : ProfessionalColorTable
        {
            public MenuColorTable()
            {
                UseSystemColors = false;
            }
            public override Color MenuBorder
            {
                get { return Color.FromArgb(46, 48, 59); }
            }
            public override Color MenuItemBorder
            {
                get { return Color.FromArgb(200, 200, 200); }
            }
            public override Color MenuItemSelected
            {
                get { return Color.FromArgb(69, 72, 90); }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.FromArgb(69, 72, 90); }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.FromArgb(69, 72, 90); }
            }
            public override Color CheckBackground
            {
                get { return Color.FromArgb(46, 48, 59); }
            }
            public override Color CheckPressedBackground
            {
                get { return Color.FromArgb(46, 48, 59); }
            }
            public override Color CheckSelectedBackground
            {
                get { return Color.FromArgb(46, 48, 59); }
            }
            public override Color ImageMarginGradientBegin
            {
                get { return Color.FromArgb(36, 38, 49); }
            }
            public override Color ImageMarginGradientMiddle
            {
                get { return Color.FromArgb(36, 38, 49); }
            }
            public override Color ImageMarginGradientEnd
            {
                get { return Color.FromArgb(36, 38, 49); }
            }
            public override Color GripLight
            {
                get { return Color.FromArgb(200, 200, 200); }
            }
        }

        private void CustomComboBox()
        {
            // Set the DrawMode to OwnerDrawFixed
            availableResolutionsCB.DrawMode = DrawMode.OwnerDrawFixed;
            availableResolutionsCB.DrawItem += new DrawItemEventHandler(resCB_DrawItem);

            availableResolutionsCB2.DrawMode = DrawMode.OwnerDrawFixed;
            availableResolutionsCB2.DrawItem += new DrawItemEventHandler(resCB_DrawItem);

            availableResolutionsCB3.DrawMode = DrawMode.OwnerDrawFixed;
            availableResolutionsCB3.DrawItem += new DrawItemEventHandler(resCB_DrawItem);
        }

        private void resCB_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Check if the item index is valid
            if (e.Index < 0) return;

            // Get the ComboBox control
            System.Windows.Forms.ComboBox comboBox = (System.Windows.Forms.ComboBox)sender;

            // Get the item to be drawn
            string item = comboBox.Items[e.Index].ToString();

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(comboBox.BackColor), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(comboBox.BackColor), e.Bounds);
            }

            // Set the text color for the item
            e.Graphics.DrawString(item, e.Font, new SolidBrush(comboBox.ForeColor), e.Bounds);

            // Draw the focus rectangle if the item has focus
            e.DrawFocusRectangle();
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 32;
            private const int CCHFORMNAME = 32;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string dmDeviceName;
            public ushort dmSpecVersion;
            public ushort dmDriverVersion;
            public ushort dmSize;
            public ushort dmDriverExtra;
            public uint dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public uint dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
            public string dmFormName;
            public ushort dmLogPixels;
            public uint dmBitsPerPel;
            public uint dmPelsWidth;
            public uint dmPelsHeight;
            public uint dmDisplayFlags;
            public uint dmDisplayFrequency;
            public uint dmICMMethod;
            public uint dmICMIntent;
            public uint dmMediaType;
            public uint dmDitherType;
            public uint dmReserved1;
            public uint dmReserved2;
            public uint dmPanningWidth;
            public uint dmPanningHeight;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DISPLAY_DEVICE
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceString;
            [MarshalAs(UnmanagedType.U4)]
            public int StateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceKey;
        }

        public enum DISP_CHANGE : int
        {
            Successful = 0,
            Restart = 1,
            Failed = -1
        }

        [Flags()]
        public enum DM : int
        {
            PelsWidth = 0x80000,
            PelsHeight = 0x100000,
            BitsPerPixel = 0x40000,
            DisplayFrequency = 0x400000
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern DISP_CHANGE ChangeDisplaySettingsEx(
            string lpszDeviceName, ref DEVMODE lpDevMode, IntPtr hwnd, uint dwflags, IntPtr lParam);

        public const int CDS_UPDATEREGISTRY = 0x01;
        public const int CDS_GLOBAL = 0x08;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EnumDisplaySettings(
            string deviceName, int modeNum, ref DEVMODE devMode);

        [DllImport("user32.dll")]
        public static extern int EnumDisplayDevices(
            string lpDevice, int iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, int dwFlags);

        public const int ENUM_CURRENT_SETTINGS = -1;
        public const int ENUM_REGISTRY_SETTINGS = -2;

        private void FillCBWithRes1()
        {
            DEVMODE dm = new DEVMODE();
            dm.dmSize = (ushort)Marshal.SizeOf(typeof(DEVMODE));
            int modeNum = 0;

            var resolutionDictionary = new Dictionary<string, uint>();

            while (EnumDisplaySettings(null, modeNum, ref dm))
            {
                string resolution = $"{dm.dmPelsWidth}x{dm.dmPelsHeight}";
                uint refreshRate = dm.dmDisplayFrequency;

                if (resolutionDictionary.ContainsKey(resolution))
                {
                    if (refreshRate > resolutionDictionary[resolution])
                    {
                        resolutionDictionary[resolution] = refreshRate;
                    }
                }
                else
                {
                    resolutionDictionary.Add(resolution, refreshRate);
                }
                modeNum++;
            }

            foreach (var item in resolutionDictionary)
            {
                availableResolutionsCB.Items.Add($"{item.Key}@{item.Value}");
                availableResolutionsCB2.Items.Add($"{item.Key}@{item.Value}");
                availableResolutionsCB3.Items.Add($"{item.Key}@{item.Value}");
            }

            //Optionally select the current resolution
            availableResolutionsCB.SelectedIndex = availableResolutionsCB.Items.IndexOf($"{primWidth}x{primHeight}@{dm.dmDisplayFrequency}");
            availableResolutionsCB2.SelectedIndex = availableResolutionsCB2.Items.IndexOf($"{primWidth}x{primHeight}@{dm.dmDisplayFrequency}");
            availableResolutionsCB3.SelectedIndex = availableResolutionsCB3.Items.IndexOf($"{primWidth}x{primHeight}@{dm.dmDisplayFrequency}");

            resolution1ToolStripMenuItem.Text = availableResolutionsCB.SelectedItem.ToString();
            resolution2ToolStripMenuItem.Text = availableResolutionsCB2.SelectedItem.ToString();
            resolution3ToolStripMenuItem.Text = availableResolutionsCB3.SelectedItem.ToString();
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(
            IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 9000; // Arbitrary ID for the hotkey
        private const int HOTKEY_ID2 = 9001;
        private const int HOTKEY_ID3 = 9002;
        private const uint MOD_CONTROL = 0x0002; // Control key modifier
        private const uint MOD_SHIFT = 0x0004; // Shift key modifier
        private const uint VK_1 = 0x31; // '1' key virtual key code
        private const uint VK_2 = 0x32; // '2' key virtual key code
        private const uint VK_3 = 0x33; // '3' key virtual key code

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            if (m.Msg == WM_HOTKEY)
            {
                int id = m.WParam.ToInt32();
                if (id == HOTKEY_ID)
                {
                    ChangeResolution1();
                }
                else if (id == HOTKEY_ID2)
                {
                    ChangeResolution2();
                }
                else if (id == HOTKEY_ID3)
                {
                    ChangeResolution3();
                }
            }
            base.WndProc(ref m);
        }

        private void SaveResolutionsToRegistry(string resolution1, string resolution2, string resolution3)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath))
            {
                if (key != null)
                {
                    key.SetValue(RegistryValueName1, resolution1);
                    key.SetValue(RegistryValueName2, resolution2);
                    key.SetValue(RegistryValueName3, resolution3);
                }
            }
        }

        private void LoadResolutionsFromRegistry()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
            {
                if (key != null)
                {
                    object resolutionValue = key.GetValue(RegistryValueName1);
                    object resolutionValue2 = key.GetValue(RegistryValueName2);
                    object resolutionValue3 = key.GetValue(RegistryValueName3);

                    if (resolutionValue != null)
                    {
                        availableResolutionsCB.SelectedItem = resolutionValue.ToString();
                        resolution1ToolStripMenuItem.Text = resolutionValue.ToString();
                    }
                    if (resolutionValue2 != null)
                    {
                        availableResolutionsCB2.SelectedItem = resolutionValue2.ToString();
                        resolution2ToolStripMenuItem.Text = resolutionValue2.ToString();
                    }
                    if (resolutionValue3 != null)
                    {
                        availableResolutionsCB3.SelectedItem = resolutionValue3.ToString();
                        resolution3ToolStripMenuItem.Text = resolutionValue3.ToString();
                    }
                }
            }
        }

        private void ChangeResolution1()
        {
            string selectedResolution = availableResolutionsCB.SelectedItem.ToString();
            string[] dimensions = selectedResolution.Split('x', '@');
            int width = int.Parse(dimensions[0]);
            int height = int.Parse(dimensions[1]);
            int refresh = int.Parse(dimensions[2]);

            DEVMODE dm = new DEVMODE();
            dm.dmSize = (ushort)Marshal.SizeOf(typeof(DEVMODE));
            dm.dmPelsWidth = (uint)width;
            dm.dmPelsHeight = (uint)height;
            dm.dmDisplayFrequency = (uint)refresh;
            dm.dmFields = (uint)(DM.PelsWidth | DM.PelsHeight);

            DISP_CHANGE result = ChangeDisplaySettingsEx(null, ref dm, IntPtr.Zero, CDS_UPDATEREGISTRY | CDS_GLOBAL, IntPtr.Zero);

            resolution1ToolStripMenuItem.Checked = true;
            resolution2ToolStripMenuItem.Checked = false;
            resolution3ToolStripMenuItem.Checked = false;
        }

        private void ChangeResolution2()
        {
            string selectedResolution = availableResolutionsCB2.SelectedItem.ToString();
            string[] dimensions = selectedResolution.Split('x', '@');
            int width = int.Parse(dimensions[0]);
            int height = int.Parse(dimensions[1]);
            int refresh = int.Parse(dimensions[2]);

            DEVMODE dm = new DEVMODE();
            dm.dmSize = (ushort)Marshal.SizeOf(typeof(DEVMODE));
            dm.dmPelsWidth = (uint)width;
            dm.dmPelsHeight = (uint)height;
            dm.dmDisplayFrequency = (uint)refresh;
            dm.dmFields = (uint)(DM.PelsWidth | DM.PelsHeight);

            DISP_CHANGE result = ChangeDisplaySettingsEx(null, ref dm, IntPtr.Zero, CDS_UPDATEREGISTRY | CDS_GLOBAL, IntPtr.Zero);

            resolution1ToolStripMenuItem.Checked = false;
            resolution2ToolStripMenuItem.Checked = true;
            resolution3ToolStripMenuItem.Checked = false;
        }

        private void ChangeResolution3()
        {
            string selectedResolution = availableResolutionsCB3.SelectedItem.ToString();
            string[] dimensions = selectedResolution.Split('x', '@');
            int width = int.Parse(dimensions[0]);
            int height = int.Parse(dimensions[1]);
            int refresh = int.Parse(dimensions[2]);

            DEVMODE dm = new DEVMODE();
            dm.dmSize = (ushort)Marshal.SizeOf(typeof(DEVMODE));
            dm.dmPelsWidth = (uint)width;
            dm.dmPelsHeight = (uint)height;
            dm.dmDisplayFrequency = (uint)refresh;
            dm.dmFields = (uint)(DM.PelsWidth | DM.PelsHeight);

            DISP_CHANGE result = ChangeDisplaySettingsEx(null, ref dm, IntPtr.Zero, CDS_UPDATEREGISTRY | CDS_GLOBAL, IntPtr.Zero);

            resolution1ToolStripMenuItem.Checked = false;
            resolution2ToolStripMenuItem.Checked = false;
            resolution3ToolStripMenuItem.Checked = true;
        }

        private void CheckPrimResOnLoad()
        {
            string primRes = $"{primWidth}x{primHeight}";

            string selectedResolution1 = availableResolutionsCB.SelectedItem.ToString();
            string[] dimensions1 = selectedResolution1.Split('x', '@');
            int width = int.Parse(dimensions1[0]);
            int height = int.Parse(dimensions1[1]);
            int refresh = int.Parse(dimensions1[2]);
            string res1 = $"{width}x{height}";

            string selectedResolution2 = availableResolutionsCB2.SelectedItem.ToString();
            string[] dimensions2 = selectedResolution2.Split('x', '@');
            int width2 = int.Parse(dimensions2[0]);
            int height2 = int.Parse(dimensions2[1]);
            int refresh2 = int.Parse(dimensions2[2]);
            string res2 = $"{width2}x{height2}";

            string selectedResolution3 = availableResolutionsCB3.SelectedItem.ToString();
            string[] dimensions3 = selectedResolution3.Split('x', '@');
            int width3 = int.Parse(dimensions3[0]);
            int height3 = int.Parse(dimensions3[1]);
            int refresh3 = int.Parse(dimensions3[2]);
            string res3 = $"{width3}x{height3}";

            switch (res1, res2, res3)
            {
                case var _ when res1 == primRes:
                    resolution1ToolStripMenuItem.Checked = true;
                    resolution2ToolStripMenuItem.Checked = false;
                    resolution3ToolStripMenuItem.Checked = false;
                    break;
                case var _ when res2 == primRes:
                    resolution1ToolStripMenuItem.Checked = false;
                    resolution2ToolStripMenuItem.Checked = true;
                    resolution3ToolStripMenuItem.Checked = false;
                    break;
                case var _ when res3 == primRes:
                    resolution1ToolStripMenuItem.Checked = false;
                    resolution2ToolStripMenuItem.Checked = false;
                    resolution3ToolStripMenuItem.Checked = true;
                    break;
                default:
                    resolution1ToolStripMenuItem.Checked = false;
                    resolution2ToolStripMenuItem.Checked = false;
                    resolution3ToolStripMenuItem.Checked = false;
                    break;
            }
        }

        public ResolutionChanger()
        {
            InitializeComponent();

            contextMenuStrip.Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());
            contextMenuStripAddedList.Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());

            toolTip_admin.SetToolTip(label_isAdmin, "It is recommended to run as Admin when running the Process Explorer");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.WindowState = FormWindowState.Minimized;
            this.Hide();

            UnregisterHotKey(this.Handle, HOTKEY_ID); // Unregister first to avoid duplicates
            RegisterHotKey(this.Handle, HOTKEY_ID, MOD_CONTROL | MOD_SHIFT, VK_1);
            UnregisterHotKey(this.Handle, HOTKEY_ID2); // Unregister first to avoid duplicates
            RegisterHotKey(this.Handle, HOTKEY_ID2, MOD_CONTROL | MOD_SHIFT, VK_2);
            UnregisterHotKey(this.Handle, HOTKEY_ID3); // Unregister first to avoid duplicates
            RegisterHotKey(this.Handle, HOTKEY_ID3, MOD_CONTROL | MOD_SHIFT, VK_3);
        }

        private void ResolutionChanger_Load(object sender, EventArgs e)
        {
            // Check if the app is set to run at startup
            runOnStartupToolStripMenuItem.Checked = IsRunAtStartup();
            notifyIcon.Visible = true;

            if (IsRunAtStartup() != false)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
                this.ShowInTaskbar = false;
            }

            CustomComboBox();

            FillCBWithRes1();
            LoadResolutionsFromRegistry();
            CheckPrimResOnLoad();
            LoadAddedProcesses();

            activeProcesses = new HashSet<string>();

            notifyIcon.Text = $"Resolution Changer {fileVersion}";

            StartProcessMonitoring();

        }

        private void ResolutionChanger_Shown(object sender, EventArgs e)
        {
            if (Utils.IsRunAsAdmin())
            {
                label_isAdmin.Text = "Running as Admin";
                label_isAdmin.ForeColor = Color.FromArgb(0, 200, 0);
                label_isAdmin.Left = (400 - label_isAdmin.Width) / 2;
            }
            else
            {
                label_isAdmin.Text = "Not running as Admin";
                label_isAdmin.ForeColor = Color.FromArgb(200, 0, 0);
                label_isAdmin.Left = (400 - label_isAdmin.Width) / 2;
            }
        }

        private void ShowForm()
        {
            this.ShowInTaskbar = true;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            this.Activate();

            UnregisterHotKey(this.Handle, HOTKEY_ID); // Unregister first to avoid duplicates
            RegisterHotKey(this.Handle, HOTKEY_ID, MOD_CONTROL | MOD_SHIFT, VK_1);
            UnregisterHotKey(this.Handle, HOTKEY_ID2); // Unregister first to avoid duplicates
            RegisterHotKey(this.Handle, HOTKEY_ID2, MOD_CONTROL | MOD_SHIFT, VK_2);
            UnregisterHotKey(this.Handle, HOTKEY_ID3); // Unregister first to avoid duplicates
            RegisterHotKey(this.Handle, HOTKEY_ID3, MOD_CONTROL | MOD_SHIFT, VK_3);
        }

        private void ResolutionChanger_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
            else if (this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Maximized)
            {
                ShowForm();
            }
        }

        public List<string> GetRunningProcesses()
        {
            var processNames = new List<string>();

            try
            {
                var searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_Process");

                foreach (ManagementObject obj in searcher.Get())
                {
                    try
                    {
                        string nameFull = obj["Name"]?.ToString();

                        if (!string.IsNullOrEmpty(nameFull))
                        {
                            // Remove the file extension (e.g., .exe)
                            string name = Path.GetFileNameWithoutExtension(nameFull);
                            if (!processNames.Contains(name))
                            {
                                processNames.Add(name);
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"WMI query failed: {ex.Message}");
            }

            return processNames;
        }

        private void StartProcessMonitoring()
        {
            // Start a background thread to monitor for target processes
            Thread processMonitorThread = new Thread(() =>
            {
                while (true)
                {
                    targetProcesses = ReadListFromRegistry();
                    MonitorProcesses();
                    Thread.Sleep(2000); // Check every X miliseconds
                }
            });

            processMonitorThread.IsBackground = true;
            processMonitorThread.Start();
        }

        private void MonitorProcesses()
        {
            var runningProcesses = GetRunningProcesses();

            // Find all matching processes with their tags
            var matchedProcesses = ReadListFromRegistry()
                .Select(tp =>
                {
                    var parts = tp.Split(new[] { '@', '#' }, StringSplitOptions.RemoveEmptyEntries);
                    return
                    (
                        ProcName: parts[0],
                        ProcRes: parts[1],
                        ProcPrio: int.Parse(parts[2])
                    );
                })
                .Where(tp => tp != default && runningProcesses
                .Contains(tp.ProcName, StringComparer.OrdinalIgnoreCase))
                .ToList();

            // Check for target processes that have been closed
            foreach (string processName in activeProcesses.ToList()) // Use ToList to avoid modifying collection during iteration
            {
                if (!runningProcesses.Contains(processName, StringComparer.OrdinalIgnoreCase))
                {
                    activeProcesses.Clear();
                    OnTargetProcessClosed(processName);

                    // re-evaluate priority
                    PromoteHighestPriority(matchedProcesses);
                }
            }

            PromoteHighestPriority(matchedProcesses);
        }

        private void PromoteHighestPriority(List<(string ProcName, string ProcRes, int ProcPrio)> matchedProcesses)
        {
            if (matchedProcesses.Any())
            {
                // find process with lowest prio value
                var highestPriority = matchedProcesses
                    .OrderBy(tp => tp.ProcPrio)
                    .First();

                // process if not already active
                if (!activeProcesses.Contains(highestPriority.ProcName))
                {
                    activeProcesses.Add(highestPriority.ProcName);
                    OnTargetProcessDetected(highestPriority.ProcRes);
                }
                else { OnTargetProcessDetected(highestPriority.ProcRes); }
            }
        }

        private void OnTargetProcessDetected(string processName)
        {
            int monitorWidth = Screen.PrimaryScreen.Bounds.Width;
            int monitorHeight = Screen.PrimaryScreen.Bounds.Height;

            if (processName == "1")
            {
                string selectedResolution = availableResolutionsCB.SelectedItem.ToString();
                string[] dimensions = selectedResolution.Split('x', '@');
                int width = int.Parse(dimensions[0]);
                int height = int.Parse(dimensions[1]);

                if (monitorWidth != width || monitorHeight != height)
                {
                    ChangeResolution1();
                }
            }
            else if (processName == "2")
            {
                string selectedResolution = availableResolutionsCB2.SelectedItem.ToString();
                string[] dimensions = selectedResolution.Split('x', '@');
                int width = int.Parse(dimensions[0]);
                int height = int.Parse(dimensions[1]);

                if (monitorWidth != width || monitorHeight != height)
                {
                    ChangeResolution2();
                }
            }
            else if (processName == "3")
            {
                string selectedResolution = availableResolutionsCB3.SelectedItem.ToString();
                string[] dimensions = selectedResolution.Split('x', '@');
                int width = int.Parse(dimensions[0]);
                int height = int.Parse(dimensions[1]);

                if (monitorWidth != width || monitorHeight != height)
                {
                    ChangeResolution3();
                }
            }
        }

        private void OnTargetProcessClosed(string processName)
        {
            int monitorWidth = Screen.PrimaryScreen.Bounds.Width;
            int monitorHeight = Screen.PrimaryScreen.Bounds.Height;

            string selectedResolution = availableResolutionsCB.SelectedItem.ToString();
            string[] dimensions = selectedResolution.Split('x', '@');
            int width = int.Parse(dimensions[0]);
            int height = int.Parse(dimensions[1]);

            if (monitorWidth != width || monitorHeight != height)
            {
                ChangeResolution1();
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            object resolutionValue1 = availableResolutionsCB.SelectedItem;
            object resolutionValue2 = availableResolutionsCB2.SelectedItem;
            object resolutionValue3 = availableResolutionsCB3.SelectedItem;

            SaveResolutionsToRegistry(resolutionValue1 as string, resolutionValue2 as string, resolutionValue3 as string);

            resolution1ToolStripMenuItem.Text = resolutionValue1 as string;
            resolution2ToolStripMenuItem.Text = resolutionValue2 as string;
            resolution3ToolStripMenuItem.Text = resolutionValue3 as string;
        }

        private void applyRes1Button_Click(object sender, EventArgs e)
        {
            ChangeResolution1();
        }

        private void applyRes2Button_Click(object sender, EventArgs e)
        {
            ChangeResolution2();
        }

        private void applyRes3Button_Click(object sender, EventArgs e)
        {
            ChangeResolution3();
        }

        private void AddToStartup()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.SetValue(appName, Application.ExecutablePath);
            key.Close();
        }

        private void RemoveFromStartup()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.DeleteValue(appName, false);
            key.Close();
        }

        private bool IsRunAtStartup()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            return key.GetValue(appName) != null;
        }

        private void ResolutionChanger_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // Cancel the form closing event
            this.Hide(); // Hide the form
            notifyIcon.Visible = true; // Show the NotifyIcon
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowForm();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = true; // Hide the NotifyIcon
            Application.Exit(); // Exit the application
            Application.ExitThread();

            UnregisterHotKey(this.Handle, HOTKEY_ID);
            UnregisterHotKey(this.Handle, HOTKEY_ID2);
            UnregisterHotKey(this.Handle, HOTKEY_ID3);
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void runOnStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Toggle the checked state
            runOnStartupToolStripMenuItem.Checked = !runOnStartupToolStripMenuItem.Checked;

            if (runOnStartupToolStripMenuItem.Checked)
            {
                AddToStartup();
            }
            else
            {
                RemoveFromStartup();
            }
        }

        public void DeleteResolutionRegistry()
        {
            RegistryKey baseKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE", true);

            try
            {
                baseKey.DeleteSubKeyTree("AutistukralResolutionChanger");
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void clearRegistryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteResolutionRegistry();
            RemoveFromStartup();
            RegistryKey runKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (runKey.GetValue(appName) == null)
            {
                runOnStartupToolStripMenuItem.Checked = false;
            }
        }

        private void LoadAddedProcesses()
        {
            try
            {
                // Read the list of processes from the registry
                List<string> addedProcesses = ReadListFromRegistry();

                ImageList imageListAddedProcs = LoadIcons();

                // Set the image size to a larger resolution (e.g., 32x32 pixels)
                imageListAddedProcs.ImageSize = new Size(32, 32);

                listView_addedProcesses.SmallImageList = imageListAddedProcs;
                listView_addedProcesses.LargeImageList = imageListAddedProcs;
                listView_addedProcesses.StateImageList = imageListAddedProcs;

                // Clear the ListView
                listView_addedProcesses.Items.Clear();

                // Iterate over the processes
                foreach (string process in addedProcesses)
                {
                    var listViewItem = new ListViewItem();

                    // Split the process name and any tags (e.g., "process@1#1")
                    string[] parts = process.Split('@', '#');
                    string processName = parts[0];
                    string processRes = parts[1];
                    string processPrio = parts[2];

                    // Set the text of the ListViewItem
                    listViewItem.Text = process;

                    // Check if the ImageList contains the process icon
                    if (imageListAddedProcs.Images.ContainsKey(processName))
                    {
                        // Set the ImageKey to the process name (make sure it matches the key in the ImageList)
                        listViewItem.ImageKey = processName;
                    }

                    // Add the item to the ListView
                    listView_addedProcesses.Items.Add(listViewItem);
                }
            }
            catch (Exception ex) { MessageBox.Show($"Error loading processes: {ex.Message}"); }
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

            return new List<string>(); // return empty if no value found
        }

        private void btn_addProcess_Click(object sender, EventArgs e)
        {
            ProcessExplorer processExplorerForm = new ProcessExplorer();
            processExplorerForm.Show();
        }

        private void btn_addProcessWinExp_Click(object sender, EventArgs e)
        {

        }

        private void SaveListToRegistry(List<string> processList)
        {
            string serializedList = JsonSerializer.Serialize(processList);

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath))
            {
                key?.SetValue(RegistryValueApps, serializedList);
            }
        }

        private void AssignResolutionToProcess(int res)
        {
            if (listView_addedProcesses.SelectedItems.Count > 0)
            {
                // get the selected item
                string selectedProcess = listView_addedProcesses.SelectedItems[0].Text;

                // append option to item and update registry
                string[] selectedSplit = selectedProcess.Split('@', '#');
                string valueToSave = $"{selectedSplit[0]}@{res}#{selectedSplit[2]}";

                // update registry
                List<string> updatedProcesses = ReadListFromRegistry();
                updatedProcesses.Remove(selectedProcess);
                updatedProcesses.Add(valueToSave);
                SaveListToRegistry(updatedProcesses);
                LoadAddedProcesses();
            }
        }

        private void AssignPriority(int prio)
        {
            if (listView_addedProcesses.SelectedItems.Count > 0)
            {
                // get the selected item
                string selectedProcess = listView_addedProcesses.SelectedItems[0].Text;

                // append option to item and update registry
                string[] selectedSplit = selectedProcess.Split('#');
                string valueToSave = $"{selectedSplit[0]}#{prio}";
                // update registry
                List<string> updatedProcesses = ReadListFromRegistry();
                updatedProcesses.Remove(selectedProcess);
                updatedProcesses.Add(valueToSave);
                SaveListToRegistry(updatedProcesses);
                LoadAddedProcesses();
            }
        }

        private ImageList LoadIcons()
        {
            string saveDirectory = GetIconSaveDirectory();
            var imageList = new ImageList();

            try
            {
                // Get all image files in the directory
                string[] imageFiles = Directory.GetFiles(saveDirectory, "*.png");

                // Iterate over each image file in the folder
                foreach (var filePath in imageFiles)
                {
                    // Get the file name without the extension to use as the key
                    string fileName = Path.GetFileNameWithoutExtension(filePath);

                    // Check if the file exists and load the image
                    if (File.Exists(filePath))
                    {
                        imageList.Images.Add(fileName, Image.FromFile(filePath));
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return imageList;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView_addedProcesses.SelectedItems.Count > 0)
            {
                // get the selected item
                string selectedProcess = listView_addedProcesses.SelectedItems[0].Text;

                // remove it from the ListView
                listView_addedProcesses.Items.Remove(listView_addedProcesses.SelectedItems[0]);

                // update registry
                List<string> updatedProcesses = ReadListFromRegistry();
                updatedProcesses.Remove(selectedProcess);
                SaveListToRegistry(updatedProcesses);
            }
        }

        private void resolution1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AssignResolutionToProcess(1);
        }

        private void resolution2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AssignResolutionToProcess(2);
        }

        private void resolution3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AssignResolutionToProcess(3);
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadAddedProcesses();
        }

        private void priority1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssignPriority(1);
        }

        private void priority1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AssignPriority(2);
        }

        private void priority3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssignPriority(3);
        }

        private void priority4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssignPriority(4);
        }
    }
}
