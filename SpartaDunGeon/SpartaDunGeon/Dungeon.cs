using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spartadungeon
{
    internal class Dungeon
    {
        private List<Monster> spawnList;
        //Monster monster;

        public Dungeon()
        {
            spawnList = new List<Monster>();
        }

        public void DungeonScene(Player player)
        {
            Console.Clear();
            MonsterSpawn();
            ConsoleUtility.PrintColoredText(Color.Red, "Battle!!\n");
            Console.WriteLine();

            foreach (Monster monster in spawnList)
            {
                Console.WriteLine($"Lv. {monster.Lv} {monster.Name}  HP {monster.Hp}");
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
                        spawnList.Add(new Monster("슬라임",0 , 1, 1, 1, 3, 5));
                        break;
                    case 1:
                        spawnList.Add(new Monster("고블린", 1, 2, 2, 1, 5, 10));
                        break;
                    case 2:
                        spawnList.Add(new Monster("코볼트", 2, 3, 7, 3, 10, 30));
                        break;
                    case 3:
                        spawnList.Add(new Monster("오크", 3, 5, 10, 5, 20, 50));
                        break;
                    case 4:
                        spawnList.Add(new Monster("드래곤", 4, 20, 20, 20, 100, 200));
                        break;
                }
                
            }
        }

        public void PlayerTurn(Player player)
        {
            Console.Clear();
            ConsoleUtility.PrintColoredText(Color.Red, "Battle!!\n");
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
                Console.Clear();
                ConsoleUtility.PrintColoredText(Color.Red, "Battle!!\n");
                Console.WriteLine();
                Console.WriteLine($"{player.Name}의 공격!");
                Console.WriteLine($"{selectMonster.Name} 을(를) 맞췄습니다. [데미지 : {player.Atk}]\n");

                if (selectMonster.Hp <= 0)
                {
                    Console.WriteLine($"HP {selectMonster.Hp} -> Dead");
                }

                else if (selectMonster.Hp > 0)
                {
                    int damagedHp = selectMonster.Hp - player.Atk;

                    if (damagedHp < 0)
                    {
                        damagedHp = 0;
                    }
                    Console.WriteLine($"HP {selectMonster.Hp} -> {damagedHp}\n");

                    selectMonster.Hp -= player.Atk;
                }
            }

            Console.WriteLine("0. 다음\n");

            ConsoleUtility.ChoiceMenu(0, 0);

            Enemyturn(player);
        }

        public void Enemyturn(Player player)
        {
            bool allMonsterDead = true;
            bool dead = false;

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
                ConsoleUtility.PrintColoredText(Color.Red, "Battle!!\n");
                Console.WriteLine();
                
                if(monster.Hp <= 0)
                {
                    continue;
                }

                Console.WriteLine($"Lv.{monster.Lv} {monster.Name} 의 공격!");
                Console.WriteLine($"{player.Name} 을(를) 맞췄습니다.  [데미지 : {monster.Atk}\n");
                Console.WriteLine($"Lv {player.Lv} {player.Name}");
                Console.WriteLine($"HP {player.Hp} -> {player.Hp -= monster.Atk}\n");

                if (player.Hp <= 0)
                {
                    player.Hp = 0;
                    Lose(player);
                }

                Console.WriteLine("0. 다음\n");
                ConsoleUtility.ChoiceMenu(0, 0);
            }

            

            PlayerTurn(player);
        }

        public void Win(Player player)
        {
            Console.Clear();
            ConsoleUtility.PrintColoredText(Color.Red, "Battle!! - Result\n");
            Console.WriteLine();

            ConsoleUtility.PrintColoredText(Color.Green, "Victory\n");
            Console.WriteLine();

            Console.WriteLine($"던전에서 몬스터 {spawnList.Count}마리를 잡았습니다.");

            Console.WriteLine($"Lv {player.Lv} {player.Name}");
            Console.WriteLine($"HP {player.MaxHp} -> {player.Hp}\n");

            Console.WriteLine("0. 다음\n");

            ConsoleUtility.ChoiceMenu(0, 0);

            //메인 화면 불러오기
        }

        public void Lose(Player player)
        {
            Console.Clear();
            ConsoleUtility.PrintColoredText(Color.Red, "Battle!! - Result\n");
            Console.WriteLine();

            ConsoleUtility.PrintColoredText(Color.Red, "You Lose\n");
            Console.WriteLine();

            Console.WriteLine($"Lv {player.Lv} {player.Name}");
            Console.WriteLine($"HP {player.MaxHp} -> {player.Hp}\n");

            Console.WriteLine("0. 다음\n");

            ConsoleUtility.ChoiceMenu(0, 0);
            //게임오버 후 어떻게 할지
        }
    }
}
