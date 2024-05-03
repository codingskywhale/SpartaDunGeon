using Spartadungeon;
using SpartaDunGeon;
using System.Xml.Linq;

public class GameManager
{
    public Player player;
    public static List<Item> potionInventory { get; private set; }
    private static Inventory inventory;
    private static Store store;
    public static Dungeon dungeon;

    public GameManager()//생성자 없어서 추가했습니다.
    {
        InitializeGame();
    }

    public void InitializeGame()
    {
        dungeon = new Dungeon();
        inventory = new Inventory();
        store = new Store();
        potionInventory = new List<Item>();
        potionInventory.Add(new Item("포션", "포션을 사용하면 체력을 30 회복 할 수 있습니다.", ItemType.POTION, 0, 0, 30, 300));
        potionInventory.Add(new Item("포션", "포션을 사용하면 체력을 30 회복 할 수 있습니다.", ItemType.POTION, 0, 0, 30, 300));
        potionInventory.Add(new Item("포션", "포션을 사용하면 체력을 30 회복 할 수 있습니다.", ItemType.POTION, 0, 0, 30, 300));
    }


    //이름 입력
    public void NameChoise()
    {
        Console.Clear();

        Console.Write("이름을 입력해 주세요: ");
        string name = Console.ReadLine();

        Console.Clear();

        ConsoleUtility.PrintTextHighlight(ConsoleColor.Yellow, "입력하신 이름은 '", name, "' 입니다.\n");
        Console.WriteLine("1. 예");
        Console.WriteLine("2. 아니오\n");

        int Choise = ConsoleUtility.ChoiceMenu(1, 2);

        switch (Choise)
        {
            case 1:
                JobChoise(name);
                break;
            case 2:
                NameChoise();
                break;
        }
    }

    //직업 선택
    public void JobChoise(string name)
    {
        Console.Clear();

        Console.WriteLine("직업을 선택해 주세요.\n");
        Console.WriteLine("1. 전사");
        Console.WriteLine("2. 마법사");
        Console.Write("\n>>");

        while (true)
        {
            string jobChoise = Console.ReadLine();
            if (jobChoise == "1")
            {
                Console.Clear();
                ConsoleUtility.PrintTextHighlight(ConsoleColor.Red, "당신은 용맹한 ", "전사", "를 선택하셨습니다.");
                player = new Player(name, "전사", 1, 0, 20, 10, 7, 70, 1, 20, 70, 20, 700) ;
                //Thread.Sleep(2000);
                MainMenu(player);
                break;
            }
            else if (jobChoise == "2")
            {
                Console.Clear();
                ConsoleUtility.PrintTextHighlight(ConsoleColor.Blue, "당신은 현명한 ", "마법사", "를 선택하셨습니다.");
                player = new Player(name, "마법사", 1, 0, 20, 5, 5, 50, 1, 70, 50, 70, 500);
                Thread.Sleep(1500);
                MainMenu(player);
                break;
            }
            else
            {
                Console.Write("선택지에서 골라주세요.\n>>");
            }
        }
    }

    //게임 시작
    public void StartGame()
    {
        NameChoise();
    }

    //메인 메뉴
    public static void MainMenu(Player player)
    {
        Console.Clear();
        //인트로
        Console.WriteLine("##############################");
        Console.WriteLine("스파르타 게임을 시작한다");
        Console.WriteLine("원하는 행동을 고르세요.");
        Console.WriteLine("##############################\n");

        //선택지
        Console.WriteLine("1. 상태창");
        Console.WriteLine("2. 장비창");
        Console.WriteLine("3. 상점");
        Console.WriteLine("4. 전투 시작");
        Console.WriteLine("5. 회복 아이템");
        Console.WriteLine("7. 퀘스트\n");

        //선택지 검증
        int Choise = ConsoleUtility.ChoiceMenu(1, 7);

        //메뉴 중에서 선택
        switch (Choise)
        {
            case 1:
                StateMenu(player);
                break;
            case 2:
                inventory.InventoryMenu(player);
                break;
            case 3:
                store.StoreMenu(player);
                break;
            case 4:
                dungeon.StageScene(player);
                break;
            case 5:
                PotionMenu(player);
                break;
            case 6:
                player.ExpAdd(10);
                StateMenu(player);
                break;
            case 7:
                QuestManager.PrintQuestList(player);
                break;
        }
    }

    //상태창
    private static void StateMenu(Player player)
    {
        Console.Clear();

        ConsoleUtility.PrintColoredText(ConsoleColor.Yellow,"# 상태 보기 #\n");
        Console.WriteLine("캐릭터의 정보가 표기됩니다.\n");

        Console.WriteLine($"{player.Name} ({player.Job})");
        Console.WriteLine($"Lv. {player.Lv}");
        Console.WriteLine($"{player.Exp}/{player.MaxExp}");
        Console.Write($"공격력 : {player.Atk + player.BonusAtk}");
        Console.WriteLine(player.BonusAtk > 0 ? $" (+{player.BonusAtk})" : "");
        Console.Write($"방어력 : {player.Def + player.BonusDef}");
        Console.WriteLine(player.BonusDef > 0 ? $" (+{player.BonusDef})" : "");
        Console.WriteLine($"체  력 : {player.Hp}/{player.MaxHp}");
        Console.WriteLine($"마  력 : {player.Mp}/{player.MaxMp}");

        Console.WriteLine($"Gold : {player.Gold}");

        Console.WriteLine("\n0. 나가기\n");

        int Choise = ConsoleUtility.ChoiceMenu(0, 0);

        switch (Choise)
        {
            case 0:
                MainMenu(player);
                break;
        }
    }

    //회복 아이템
    private static void PotionMenu(Player player)
    {
        Console.Clear();
        Console.Write("포션을 사용하면 체력을 30 회복 할 수 있습니다.");
        Console.WriteLine($" (남은 포션 : {potionInventory.Count} )\n");
        Console.WriteLine("[현재 체력]");
        Console.WriteLine($"{player.Hp}/{player.MaxHp}\n");
        Console.WriteLine("1. 사용하기");
        ConsoleUtility.PrintColoredText(ConsoleColor.Red, "0. 나가기\n");
        int choice = ConsoleUtility.ChoiceMenu(0, 1);
        switch (choice)
        {
            case 0:
                MainMenu(player);
                break;
            case 1:
                if (potionInventory.Count == 0)
                {
                    ConsoleUtility.PrintColoredText(ConsoleColor.Red, "포션이 부족합니다.");
                    Thread.Sleep(500);
                    PotionMenu(player);
                    break;
                }
                Item Use = potionInventory[choice - 1];
                potionInventory.Remove(Use);
                player.Hp += 30;
                if (player.Hp >= player.MaxHp)
                {
                    player.Hp = player.MaxHp;
                }
                ConsoleUtility.PrintColoredText(ConsoleColor.Green, "회복을 완료했습니다.");
                Thread.Sleep(500);
                PotionMenu(player);
                break;
        }
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        ConsoleUtility.PrintColoredText(ConsoleColor.Red, "");

        GameManager gameManager = new GameManager();
        QuestManager questManager = QuestManager.Instance();
               
        gameManager.StartGame();
    }
}