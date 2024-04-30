internal class consoleUtility
{
    internal static int PromptMenuChoise(int min, int max)
    {
        while (true)
        {
            if(int.TryParse(Console.ReadLine(), out int choise) && choise>=min && choise <= max)
            {
                return choise;
            }
            else
            {
                Console.Write("잘못된 입력입니다. 원하시는 선택지의 숫자를 입력하세요.\n>>");

            }
        }
    }
}