using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using static Resolution_Changer.ResolutionChanger;

namespace Resolution_Changer
{
    public partial class ResolutionChanger : Form
    {
        private const string RegistryKeyPath = @"Software\AutistukralResolutionChanger";
        private const string RegistryValueName1 = "Resolution1";
        private const string RegistryValueName2 = "Resolution2";
        private const string RegistryValueName3 = "Resolution3";
        private const string appName = "Autistukral Resolution Changer";
        string fileVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;

        int primWidth = Screen.PrimaryScreen.Bounds.Width;
        int primHeight = Screen.PrimaryScreen.Bounds.Height;

        private System.Windows.Forms.Timer updateTimer;

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

            if (IsRunAtStartup() != null)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
                this.ShowInTaskbar = false;
            }

            CustomComboBox();

            FillCBWithRes1();
            LoadResolutionsFromRegistry();
            CheckPrimResOnLoad();

            notifyIcon.Text = $"Resolution Changer {fileVersion}";
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
    }
}
