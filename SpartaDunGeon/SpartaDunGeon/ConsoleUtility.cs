internal class ConsoleUtility
{
    public static int PromptMenuChoise(int min, int max)
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int input) && input >= min && input <= max)
            {
                return input;
            }
            Console.Write("잘못 입력하셨습니다. 원하시는 선택지의 숫자를 입력해 주세요: ");
        }
    }
}