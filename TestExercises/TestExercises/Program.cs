namespace TestExercises
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 람다식 실습 시작 ===");

            // 문제 A 정답
            // 정수 하나를 받아서 제곱을 반환하는 람다식
            Func<int, int> square = x => x * x;
            int value = 5;
            int squared = square(value);
            Console.WriteLine($"{value}의 제곱은 {squared} 입니다.");



            // 문제 B 정답
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            
            // n 이 짝수인지 검사하는 람다식 사용
            
            //IEnumerable<int> evenNumbers = numbers.Where(n => n % 2 == 0);
            List<int> evenNumbers = numbers.Where(n => n % 2 == 0).ToList<int>();

            Console.WriteLine("짝수만: " + string.Join(", ", evenNumbers));



            // 문제 C 정답
            List<string> names = new List<string> { "lee", "park", "kim", "han", "choi" };
            // 글자 수가 4 이상인 이름만 필터링
            //IEnumerable<string> longNames = names.Where(name => name.Length >= 4);
            List<string> longNames = names.Where(name => name.Length >= 4).ToList<string>();

            Console.WriteLine("글자 수 4 이상 이름: " + string.Join(", ", longNames));



            // 문제 D 정답
            List<double> celsiusList = new List<double> { 0, 10, 20, 30 };
            // 섭씨를 받아서 화씨로 바꾸는 람다식
            //IEnumerable<double> fahrenheitList = celsiusList.Select(c => c * 9.0 / 5.0 + 32.0);

            List<double> fahrenheitList = celsiusList.Select(c => c * 9.0 / 5.0 + 32.0).ToList<double>();

            Console.WriteLine("섭씨: " + string.Join(", ", celsiusList));
            Console.WriteLine("화씨: " + string.Join(", ", fahrenheitList));



            // 문제 E 정답
            // 문자열을 받아서 [LOG] 를 붙여 출력하는 람다식
            Action<string> log = message =>
            {
                Console.WriteLine("[LOG] " + message);
            };

            log("프로그램 시작");
            log("사용자 로그인");
            log("프로그램 종료 준비");



            // 문제 F 정답
            List<string> words = new List<string> { "apple", "cat", "banana", "kiwi", "dog" };
            // 단어 길이를 기준으로 오름차순 정렬
            //IEnumerable<string> sortedByLength = words.OrderBy(word => word.Length);

            List<string> sortedByLength = words.OrderBy(word => word.Length).ToList<string>();

            Console.WriteLine("길이 기준 정렬: " + string.Join(", ", sortedByLength));

            ///////////////////////////////////////////////////////////////////////////////////////
            ///


            // 문제 - 추가: 각 숫자를 제곱한 새 리스트를 LINQ로 생성하여 출력
            List<int> squaredNumbers = numbers.Select(n => n * n).ToList();
            Console.WriteLine("제곱 리스트: " + string.Join(", ", squaredNumbers));



            List<Student> students = new List<Student>
            {
                new Student { Name = "민수", Score = 95 },
                new Student { Name = "지영", Score = 80 },
                new Student { Name = "현우", Score = 88 },
                new Student { Name = "수민", Score = 75 },
                new Student { Name = "동철", Score = 98 },
            };

            List<string> lowScoreNames = students
                .Where(s => s.Score <= 80)
                .Select(s => s.Name)
                .ToList();

            Console.WriteLine("점수 80점 이하 학생: " + string.Join(", ", lowScoreNames));



            // 추가 문제 2: Product 리스트에서 가격이 1000 이상 2000 이하인 항목을 가격 오름차순 정렬하여 출력
            List<Product> products = new List<Product>
            {
                new Product { Name = "사과", Price = 1200 },
                new Product { Name = "바나나", Price = 800 },
                new Product { Name = "체리", Price = 2500 },
                new Product { Name = "포도", Price = 1500 },
            };

            List<Product> filteredSortedProducts = products
                .Where(p => p.Price >= 1000 && p.Price <= 2000)
                .OrderBy(p => p.Price)
                .ToList();

            // 출력 형식: "사과(1200), 포도(1500)"
            Console.WriteLine("가격 1000~2000인 상품(가격 오름차순): " +
                string.Join(", ", filteredSortedProducts.Select(p => $"{p.Name}({p.Price})")));
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
