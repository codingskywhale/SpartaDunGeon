public enum Color
{
    Red,
    Green,
    Blue,
    Yellow
}

public class consoleUtility
{
    public static int ChoiceMenu(int min, int max)
    {
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        while (true)
        {            
            PrintColoredText(Color.Yellow, ">> ");
                        
            if(int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
            {
                return choice;
            }
            Console.WriteLine("잘못된 입력입니다.");
        }
    }

    public static void PrintColoredText(Color color, string text)
    {
        switch (color)
        {
            case Color.Red:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(text);
                Console.ResetColor();
                break;
            case Color.Blue:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(text);
                Console.ResetColor();
                break;
            case Color.Yellow:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(text);
                Console.ResetColor();
                break;
            case Color.Green:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(text);
                Console.ResetColor();
                break;
        }   
    }
}