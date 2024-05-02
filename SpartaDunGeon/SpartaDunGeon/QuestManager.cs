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
        public static List<QuestData> questList;

        public QuestManager()
        {
            InitQuest();
        }

        // 퀘스트 목록 생성
        public void InitQuest()
        {
            questList = new List<QuestData> ();
            questList.Add(new QuestData(1, "초보 용사를 위한 슬라임 처치", "1번 내용입니다"));
            questList.Add(new QuestData(2, "마을을 위협하는 고블린 무리 소탕", "2번 내용입니다"));
            questList.Add(new QuestData(3, "강력한 장비를 장착해보자", "3번 내용입니다"));
            questList.Add(new QuestData(4, "더욱 더 강해지기!", "4번 내용입니다"));
        }

        // 퀘스트 목록 출력
        public static void PrintQuestList(Player player)
        {
            Console.Clear();
            
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, "<퀘스트 목록>\n\n");
            for (int i = 0; i < questList.Count; i++)
            {
                ConsoleUtility.PrintTextHighlight(ConsoleColor.Red, "", $"{i + 1}. ", questList[i].QuestName);
                Console.WriteLine();
            }

            Console.WriteLine();

            int number = ConsoleUtility.ChoiceMenu(1, questList.Count);
            switch (number)
            {
                case 1:
                    PrintQuestData(number, player);
                    break;
            }
        }

        // 선택한 퀘스트 정보
        public static void PrintQuestData(int index, Player player)
        {
            Console.Clear();
            // 제목 내용 출력
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, "Quest!!\n\n");
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, questList[index - 1].QuestName + "\n\n");
            Console.WriteLine(questList[index - 1].Description + "\n");

            // 완료 조건 출력
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, "[퀘스트 임무]\n");
            Console.WriteLine(); // + 완료 조건 출력

            // 보상 출력
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, "[보상]\n");
            Console.WriteLine(); // + 보상 출력

            ConsoleUtility.PrintTextHighlight(ConsoleColor.Red, "", "1. ", "수락\n");
            ConsoleUtility.PrintTextHighlight(ConsoleColor.Red, "", "2. ", "거절\n\n");
                        
            switch (ConsoleUtility.ChoiceMenu(1, 2))
            {
                case 1: // 수락
                    GameManager.MainMenu(player);
                    break;
                case 2: // 거절
                    GameManager.MainMenu(player);
                    break;
            }
        }

        // 퀘스트 완료 화면
        public static void SetQuestComplete(int index, Player player)
        {
            Console.Clear();
            // 제목 내용 출력
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, "Quest!!\n\n");
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, questList[index - 1].questName + "\n\n");           
            Console.WriteLine(questList[index - 1].description + "\n");
            
            // 완료 조건 출력
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, "[퀘스트 임무]\n");
            Console.WriteLine(); // + 완료 조건 출력

            // 보상 출력
            ConsoleUtility.PrintColoredText(ConsoleColor.DarkYellow, "[보상]\n");
            Console.WriteLine(); // + 보상 출력

            ConsoleUtility.PrintTextHighlight(ConsoleColor.Red, "", "1. ", "보상 받기\n");
            ConsoleUtility.PrintTextHighlight(ConsoleColor.Red, "", "2. ", "돌아가기\n\n");

            switch (ConsoleUtility.ChoiceMenu(1, 2))
            {
                case 1: // 보상 받기
                    GameManager.MainMenu(player);
                    break;
                case 2: // 돌아가기
                    GameManager.MainMenu(player);
                    break;
            }
        }

        // public QuestManager() { }
    }

    
    
}
