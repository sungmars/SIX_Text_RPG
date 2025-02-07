namespace SIX_Text_RPG.Scenes;

internal class Scene_EnemyPhase : Scene_Base
{
    private Player player = GameManager.Instance.Player;
    private List<Monster> monsters = GameManager.Instance.Monsters;
    private int monsterCount;

    public Scene_EnemyPhase(int monsterCount)
    {
        this.monsterCount = monsterCount;
    }
    
    public override void Awake()
    {
        base.Awake();
        hasZero = true;
        sceneTitle = "Battle!!";
        sceneInfo = "";
        zeroText = "다음";

        EnemyAttack();
    }

    public override int Update()
    {
        switch (base.Update())
        {
            case 0 :
                monsterCount++;
                if (player.Stats.HP <= 0)
                {
                    // 플레이어 사망시 결과로
                    Program.CurrentScene = new Scene_BattleResult(false);
                    return 0;
                }
                if (monsters.Count == monsterCount) 
                {
                    // 몬스터들 공격 완료후 player턴
                    Program.CurrentScene = new Scene_BattleStart();
                    return 0;
                }
                
                Program.CurrentScene = new Scene_EnemyPhase(monsterCount);
                return 0;
        }
        return 0;
    }

    protected override void Display()
    {
        // enemy 1 scene에 1마리
        if (!monsters[monsterCount].IsDead)
        {
            DisplayMonsterAttack();
        }
        
    }

    private void EnemyAttack()
    {
        if (!monsters[monsterCount].IsDead)
            player.Damaged(monsters[monsterCount].Stats.ATK);
    }

    private void DisplayMonsterAttack()
    {
        var monster = monsters[monsterCount];
        var damage = monster.Stats.ATK;
        Console.WriteLine($"Lv .{monster.Stats.Level} {monster.Stats.Name}의 공격!");
        Console.WriteLine($"{player.Stats.Name}을(를) 맞췄습니다.     " + $"[데미지 : {damage}]");
        Console.WriteLine("");
        Console.WriteLine($"Lv . {player.Stats.Level} {player.Stats.Name}");
        Console.WriteLine($"HP {player.Stats.HP} / {player.Stats.MaxHP} -> {player.Stats.HP} / {player.Stats.MaxHP}");
    }
}