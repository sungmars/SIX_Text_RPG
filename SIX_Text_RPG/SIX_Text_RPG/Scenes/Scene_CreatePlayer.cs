namespace SIX_Text_RPG.Scenes
{
    internal class Scene_CreatePlayer : Scene_Base
    {
        public static string PlayerName { get; set; } = string.Empty;

        private static readonly string[] questions =
        {
            " 안녕하세요. 이름이 무엇인가요?",
            " 음... 죄송하지만, 저희 클럽이랑 name님 스타일이 안 맞아서 입장이 힘드실 것 같아요.",
            " 흠... 알겠습니다. 저희 클럽은 어떤 경로로 알게되셨나요?",
            " 좋습니다. 마지막으로 하고 싶으신 말씀이라도 있으실까요?"
        };

        private readonly string[] answers = new string[questions.Length];

        public override void Awake()
        {
            base.Awake();

            hasZero = false;
            sceneTitle = $"{Define.GAME_TITLE} 입구";
            sceneInfo = "" +
                "\n  ##      ## ######## ##        ######   #######  ##     ##    ##    ##  #######  ##     ##    ####  " +
                "\n  ##  ##  ## ##       ##       ##    ## ##     ## ###   ###     ##  ##  ##     ## ##     ##    ####  " +
                "\n  ##  ##  ## ##       ##       ##       ##     ## #### ####      ####   ##     ## ##     ##    ####  " +
                "\n  ##  ##  ## ######   ##       ##       ##     ## ## ### ##       ##    ##     ## ##     ##     ##   " +
                "\n  ##  ##  ## ##       ##       ##       ##     ## ##     ##       ##    ##     ## ##     ##          " +
                "\n  ##  ##  ## ##       ##       ##    ## ##     ## ##     ##       ##    ##     ## ##     ##    ####  " +
                "\n   ###  ###  ######## ########  ######   #######  ##     ##       ##     #######   #######     ####\n";
        }

        public override int Update()
        {
            Program.CurrentScene = new Scene_CreateCharacter();
            return 0;
        }

        protected override void Display()
        {
            for (int i = 0; i < questions.Length; i++)
            {
                if (i == 1) PlayerName = answers[0];

                Utils.WriteColorLine(" 윤수빈 매니저님", ConsoleColor.DarkCyan);
                Utils.WriteName(questions[i]);

                answers[i] = ReadLine();
                Console.WriteLine();
            }

            Utils.WriteAnim(" 플레이어 이름을 추첨 중입니다...");

            Random random = new Random();
            int randomIndex = random.Next(1, questions.Length);
            PlayerName = answers[randomIndex];

            Utils.WriteColor("\n >> ", ConsoleColor.DarkYellow);
            Utils.WriteName($"축하합니다! 당신의 이름은 이제부터 name입니다.");
            Console.ReadKey();
        }

        private string ReadLine()
        {
            while (true)
            {
                Utils.WriteColor("\n >> ", ConsoleColor.DarkYellow);

                int top = Console.CursorTop;
                string? input = Console.ReadLine();

                if (input == null)
                {
                    Console.SetCursorPosition(4, top);
                    Console.Write(Define.ERROR_MESSAGE_INPUT);
                    continue;
                }

                input = input.Replace(" ", string.Empty);
                if (input.Length == 0)
                {
                    Console.SetCursorPosition(4, top);
                    Console.Write(Define.ERROR_MESSAGE_INPUT);
                    continue;
                }

                return input;
            }
        }
    }
}