﻿using SIX_Text_RPG.Scenes;

namespace SIX_Text_RPG
{
    internal class Program
    {
        public static Scene_Base CurrentScene { get; set; } = new Scene_Title();
        public static Scene_Base PreviousScene { get; set; } = CurrentScene;

        static void Main()
        {
            // 콘솔 설정
            Console.CursorVisible = false;

            // 게임 설정
            Initialize();

            // 게임 로직
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

        private static void Initialize()
        {
            // 경험치 테이블 설정
            for (int i = 0; i < Define.PLAYER_EXP_TABLE.Length; i++)
            {
                Define.PLAYER_EXP_TABLE[i] = 10 + i;
            }
        }
    }
}