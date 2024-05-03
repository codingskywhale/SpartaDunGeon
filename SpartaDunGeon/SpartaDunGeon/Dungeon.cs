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
    internal class Dungeon
    {
        public int stage = 1;
        public int stageSelect;

        private List<Monster> spawnList;

        public Dungeon()
        {            
            spawnList = new List<Monster>();
        }

        public void StageScene(Player player)
        {
            Console.Clear();
            Console.WriteLine("스테이지를 선택해주세요\n");
            Console.WriteLine("1. 스테이지 1");
            if(stage == 2)
            {
                Console.WriteLine("2. 스테이지 2");
            }
            
            else if(stage == 3)
            {
                ConsoleUtility.PrintColoredText(ConsoleColor.Red, "3. 스테이지 3 - BOSS");
            }

            Console.WriteLine();
            Console.WriteLine("0. 이전\n");

            stageSelect = ConsoleUtility.ChoiceMenu(0, 3);

            if(stageSelect == 0)
            {
                GameManager.MainMenu(player);
            }
            
            MonsterSpawn(player, stageSelect);
        }

        public void DungeonScene(Player player)
        {
            Console.Clear();
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "Battle!!\n");
            Console.WriteLine();

            foreach (Monster monster in spawnList)
            {
                if(monster.Hp < 0)
                {
                    ConsoleUtility.PrintColoredText(ConsoleColor.DarkGray, $"Lv. {monster.Lv} {monster.Name}  Dead\n");
                }

                else
                {
                    Console.WriteLine($" Lv. {monster.Lv} {monster.Name}  HP {monster.Hp}");
                }
                
            }

            Console.WriteLine();

            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Lv}  {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHp}\n");

            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬 사용\n");

            int input = ConsoleUtility.ChoiceMenu(1, 2);
            switch(input)
            {
                case 1:
                    PlayerTurn(player);
                    break;
                case 2:
                    Skill(player);
                    break;
            }
        }

        public void Skill(Player player)
        {
            Console.WriteLine("사용할 스킬을 선택하세요.\n");

            Console.WriteLine("1. 힘껏치기\t2. 휴식하기\n");

            int Choise = ConsoleUtility.ChoiceMenu(1, 4);

            switch (Choise)
            {
                case 1:
                    player.Atk *= 2;
                    PlayerTurn(player);
                    break;
                case 2:
                    player.Hp += player.Atk;
                    if (player.Hp >= player.MaxHp)
                    {
                        player.Hp = player.MaxHp;
                    }
                    PlayerTurn(player);
                    break;
            }
        }

        public void PlayerTurn(Player player)
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
                    Console.WriteLine($" {index} - Lv. {monster.Lv} {monster.Name}  HP {monster.Hp}");
                }

                index++;
            }

            Console.WriteLine();

            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Lv}  {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHp}\n");

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

            Console.WriteLine("0. 다음\n");

            ConsoleUtility.ChoiceMenu(0, 0);

            Enemyturn(player);
        }

        public void Enemyturn(Player player)
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

                Console.WriteLine($"Lv {player.Lv} {player.Name}");

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

        public void Attack(Character attacker, Character target, Player player)
        {
            Random avoid = new Random();
            Random critical = new Random();

            double criticalDamage;
            int sumDamage = 0;
            bool isCritical = false;

            Console.Clear();
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "Battle!!\n");
            Console.WriteLine();

            Console.WriteLine($"Lv.{attacker.Lv} {attacker.Name} 의 공격!");

            if (avoid.Next(0, 100) <= 10)
            {
                Console.WriteLine($"{target.Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.\n");
                return;
            }

            if (critical.Next(0, 100) <= 15)
            {
                criticalDamage = attacker.Atk * 1.6;
                criticalDamage = Math.Round(criticalDamage);
                sumDamage = (int)criticalDamage - target.Def;
                isCritical = true;
            }

            else
            {
                sumDamage = attacker.Atk - target.Def;
            }

            if(sumDamage < 0)
            {
                sumDamage = 0;
            }

            int damagedHp = target.Hp - sumDamage;

            Console.WriteLine($"{target.Name} 을(를) 맞췄습니다. [데미지 : {sumDamage}]\n");

            if (isCritical == false)
            {
                if (damagedHp < 0)
                {
                    Console.WriteLine($"HP {target.Hp} -> Dead");
                    // 슬라임
                    if (target.Id == 1000)
                    {
                        QuestManager.questList[0].UpdateQuestProgress(player);
                    }
                    // 고블린
                    if (target.Id == 1001)
                    {
                        QuestManager.questList[1].UpdateQuestProgress(player);
                    }
                }

                else
                {
                    Console.WriteLine($"HP {target.Hp} -> {damagedHp}\n");
                }
            }

            else
            {
                if (damagedHp < 0)
                {
                    Console.WriteLine($"HP {target.Hp} -> Dead");
                    // 슬라임
                    if (target.Id == 1000)
                    {
                        QuestManager.questList[0].UpdateQuestProgress(player);
                    }
                    // 고블린
                    if (target.Id == 1001)
                    {
                        QuestManager.questList[1].UpdateQuestProgress(player);
                    }
                }

                else
                {
                    Console.WriteLine($"HP {target.Hp} -> {damagedHp} - 치명타 공격!!\n");
                }

                isCritical = false;
            }
            target.Hp -= sumDamage;
        }

        public void Win(Player player)
        {
            Console.Clear();
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "Battle!! - Result\n");
            Console.WriteLine();

            ConsoleUtility.PrintColoredText(ConsoleColor.Green, "Victory\n");
            Console.WriteLine();

            Console.WriteLine($"던전에서 몬스터 {spawnList.Count}마리를 잡았습니다.");

            Console.WriteLine($"Lv {player.Lv} {player.Name}");
            Console.WriteLine($"HP {player.MaxHp} -> {player.Hp}\n");

            Console.WriteLine("[획득 아이템]"); int totalGold = 0;

            foreach(Monster monster in spawnList)
            {
                totalGold += monster.Gold;
            }

            Console.WriteLine($"{totalGold} Gold\n");

            player.Gold += totalGold;

            ItemDrop();
            spawnList.Clear();
            stage++;

            if(stage > 3)
            {
                stage = 3;
            }

            Console.WriteLine("0. 다음\n");

            ConsoleUtility.ChoiceMenu(0, 0);

            //메인 화면 불러오기
            GameManager.MainMenu(player);
        }

        public void Lose(Player player)
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

        public void MonsterSpawn(Player player, int StageSelect)
        {
            Random randomNum = new Random();
            Random randomSpawn = new Random();

            int spawnConunt = randomSpawn.Next(1, 5);

            for (int i = 0; i < spawnConunt; i++)
            {
                int minMonster = 0;
                int maxMonster = 0;

                if (stage == 1 && StageSelect == 1)
                {
                    minMonster = 0;
                    maxMonster = 2;
                }

                else if ( stage == 2 && stageSelect == 2)
                {
                    minMonster = 1;
                    maxMonster = 3;
                }

                else if (stage == 3 && stageSelect == 3)
                {
                    minMonster = 4;
                    maxMonster = 4;
                }

                if(stage == 1)
                {
                    switch (randomNum.Next(minMonster, maxMonster))
                    {
                        case 0:
                            spawnList.Add(new Monster("슬라임", 0, 1, 1, 1, 3, 5, 1000));
                            break;
                        case 1:
                            spawnList.Add(new Monster("고블린", 1, 2, 2, 1, 5, 10, 1001));
                            break;
                        case 2:
                            spawnList.Add(new Monster("코볼트", 2, 3, 7, 3, 10, 30, 1002));
                            break;
                        case 3:
                            spawnList.Add(new Monster("오크", 3, 5, 10, 5, 20, 50, 1003));
                            break;
                        case 4:
                            spawnList.Add(new Monster("드래곤", 4, 20, 20, 20, 100, 200, 1004));
                            break;
                    }
                }
                
            }

            DungeonScene(player);
        }

        public void ItemDrop()
        {
            Random potionDrop = new Random();
            if (potionDrop.Next(0, 100) <= 90)
            {
                GameManager.potionInventory.Add(new Item("포션", "포션을 사용하면 체력을 30 회복 할 수 있습니다.", ItemType.POTION, 0, 0, 30, 300));
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
    }
}