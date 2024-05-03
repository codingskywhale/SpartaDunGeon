using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Character
{
    public string Name { get; set; }
    public int Id { get; set; }
    public int Lv { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Hp { get; set; }

    public Character(string name, int lv, int atk, int def, int hp, int id)
    {
        Name = name;
        Lv = lv;
        Atk = atk;
        Def = def;
        Hp = hp;
        Id = id;
    }

}
