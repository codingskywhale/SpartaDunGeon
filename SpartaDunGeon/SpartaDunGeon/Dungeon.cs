using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartadungeon
{
    internal class Dungeon
    {
        private Player player;
        private List<Monster> spawnList;

        public void DungeonScene()
        {
            MonsterSpawn();
            ConsoleUtility.PrintColoredText(Color.Red, "Battle!!\n");

            foreach (Monster monster in spawnList)
            {
                Console.WriteLine($"Lv. {monster.Lv} {monster.Name}  HP {monster.Hp}");
            }

            Console.WriteLine();

            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Lv}  {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHp}\n");

            Console.WriteLine("1. 공격\n"); ;

            switch(ConsoleUtility.ChoiceMenu(1, 1))
            {
                case 1:
                    Battle();
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
                switch (randomNum.Next(1, 4))
                {
                    case 1:
                        
                        break;
                    case 2:
                        
                        break;
                    case 3:
                        
                        break;
                }
            }
        }

        public void Battle()
        {
            bool dead = false;

            while(!dead)
            {
                Console.Clear();
                ConsoleUtility.PrintColoredText(Color.Red, "Battle!!\n");

                int index = 1;

                foreach (Monster monster in spawnList)
                {
                    if (monster.Hp <= 0)
                    {
                        monster.IsDead = true;
                    }

                    if (monster.IsDead == false)
                    {
                        Console.WriteLine($" {index} - Lv. {monster.Lv} {monster.Name}  HP {monster.Hp}");
                    }

                    else if(monster.IsDead == true)
                    {
                        Console.WriteLine($" {index} - Lv. {monster.Lv} {monster.Name}  Dead");
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
                    Console.WriteLine("잘못된 입력입니다.");
                    selectMonsterindex = ConsoleUtility.ChoiceMenu(1, spawnList.Count) - 1;
                }

                else if (selectMonster.IsDead == false)
                {
                    selectMonster.Hp -= player.Atk;
                }
                

                
            }
            
        }
    }
}
