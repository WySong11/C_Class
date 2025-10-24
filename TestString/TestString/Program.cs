using System;
using static System.Console;


namespace TestString
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s = "Hello, World!";

            string korean = "안녕하세요";

            WriteLine(korean);
            WriteLine($"String length: {korean.Length}");
            WriteLine($"첫글자 : {korean[3]}");


            WriteLine();


            string hello = "Hello";
            string world = "World";

            string combined = hello + ", " + world + "!";

            string greetingOld = string.Format("{0}, {1}! {2} {3}", hello, world, world, hello);
            string greeting = $"{hello}, {world}!";

            WriteLine(combined);
            WriteLine(greetingOld);
            WriteLine(greeting);

            string itemName = "ShotGun";

            WriteLine(string.Format("Item Name : {0}", itemName));

            WriteLine($"Item Name : {itemName}");

            // \n : 줄바꿈

            WriteLine($"Item Name \\n : {itemName}");

            string testline = "abcdEFGH";

            // 문자 -> 소문자
            WriteLine(testline.ToLower());

            // 문자 -> 대문자
            WriteLine(testline.ToUpper());

            if (testline.Equals("abcdEFGH"))
            {
                WriteLine("문자열이 동일합니다.");
            }
            else
            {
                WriteLine("문자열이 동일하지 않습니다.");
            }

            string id = "abcdefgh";

            if (testline.ToLower().Equals("abcdefgh"))
            {
                WriteLine("문자열이 동일합니다.");
            }
            else
            {
                WriteLine("문자열이 동일하지 않습니다.");
            }

            if (id.Contains("def"))
            {
                WriteLine("id 문자열에 def가 포함되어 있습니다.");
            }
            else
            {
                WriteLine("id 문자열에 def가 포함되어 있지 않습니다.");
            }

            string title = "Captin America,Iron Man,Hulk,Thor,SpiderMan";

            // Split : 문자열 나누기
            string[] testSplit = title.Split(',');

            foreach (string sSplit in testSplit)
            {
                WriteLine(sSplit);
            }

            // Trim : 공백제거
            string trimTest = "   Trim Test   ";
            WriteLine($"[{trimTest.Trim()}]");

            // Replace : 문자열 치환
            string replaceTest = "Hello World! World!";
            WriteLine(replaceTest.Replace("World", "C#"));

            // Substring : 문자열 자르기
            string substringTest = "0123456789";
            WriteLine(substringTest.Substring(3, 4)); // 3번 인덱스부터 4글자

            // join : 문자열 합치기
            string[] joinTest = new string[] { "010", "1234", "5678" };
            WriteLine(string.Join("-", joinTest));

            // IndexOf : 특정 문자나 문자열의 인덱스 반환
            string indexOfTest = "Hello, World! Welcome to C# Programming World!";
            WriteLine(indexOfTest.IndexOf("World")); // 처음 나오는 World의 인덱스 반환

            double ratio = 0.12345;
            int number = 1234567;
            DateTime now = DateTime.Now;

            // 백분율 표시
            WriteLine(string.Format("{0:P1}", ratio));

            // 천 단위 구분기호 표시
            WriteLine(string.Format("{0:N0}", number));

            // 날짜 표시
            WriteLine(string.Format("{0:yyyy-MM-dd HH:mm:ss}", now));

            WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss"));
            WriteLine(now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초"));            
        }
    }
}
