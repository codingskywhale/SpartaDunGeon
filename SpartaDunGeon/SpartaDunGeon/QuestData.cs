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
        //public Item RewardItem;        
        public bool IsCompleted { get; set; }
        public bool IsProceeding { get; set; }
        public bool CanCompleted { get; set; }

        public Quest(int id, string name, string description)
        {
            QuestId = id;
            QuestName = name;
            Description = description;            
        }

        // 퀘스트 진행 상황 체크 및 업데이트
        public virtual void UpdateQuestProgress(Player player)
        {

        }

        // 퀘스트 완료 여부 판단
        public bool isComplete(int questId)
        {
            return false;
        }

        // 퀘스트 목표 출력
        public virtual void PrintGoal() { }

        // 퀘스트 보상 출력
        public virtual void PrintRewards() { }

        // 퀘스트 완료 시 보상 지급
        public virtual void GetQuestRewards(Player player)
        {
            //Console.WriteLine("퀘스트 완료!!");
        }
    }

    // 슬라임 처치 퀘스트
    public class Quest1 : Quest
    {
        int goal = 3;
        int tmpKill;

        public Quest1(int id, string name, string description, int targetIndex, int rewardGold = 100, bool isProceeding = false, bool canCompleted = false) : base(id, name, description)
        {
            QuestId = id;
            QuestName = name;
            Description = description;
            RewardGold = rewardGold;
            IsProceeding = isProceeding;
            CanCompleted = canCompleted;
        }

        public override void UpdateQuestProgress(Player player)
        {
            tmpKill++;
            if(tmpKill >= goal)
            {                
                CanCompleted = true;
            }
        }

        public override void PrintGoal()
        {
            Console.Write("\n 슬라임 처치 ");
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkRed, $"({tmpKill} / {goal})\n");
        }

        public override void PrintRewards()
        {
            Console.WriteLine();
            Console.WriteLine($" {RewardGold} Gold");
        }

        public override void GetQuestRewards(Player player)
        {
            player.Gold += RewardGold;
            IsCompleted = true;
            
            QuestManager.PrintQuestList(player);
        }
    }

    public class Quest2 : Quest
    {
        int goal = 5;
        int tmpKill;

        public Quest2(int id, string name, string description, int targetIndex, int rewardGold = 200, bool isProceeding = false, bool canCompleted = false) : base(id, name, description)
        {
            QuestId = id;
            QuestName = name;
            Description = description;
            IsProceeding = isProceeding;
            CanCompleted = canCompleted;
        }

        public override void UpdateQuestProgress(Player player)
        {
            tmpKill++;
            if (tmpKill >= goal)
            {
                CanCompleted = true;
            }
        }

        public override void PrintGoal()
        {
            Console.Write("\n 고블린 처치 ");
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkRed, $"({tmpKill} / {goal})\n");            
        }

        public override void PrintRewards()
        {
            Console.WriteLine();
            ConsoleUtility.PrintTextHighlight(ConsoleColor.DarkRed, " 낡은 검 x ","1","\n");
            Console.WriteLine($" {RewardGold} Gold");
        }

        public override void GetQuestRewards(Player player)
        {
            player.Gold += RewardGold;
            IsCompleted = true;

            QuestManager.PrintQuestList(player);
        }
    }

    public class Quest3 : Quest
    {
        bool isEquipped;

        public Quest3(int id, string name, string description, bool isEquipped = false, bool isProceeding = false, bool canCompleted = false) : base(id, name, description)
        {
            QuestId = id;
            QuestName = name;
            Description = description;
            this.isEquipped = isEquipped;
        }
    }

}
