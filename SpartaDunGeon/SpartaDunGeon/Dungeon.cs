using SpartaDunGeon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Spartadungeon
{
    public class Dungeon
    {
        public static int stageSelect;

        public static List<Monster> spawnList;

        public Dungeon()
        {
            spawnList = new List<Monster>();
        }

        public void StageScene(Player player)
        {
            Console.Clear();
            ConsoleUtility.PrintColoredText(ConsoleColor.Yellow, "던전입장\n");
            Console.WriteLine("던전에 입장할 수 있습니다.\n");
            ConsoleUtility.PrintTextHighlight(ConsoleColor.DarkYellow, "", "1 ", "Stage 1");
            Console.WriteLine();
            if(GameManager.stage.num >= 2)
            {
                ConsoleUtility.PrintTextHighlight(ConsoleColor.DarkYellow, "", "2 ", "Stage 2");
                Console.WriteLine();
            }
            
            if(GameManager.stage.num >= 3)
            {
                ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, "3 ");
                ConsoleUtility.PrintColoredText(ConsoleColor.Red, "BOSS Stage");
                Console.WriteLine();
            }

            Console.WriteLine();
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "0. 나가기\n\n");

            if(GameManager.stage.num <= 1)
            {
                stageSelect = ConsoleUtility.ChoiceMenu(0, 1);
            }

            else if (GameManager.stage.num <= 2)
            {
                stageSelect = ConsoleUtility.ChoiceMenu(0, 2);
            }

            else
            {
                stageSelect = ConsoleUtility.ChoiceMenu(0, 3);
            }
            
            if(stageSelect == 0)
            {
                GameManager.MainMenu(player);
            }
            
            MonsterSpawn(player, stageSelect);
        }

        public static void DungeonScene(Player player)
        {
            Console.Clear();
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "Battle!!\n");
            Console.WriteLine();

            foreach (Monster monster in spawnList)
            {
                if(monster.Hp < 0)
                {
                    ConsoleUtility.PrintColoredText(ConsoleColor.DarkGray, $" Lv. {monster.Lv} {monster.Name}  Dead\n");
                }

                else
                {
                    ConsoleUtility.PrintTextHighlight(ConsoleColor.Magenta, " Lv.", $"{monster.Lv} ", $"{monster.Name}");
                    ConsoleUtility.PrintTextHighlight(ConsoleColor.Magenta, "  Hp", $" {monster.Hp}");
                    Console.WriteLine();
                }
                
            }

            Console.WriteLine();

            Console.WriteLine("[내정보]");
            PrintPlayerInfo(player);
            Console.WriteLine();

            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬 사용");
            Console.WriteLine("3. 회복 아이템\n");

            int input = ConsoleUtility.ChoiceMenu(1, 3);
            switch(input)
            {
                case 1:
                    PlayerTurn(player);
                    break;
                case 2:
                    Player.SkillUse(player);
                    break;
                case 3:
                    GameManager.PotionMenu(player, true);
                    break;
            }
        }

        public static void PlayerTurn(Player player)
        {
            Console.Clear();
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "Battle!!\n");
            Console.WriteLine();

            int index = 1;
            
            foreach (Monster monster in spawnList)
            {
                if (monster.Hp <= 0)
                {
                    monster.IsDead = true;
                    ConsoleUtility.PrintColoredText(ConsoleColor.DarkGray, $"{index} - Lv. {monster.Lv} {monster.Name}  Dead\n");
                }

                else if (monster.IsDead == false)
                {
                    ConsoleUtility.PrintColoredText(ConsoleColor.Yellow, $" {index} ");
                    ConsoleUtility.PrintTextHighlight(ConsoleColor.Magenta, "- Lv.", $"{monster.Lv}", $" {monster.Name} ");
                    ConsoleUtility.PrintTextHighlight(ConsoleColor.Magenta, " HP ", $"{monster.Hp}");
                    Console.WriteLine();
                }

                index++;
            }

            Console.WriteLine();

            Console.WriteLine("[내정보]");
            PrintPlayerInfo(player);
            Console.WriteLine();

            Console.WriteLine("공격할 몬스터 번호를 입력해주세요.");
            int selectMonsterindex = ConsoleUtility.ChoiceMenu(1, spawnList.Count) - 1;

            Monster selectMonster = spawnList[selectMonsterindex];

            if (selectMonster.IsDead == true)
            {
                Console.WriteLine("잘못된 입력입니다.\n");
                PlayerTurn(player);
            }

            else if (selectMonster.IsDead == false)
            {
                Attack(player, selectMonster, player);
            }

            player.ResetSkill();

            Console.WriteLine("0. 다음\n");

            ConsoleUtility.ChoiceMenu(0, 0);

            Enemyturn(player);
        }

        public static void Enemyturn(Player player)
        {
            bool allMonsterDead = true;

            foreach (Monster monster in spawnList)
            {
                if (monster.Hp > 0)
                {
                    allMonsterDead = false;
                    break;
                }

                else if (monster.Hp <= 0)
                {
                    allMonsterDead = true;
                }
            }

            if (allMonsterDead == true)
            {
                Console.Clear();
                Win(player);
            }

            foreach (Monster monster in spawnList)
            {
                Console.Clear();
                ConsoleUtility.PrintColoredText(ConsoleColor.Red, "Battle!!\n");
                Console.WriteLine();
                
                if(monster.Hp <= 0)
                {
                    continue;
                }

                Attack(monster, player, player);

                ConsoleUtility.PrintTextHighlight(ConsoleColor.Magenta, "Lv.", $"{player.Lv}", $"{player.Name} ({player.Job})\n");
                ConsoleUtility.PrintTextHighlight(ConsoleColor.Magenta, "HP ", $"{player.Hp}");
                ConsoleUtility.PrintColoredText(ConsoleColor.Yellow, " / ");
                ConsoleUtility.PrintColoredText(ConsoleColor.Magenta, $"{player.MaxHp}\n");
                Console.WriteLine();

                if (player.Hp <= 0)
                {
                    player.Hp = 0;
                    Lose(player);
                }

                Console.WriteLine("0. 다음\n");
                ConsoleUtility.ChoiceMenu(0, 0);
            }

            DungeonScene(player);
        }

        public static void Attack(Character attacker, Character target, Player player)
        {
            Random avoid = new Random();
            Random critical = new Random();

            double criticalDamage;
            int sumDamage = 0;
            bool isCritical = false;

            Console.Clear();
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "Battle!!\n");
            Console.WriteLine();

            ConsoleUtility.PrintTextHighlight(ConsoleColor.Magenta, "Lv.", $"{attacker.Lv} ", $"{attacker.Name}");
            ConsoleUtility.PrintTextHighlight(ConsoleColor.Red, " 의 ", "공격!\n");

            if (avoid.Next(0, 100) <= 10)
            {
                ConsoleUtility.PrintTextHighlight(ConsoleColor.Yellow, "", $"{target.Name}", " 을(를) 공격했지만 아무일도 일어나지 않았습니다.\n");
                Console.WriteLine();
                return;
            }

            if (critical.Next(0, 100) <= 15)
            {
                criticalDamage = attacker.BaseAtk * 1.6;
                criticalDamage = Math.Round(criticalDamage);
                sumDamage = (int)criticalDamage - target.Def;
                isCritical = true;
            }

            else
            {
                sumDamage = attacker.BaseAtk - target.Def;
            }

            if(sumDamage < 0)
            {
                sumDamage = 0;
            }

            int damagedHp = target.Hp - sumDamage;

            ConsoleUtility.PrintTextHighlight(ConsoleColor.Magenta, "Lv.", $"{target.Lv} ", "");
            ConsoleUtility.PrintTextHighlight(ConsoleColor.Yellow, "", $"{target.Name}", $" 을(를) 맞췄습니다. [데미지 : {sumDamage}]\n");
            Console.WriteLine();

            if (isCritical == false)
            {
                if (damagedHp < 0)
                {
                    ConsoleUtility.PrintTextHighlight(ConsoleColor.Magenta, "HP ", $"{target.Hp} ");
                    ConsoleUtility.PrintColoredText(ConsoleColor.Yellow, "-> ");
                    ConsoleUtility.PrintColoredText(ConsoleColor.Magenta, "Dead\n");
                    Console.WriteLine();

                    // 슬라임
                    if (target.Id == 1000)
                    {
                        QuestManager.questList[0].UpdateQuestProgress(player, 0);
                    }
                    // 고블린
                    if (target.Id == 1001)
                    {
                        QuestManager.questList[1].UpdateQuestProgress(player, 1);
                    }
                }

                else
                {
                    ConsoleUtility.PrintTextHighlight(ConsoleColor.Magenta, "HP ", $"{target.Hp} ");
                    ConsoleUtility.PrintColoredText(ConsoleColor.Yellow, "-> ");
                    ConsoleUtility.PrintColoredText(ConsoleColor.Magenta, $"{damagedHp}\n");
                    Console.WriteLine();
                }
            }

            else
            {
                if (damagedHp < 0)
                {
                    ConsoleUtility.PrintTextHighlight(ConsoleColor.Magenta, "HP ", $"{target.Hp} ");
                    ConsoleUtility.PrintColoredText(ConsoleColor.Yellow, "-> ");
                    ConsoleUtility.PrintColoredText(ConsoleColor.Magenta, "Dead");
                    ConsoleUtility.PrintColoredText(ConsoleColor.Red, " - 치명타 공격!!\n");
                    Console.WriteLine();

                    // 슬라임
                    if (target.Id == 1000)
                    {
                        QuestManager.questList[0].UpdateQuestProgress(player, 0);
                    }
                    // 고블린
                    if (target.Id == 1001)
                    {
                        QuestManager.questList[1].UpdateQuestProgress(player, 1);
                    }
                }

                else
                {
                    ConsoleUtility.PrintTextHighlight(ConsoleColor.Magenta, "HP ", $"{target.Hp} ");
                    ConsoleUtility.PrintColoredText(ConsoleColor.Yellow, "-> ");
                    ConsoleUtility.PrintColoredText(ConsoleColor.Magenta, $"{damagedHp}");
                    ConsoleUtility.PrintColoredText(ConsoleColor.Red, " - 치명타 공격!!\n");
                    Console.WriteLine();
                }

                isCritical = false;
            }
            target.Hp -= sumDamage;
        }

        public static void Win(Player player)
        {
            Console.Clear();
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "Battle!! - Result\n");
            Console.WriteLine();

            ConsoleUtility.PrintColoredText(ConsoleColor.Green, "Victory\n");
            Console.WriteLine();

            Console.WriteLine($"던전에서 몬스터 {spawnList.Count}마리를 잡았습니다.");

            Console.WriteLine($"Lv {player.Lv} {player.Name}");
            Console.WriteLine($"HP {player.MaxHp} -> {player.Hp}\n");

            int totalGold = 0;
            int totalExp = 0;

            foreach(Monster monster in spawnList)
            {
                totalGold += monster.Gold;
                totalExp += monster.Exp;
                player.ExpAdd(monster.Exp);
            }

            Console.WriteLine($"[획득 경험치]\nExp: {totalExp}");
            Console.WriteLine("[획득 아이템]");

            Console.WriteLine($"{totalGold} Gold\n");

            player.Gold += totalGold;

            ItemDrop();
            spawnList.Clear();

            if(GameManager.stage.num == 1 && stageSelect == 1)
            {
                GameManager.stage.num = 2;
            }

            else if(GameManager.stage.num == 2 && stageSelect == 2)
            {
                GameManager.stage.num = 3;
            }
            

            if(GameManager.stage.num > 3)
            {
                GameManager.stage.num = 3;
            }

            Console.WriteLine("0. 다음\n");

            ConsoleUtility.ChoiceMenu(0, 0);

            //메인 화면 불러오기
            GameManager.MainMenu(player);
        }

        public static void Lose(Player player)
        {
            Console.Clear();
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "Battle!! - Result\n");
            Console.WriteLine();

            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "You Lose\n");
            Console.WriteLine();

            Console.WriteLine($"Lv {player.Lv} {player.Name}");
            Console.WriteLine($"HP {player.MaxHp} -> {player.Hp}\n");

            spawnList.Clear();

            Console.WriteLine("0. 다음\n");

            ConsoleUtility.ChoiceMenu(0, 0);

            GameManager.MainMenu(player);
        }

        public static void MonsterSpawn(Player player, int StageSelect)
        {
            Random randomNum = new Random();
            Random randomSpawn = new Random();

            int spawnConunt = randomSpawn.Next(1, 5);

            for (int i = 0; i < spawnConunt; i++)
            {
                int minMonster = 0;
                int maxMonster = 0;

                if (GameManager.stage.num >= 1 && StageSelect == 1)
                {
                    minMonster = 0;
                    maxMonster = 3;
                }

                else if (GameManager.stage.num >= 2 && stageSelect == 2)
                {
                    minMonster = 1;
                    maxMonster = 4;
                }

                else if (GameManager.stage.num >= 3 && stageSelect == 3)
                {
                    spawnConunt = 1;
                    minMonster = 4;
                    maxMonster = 4;
                }

                switch (randomNum.Next(minMonster, maxMonster))
                {
                    case 0:
                        spawnList.Add(new Monster("슬라임", 0, 1, 3, 1, 1, 3, 5, 1000));
                        break;
                    case 1:
                        spawnList.Add(new Monster("고블린", 1, 2, 5, 2, 1, 5, 10, 1001));
                        break;
                    case 2:
                        spawnList.Add(new Monster("코볼트", 2, 3, 7, 7, 3, 10, 30, 1002));
                        break;
                    case 3:
                        spawnList.Add(new Monster("오크", 3, 5, 10, 10, 5, 20, 50, 1003));
                        break;
                    case 4:
                        spawnList.Add(new Monster("드래곤", 4, 20, 40, 20, 20, 100, 200, 1004));
                        break;
                }
            }

            DungeonScene(player);
        }

        public static void ItemDrop()
        {
            Random potionDrop = new Random();
            if (potionDrop.Next(0, 100) <= 90)
            {
                Inventory.potionInventory.Add(new Item("포션", "포션을 사용하면 체력을 30 회복 할 수 있습니다.", ItemType.POTION, 0, 0, 30, 300));
                Console.WriteLine("포션 - 1");
            }

            Random equipItemDrop = new Random();
            int dropChance = equipItemDrop.Next(0, 100);
            if (dropChance <= 20)
            {
                Inventory.inventory.Add(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", ItemType.WEAPON, 2, 0, 0, 100));
                Console.WriteLine("낡은 검 - 1");
            }

            else if (dropChance > 20 && dropChance <= 40)
            {
                Inventory.inventory.Add(new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", ItemType.ARMOR, 0, 5, 0, 100));
                Console.WriteLine("수련자 갑옷 - 1");
            }

            else if (dropChance > 40 && dropChance <= 47)
            {
                Inventory.inventory.Add(new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", ItemType.WEAPON, 5, 0, 0, 200));
                Console.WriteLine("청동 도끼 - 1");
            }

            else if (dropChance > 47 && dropChance <= 54)
            {
                Inventory.inventory.Add(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", ItemType.ARMOR, 0, 9, 0, 200));
                Console.WriteLine("무쇠갑옷 - 1");
            }

            else if (dropChance > 54 && dropChance <= 57)
            {
                Inventory.inventory.Add(new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", ItemType.WEAPON, 7, 0, 0, 3000));
                Console.WriteLine("스파르타의 창 - 1");
            }

            else if (dropChance > 57 && dropChance <= 60)
            {
                Inventory.inventory.Add(new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", ItemType.ARMOR, 0, 15, 0, 3500));
                Console.WriteLine("스파르타의 갑옷 - 1");
            }
        }

        public static void PrintPlayerInfo(Player player)
        {
            ConsoleUtility.PrintTextHighlight(ConsoleColor.Magenta, "Lv.", $"{player.Lv}  ", $"{player.Name} ({player.Job})\n");
            PrintHpInfo(player);
            PrintMpInfo(player);
        }

        public static void PrintHpInfo(Player player)
        {
            Console.Write("Hp ");
            ConsoleUtility.PrintColoredText(ConsoleColor.Magenta, $"{player.Hp}");
            ConsoleUtility.PrintColoredText(ConsoleColor.Yellow, " / ");
            ConsoleUtility.PrintColoredText(ConsoleColor.Magenta, $"{player.MaxHp}\n");
        }

        public static void PrintMpInfo(Player player)
        {
            Console.Write("Mp ");
            ConsoleUtility.PrintColoredText(ConsoleColor.Magenta, $"{player.Mp}");
            ConsoleUtility.PrintColoredText(ConsoleColor.Yellow, " / ");
            ConsoleUtility.PrintColoredText(ConsoleColor.Magenta, $"{player.MaxMp}\n");
        }
    }
}