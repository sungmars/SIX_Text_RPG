namespace SIX_Text_RPG
{
    internal class Define
    {
        #region VALUENS
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
            new() { Level = 1, ATK = 12, DEF = 0, HP = 60, MP = 50, Gold = 1500 },  // 마계조단
            new() { Level = 1, ATK = 4, DEF = 2, HP = 120, MP = 50, Gold = 1500 }   // 천계조단
        };

        public static readonly int[] PLAYER_EXP_TABLE = new int[999];

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
        public static readonly ItemInfo[,] ITEM_INFOS =
        {
            //Armor
            {
                new() { Name = "손가락골무", Description = "찌르는 손가락이 아프지 않도록 지켜주세요.",ATK = 5, DEF = 0, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 700, Graphic = '☞', Color = ConsoleColor.Magenta },
                new() { Name = "말랑한 정신", Description = "생각이 말랑해야 정신이 건강합니다...",ATK = 0, DEF = 10, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 1500, Graphic = '♣', Color = ConsoleColor.DarkGreen },
                new() { Name = "초랭이", Description = "수생거북 초랭이입니다. 곁에만 있어도 든든합니다.",ATK = 0, DEF = 17, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 2500, Graphic = '¤', Color = ConsoleColor.Green },
                new() { Name = "상어옷", Description = "놀랍도록 파랗습니다.. 피부까지도...",ATK = 0, DEF = 30, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 4000,Graphic = 'δ', Color = ConsoleColor.Blue },
                new() { Name = "노이즈캔슬링", Description = "아무 것도 들리지 않습니다..",ATK = 0, DEF = 45, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 5900, Graphic = '¶', Color = ConsoleColor.Blue },
            },
            //Accessory
            {
                new() { Name = "입1", Description = "이가 보이게 활짝웃어봐요",ATK = 0, DEF = 0, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 300, Graphic = '∀', Color = ConsoleColor.Gray},
                new() { Name = "입2", Description = "입을 Woo~아하게 만들어줍니다",ATK = 0, DEF = 0, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 300, Graphic = 'ω' , Color = ConsoleColor.Gray },
                new() { Name = "입3", Description = "입을 빵끗 웃을 수 있습니다",ATK = 0, DEF = 0, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 300 , Graphic = 'ⅴ', Color = ConsoleColor.Gray},
                new() { Name = "입4", Description = "모기와 친구먹으세요",ATK = 0, DEF = 0, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 300 , Graphic = 'ε', Color = ConsoleColor.Gray},
                new() { Name = "입5", Description = "조용히 입을 다물어요..",ATK = 0, DEF = 0, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 300 , Graphic = 'ⅹ', Color = ConsoleColor.Gray}
            },
            //Potion
            {
                new() { Name = "최대체력 증가포션", Description = "최대 체력이 20만큼 증가합니다..",ATK = 0, DEF = 0, HP = 20, MaxHP = 20, MP = 0, MaxMP = 0, Price = 1500 , Graphic = '♨', Color = ConsoleColor.DarkRed},
                new() { Name = "체력 회복포션", Description = "현재 체력을 20만큼 회복시켜줍니다.",ATK = 0, DEF = 0, HP = 20, MaxHP = 0, MP = 0, MaxMP = 0, Price = 400 , Graphic = 'δ', Color = ConsoleColor.Red},
                new() { Name = "마나 회복포션", Description = "현재 마나를 20만큼 회복시켜줍니다",ATK = 0, DEF = 0, HP = 0, MaxHP = 0, MP = 20, MaxMP = 0, Price = 400 , Graphic = 'δ', Color = ConsoleColor.Blue},
                new() { Name = "최대마나 증가포션", Description = "최대 마나를 20만큼 증가합니다.",ATK = 0, DEF = 0, HP = 0, MaxHP = 0, MP = 20, MaxMP = 20, Price = 1500, Graphic = '♨', Color = ConsoleColor.DarkBlue },
                new() { Name = "회복약", Description = "더 저렴한 가격에 두 가지를 동시에!",ATK = 0, DEF = 0, HP = 20, MaxHP = 0, MP = 20, MaxMP = 0, Price = 700 , Graphic = 'δ', Color = ConsoleColor.Magenta},
            },
            //Weapon
            {
                new() { Name = "강력한 손가락", Description = "보기만 해도 따끔합니다!",ATK = 7, DEF = 0, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 1000, Graphic = '☞', Color = ConsoleColor.Magenta},
                new() { Name = "펀치", Description = "튜터님들에게 강력한 \"한 방\"을 선사하세요!",ATK = 15, DEF = 0, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 2000, Graphic = '⊃', Color = ConsoleColor.Red},
                new() { Name = "물풍선", Description = "추운 겨울날 튜터님을 Cool하게 만들어드려요...",ATK = 20, DEF = 0, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 3000, Graphic = '∞', Color = ConsoleColor.Cyan },
                new() { Name = "싸커킥", Description = "아야! 절대절대 \"엉덩이\"를 조심하세요..",ATK = 30, DEF = 0, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 4000, Graphic = 'ζ', Color = ConsoleColor.DarkRed},
                new() { Name = "뿅망치", Description = "이거 진짜 아파보이는데... 진짜 뿅망치 맞죠...?",ATK = 45, DEF = 0, HP = 0, MaxHP = 0, MP = 0, MaxMP = 0, Price = 5000, Graphic = 'ф', Color = ConsoleColor.Gray},
            }
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