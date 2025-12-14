using System;
using System.Windows;
using System.Windows.Interop;

namespace OverlayDemo
{
    public partial class MainWindow : Window
    {
        private GlobalHotkey? _hotkey;
        private bool _visible = true;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += (_, _) =>
            {
                // Position overlay
                Left = 20;
                Top = 20;

                RegisterHotkey();
            };
        }

        private void RegisterHotkey()
        {
            var handle = new WindowInteropHelper(this).Handle;
            var source = HwndSource.FromHwnd(handle);

            _hotkey = new GlobalHotkey(handle);
            _hotkey.Pressed += ToggleOverlay;

            if (!_hotkey.Register())
            {
                MessageBox.Show(
                    "Hotkey Ctrl+Shift+Space is already in use.",
                    "OverlayDemo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }

            source!.AddHook(WndProc);
        }

        private void ToggleOverlay()
        {
            _visible = !_visible;

            if (_visible)
                Show();
            else
                Hide();
        }

        private IntPtr WndProc(
            IntPtr hwnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam,
            ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;

            if (msg == WM_HOTKEY)
            {
                _hotkey?.HandleMessage(wParam);
                handled = true;
            }

            return IntPtr.Zero;
        }

        protected override void OnClosed(EventArgs e)
        {
            _hotkey?.Dispose();
            base.OnClosed(e);
        }
    }
}
