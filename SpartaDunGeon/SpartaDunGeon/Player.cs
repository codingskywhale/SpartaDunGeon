public class Player
{
    public string Name { get; set; }
    public string Job { get; }
    public int Lv { get; }
    public int Atk { get; }
    public int Def { get; }
    public int Hp { get; }
    public int Gold { get; set; }

    public Player(string name, string job, int lv, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Lv = lv;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
    }
}