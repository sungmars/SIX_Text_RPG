using System.Runtime.InteropServices;
using SIX_Text_RPG.Scenes;

namespace SIX_Text_RPG
{
    internal class Program
    {
        #region Externs
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleOutputCP(uint wCodePageID);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleScreenBufferSize(IntPtr hConsoleOutput, Coord size);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        private const int STD_OUTPUT_HANDLE = -11;

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;
            public Coord(short x, short y) { X = x; Y = y; }
        }
        #endregion

        public static Scene_Base CurrentScene { get; set; } = new Scene_Title();
        public static Scene_Base PreviousScene { get; set; } = CurrentScene;

        static void Main()
        {
            // 콘솔 설정
            Console.CursorVisible = false;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            SetConsoleOutputCP(65001);

            // 콘솔 창 설정
            IntPtr handle = GetStdHandle(STD_OUTPUT_HANDLE);
            SetConsoleScreenBufferSize(handle, new Coord(120, 300));

            // 게임 설정
            CurrentScene = new Scene_Title();
            while (CurrentScene != null)
            {
                CurrentScene.Awake();
                CurrentScene.Start();
                CurrentScene.LateStart();

                do if (CurrentScene.Update() == 0) break;
                while (true);
            }
        }
    }
}