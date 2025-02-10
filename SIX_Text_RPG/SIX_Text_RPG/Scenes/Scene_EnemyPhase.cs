namespace SIX_Text_RPG.Scenes;

internal class Scene_EnemyPhase : Scene_Base
{
    private List<Monster> monsters = new List<Monster>();
    private int monsterCount;

    public Scene_EnemyPhase(List<Monster> monsters, int monsterCount)
    {
        this.monsters = monsters;
        this.monsterCount = monsterCount;
    }

    public override void Awake()
    {
        base.Awake();
        hasZero = false;
        sceneTitle = "Battle!!";
        sceneInfo = "???";
        Menu.Add("����");

        EnemyAttack();
    }

    public override int Update()
    {
        Player? player = GameManager.Instance.Player;
        if (player == null)
        {
            return 0;
        }

        switch (base.Update())
        {
            case 1:
                monsterCount++;
                if (player.Stats.HP <= 0)
                {
                    // �÷��̾� ����� �����
                    Program.CurrentScene = new Scene_BattleResult(false);
                    break;
                }
                if (monsters.Count == monsterCount)
                {
                    // ���͵� ���� �Ϸ��� player��
                    Program.CurrentScene = new Scene_BattleLobby();
                    break;
                }

                Program.CurrentScene = new Scene_EnemyPhase(monsters, monsterCount);
                break;
        }

        return 0;
    }

    protected override void Display()
    {
        // enemy 1 scene�� 1����
        if (!monsters[monsterCount].IsDead)
        {
            DisplayMonsterAttack();
        }

    }

    private void EnemyAttack()
    {
        Player? player = GameManager.Instance.Player;
        if (player == null)
        {
            return;
        }

        if (!monsters[monsterCount].IsDead)
            player.Damaged(monsters[monsterCount].Stats.ATK);
    }

    private void DisplayMonsterAttack()
    {
        Player? player = GameManager.Instance.Player;
        if (player == null)
        {
            return;
        }

        var monster = monsters[monsterCount];
        var damage = monster.Stats.ATK;
        Console.WriteLine($"Lv .{monster.Stats.Level} {monster.Stats.Name}�� ����!");
        Console.WriteLine($"{player.Stats.Name}��(��) ������ϴ�.     " + $"[������ : {damage}]");
        Console.WriteLine("");
        Console.WriteLine($"Lv . {player.Stats.Level} {player.Stats.Name}");
        Console.WriteLine($"HP {player.Stats.HP} / {player.Stats.MaxHP} -> {player.Stats.HP} / {player.Stats.MaxHP}");
    }
}