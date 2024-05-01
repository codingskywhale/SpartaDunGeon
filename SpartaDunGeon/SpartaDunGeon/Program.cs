using System.Xml.Linq;

public class GameManager
{
    public Player player;

    //게임 시작
    public void StartGame()
    {
        NameChoise();
    }

    //이름 입력
    private void NameChoise()
    {
        Console.Clear();

        Console.Write("이름을 입력해 주세요: ");
        string name = Console.ReadLine();

        Console.Clear();

        ConsoleUtility.PrintTextHighlight(Color.Yellow,"입력하신 이름은 '",name,"' 입니다.\n");
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
    private void JobChoise(string name)
    {
        Console.Clear ();

        Console.WriteLine("직업을 선택해 주세요.\n");
        Console.WriteLine("1. 전사");
        Console.WriteLine("2. 마법사");
        Console.Write("\n>>");

        while (true)
        {
            string jobChoise = Console.ReadLine();
            if (jobChoise == "1")
            {
                Console.Clear() ;
                ConsoleUtility.PrintTextHighlight(Color.Red,"당신은 용맹한 ","전사","를 선택하셨습니다.");
                player = new Player(name, "전사", 1, 10, 7, 70, 70, 700);
                Thread.Sleep(2000);
                MainMenu();
                break;
            }
            else if (jobChoise == "2")
            {
                Console.Clear();
                ConsoleUtility.PrintTextHighlight(Color.Blue,"당신은 현명한 ","마법사", "를 선택하셨습니다.");
                player = new Player(name, "마법사", 1, 5, 5, 50, 50, 500);
                Thread.Sleep(1500);
                MainMenu();
                break;
            }
            else
            {
                Console.Write("선택지에서 골라주세요.\n>>");
            }
        }
    }

    //메인 메뉴
    private void MainMenu()
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
        Console.WriteLine("4. 전투 시작\n");

        //선택지 검증
        int Choise = ConsoleUtility.ChoiceMenu(1, 4);

        //메뉴 중에서 선택
        switch (Choise)
        {
            case 1:
                StateMenu();
                break;
            case 2:
                InventoryMenu();
                break;
            case 3:
                StoreMenu();
                break;
            case 4:
                Console.WriteLine("전투 시작");
                break;
        }
    }

    //상태창
    private void StateMenu()
    {
        Console.Clear();

        ConsoleUtility.PrintColoredText(Color.Yellow,"# 상태 보기 #\n");
        Console.WriteLine("캐릭터의 정보가 표기됩니다.\n");

        Console.WriteLine($"{player.Name} ({player.Job})");
        Console.WriteLine($"공격력 : {player.Atk}");
        Console.WriteLine($"방어력 : {player.Def}");
        Console.WriteLine($"체  력 : {player.Hp}/{player.MaxHp}");
        Console.WriteLine($"Gold : {player.Gold}");

        Console.WriteLine("\n0. 나가기\n");

        int Choise = ConsoleUtility.ChoiceMenu(0, 0);

        switch (Choise)
        {
            case 0:
                MainMenu();
                break;
        }
    }

    //인벤토리
    private void InventoryMenu()
    {
        Console.Clear();

        ConsoleUtility.PrintColoredText(Color.Yellow, "# 인벤토리 #\n");
        Console.WriteLine("캐릭터가 보유한 아이템이 표기됩니다.\n");



        Console.WriteLine("\n0. 나가기");
        Console.WriteLine("1. 장비 장착\n");

        int Choise = ConsoleUtility.ChoiceMenu(0, 1);

        switch (Choise)
        {
            case 0:
                MainMenu();
                break;
            case 1:
                EquipItem();
                break;
        }
    }

    //장비 장착
    private void EquipItem()
    {
        Console.Clear();

        ConsoleUtility.PrintColoredText(Color.Yellow, "# 인벤토리 #\n");
        Console.WriteLine("캐릭터가 보유한 아이템이 표기됩니다.\n");




        Console.WriteLine("\n0. 나가기\n");

        int Choise = ConsoleUtility.ChoiceMenu(0, 0);

        switch (Choise)
        {
            case 0:
                MainMenu();
                break;
        }
    }

    //상점
    private void StoreMenu()
    {
        Console.Clear();

        ConsoleUtility.PrintColoredText(Color.Yellow, "# 상  점 #\n");
        Console.WriteLine("구매 가능한 아이템이 표기됩니다.\n");



        Console.WriteLine("\n0. 나가기\n");

        int Choise = ConsoleUtility.ChoiceMenu(0, 0);

        switch (Choise)
        {
            case 0:
                MainMenu();
                break;
        }
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        GameManager gameManager = new GameManager();
        gameManager.StartGame();
    }
}