using static System.Net.Mime.MediaTypeNames;

public enum Color
{
    Red,
    Green,
    Blue,
    Yellow
}


public class ConsoleUtility
{
    public static int ChoiceMenu(int min, int max)
    {
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        while (true)
        {
            PrintColoredText(Color.Yellow, ">> ");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
            {
                return choice;
            }
            Console.WriteLine("잘못된 입력입니다.");
        }
    }


    // 텍스트 전체 색상 변경(줄바꿈X)
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

    // 텍스트 색상 변경(하이라이트, 줄바꿈O)
    public static void PrintTextHighlight(Color color, string str1, string str2, string str3 = "")
    {
        switch (color)
        {
            case Color.Red:
                Console.Write(str1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(str2);
                Console.ResetColor();
                Console.WriteLine(str3);
                break;
            case Color.Blue:
                Console.Write(str1);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(str2);
                Console.ResetColor();
                Console.WriteLine(str3);
                break;
            case Color.Yellow:
                Console.Write(str1);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(str2);
                Console.ResetColor();
                Console.WriteLine(str3);
                break;
            case Color.Green:
                Console.Write(str1);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(str2);
                Console.ResetColor();
                Console.WriteLine(str3);
                break;
        }
    }

    //문자열 길이 계산
    public static int GetTextLength(string str)
    {
        int length = 0;
        foreach (char c in str)
        {
            if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
            {
                length += 2; // 한글과 같은 넓은 문자에 대해 길이를 2로 취급
            }
            else
            {
                length += 1; // 나머지 문자에 대해 길이를 1로 취급
            }
        }

        return length;
    }

    //문자 정렬
    public static string PadRight(string str, int totalLength)
    {   
        int currentLength = GetTextLength(str);
        int padding = totalLength - currentLength;
        return str.PadRight(str.Length + padding);
    }
}