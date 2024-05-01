public class GameManager
{
    public Player Player;

    public void StartGame()
    {
        Console.Write("이름을 입력해 주세요: ");
        string name = Console.ReadLine();

        Console.WriteLine("직업을 선택해 주세요.");
        Console.WriteLine("1. 전사");
        Console.WriteLine("2. 마법사");
        Console.Write("\n>>");

        while (true)
        {
            string jobChoise = Console.ReadLine();
            if (jobChoise == "1")
            {
                Console.WriteLine("전사를 선택하셨습니다.");
                MainMenu();
                return;
            }
            else if (jobChoise == "2")
            {
                Console.WriteLine("마법사를 선택하셨습니다.");
                return;
            }
            else
            {
                Console.Write("선택지에서 골라주세요.\n>>");
            }
        }
    }

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
        Console.WriteLine("4. 전투 시작");
        Console.Write("\n원하는 선택지를 고르세요: ");

        //선택지 검증
        int Choise = ConsoleUtility.PromptMenuChoise(1, 4);

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

    private void StateMenu()
    {
        throw new NotImplementedException();
    }

    private void InventoryMenu()
    {
        throw new NotImplementedException();
    }

    private void StoreMenu()
    {
        throw new NotImplementedException();
    }

    internal class Program
{
    static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }
    }
}