using SpartaDunGeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        private List<Monster> spawnList;

        public Dungeon(GameManager manager)
        {            
            spawnList = new List<Monster>();
        }

        public void DungeonScene(Player player)
        {
            Console.Clear();
            MonsterSpawn();
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "Battle!!\n");
            Console.WriteLine();

            foreach (Monster monster in spawnList)
            {
                Console.WriteLine($"Lv. {monster.Lv} {monster.Name}  HP {monster.Hp}");
            }

            Console.WriteLine();

            Console.WriteLine("[������]");
            Console.WriteLine($"Lv.{player.Lv}  {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHp}\n");

            Console.WriteLine("1. ����");
            Console.WriteLine("2. ��ų ���\n");

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
            Console.WriteLine("����� ��ų�� �����ϼ���.\n");

            Console.WriteLine("1. ����ġ��\t2. �޽��ϱ�\n");

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
        
        public void MonsterSpawn()
        {
            Random randomNum = new Random();
            Random randomSpawn = new Random();

            int spawnConunt = randomSpawn.Next(1, 5);

            for (int i = 0; i < spawnConunt; i++)
            {
                /*
                int randomIndex = randomNum.Next(0, monster.monsters.Count);

                Monster randomMonster = monster.monsters[randomIndex];
                spawnList.Add(new Monster(randomMonster.Name, randomMonster.Idx, randomMonster.Lv, randomMonster.Atk, randomMonster.Def, randomMonster.Hp, randomMonster.Gold));
                */
                
                switch (randomNum.Next(0, 5))
                {
                    case 0:
                        spawnList.Add(new Monster("������", 0 , 1, 1, 1, 3, 5, 1000));
                        break;
                    case 1:
                        spawnList.Add(new Monster("���", 1, 2, 2, 1, 5, 10, 1001));
                        break;
                    case 2:
                        spawnList.Add(new Monster("�ں�Ʈ", 2, 3, 7, 3, 10, 30, 1002));
                        break;
                    case 3:
                        spawnList.Add(new Monster("��ũ", 3, 5, 10, 5, 20, 50, 1003));
                        break;
                    case 4:
                        spawnList.Add(new Monster("�巡��", 4, 20, 20, 20, 100, 200, 1004));
                        break;
                }
                
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
                    Console.WriteLine($" {index} - Lv. {monster.Lv} {monster.Name}  Dead");                    
                }

                else if (monster.IsDead == false)
                {
                    Console.WriteLine($" {index} - Lv. {monster.Lv} {monster.Name}  HP {monster.Hp}");
                }

                index++;
            }

            Console.WriteLine();

            Console.WriteLine("[������]");
            Console.WriteLine($"Lv.{player.Lv}  {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHp}\n");

            int selectMonsterindex = ConsoleUtility.ChoiceMenu(1, spawnList.Count) - 1;

            Monster selectMonster = spawnList[selectMonsterindex];

            if (selectMonster.IsDead == true)
            {
                Console.WriteLine("�߸��� �Է��Դϴ�.\n");
                PlayerTurn(player);
            }

            else if (selectMonster.IsDead == false)
            {
                Attack(player, selectMonster, player);
            }

            Console.WriteLine("0. ����\n");

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

                Console.WriteLine("0. ����\n");
                ConsoleUtility.ChoiceMenu(0, 0);
            }

            PlayerTurn(player);
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

            Console.WriteLine($"Lv.{attacker.Lv} {attacker.Name} �� ����!");

            if (avoid.Next(0, 100) <= 10)
            {
                Console.WriteLine($"{target.Name} ��(��) ���������� �ƹ��ϵ� �Ͼ�� �ʾҽ��ϴ�.\n");

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

            Console.WriteLine($"{target.Name} ��(��) ������ϴ�. [������ : {sumDamage}]\n");


            if (isCritical == false)
            {
                if (damagedHp < 0)
                {
                    Console.WriteLine($"HP {target.Hp} -> Dead");
                    // ������
                    if (target.Id == 1000)
                    {
                        QuestManager.questList[0].UpdateQuestProgress(player);
                    }
                    // ���
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
                    // ������
                    if (target.Id == 1000)
                    {
                        QuestManager.questList[0].UpdateQuestProgress(player);
                    }
                    // ���
                    if (target.Id == 1001)
                    {
                        QuestManager.questList[1].UpdateQuestProgress(player);
                    }
                }

                else
                {
                    Console.WriteLine($"HP {target.Hp} -> {damagedHp} - ġ��Ÿ ����!!\n");
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

            Console.WriteLine($"�������� ���� {spawnList.Count}������ ��ҽ��ϴ�.");

            Console.WriteLine($"Lv {player.Lv} {player.Name}");
            Console.WriteLine($"HP {player.MaxHp} -> {player.Hp}\n");

            Console.WriteLine("[ȹ�� ������]");
            int totalGold = 0;

            foreach(Monster monster in spawnList)
            {
                totalGold += monster.Gold;
            }

            Console.WriteLine($"{totalGold} Gold\n");

            player.Gold += totalGold;
            Random potionDrop = new Random();


            Console.WriteLine("0. ����\n");

            ConsoleUtility.ChoiceMenu(0, 0);

            //메인 ?�면 불러?�기
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

            Console.WriteLine("0. ����\n");

            ConsoleUtility.ChoiceMenu(0, 0);

            GameManager.MainMenu(player);
        }
    }
}