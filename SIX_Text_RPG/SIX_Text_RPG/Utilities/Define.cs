namespace SIX_Text_RPG
{
    internal class Define
    {
        #region VALUENS
        public static readonly int INVENTORY_CAPACITY = 9;
        public static readonly int MONSTERS_CAPACITY = 4;
        public static readonly int MONSTER_SPAWN_X = 78;
        public static readonly int[] TIMES = { 0900, 1200, 1500, 1800, 2059 };
        #endregion

        #region TEXTS
        public static readonly string GAME_TITLE = "스파게티 코딩클럽";
        public static readonly string ERROR_MESSAGE_DATA = "[!] 저장된 데이터가 없습니다.";
        public static readonly string ERROR_MESSAGE_INPUT = "[!] 잘못된 입력입니다.";
        #endregion

        #region Players
        public static readonly Stats[] PLAYERS_STATS =
        {
            new() { Level = 1, ATK = 12, DEF = 0, HP = 60, MP = 100, Gold = 1500 },  // 마계조단
            new() { Level = 1, ATK = 4, DEF = 2, HP = 120, MP = 100, Gold = 1500 }   // 천계조단
        };

        public static readonly int[] PLAYER_EXP_TABLE =
        {
            10, // Level 1 -> 2
        };

        public static readonly string[] PLAYER_ATK_SCRIPTS =
        {
            "띵동?띵동?띵딩동?",
            "만나서 반갑습니다?",
            "머지가 머죠?",
            "안녕하세요?튜터님?",
            "제가 뭘 질문해야 할까요?ㅠㅠ",
            "튜터님 자리에 계세요?",
            "튜터님 저 또 왔어요!",
            "튜터님 해주세요 ㅎㅎ;ㅈㅅ;",
        };
        #endregion

        #region Monsters
        public static readonly Stats[] MONSTERS_STATS =
        {
            new() { Name = "염예찬 튜터님", Level = 1, ATK = 1, DEF = 0, HP = 100, EXP = 0, Gold = 0 },
            new() { Name = "강성훈 튜터님", Level = 2, ATK = 2, DEF = 0, HP = 100, EXP = 0, Gold = 0 },
            new() { Name = "김재경 튜터님", Level = 3, ATK = 3, DEF = 0, HP = 100, EXP = 0, Gold = 0 },
            new() { Name = "이승환 튜터님", Level = 4, ATK = 4, DEF = 0, HP = 100, EXP = 0, Gold = 0 },
            new() { Name = "이성언 튜터님", Level = 5, ATK = 5, DEF = 0, HP = 100, EXP = 0, Gold = 0 },
            new() { Name = "김명민 튜터님", Level = 6, ATK = 6, DEF = 0, HP = 100, EXP = 0, Gold = 0 },
            new() { Name = "김영호 튜터님", Level = 7, ATK = 7, DEF = 0, HP = 100, EXP = 0, Gold = 0 },
            new() { Name = "김현정 튜터님", Level = 8, ATK = 8, DEF = 0, HP = 100, EXP = 0, Gold = 0 },
            new() { Name = "강  인 튜터님", Level = 9, ATK = 9, DEF = 0, HP = 100, EXP = 0, Gold = 0 },
            new() { Name = "오정환 튜터님", Level = 10, ATK = 10, DEF = 0, HP = 100, EXP = 0, Gold = 0 },
            new() { Name = "권관우 튜터님", Level = 11, ATK = 11, DEF = 0, HP = 100, EXP = 0, Gold = 0 },
            new() { Name = "소재철 튜터님", Level = 12, ATK = 12, DEF = 0, HP = 100, EXP = 0, Gold = 0 },
            new() { Name = "김주안 튜터님", Level = 13, ATK = 13, DEF = 0, HP = 100, EXP = 0, Gold = 0 }
        };

        public static readonly string[] MONSTER_ATK_SCRIPTS =
        {
            "이제 돌아가세요.",
            "이해 하셨죠?",
        };
        #endregion

        #region Items
        public static readonly ItemInfo[] ITEM_INFOS =
        {
            new() { Name = "테스트", Description = "",ATK = 0, DEF = 0, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 0 },
        };
        #endregion

        #region Graphics
        public static readonly char[] FACES =
        {
            'ㅅ',
            'ㅇ',
            'ㅗ',
            'ω',
            '△',
            '∇',
        };

        public static readonly char[] EYES_MONSTER =
        {
            '8',
            'b',
            'B',

            '\'',
            '~',
            '"',

            '@',
            '#',
            '$',
            '^',
            '*',
            '-',
            '\\',
        };
        #endregion
    }
}