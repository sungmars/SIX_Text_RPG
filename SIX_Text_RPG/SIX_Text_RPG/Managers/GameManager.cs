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

        public List<Item> Inventory { get; private set; } = new();
        public List<Monster> Monsters { get; private set; } = new(Define.MONSTERS_CAPACITY);

        public int CurrentStage { get; set; } = -1;
        public int TargetStage { get; set; } = 0;
        public float TotalDamage { get; set; } = 0;

        private readonly Random random = new();

        public void DisplayBattle()
        {
            if (Player == null)
            {
                return;
            }

            Console.SetCursorPosition(0, 18);
            DisplayBattle_Ground();
            (int left, int top) = Console.GetCursorPosition();

            DisplayBatte_Monsters();

            Console.SetCursorPosition(left, top);
            Utils.ClearBuffer();
        }

        public void DisplayBattle_Attack(int targetIndex, int attackCount, Action? onHit)
        {
            if (Player == null)
            {
                return;
            }

            int[] startX = new int[attackCount];
            int endX = Define.MONSTER_SPAWN_X - 3;
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
                    Utils.WriteColor(value, Player.Color_Weapon);
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

        public void DisplayBattle_Damage(Action[] onDamage)
        {
            if (Player == null)
            {
                return;
            }

            int monsterCount = onDamage.Length;
            int[] startX = new int[monsterCount];
            int[] startY = new int[monsterCount];
            int endX = Player.Position.X;

            // 투사체 발사지점을 설정합니다.
            for (int i = 0; i < monsterCount; i++)
            {
                startX[i] = Define.MONSTER_SPAWN_X + i * 36 - 1;
                startY[i] = Monsters[i].Position.Y;
            }

            // 마지막 투사체가 목표 지점에 도착하면 종료됩니다.
            int[] count = new int[monsterCount];  // 공백은 1글자로 취급되기 때문에 글자간 공백을 맞추기 위해 공백 숫자를 저장해둘 변수입니다.
            int[] randomIndex = new int[monsterCount];
            char[][] charArray = new char[monsterCount][];
            int maxLength = 0;  // charArray 중 가장 긴 문자열을 확인합니다.
            for (int i = 0; i < randomIndex.Length; i++)
            {
                randomIndex[i] = random.Next(0, Define.MONSTER_ATK_SCRIPTS.Length);
                charArray[i] = $"{Define.MONSTER_ATK_SCRIPTS[randomIndex[i]]} ".ToCharArray();
                maxLength = Math.Max(charArray[i].Length, maxLength);
            }

            while (startX[^1] + charArray[^1].Length * 2 - count[^1] > endX)
            {
                // 몬스터 수만큼 반복합니다.
                for (int i = 0; i < monsterCount; i++)
                {
                    if (Monsters[i].IsDead)
                    {
                        startX[i]--;
                        continue;
                    }

                    // 공백 수를 초기화합니다.
                    count[i] = 0;

                    // 투사체 좌표를 설정합니다.
                    startX[i]--;

                    // 투사체가 목표지점에 도달할 경우
                    if (startX[i] == endX)
                    {
                        // Hit 애니메이션과 onDamage 콜백을 호출합니다.
                        if (onDamage == null)
                        {
                            AudioManager.Instance.Play(AudioClip.SoundFX_Avoid);
                        }
                        else
                        {
                            onDamage[i].Invoke();
                            Player.Render_Hit();
                        }
                    }

                    // 투사체를 렌더링합니다.
                    int length = charArray[i].Length;
                    for (int j = 0; j < length + 1; j++)
                    {
                        // 투사체 X 좌표
                        int projX = startX[i] + j * 2;

                        // 투사체가 몬스터보다 뒤에 있거나, 목표지점을 초과하면 지우지 않음
                        if (projX >= Monsters[i].Position.X || projX < endX)
                        {
                            continue;
                        }

                        if (startX[i] % 2 != 0)
                        {
                            continue;
                        }

                        Console.SetCursorPosition(projX - count[i], startY[i]);

                        char c = j == length ? ' ' : charArray[i][j];
                        if (c == ' ')
                        {
                            count[i]++;
                        }
                        Console.Write(c);
                    }
                }

                Thread.Sleep(5);
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
            Player.Render();

            Console.WriteLine();
            Utils.ClearLine(0, Console.CursorTop);
            Console.WriteLine();
            Utils.DisplayLine();
            Console.SetCursorPosition(0, Console.CursorTop - 1);
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