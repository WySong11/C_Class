using System;

public class NumberBaseballGame
{
    public void StartGame()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Number Baseball Game!\n");
        // Game logic goes here
        // For example, generate random numbers, get user input, check for matches, etc.
        
        InputDelegate inputDelegate = new InputDelegate();
        OutpuDelegate outputDelegate = new OutpuDelegate();

        // ?.Invoke() : 널 조건부 호출 연산자. delegate가 null이 아닐 때만 호출합니다.
        // ?? Array.Empty<int>() : delegate가 null인 경우 빈 배열을 반환합니다.
        int[] question = inputDelegate.CreateQuestionDelegateMethod?.Invoke() ?? Array.Empty<int>();
        if (question.Length == 0)
        {
            Console.WriteLine("Failed to generate a question. Exiting game.");
            return;
        }

        // question을 출력합니다. string.Join을 사용하여 배열을 문자열로 변환합니다.
        Console.WriteLine($"Generated question: {string.Join(", ", question)}\n");

        while (true)
        {
            Console.Write("Enter your answer (3 digits): ");
            string userInput = Console.ReadLine() ?? string.Empty;
            
            int[] answer = inputDelegate.SubmitAnswerActionMethod?.Invoke(userInput) ?? Array.Empty<int>();
            if (answer.Length == 0)
            {
                continue; // 유효하지 않은 입력인 경우 다시 입력 받기
            }
            // 스트라이크와 볼을 계산하는 delegate를 호출합니다.
            int result = outputDelegate.CompareQuestionAndAnswer?.Invoke(question, answer) ?? 0;
            
            if (result == 1) // 게임 종료 조건: 스트라이크가 3개인 경우
            {
                Console.WriteLine("Congratulations! You've guessed the number correctly!");
                break;
            }
        }

        Console.WriteLine("\nDo you want to play again? (yes/no)");
        string playAgain = Console.ReadLine()?.Trim().ToLower() ?? "no";
        if (playAgain == "yes" || playAgain == "y")
        {
            StartGame(); // 재귀 호출로 게임을 다시 시작합니다.
        }
        else
        {
            Console.WriteLine("\nThank you for playing! Goodbye!");
        }
    }
}