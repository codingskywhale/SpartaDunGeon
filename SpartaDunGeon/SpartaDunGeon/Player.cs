using Spartadungeon;
using SpartaDunGeon;
using System.Numerics;

public class Player : Character
{
    public bool IsSkillUse = false;

    public string Job { get; }
    public int Exp { get; set;}
    public int MaxExp { get; set; }
    public int Mp { get; set; }
    public int MaxHp { get; set; }
    public int MaxMp { get; set; }
    public int Gold { get; set; }
    public int BonusAtk { get; set; }
    public int BonusDef { get; set; }

    public Player(string name, string job, int lv, int exp, int maxExp, int atk, int def, int hp, int id, int mp, int maxHp, int maxMp, int gold, int bonusAtk = 0, int bonusDef = 0) : base(name, lv, exp, atk, def, hp, id)
    {
        Job = job;
        MaxExp = maxExp;
        Mp = mp;
        MaxHp = maxHp;
        MaxMp = maxMp;
        Gold = gold;
        BonusAtk = bonusAtk;
        BonusDef = bonusDef;
        Id = id;
        Exp = exp;
    }

    public void ExpAdd(int AddExp)
    {
        Exp += AddExp;

        if ( Exp >= MaxExp )
        {
            Lv++;
            Exp -= MaxExp;
            MaxExp += Lv * 20;

            // 레벨업 퀘스트
            if (QuestManager.questList[3].IsProceeding)
            {
                QuestManager.questList[3].UpdateQuestProgress(this);
            }

            if(Job == "전사")
            {
                MaxHp += 5;
                MaxMp += 2;
                Hp += 5;
                Mp += 2;
                Atk += 1;
                Def += 1;
            }
            if (Job == "마법사")
            {
                MaxHp += 2;
                MaxMp += 5;
                Hp += 2;
                Mp += 5;
                Atk += 1;
                Def += 1;
            }
        }
    }

    public static void SkillUse(Player player)
    {
        player.IsSkillUse = true;
        if (player.Job == "전사")
        {
            Player.WarriorSkill(player);
        }
        if (player.Job == "마법사")
        {
            Player.WizardSkill(player);
        }
    }

    public static void WarriorSkill(Player player)
    {
        Console.WriteLine("사용할 스킬을 선택하세요.\n");

        Console.WriteLine("1. 힘껏치기\t2. 휴식하기\n3. 급소공격\t4. 하늘가르기");

        int Choise = ConsoleUtility.ChoiceMenu(1, 4);

        switch (Choise)
        {
            

            case 1:
                if (player.Mp >= 10)
                {
                    player.Mp -= 20;
                    player.BaseAtk *= 2;
                    Dungeon.PlayerTurn(player);
                    break;
                }
                else
                {
                    Console.WriteLine("마나가 부족합니다.");
                    Thread.Sleep(1000);
                    Dungeon.DungeonScene(player);
                    break;
                }
            case 2:
                if (player.Mp >= 20)
                {
                    player.Hp += player.Atk;
                    if (player.Hp >= player.MaxHp)
                    {
                        player.Hp = player.MaxHp;
                    }
                    Dungeon.PlayerTurn(player);
                    break;
                }
                else
                {
                    Console.WriteLine("마나가 부족합니다.");
                    Thread.Sleep(1000);
                    Dungeon.DungeonScene(player);
                    break;
                }
            case 3:
                if (player.Mp >= 30)
                {
                    player.Mp -= 30;
                    player.BaseAtk *= 3;
                    Dungeon.PlayerTurn(player);
                    break;
                }
                else
                {
                    Console.WriteLine("마나가 부족합니다.");
                    Thread.Sleep(1000);
                    Dungeon.DungeonScene(player);
                    break;
                }
            case 4:
                if (player.Mp >= 50)
                {
                    player.Mp -= 50;
                    player.BaseAtk *= 5;
                    Dungeon.PlayerTurn(player);
                    break;
                }
                else
                {
                    Console.WriteLine("마나가 부족합니다.");
                    Thread.Sleep(1000);
                    Dungeon.DungeonScene(player);
                    break;
                }
        }
    }

    public static void WizardSkill(Player player)
    {
        Console.WriteLine("사용할 스킬을 선택하세요.\n");

        Console.WriteLine("1. 파이어\t2. 힐\n3. 다크홀\n 4.빅뱅\n");

        int Choise = ConsoleUtility.ChoiceMenu(1, 4);

        switch (Choise)
        {
            case 1:
                if (player.Mp >= 10)
                {
                    player.Mp -= 10;
                    player.BaseAtk *= 2;
                    Dungeon.PlayerTurn(player);
                    break;
                }
                else
                {
                    Console.WriteLine("마나가 부족합니다.");
                    Thread.Sleep(1000);
                    Dungeon.DungeonScene(player);
                    break;
                }
            case 2:
                if (player.Mp >= 20)
                {
                    player.Hp += player.Atk;
                    if (player.Hp >= player.MaxHp)
                    {
                        player.Hp = player.MaxHp;
                    }
                    Dungeon.PlayerTurn(player);
                    break;
                }
                else
                {
                    Console.WriteLine("마나가 부족합니다.");
                    Thread.Sleep(1000);
                    Dungeon.DungeonScene(player);
                    break;
                }
            case 3:
                if (player.Mp >= 30)
                {
                    player.Mp -= 30;
                    player.BaseAtk *= 3;
                    Dungeon.PlayerTurn(player);
                    break;
                }
                else
                {
                    Console.WriteLine("마나가 부족합니다.");
                    Thread.Sleep(1000);
                    Dungeon.DungeonScene(player);
                    break;
                }
            case 4:
                if (player.Mp >= 50)
                {
                    player.Mp -= 50;
                    player.BaseAtk *= 5;
                    Dungeon.PlayerTurn(player);
                    break;
                }
                else
                {
                    Console.WriteLine("마나가 부족합니다.");
                    Thread.Sleep(1000);
                    Dungeon.DungeonScene(player);
                    break;
                }
        }
    }

    public void ResetSkill()
    {
        IsSkillUse = false;

        BaseAtk = Atk;
    }
}