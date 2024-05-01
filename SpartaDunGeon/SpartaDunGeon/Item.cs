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
        internal void InventoryItemList(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"{idx} ");
                Console.ResetColor();
            }
            if (IsEquipped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(ConsoleUtility.PadRight(Name, 16));
            }
            else Console.Write(ConsoleUtility.PadRight(Name, 19));
            Console.Write(" | ");
            if (Atk != 0) Console.Write(ConsoleUtility.PadRight($"공격력 {(Atk >= 0 ? "+" : "")}{Atk} ", 11));
            if (Def != 0) Console.Write(ConsoleUtility.PadRight($"방어력 {(Def >= 0 ? "+" : "")}{Def} ", 11));

            Console.Write(" | ");

            Console.WriteLine(ConsoleUtility.PadRight(Desc, 50));
            Console.ResetColor();
        }
        internal void toggleEquipStatus()
        {
            IsEquipped = !IsEquipped;
        }
        public void StoreItemList(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"{idx} ");
                Console.ResetColor();
                if (IsPurchased)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write(ConsoleUtility.PadRight(Name, 17));
            }
            else
            {
                if (IsPurchased)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write(ConsoleUtility.PadRight(Name, 19));
            }

            Console.Write(" | ");

            if (Atk != 0) Console.Write(ConsoleUtility.PadRight($"공격력 {(Atk >= 0 ? "+" : "")}{Atk} ", 11));
            if (Def != 0) Console.Write(ConsoleUtility.PadRight($"방어력 {(Def >= 0 ? "+" : "")}{Def} ", 11));

            Console.Write(" | ");

            Console.Write(ConsoleUtility.PadRight(Desc, 50));

            Console.Write(" | ");

            if (IsPurchased)
            {
                ConsoleUtility.PrintColoredText(Color.Yellow, "구매완료\n");
            }
            else
            {
                Console.WriteLine($"{Price} G");
            }
        }
        internal void StoreItemSellList(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"{idx} ");
                Console.ResetColor();
            }
            if (IsEquipped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(ConsoleUtility.PadRight(Name, 16));
            }
            else Console.Write(ConsoleUtility.PadRight(Name, 19));
            Console.Write(" | ");
            if (Atk != 0) Console.Write(ConsoleUtility.PadRight($"공격력 {(Atk >= 0 ? "+" : "")}{Atk} ", 11));
            if (Def != 0) Console.Write(ConsoleUtility.PadRight($"방어력 {(Def >= 0 ? "+" : "")}{Def} ", 11));

            Console.Write(" | ");

            Console.Write(ConsoleUtility.PadRight(Desc, 50));

            Console.Write(" | ");

            Console.WriteLine($"{Math.Round(Price * 0.85)} G");
            Console.ResetColor();
        }
        internal void Buy()
        {
            IsPurchased = !IsPurchased;
        }
        internal void Sell()
        {
            if (IsEquipped)
            {
                IsEquipped = false;
            }
            IsPurchased = false;
        }
    }
}
