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
        Console.WriteLine("안녕하세요.");
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