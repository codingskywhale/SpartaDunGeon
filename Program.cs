public class Manager
{
    internal void StartGame()
    {
        MainMenu();
    }

    private void MainMenu()
    {
        Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
        Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");

        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 전투 시작\n");

        Console.Write("원하시는 행동을 입력해 주세요.\n>>");

        switch (consoleUtility.PromptMenuChoise(1, 2))
        {
            case 1:
                Console.WriteLine("상태 보기");
                break;
            case 2:
                Console.WriteLine("전투 시작");
                break;
        }
    }
}

internal class Program
{
    public static void Main(string[] args)
    {
        Manager manager = new Manager();
        manager.StartGame();
    }
}