internal class NumberBaseBall
{
    private static void Main(string[] args)
    {
        Console.WriteLine("I want to play a game~!!\n");

        Random random = new Random();

        int q1 = random.Next(0, 9);
        int q2 = random.Next(0, 9);
        int q3 = random.Next(0, 9);

        while (((q1 != q2) && (q2 != q3) && (q1 != q3)) == false)
        {
            if (q1 == q2)
            {
                q2 = random.Next(0, 9);
            }
            else if (q2 == q3)
            {
                q3 = random.Next(0, 9);
            }
            else if (q1 == q3)
            {
                q3 = random.Next(0, 9);
            }
        }

        Console.WriteLine($"정답 : {q1} , {q2} , {q3}");

        int[] answerDigits = new int[3] { q1, q2, q3 };
        int strike = 0;
        int ball = 0;

        do
        {
            Console.WriteLine("\n세 자리 숫자를 입력하세요: ");
            string? answer = Console.ReadLine();

            if (string.IsNullOrEmpty(answer) || answer.Length != 3)
            {
                Console.WriteLine("\n세 자리 숫자를 입력해야 합니다.");
                continue;
            }

            if (!int.TryParse(answer, out int userInput) || userInput < 0 || userInput > 999)
            {
                Console.WriteLine("\n유효한 세 자리 숫자를 입력하세요.");
                continue;
            }

            CompareDigits(answerDigits, userInput, out strike, out ball);
            Console.WriteLine($"\n{userInput} : {strike} Strike, {ball} Ball");

        } while (strike != 3);

        Console.WriteLine("\nStrike Out~!!! Game Over.");
    }

    public static void CompareDigits(int[] answerDigits, int userInput, out int strike, out int ball)
    {
        strike = 0;
        ball = 0;
        int[] userDigits = new int[3]
        {
            userInput / 100 % 10,
            userInput / 10 % 10,
            userInput % 10
        };
        for (int i = 0; i < 3; i++)
        {
            if (userDigits[i] == answerDigits[i])
            {
                strike++;
            }
            else if (Array.Exists(answerDigits, element => element == userDigits[i]))
            {
                ball++;
            }
        }
    }
}