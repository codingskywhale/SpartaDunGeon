public class Monster
{
    public List<Monster> monsters;
    public string Name { get; set; }
    public int Idx { get; }
    public int Lv { get; }
    public int Atk { get; }
    public int Def { get; }
    public int Hp { get; }
    public int Gold { get; set; }

    public Monster( string name, int idx, int lv, int atk, int def, int hp, int gold)
    {

        Name = name;
        Idx = idx;
        Lv = lv;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
    }

    public void monsterList()
    {
        monsters = new List<Monster>();

        monsters.Add(new Monster("슬라임",0 , 1, 1, 1, 3, 5));
        monsters.Add(new Monster("고블린",1 , 2, 2, 1, 5, 10));
        monsters.Add(new Monster("코볼트",2 , 3, 7, 3, 10, 30));
        monsters.Add(new Monster("오크", 3, 5, 10, 5, 20, 50));
        monsters.Add(new Monster("드래곤", 4, 20, 20, 20, 100, 200));
    }
}