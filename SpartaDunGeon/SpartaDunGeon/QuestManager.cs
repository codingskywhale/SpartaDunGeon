using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaDunGeon
{
    public class QuestManager
    {
        private static QuestManager instance;

        public static QuestManager Instance()
        {
            if(instance == null)
            {
                instance = new QuestManager();
            }                
            return instance;
        }

        public static List<Quest> questList;
        public static List<QuestSaveData> questSaveList;

        public QuestManager()
        {            
            questSaveList = new List<QuestSaveData>();
            questSaveList.Add(new QuestSaveData(1));
            questSaveList.Add(new QuestSaveData(2));
            questSaveList.Add(new QuestSaveData(3));
            questSaveList.Add(new QuestSaveData(4));
            questList = new List<Quest>();
            questList.Add(new Quest1(1, "초보 모험가를 위한 슬라임 처치", " 이봐! 마을 근처에 슬라임들이 너무 많아졌다고 생각하지 않나?\n 마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n 모험가인 자네가 좀 처치해주게!"));
            questList.Add(new Quest2(2, "마을을 위협하는 고블린 무리 소탕", " 최근 고블린들의 행동이 심상치 않네!\n 무리를 지어 마을을 습격해 피해가 이만저만이 아니지... \n 고블린 소굴을 찾아 전부 소탕해주겠나? "));
            questList.Add(new Quest3(3, "강력한 장비를 장착해보자", " 새로운 장비를 얻었다면 장착해보게나.\n 장비를 장착하면 여러 가지 능력을 추가로 얻을 수 있다네! "));
            questList.Add(new Quest4(4, "더욱 더 강해지기!", " 레벨은 곧 국력이지!\n 몬스터를 쓰러뜨려 경험치를 얻고 레벨을 올려보게나.\n 5레벨을 달성하면 자네에게 어울리는 선물을 주겠네."));
        }

        // 퀘스트 목록 출력
        public static void PrintQuestList(Player player)
        {
            Console.Clear();
            
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, "퀘스트\n");
            Console.WriteLine("퀘스트를 관리할 수 있습니다.\n");
            Console.WriteLine("[퀘스트 목록]");
            for (int i = 0; i < questList.Count; i++)
            {
                if (!questSaveList[i].IsCompleted)
                {
                    ConsoleUtility.PrintTextHighlight(ConsoleColor.DarkYellow, "", $"{i + 1}. ", questList[i].QuestName);

                    if (questSaveList[i].IsProceeding)
                    {
                        if (questSaveList[i].CanCompleted)
                        {
                            ConsoleUtility.PrintColoredText(ConsoleColor.Magenta, " [완료 가능]");
                        }
                        else
                        {
                            ConsoleUtility.PrintColoredText(ConsoleColor.Green, " [진행중]");
                        }
                    }
                }
                else
                {
                    ConsoleUtility.PrintColoredText(ConsoleColor.DarkGray, $"{i + 1}. {questList[i].QuestName} [진행 불가]");
                }
                Console.WriteLine();
            }
            
            ConsoleUtility.PrintColoredText(ConsoleColor.Red, "\n0. 나가기\n\n");

            int number = ConsoleUtility.ChoiceMenu(0, questList.Count);
            switch (number)
            {
                case 0:
                    GameManager.MainMenu(player);
                    break;
                case 1:
                    if (questSaveList[number - 1].IsCompleted)
                    {
                        Console.WriteLine("해당 퀘스트는 더 이상 진행할 수 없습니다.");
                        Thread.Sleep(100);
                        PrintQuestList(player);
                        break;
                    }
                    else
                    {
                        PrintQuestData(number, player);
                        break;
                    }
                case 2:
                    if (questSaveList[number - 1].IsCompleted)
                    {
                        Console.WriteLine("해당 퀘스트는 더 이상 진행할 수 없습니다.");
                        Thread.Sleep(100);
                        PrintQuestList(player);
                        break;
                    }
                    else
                    {
                        PrintQuestData(number, player);
                        break;
                    }
                case 3:
                    if (questSaveList[number - 1].IsCompleted)
                    {
                        Console.WriteLine("해당 퀘스트는 더 이상 진행할 수 없습니다.");
                        Thread.Sleep(100);
                        PrintQuestList(player);
                        break;
                    }
                    else
                    {
                        PrintQuestData(number, player);
                        break;
                    }
                case 4:
                    if (questSaveList[number - 1].IsCompleted)
                    {
                        Console.WriteLine("해당 퀘스트는 더 이상 진행할 수 없습니다.");
                        Thread.Sleep(100);
                        PrintQuestList(player);
                        break;
                    }
                    else
                    {
                        PrintQuestData(number, player);
                        break;
                    }
                case 5:
                    if (questSaveList[number - 1].IsCompleted)
                    {
                        Console.WriteLine("해당 퀘스트는 더 이상 진행할 수 없습니다.");
                        Thread.Sleep(100);
                        PrintQuestList(player);
                        break;
                    }
                    else
                    {
                        PrintQuestData(number, player);
                        break;
                    }            
            }
        }

        // 선택한 퀘스트 정보
        public static void PrintQuestData(int index, Player player)
        {
            Console.Clear();
            // 제목 내용 출력
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, "Quest!!\n\n");
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, $"[{questList[index - 1].QuestName}]\n\n");
            Console.WriteLine(questList[index - 1].Description + "\n");

            // 완료 조건 출력
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, "[퀘스트 임무]\n");
            questList[index - 1].PrintGoal(player, questSaveList[index - 1]);
            Console.WriteLine(); // + 완료 조건 출력

            // 보상 출력
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, "[보상]\n");
            questList[index - 1].PrintRewards();
            
            Console.WriteLine(); // + 보상 출력

            // 퀘스트를 수행 중일 때
            if(questSaveList[index - 1].IsProceeding)
            {
                if (questSaveList[index - 1].CanCompleted)
                {
                    ConsoleUtility.PrintTextHighlight(ConsoleColor.DarkYellow, "", "1. ", "보상 받기\n");
                }                
                ConsoleUtility.PrintTextHighlight(ConsoleColor.DarkYellow, "", "0. ", "돌아가기\n\n");

                switch (ConsoleUtility.ChoiceMenu(0, 1))
                {                    
                    case 0:
                        QuestManager.PrintQuestList(player);
                        break;
                    case 1:
                        questList[index - 1].GetQuestRewards(player, questSaveList[index - 1]);
                        break;
                }
            }
            // 퀘스트를 수행 중이지 않을 때
            else
            {
                ConsoleUtility.PrintTextHighlight(ConsoleColor.DarkYellow, "", "1. ", "수락\n");
                ConsoleUtility.PrintTextHighlight(ConsoleColor.DarkYellow, "", "2. ", "거절\n\n");

                switch (ConsoleUtility.ChoiceMenu(1, 2))
                {
                    case 1: // 수락
                        questSaveList[index - 1].IsProceeding = true;
                        QuestManager.PrintQuestList(player);
                        break;
                    case 2: // 거절
                        QuestManager.PrintQuestList(player);
                        break;
                }
            }            
        }

        // 퀘스트 완료 화면
        public void PrintQuestComplete(int index, Player player)
        {
            Console.Clear();
            // 제목 내용 출력
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, "Quest!!\n\n");
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, questList[index - 1].QuestName + "\n\n");           
            Console.WriteLine(questList[index - 1].QuestName + "\n");
            
            // 완료 조건 출력
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, "[퀘스트 임무]\n");
            Console.WriteLine(); //

            // 보상 출력
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, "[보상]\n");
            Console.WriteLine(); //

            ConsoleUtility.PrintTextHighlight(ConsoleColor.DarkYellow, "", "1. ", "보상 받기\n");
            ConsoleUtility.PrintTextHighlight(ConsoleColor.DarkYellow, "", "2. ", "돌아가기\n\n");

            int keyInput = ConsoleUtility.ChoiceMenu(1, 2);
            switch (keyInput)
            {
                case 1: // 보상 받기
                    questList[index - 1].GetQuestRewards(player, questSaveList[index - 1]);
                    break;
                case 2: // 돌아가기
                    GameManager.MainMenu(player);
                    break;
            }
        }       
    }
}
