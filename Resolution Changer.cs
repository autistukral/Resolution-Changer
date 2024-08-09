using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Resolution_Changer
{
    public partial class ResolutionChanger : Form
    {
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

            while (EnumDisplaySettings(null, modeNum, ref dm))
            {
                string resolution = $"{dm.dmPelsWidth}x{dm.dmPelsHeight}";
                if (!availableResolutionsCB.Items.Contains(resolution))
                {
                    availableResolutionsCB.Items.Add(resolution);
                }
                modeNum++;
            }

            //Optionally select the current resolution
            availableResolutionsCB.SelectedIndex = availableResolutionsCB.Items.IndexOf($"{Screen.PrimaryScreen.Bounds.Width}x{Screen.PrimaryScreen.Bounds.Height}");
        }

        private void FillCBWithRes2()
        {
            DEVMODE dm = new DEVMODE();
            dm.dmSize = (ushort)Marshal.SizeOf(typeof(DEVMODE));
            int modeNum = 0;

            while (EnumDisplaySettings(null, modeNum, ref dm))
            {
                string resolution = $"{dm.dmPelsWidth}x{dm.dmPelsHeight}";
                if (!availableResolutionsCB.Items.Contains(resolution))
                {
                    availableResolutionsCB.Items.Add(resolution);
                }
                modeNum++;
            }

            //Optionally select the current resolution
            availableResolutionsCB.SelectedIndex = availableResolutionsCB.Items.IndexOf($"{Screen.PrimaryScreen.Bounds.Width}x{Screen.PrimaryScreen.Bounds.Height}");
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(
            IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 9000; // Arbitrary ID for the hotkey
        private const int HOTKEY_ID2 = 9001; // Arbitrary ID for the hotkey
        private const uint MOD_CONTROL = 0x0002; // Control key modifier
        private const uint MOD_SHIFT = 0x0004; // Shift key modifier
        private const uint VK_1 = 0x31; // '1' key virtual key code
        private const uint VK_2 = 0x32; // '2' key virtual key code

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            if (m.Msg == WM_HOTKEY)
            {
                if (m.WParam.ToInt32() == HOTKEY_ID)
                {
                    ChangeResolution1();
                }
                else if (m.WParam.ToInt32() == HOTKEY_ID2)
                {
                    ChangeResolution2();
                }
            }
            base.WndProc(ref m);
        }

        private void ChangeResolution1()
        {
            string selectedResolution = availableResolutionsCB.SelectedItem.ToString();
            string[] dimensions = selectedResolution.Split('x');
            int width = int.Parse(dimensions[0]);
            int height = int.Parse(dimensions[1]);

            DEVMODE dm = new DEVMODE();
            dm.dmSize = (ushort)Marshal.SizeOf(typeof(DEVMODE));
            dm.dmPelsWidth = (uint)width;
            dm.dmPelsHeight = (uint)height;
            dm.dmFields = (uint)(DM.PelsWidth | DM.PelsHeight);

            DISP_CHANGE result = ChangeDisplaySettingsEx(null, ref dm, IntPtr.Zero, CDS_UPDATEREGISTRY | CDS_GLOBAL, IntPtr.Zero);
        }

        private void ChangeResolution2()
        {
            string selectedResolution = availableResolutionsCB2.SelectedItem.ToString();
            string[] dimensions = selectedResolution.Split('x');
            int width = int.Parse(dimensions[0]);
            int height = int.Parse(dimensions[1]);

            DEVMODE dm = new DEVMODE();
            dm.dmSize = (ushort)Marshal.SizeOf(typeof(DEVMODE));
            dm.dmPelsWidth = (uint)width;
            dm.dmPelsHeight = (uint)height;
            dm.dmFields = (uint)(DM.PelsWidth | DM.PelsHeight);

            DISP_CHANGE result = ChangeDisplaySettingsEx(null, ref dm, IntPtr.Zero, CDS_UPDATEREGISTRY | CDS_GLOBAL, IntPtr.Zero);
        }

        public ResolutionChanger()
        {
            InitializeComponent();
        }

        private void ResolutionChanger_Load(object sender, EventArgs e)
        {
            FillCBWithRes1();
            FillCBWithRes2();
            RegisterHotKey(this.Handle, HOTKEY_ID, MOD_CONTROL | MOD_SHIFT, VK_1);
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
