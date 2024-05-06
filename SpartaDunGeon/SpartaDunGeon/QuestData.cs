using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDunGeon
{   
    public class Quest
    {
        public string QuestName { get; set; }
        public string Description { get; set; }
        public int QuestId { get; set; }
        public int RewardGold { get; set; }
        public Item RewardItem;        
        //public bool IsCompleted { get; set; }
        //public bool IsProceeding { get; set; }
        //public bool CanCompleted { get; set; }

        public Quest(int id, string name, string description)
        {
            QuestId = id;
            QuestName = name;
            Description = description;            
        }

        // 퀘스트 진행 상황 체크 및 업데이트
        public virtual void UpdateQuestProgress(Player player, int id) { }
        
        // 퀘스트 목표 출력
        public virtual void PrintGoal(Player player, QuestSaveData SaveData) { }

        // 퀘스트 보상 출력
        public virtual void PrintRewards() { }

        // 퀘스트 완료 시 보상 지급
        public virtual void GetQuestRewards(Player player, QuestSaveData SaveData)
        {
            //Console.WriteLine("퀘스트 완료!!");
        }
    }

    // 슬라임 처치 퀘스트
    public class Quest1 : Quest
    {
        int goal = 3; // 퀘스트 목표
        //int tmpKill; // 진행도 카운트
        
        public Quest1(int id, string name, string description, /*int targetIndex,*/ int rewardGold = 100/*, bool isProceeding = false, bool canCompleted = false*/) : base(id, name, description)
        {
            QuestId = id;
            QuestName = name;
            Description = description;
            RewardGold = rewardGold;
        }

        public override void UpdateQuestProgress(Player player, int id)
        {
            QuestManager.questSaveList[id].TmpKill++;
            if(QuestManager.questSaveList[id].TmpKill >= goal)
            {
                QuestManager.questSaveList[id].CanCompleted = true;
            }
        }

        public override void PrintGoal(Player player, QuestSaveData SaveData)
        {
            Console.Write("\n 슬라임 처치 ");
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkRed, $"({SaveData.TmpKill} / {goal})\n");
        }

        public override void PrintRewards()
        {
            Console.WriteLine();
            Console.WriteLine($" {RewardGold} Gold");
        }

        public override void GetQuestRewards(Player player, QuestSaveData SaveData)
        {
            player.Gold += RewardGold;
            SaveData.IsCompleted = true;
            
            QuestManager.PrintQuestList(player);
        }
    }
    // 고블린 처치 퀘스트
    public class Quest2 : Quest
    {
        int goal = 1;
        //int tmpKill;

        public Quest2(int id, string name, string description, /*int targetIndex,*/ int rewardGold = 200/*, bool isProceeding = false, bool canCompleted = false*/) : base(id, name, description)
        {
            QuestId = id;
            QuestName = name;
            Description = description;
            RewardGold = rewardGold;
            //IsProceeding = isProceeding;
            //CanCompleted = canCompleted;
        }

        public override void UpdateQuestProgress(Player player, int id)
        {
            QuestManager.questSaveList[id].TmpKill++;
            if (QuestManager.questSaveList[id].TmpKill >= goal)
            {
                QuestManager.questSaveList[id].CanCompleted = true;
            }
        }

        public override void PrintGoal(Player player, QuestSaveData SaveData)
        {
            Console.Write("\n 고블린 처치 ");
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkRed, $"({SaveData.TmpKill} / {goal})\n");            
        }

        public override void PrintRewards()
        {
            Console.WriteLine();
            ConsoleUtility.PrintTextHighlight(ConsoleColor.DarkRed, " 낡은 검 x ","1","\n");
            Console.WriteLine($" {RewardGold} Gold");
        }

        public override void GetQuestRewards(Player player, QuestSaveData SaveData)
        {
            player.Gold += RewardGold;
            Inventory.inventory.Add(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", ItemType.WEAPON, 2, 0, 0, 100));
            SaveData.IsCompleted = true;

            QuestManager.PrintQuestList(player);
        }
    }

    public class Quest3 : Quest
    {
        bool isEquipped;

        public Quest3(int id, string name, string description, bool isEquipped = false, int rewardGold = 150/*, bool isProceeding = false, bool canCompleted = false*/) : base(id, name, description)
        {

            QuestId = id;
            QuestName = name;
            Description = description;
            RewardGold = rewardGold;
            this.isEquipped = isEquipped;
        }

        public override void UpdateQuestProgress(Player player, int id)
        {
            QuestManager.questSaveList[id].CanCompleted = true;
        }

        public override void PrintGoal(Player player, QuestSaveData SaveData)
        {
            Console.Write("\n 새로운 장비를 장착해보기\n");
            //ConsoleUtility.PrintColoredText(ConsoleColor.DarkRed, $"({tmpKill} / {goal})\n");
        }

        public override void PrintRewards()
        {
            Console.WriteLine();
            //ConsoleUtility.PrintTextHighlight(ConsoleColor.DarkRed, " 낡은 검 x ", "1", "\n");
            Console.WriteLine($" {RewardGold} Gold");
        }

        public override void GetQuestRewards(Player player, QuestSaveData SaveData)
        {
            player.Gold += RewardGold;
            //Inventory.inventory.Add(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", ItemType.WEAPON, 2, 0, 0, 100));
            SaveData.IsCompleted = true;

            QuestManager.PrintQuestList(player);
        }
    }

    // 레벨 달성 퀘스트
    public class Quest4 : Quest
    {
        int targetLevel = 5;
        
        public Quest4(int id, string name, string description, bool isEquipped = false, int rewardGold = 200/*, bool isProceeding = false, bool canCompleted = false*/) : base(id, name, description)
        {
            QuestId = id;
            QuestName = name;
            Description = description;
            RewardGold = rewardGold;
            RewardGold = rewardGold;            
        }

        public override void UpdateQuestProgress(Player player, int id)
        {
            if(player.Lv == targetLevel)
            {
                QuestManager.questSaveList[id].CanCompleted = true;
            }
        }

        public override void PrintGoal(Player player, QuestSaveData SaveData)
        {
            int tmpLevel = player.Lv;

            Console.Write($"\n {targetLevel}레벨 달성하기 ");
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkRed, $"({tmpLevel} / {targetLevel})\n");
        }

        public override void PrintRewards()
        {
            Console.WriteLine();
            ConsoleUtility.PrintTextHighlight(ConsoleColor.DarkRed, "레벨업 테스트 x ", "1", "\n");
            Console.WriteLine($" {RewardGold} Gold");
        }

        public override void GetQuestRewards(Player player, QuestSaveData SaveData)
        {
            player.Gold += RewardGold;
            Inventory.inventory.Add(new Item("레벨업 테스트", "테스트", ItemType.WEAPON, 2, 0, 0, 100));
            SaveData.IsCompleted = true;

            QuestManager.PrintQuestList(player);
        }
    }

}
