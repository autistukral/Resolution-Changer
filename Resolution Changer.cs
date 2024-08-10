using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Resolution_Changer
{
    public partial class ResolutionChanger : Form
    {
        private const string RegistryKeyPath = @"Software\AutistukralResolutionChanger";
        private const string RegistryValueName1 = "Resolution1";
        private const string RegistryValueName2 = "Resolution2";
        private const string RegistryValueName3 = "Resolution3";
        private const string appName = "Autistukral Resolution Changer";

        private System.Windows.Forms.Timer updateTimer;

        // Code to make the application top bar colored by the windows
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
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
            availableResolutionsCB.SelectedIndex = availableResolutionsCB.Items.IndexOf($"{Screen.PrimaryScreen.Bounds.Width}x{Screen.PrimaryScreen.Bounds.Height}@{dm.dmDisplayFrequency}");
            availableResolutionsCB2.SelectedIndex = availableResolutionsCB2.Items.IndexOf($"{Screen.PrimaryScreen.Bounds.Width}x{Screen.PrimaryScreen.Bounds.Height}@{dm.dmDisplayFrequency}");
            availableResolutionsCB3.SelectedIndex = availableResolutionsCB3.Items.IndexOf($"{Screen.PrimaryScreen.Bounds.Width}x{Screen.PrimaryScreen.Bounds.Height}@{dm.dmDisplayFrequency}");
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
                    }
                    if (resolutionValue2 != null)
                    {
                        availableResolutionsCB2.SelectedItem = resolutionValue2.ToString();
                    }
                    if (resolutionValue3 != null)
                    {
                        availableResolutionsCB3.SelectedItem = resolutionValue3.ToString();
                    }
                }
            }
        }

        private void ChangeResolution1()
        {
            string selectedResolution = availableResolutionsCB.SelectedItem.ToString();
            string[] dimensions = selectedResolution.Split('x','@');
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
        }

        public ResolutionChanger()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
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

            if (IsRunAtStartup() != null)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                this.Hide();
                notifyIcon.Visible = true;
            }

            CustomComboBox();

            FillCBWithRes1();
            LoadResolutionsFromRegistry();
        }

        private void ShowForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
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
            }
            else if (this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Maximized)
            {
                ShowForm();
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            object resolutionValue1 = availableResolutionsCB.SelectedItem;
            object resolutionValue2 = availableResolutionsCB2.SelectedItem;
            object resolutionValue3 = availableResolutionsCB3.SelectedItem;

            SaveResolutionsToRegistry(resolutionValue1 as string, resolutionValue2 as string, resolutionValue3 as string);
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
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = true;
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

    }
}
