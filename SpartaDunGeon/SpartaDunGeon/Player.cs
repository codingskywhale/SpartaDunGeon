using System.Numerics;

public class Player
{
    public string Name { get; set; }
    public string Job { get; }
    public int Lv { get; set; }
    public int Exp { get; set;}
    public int MaxExp { get; set; }
    public int Atk { get; set;}
    public int Def { get; set; }
    public int Hp { get; set; }
    public int Mp { get; set; }
    public int MaxHp { get; set; }
    public int MaxMp { get; set; }
    public int Gold { get; set; }
    public int BonusAtk { get; set; }
    public int BonusDef { get; set; }

    public Player(string name, string job, int lv, int exp, int maxExp, int atk, int def, int hp, int mp, int maxHp, int maxMp, int gold, int bonusAtk = 0, int bonusDef = 0)
    {
        Name = name;
        Job = job;
        Lv = lv;
        Exp = exp;
        MaxExp = maxExp;
        Atk = atk;
        Def = def;
        Hp = hp;
        Mp = mp;
        MaxHp = maxHp;
        MaxMp = maxMp;
        Gold = gold;
        BonusAtk = bonusAtk;
        BonusDef = bonusDef;
    }

    public void ExpAdd(int AddExp)
    {
        Exp += AddExp;

        Console.WriteLine($"{AddExp}의 경험치를 획득했습니다.");

        if ( Exp >= MaxExp )
        {
            Lv++;
            Exp -= MaxExp;
            MaxExp += Lv * 20;

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
}