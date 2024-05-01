namespace SpartaDunGeon
{
    public enum ItemType
    {
        WEAPON,
        ARMOR,
        POTION
    }
    internal class Item
    {
        public string Name { get; }
        public string Desc { get; }

        public ItemType Type;
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Price { get; }
        public bool IsEquipped { get; private set; }
        public bool IsPurchased { get; private set; }
        public Item(string name, string desc, ItemType type, int atk, int def, int hp, int price, bool isEquipped = false, bool isPurchased = false)
        {
            Name = name;
            Desc = desc;
            Type = type;
            Atk = atk;
            Def = def;
            Hp = hp;
            Price = price;
            IsEquipped = isEquipped;
            IsPurchased = isPurchased;
        }
    }
}
