namespace SpartaDunGeon
{
    public enum ItemType
    {
        WEAPON,
        ARMOR,
        POTION
    }
    public class Item
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
        internal static void InventoryItemList(Item item, bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"{idx} ");
                Console.ResetColor();
            }
            if (item.IsEquipped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(ConsoleUtility.PadRight(item.Name, 16));
            }
            else Console.Write(ConsoleUtility.PadRight(item.Name, 19));
            Console.Write(" | ");
            if (item.Atk != 0) Console.Write(ConsoleUtility.PadRight($"공격력 {(item.Atk >= 0 ? "+" : "")}{item.Atk} ", 11));
            if (item.Def != 0) Console.Write(ConsoleUtility.PadRight($"방어력 {(item.Def >= 0 ? "+" : "")}{item.Def} ", 11));

            Console.Write(" | ");

            Console.WriteLine(ConsoleUtility.PadRight(item.Desc, 50));
            Console.ResetColor();
        }
        internal static void toggleEquipStatus(Item item)
        {
            item.IsEquipped = !item.IsEquipped;
        }
        public static void StoreItemList(Item item, bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"{idx} ");
                Console.ResetColor();
                if (item.IsPurchased)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write(ConsoleUtility.PadRight(item.Name, 17));
            }
            else
            {
                if (item.IsPurchased)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write(ConsoleUtility.PadRight(item.Name, 19));
            }

            Console.Write(" | ");

            if (item.Atk != 0) Console.Write(ConsoleUtility.PadRight($"공격력 {(item.Atk >= 0 ? "+" : "")}{item.Atk} ", 11));
            if (item.Def != 0) Console.Write(ConsoleUtility.PadRight($"방어력 {(item.Def >= 0 ? "+" : "")}{item.Def} ", 11));

            Console.Write(" | ");

            Console.Write(ConsoleUtility.PadRight(item.Desc, 50));

            Console.Write(" | ");

            if (item.IsPurchased)
            {
                ConsoleUtility.PrintColoredText(ConsoleColor.Yellow, "구매완료\n");
            }
            else
            {
                Console.WriteLine($"{item.Price} G");
            }
        }
        public static void StoreItemSellList(Item item, bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"{idx} ");
                Console.ResetColor();
            }
            if (item.IsEquipped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(ConsoleUtility.PadRight(item.Name, 16));
            }
            else Console.Write(ConsoleUtility.PadRight(item.Name, 19));
            Console.Write(" | ");
            if (item.Atk != 0) Console.Write(ConsoleUtility.PadRight($"공격력 {(item.Atk >= 0 ? "+" : "")}{item.Atk} ", 11));
            if (item.Def != 0) Console.Write(ConsoleUtility.PadRight($"방어력 {(item.Def >= 0 ? "+" : "")}{item.Def} ", 11));

            Console.Write(" | ");

            Console.Write(ConsoleUtility.PadRight(item.Desc, 50));

            Console.Write(" | ");

            Console.WriteLine($"{Math.Round(item.Price * 0.85)} G");
            Console.ResetColor();
        }
        internal static void Buy(Item item)
        {
            item.IsPurchased = !item.IsPurchased;
        }
        internal static void Sell(Item item)
        {
            if (item.IsEquipped)
            {
                item.IsEquipped = false;
            }
            item.IsPurchased = false;
        }
    }
}
