﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDunGeon
{
    internal class Inventory
    {
        public static List<Item> inventory;
        public Inventory()
        {
            inventory = new List<Item>();
        }
        public void InventoryMenu(Player player)
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < inventory.Count; i++)
            {
                Item.InventoryItemList(inventory[i]);
            }
            Console.WriteLine("");
            Console.WriteLine("1. 장착관리");
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "0. 나가기\n");
            Console.WriteLine("");
            switch (ConsoleUtility.ChoiceMenu(0, 1))
            {
                case 0:
                    GameManager.MainMenu(player);
                    break;
                case 1:
                    EquipMenu(player);
                    break;
            }
        }

        //장비 장착
        private void EquipMenu(Player player)
        {
            Console.Clear();

            Console.WriteLine("인벤토리 - 장착관리");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < inventory.Count; i++)
            {
                Item.InventoryItemList(inventory[i], true, i + 1);
            }
            Console.WriteLine("");
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "0. 나가기\n");

            int keyInput = ConsoleUtility.ChoiceMenu(0, inventory.Count);

            switch (keyInput)
            {
                case 0:
                    InventoryMenu(player);
                    break;
                default:
                    if (inventory[keyInput - 1].IsEquipped == false)
                    {
                        foreach (Item item in inventory)
                        {
                            if (item.IsEquipped == true && item.Type == inventory[keyInput - 1].Type)
                            {
                                Item.toggleEquipStatus(item);
                                if (item.Atk != 0) player.BonusAtk -= item.Atk;
                                if (item.Def != 0) player.BonusDef -= item.Def;
                                break;
                            }
                        }
                        Item.toggleEquipStatus(inventory[keyInput - 1]);
                        if (inventory[keyInput - 1].Atk != 0) player.BonusAtk += inventory[keyInput - 1].Atk;
                        if (inventory[keyInput - 1].Def != 0) player.BonusDef += inventory[keyInput - 1].Def;
                    }
                    else
                    {
                        Item.toggleEquipStatus(inventory[keyInput - 1]);
                        if (inventory[keyInput - 1].Atk != 0) player.BonusAtk -= inventory[keyInput - 1].Atk;
                        if (inventory[keyInput - 1].Def != 0) player.BonusDef -= inventory[keyInput - 1].Def;
                    }
                    EquipMenu(player);
                    break;
            }
        }
    }
}