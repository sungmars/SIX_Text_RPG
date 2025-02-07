namespace SIX_Text_RPG
{
    internal class Define
    {
        public static readonly string GAME_TITLE = "스파게티 코딩클럽";
        public static readonly string ERROR_MESSAGE_INPUT = "[!] 잘못된 입력입니다.";

        #region Players
        public static readonly Stats[] PLAYERS_STATS =
        {
            new() { Level = 1, ATK = 12, DEF = 0, HP = 60, Gold = 1500 },  // 마계조단
            new() { Level = 1, ATK = 4, DEF = 2, HP = 120, Gold = 1500 },  // 천계조단
        };
        #endregion

        #region Monsters
        public static readonly Stats[] MONSTERS_STATS =
        {
            new() { Name = "염예찬 튜터님", Level = 0, ATK = 0, DEF = 0, HP = 0, Gold = 0 },
            new() { Name = "강성훈 튜터님", Level = 0, ATK = 0, DEF = 0, HP = 0, Gold = 0 },
            new() { Name = "김재경 튜터님", Level = 0, ATK = 0, DEF = 0, HP = 0, Gold = 0 },
            new() { Name = "이승환 튜터님", Level = 0, ATK = 0, DEF = 0, HP = 0, Gold = 0 },
            new() { Name = "이성언 튜터님", Level = 0, ATK = 0, DEF = 0, HP = 0, Gold = 0 },
            new() { Name = "김명민 튜터님", Level = 0, ATK = 0, DEF = 0, HP = 0, Gold = 0 },
            new() { Name = "김영호 튜터님", Level = 0, ATK = 0, DEF = 0, HP = 0, Gold = 0 },
            new() { Name = "김현정 튜터님", Level = 0, ATK = 0, DEF = 0, HP = 0, Gold = 0 },
            new() { Name = "강  인 튜터님", Level = 0, ATK = 0, DEF = 0, HP = 0, Gold = 0 },
            new() { Name = "오정환 튜터님", Level = 0, ATK = 0, DEF = 0, HP = 0, Gold = 0 },
            new() { Name = "권관우 튜터님", Level = 0, ATK = 0, DEF = 0, HP = 0, Gold = 0 },
            new() { Name = "소재철 튜터님", Level = 0, ATK = 0, DEF = 0, HP = 0, Gold = 0 },
            new() { Name = "김주안 튜터님", Level = 0, ATK = 0, DEF = 0, HP = 0, Gold = 0 }
        };
        #endregion
    }
}