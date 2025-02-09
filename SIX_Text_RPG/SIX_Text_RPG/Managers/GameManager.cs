namespace SIX_Text_RPG
{
    public struct Vector2
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    internal class GameManager
    {
        public static GameManager Instance { get; private set; } = new();

        public Player? Player { get; set; }

        public List<Item> Inventory { get; private set; } = new(Define.INVENTORY_CAPACITY);
        public List<Monster> Monsters { get; private set; } = new(Define.MONSTERS_CAPACITY);

        public int CurrentStage { get; set; } = 0;
        public float TotalDamage { get; set; } = 0;

        private readonly Random random = new();

        public void DisplayBattle(int targetIndex, int attackCount = 1, Action? onHit = null)
        {
            if (Player == null)
            {
                return;
            }

            // TODO: TEST CODE, 팀 스크럼 이후 삭제
            if (Monsters.Count == 0)
            {
                Monsters.Add(new(MonsterType.염예찬));
                Monsters.Add(new(MonsterType.강성훈));
                Monsters.Add(new(MonsterType.강인));
                Monsters.Add(new(MonsterType.권관우));
            }

            DisplayBattle_Ground();
            (int left, int top) = Console.GetCursorPosition();

            DisplayBatte_Monsters();
            DisplayBattle_Attack(targetIndex, attackCount, onHit);

            Console.SetCursorPosition(left, top);
            Utils.ClearBuffer();
        }

        private void DisplayBattle_Attack(int targetIndex, int attackCount, Action? onHit)
        {
            if (Player == null)
            {
                return;
            }

            int[] startX = new int[attackCount];
            int endX = Define.MONSTER_SPAWN_X - 2;
            int targetY = Monsters[targetIndex].Position.Y;
            string value = Player.Graphic_Weapon.ToString();

            // 투사체 발사지점을 설정합니다.
            for (int i = 0; i < attackCount; i++)
            {
                startX[i] = Player.Position.X - i * 6;
            }

            // 마지막 투사체가 목표 지점에 도착하면 종료됩니다.
            int count = 0;  // 공백은 1글자로 취급되기 때문에 글자간 공백을 맞추기 위해 공백 숫자를 저장해둘 변수입니다.
            int index = 0;  // charArray를 순회하기 위한 변수입니다.
            int randomIndex = random.Next(0, Define.PLAYER_ATK_SCRIPTS.Length);
            char[] charArray = Define.PLAYER_ATK_SCRIPTS[randomIndex].ToCharArray();
            while (startX[^1] < endX)
            {
                // 공격 횟수만큼 반복합니다.
                for (int i = 0; i < attackCount; i++)
                {
                    // 투사체가 플레이어보다 뒤에 있거나, 목표지점을 초과하면 그리지 않음
                    if (startX[i]++ < Player.Position.X || startX[i] > endX)
                    {
                        continue;
                    }

                    // 투사체 좌표를 설정하고, 렌더링합니다.
                    Console.SetCursorPosition(startX[i]--, targetY);
                    Utils.WriteColor(value, ConsoleColor.Yellow);
                }

                Thread.Sleep(5);

                // 공격 횟수만큼 반복합니다.
                for (int i = 0; i < attackCount; i++)
                {
                    // 투사체가 플레이어보다 뒤에 있거나, 목표지점을 초과하면 지우지 않음
                    if (startX[i]++ < Player.Position.X || startX[i] > endX)
                    {
                        continue;
                    }

                    // 투사체가 목표지점에 도달할 경우
                    if (startX[i] == endX)
                    {
                        // Hit 애니메이션과 onHit 콜백을 호출합니다.
                        Monster targetMonster = Monsters[targetIndex];
                        onHit?.Invoke();
                        targetMonster.Render_Hit();
                    }

                    // 이전 투사체 잔흔을 지웁니다.
                    Console.SetCursorPosition(startX[i], targetY);
                    Console.Write(' ');
                }

                // 마지막 인덱스 투사체가 지나간 자리에 텍스트를 출력합니다.
                int lastX = startX[^1];
                if (lastX > Player.Position.X + 4 && index != charArray.Length)
                {
                    if ((lastX - count) % 2 == 0)
                    {
                        continue;
                    }

                    Console.SetCursorPosition(lastX - 4, targetY);

                    char c = charArray[index++];
                    if (c == ' ')
                    {
                        count++;
                    }

                    Console.Write(c);
                }
            }
        }

        private void DisplayBattle_Ground()
        {
            if (Player == null)
            {
                return;
            }

            int top = Console.CursorTop - 1;
            for (int i = 0; i < 4; i++)
            {
                Utils.ClearLine(0, top + i);
            }

            Console.SetCursorPosition(0, top);
            Console.WriteLine(" (´◎ω◎)");
            Console.Write(" (       つ");
            Player.SetPosition(Console.CursorLeft, Console.CursorTop);

            Console.WriteLine();
            Utils.ClearLine(0, Console.CursorTop);
            Utils.DisplayLine();
        }

        private void DisplayBatte_Monsters()
        {
            int top = Console.CursorTop - 5;
            for (int i = 0; i < Monsters.Count; i++)
            {
                Monsters[i].SetPosition(Monsters[i].Position.X, top);
                Console.SetCursorPosition(Monsters[i].Position.X, top++);
                Monsters[i].Render();
            }
        }
    }
}