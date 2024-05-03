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

    public void SkillUse()
    {
        IsSkillUse = true;
        if (Job == "전사")
        {
            WarriorSkill();
        }
        if (Job == "마법사")
        {
            WizardSkill();
        }
    }

    public void WarriorSkill()
    {
        Console.WriteLine("사용할 스킬을 선택하세요.\n");

        Console.WriteLine("1. 힘껏치기\t2. 휴식하기\n");

        int Choise = ConsoleUtility.ChoiceMenu(1, 4);

        switch (Choise)
        {
            

            case 1:
                if (Mp >= 10)
                {
                    Mp -= 20;
                    BaseAtk *= 2;
                    GameManager.dungeon.PlayerTurn(this);
                    break;
                }
                else
                {
                    Console.WriteLine("마나가 부족합니다.");
                    Thread.Sleep(1000);
                    GameManager.dungeon.DungeonScene(this);
                    break;
                }
            case 2:
                if (Mp >= 20)
                {
                    Hp += Atk;
                    if (Hp >= MaxHp)
                    {
                        Hp = MaxHp;
                    }
                    GameManager.dungeon.PlayerTurn(this);
                    break;
                }
                else
                {
                    Console.WriteLine("마나가 부족합니다.");
                    Thread.Sleep(1000);
                    GameManager.dungeon.DungeonScene(this);
                    break;
                }
        }
    }

    public void WizardSkill()
    {
        Console.WriteLine("사용할 스킬을 선택하세요.\n");

        Console.WriteLine("1. 파이어\t2. 힐\n");

        int Choise = ConsoleUtility.ChoiceMenu(1, 4);

        switch (Choise)
        {
            case 1:
                if (Mp >= 10)
                {
                    Mp -= 10;
                    BaseAtk *= 2;
                    GameManager.dungeon.PlayerTurn(this);
                    break;
                }
                else
                {
                    Console.WriteLine("마나가 부족합니다.");
                    Thread.Sleep(1000);
                    GameManager.dungeon.DungeonScene(this);
                    break;
                }
            case 2:
                if (Mp >= 20)
                {
                    Hp += Atk;
                    if (Hp >= MaxHp)
                    {
                        Hp = MaxHp;
                    }
                    GameManager.dungeon.PlayerTurn(this);
                    break;
                }
                else
                {
                    Console.WriteLine("마나가 부족합니다.");
                    Thread.Sleep(1000);
                    GameManager.dungeon.DungeonScene(this);
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