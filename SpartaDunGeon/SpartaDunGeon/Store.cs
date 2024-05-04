using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDunGeon
{
    internal class Store
    {
        public static List<Item> storeInventory;
        public Store()
        {
            storeInventory = new List<Item>();
            storeInventory.Add(new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", ItemType.ARMOR, 0, 5, 0, 100));
            storeInventory.Add(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", ItemType.ARMOR, 0, 9, 0, 200));
            storeInventory.Add(new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", ItemType.ARMOR, 0, 15, 0, 3500));
            storeInventory.Add(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", ItemType.WEAPON, 2, 0, 0, 100));
            storeInventory.Add(new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", ItemType.WEAPON, 5, 0, 0, 200));
            storeInventory.Add(new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", ItemType.WEAPON, 7, 0, 0, 3000));
        }
        //상점
        public void StoreMenu(Player player)
        {
            Console.Clear();

            ConsoleUtility.PrintColoredText(ConsoleColor.Yellow, "상점\n");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < storeInventory.Count; i++)
            {
                Item.StoreItemList(storeInventory[i]);
            }
            Console.WriteLine("");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("3. 아이템 일괄 판매 - 장착하지 않은 아이템 모두 판매");
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "0. 나가기\n");
            Console.WriteLine("");
            switch (ConsoleUtility.ChoiceMenu(0, 3))
            {
                case 0:
                    GameManager.MainMenu(player);
                    break;
                case 1:
                    BuyMenu(player);
                    break;
                case 2:
                    SellMenu(player);
                    break;
                case 3:
                    BulkSalesMenu(player);
                    break;
            }
        }

        //상점 - 구매
        private void BuyMenu(Player player)
        {
            Console.Clear();

            ConsoleUtility.PrintColoredText(ConsoleColor.Yellow, "상점 - 구매\n");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < storeInventory.Count; i++)
            {
                Item.StoreItemList(storeInventory[i], true, i + 1);
            }
            Console.WriteLine("");
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "0. 나가기\n");
            Console.WriteLine("");

            int keyInput = ConsoleUtility.ChoiceMenu(0, storeInventory.Count);

            switch (keyInput)
            {
                case 0:
                    StoreMenu(player);
                    break;
                default:
                    if (storeInventory[keyInput - 1].IsPurchased)
                    {
                        ConsoleUtility.PrintColoredText(ConsoleColor.Red, "이미 구매한 아이템입니다.");
                        Thread.Sleep(500);
                        BuyMenu(player);
                    }
                    else if (player.Gold >= storeInventory[keyInput - 1].Price)
                    {
                        player.Gold -= storeInventory[keyInput - 1].Price;
                        Item.Buy(storeInventory[keyInput - 1]);
                        Inventory.inventory.Add(storeInventory[keyInput - 1]);
                        ConsoleUtility.PrintColoredText(ConsoleColor.Blue, "아이템을 구매했습니다");
                        Thread.Sleep(500);
                        BuyMenu(player);
                    }
                    else
                    {
                        ConsoleUtility.PrintColoredText(ConsoleColor.Red, "Gold가 부족합니다.");
                        Thread.Sleep(500);
                        BuyMenu(player);
                    }
                    break;
            }
        }

        //상점 - 판매
        private void SellMenu(Player player)
        {
            Console.Clear();

            ConsoleUtility.PrintColoredText(ConsoleColor.Yellow, "상점 - 판매\n");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine("");
            Console.Write("[아이템 목록]  ");
            if (Inventory.inventory.Count >= 10)
            {
                ConsoleUtility.PrintColoredText(ConsoleColor.Red, $"{Inventory.inventory.Count} / 10\n");
            }
            else Console.WriteLine($"{Inventory.inventory.Count} / 10");
            for (int i = 0; i < Inventory.inventory.Count; i++)
            {
                Item.StoreItemSellList(Inventory.inventory[i], true, i + 1);
            }
            Console.WriteLine("");
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "0. 나가기\n");

            int keyInput = ConsoleUtility.ChoiceMenu(0, Inventory.inventory.Count);

            switch (keyInput)
            {
                case 0:
                    StoreMenu(player);
                    break;
                default:
                    if (Inventory.inventory[keyInput - 1].IsEquipped)
                    {
                        if (Inventory.inventory[keyInput - 1].Atk != 0) player.BonusAtk -= Inventory.inventory[keyInput - 1].Atk;
                        if (Inventory.inventory[keyInput - 1].Def != 0) player.BonusDef -= Inventory.inventory[keyInput - 1].Def;
                    }
                    foreach (Item item in Store.storeInventory)
                    {
                        if (item.Name == Inventory.inventory[keyInput - 1].Name)
                        {
                            Item.Sell(item);
                        }
                    }
                    Item.Sell(Inventory.inventory[keyInput - 1]);
                    player.Gold += (int)Math.Round(Inventory.inventory[keyInput - 1].Price * 0.85);
                    Item Sell = Inventory.inventory[keyInput - 1];
                    Inventory.inventory.Remove(Sell);
                    SellMenu(player);
                    break;
            }
        }
        // 아이템 일괄 판매
        private void BulkSalesMenu(Player player)
        {
            Console.Clear();

            ConsoleUtility.PrintColoredText(ConsoleColor.Yellow, "상점 - 일괄 판매\n");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine("");
            Console.Write("[아이템 목록]  ");
            if (Inventory.inventory.Count >= 10)
            {
                ConsoleUtility.PrintColoredText(ConsoleColor.Red, $"{Inventory.inventory.Count} / 10\n");
            }
            else Console.WriteLine($"{Inventory.inventory.Count} / 10");
            for (int i = 0; i < Inventory.inventory.Count; i++)
            {
                Item.StoreItemSellList(Inventory.inventory[i], true, i + 1);
            }
            Console.WriteLine("");
            foreach (Item item in Inventory.inventory)
            {
                if (!item.IsEquipped)
                {
                    foreach (Item _item in Store.storeInventory)
                    {
                        if (_item.Name == item.Name)
                        {
                            Item.Sell(_item);
                        }
                    }
                    Console.WriteLine("판매중...");
                    Thread.Sleep(500);
                    Item.Sell(item);
                    player.Gold += (int)Math.Round(item.Price * 0.85);
                    Inventory.inventory.Remove(item);
                    BulkSalesMenu(player);
                }
            }
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "0. 나가기\n");

            switch (ConsoleUtility.ChoiceMenu(0, 0))
            {
                case 0:
                    StoreMenu(player);
                    break;
            }
        }
    }
}
