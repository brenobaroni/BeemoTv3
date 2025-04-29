using System;
using System.Runtime.InteropServices;

namespace BeemoTv3
{
    public class WindowFunctions
    {
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        private const uint WM_KEYDOWN = 0x0100;
        private const uint WM_KEYUP = 0x0101;
        private const uint WM_LBUTTONDOWN = 0x0201;
        private const uint WM_LBUTTONUP = 0x0202;

        private IntPtr _gameWindow;

        public void Initialize()
        {
            // Encontrar a janela do jogo apenas uma vez
            _gameWindow = FindWindow(null, "LifeTO - Jewelia : Ruby Island [v04202025]");
            if (_gameWindow == IntPtr.Zero)
            {
                throw new Exception("Janela do jogo não encontrada!");
            }
        }

        public void Update()
        {
            // Removido o foco constante da janela
        }

        public void SendKey(int key)
        {
            if (_gameWindow != IntPtr.Zero)
            {
                PostMessage(_gameWindow, WM_KEYDOWN, key, 0);
                System.Threading.Thread.Sleep(50);
                PostMessage(_gameWindow, WM_KEYUP, key, 0);
            }
        }

        public void SendMouseClick(int x, int y)
        {
            if (_gameWindow != IntPtr.Zero)
            {
                int lParam = (y << 16) | x;
                PostMessage(_gameWindow, WM_LBUTTONDOWN, 0, lParam);
                System.Threading.Thread.Sleep(50);
                PostMessage(_gameWindow, WM_LBUTTONUP, 0, lParam);
            }
        }
    }
}