using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace OverlayDemo
{
    public sealed class GlobalHotkey : IDisposable
    {
        private const int HOTKEY_ID = 1;
        private readonly IntPtr _windowHandle;

        public event Action? Pressed;

        public GlobalHotkey(IntPtr windowHandle)
        {
            _windowHandle = windowHandle;
        }

        public bool Register()
        {
            return RegisterHotKey(
                _windowHandle,
                HOTKEY_ID,
                MOD_CONTROL | MOD_SHIFT,
                KeyInterop.VirtualKeyFromKey(Key.Space)
            );
        }

        public void Unregister()
        {
            UnregisterHotKey(_windowHandle, HOTKEY_ID);
        }

        public void HandleMessage(IntPtr wParam)
        {
            if (wParam.ToInt32() == HOTKEY_ID)
                Pressed?.Invoke();
        }

        public void Dispose() => Unregister();

        private const uint MOD_CONTROL = 0x0002;
        private const uint MOD_SHIFT = 0x0004;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(
            IntPtr hWnd, int id, uint fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(
            IntPtr hWnd, int id);
    }
}
