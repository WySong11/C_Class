using System;
using System.Runtime.CompilerServices;
using static System.Console;

public class Program
{
    // 프로그램 시작점(Entry Point)
    static void Main(string[] args)
    {

        // Lambda Expressions (람다 식)

        // 기본 람다 식
        // 매개 변수도 없고, 반환 값도 없는 람다 식
        //() => 10

        // 매개 변수가 하나인 람다 식
        //x => x * x

        // 매개 변수가 여러 개인 람다 식
        //(x, y) => x + y

        // 본문이 여러 개인 람다 식
        // { ... } 블록을 사용
        /*(x, y) =>
        {
            int sum = x + y;
            WriteLine($"Sum inside lambda: {sum}");
            return sum;
        };*/

        // 람다 식을 사용한 대리자
        Func<int, int, int> addFunc = (x, y) => x + y;

        int result = addFunc(5, 7);

        BasicLambda();

        ///////////////////////////////////////////
        /// Linq ( Language Integrated Query )
        /// 

        // Where : 조건에 맞는 요소 필터링
        // Select : 요소 변환
        // OrderBy : 요소 정렬 (오름차순)
        // OrderByDescending : 요소 정렬 (내림차순)
        // GroupBy : 요소 그룹화
        // Join : 두 컬렉션 결합
        // ToList : 결과를 리스트로 변환

        // Aggregate : 누적 계산. 예: 합계, 곱셈 등
        // Any : 조건에 맞는 요소가 하나라도 있는지 확인
        // All : 모든 요소가 조건에 맞는지 확인

        // Sum : 숫자 컬렉션의 합계 계산
        // Average : 숫자 컬렉션의 평균 계산
        // Count : 조건에 맞는 요소의 개수 계산

        // First : 조건에 맞는 첫 번째 요소 반환
        // FirstOrDefault : 조건에 맞는 첫 번째 요소 반환, 없으면 기본값 반환

        // ToArryay : 결과를 배열로 변환
        // Distinct : 중복 요소 제거
        // ThenBy : 다중 정렬 시 사용. OrderBy 다음에 사용

        List<int> numbers = new List<int> { 1, 12, 53, 74, 45, 66, 27, 38, 99, 10 };

        // Where 메서드를 사용하여 짝수 필터링
        // numbers 컬렉션에서 짝수만 선택
        // n => n % 2 == 0 람다 식을 사용
        // 결과를 리스트로 변환
        // n => 1 , 12, 53, 74, 45, 66, 27, 38, 99, 10
        // ToList() 메서드를 사용하여 IEnumerable<int>를 List<int>로 변환
        var evenNumbers = numbers.Where(n => n % 2 == 0).ToList();

        foreach (var num in evenNumbers)
        {
            Write(num + " ");
        }

        WriteLine("\n");

        // OrderBy 메서드를 사용하여 짝수 정렬 (오름차순)
        evenNumbers = evenNumbers.OrderBy(n => n).ToList();

        foreach (var num in evenNumbers)
        {
            Write(num + " ");
        }

        WriteLine("\n");

        ////////////////////////////////////////////////////////

        var oddNumbers = numbers.Where(n => n % 2 != 0).
            OrderByDescending(n => n).
            ToList();

        foreach (var num in oddNumbers)
        {
            Write(num + " ");
        }
        
        WriteLine("\n");

        // Count 메서드를 사용하여 50보다 큰 숫자 개수 계산
        var count = numbers.Count(n => n > 50);
        WriteLine($"Count of numbers greater than 50: {count}");

        WriteLine("\n");

        var countList = numbers.Where(n => n > 50).ToList();
 
        foreach (var num in countList)
        {
            Write(num + " ");
        }
        WriteLine("\n");

        // Aggregate 메서드를 사용하여 모든 숫자의 합계 계산
        // 초기값 없이 누적 계산
        // (acc, n) => acc + n 람다 식을 사용하여 누적 합계 계산
        // acc는 누적 값, n은 현재 요소
        var sum = numbers.Aggregate((acc, n) => acc + n);
        WriteLine($"Sum of all numbers: {sum}"); 
        WriteLine("\n");

        // Sum 메서드를 사용하여 모든 숫자의 합계 계산
        var sum2 = numbers.Sum();
        WriteLine($"Sum using Sum(): {sum2}");

        // Average 메서드를 사용하여 모든 숫자의 평균 계산
        var average = numbers.Average();
        WriteLine($"Average of all numbers: {average}");

        // 초기값을 100으로 설정하여 누적 계산
        var testAgg = numbers.Aggregate(100, (acc, n) => acc * 2 - n);
        WriteLine($"Aggregate with initial value 100: {testAgg}");

        // Select 메서드를 사용하여 각 숫자의 제곱 계산
        // n => n * n 람다 식을 사용하여 각 요소를 제곱
        // Select 메서드는 각 요소를 변환하는 데 사용
        // Select의 특징은 원본 컬렉션의 요소 개수와 동일한 개수의 요소를 생성 
        var squaredNumbers = numbers.Select(n => n * n).ToList();

        WriteLine("\n");
        foreach (var num in squaredNumbers)
        {
           Write(num + " ");
        }

        //////////////////////////////////////////////////////////////////
        ///

        Write("\n");

        List<Student> students = new List<Student>
        {
            new Student("Alice", 20, 85),
            new Student("Bob", 22, 90),
            new Student("Charlie", 21, 78),
            new Student("Diana", 23, 92),
            new Student("Ethan", 20, 88)
        };

        // 80점 이상인 학생들의 이름순으로 정렬하고, 이름만 추출
        var highScorers = students.Where(s => s.Score >= 80).
            OrderBy(s => s.Name).
            Select(s => s.Name).
            ToList();

        foreach (var name in highScorers)
        {
            WriteLine(name);
        }
        WriteLine("\n");

        // 80점 이상인 학생들의 이름, 나이, 점수를 추출하고, 이름순으로 정렬
        var highScorerlist = students.Where(s => s.Score >= 80).
            OrderBy(s => s.Name).
            ToList();

        foreach (var name in highScorerlist)
        {
            WriteLine($"{name.Name}, {name.Age}, {name.Score}");
        }

        // 나이가 21세 이상인 학생들의 평균 점수 계산
        var averageAge21 = students.Where(s => s.Age >= 21).Average(s => s.Score);
        WriteLine($"\nAverage score of students aged 21 or older: {averageAge21}");

        // 이름이 'a' 문자를 포함하는 학생들의 점수 합계 계산
        var listname = students.Where(s => s.Name.ToLower().Contains('a')).
            Sum(s => s.Score);
        WriteLine($"\nSum of scores of students whose names contain 'a': {listname}");

        ///////////////////////////////////////////////////////
        ///

        List<Item> items = new List<Item>
        {
            new Item("Apple", ItemType.Food, 100),
            new Item("Laptop", ItemType.Electronics, 1500),
            new Item("Jeans", ItemType.Clothing, 60),
            new Item("Sofa", ItemType.Furniture, 800),
            new Item("Bread", ItemType.Food, 50),
            new Item("Smartphone", ItemType.Electronics, 900)
        };

        

        // 가격이 100 이상인 전자제품 아이템의 이름과 가격을 추출하여 가격순으로 정렬
        var expensiveElectronics = items.Where(i => i.Type == ItemType.Electronics && i.Price >= 100)
                                        .OrderBy(i => i.Price)
                                        .Select(i => new { i.Name, i.Price })
                                        .ToList();

        foreach (var item in expensiveElectronics)
        {
            WriteLine($"{item.Name}: {item.Price}");
        }

        // 옷과 음식 아이템의 총 가격 계산
        var totalClothingAndFoodPrice = items.Where(i => i.Type == ItemType.Clothing || i.Type == ItemType.Food)
                                             .Sum(i => i.Price);
        WriteLine($"\nTotal price of clothing and food items: {totalClothingAndFoodPrice}");

        ///////////////////////////////////////////////////
        ///

        WriteLine("\n");

        List<Category> categories = new List<Category>
        {
            new Category { Id = 1, Type = ItemType.Food },
            new Category { Id = 2, Type = ItemType.Electronics },
            new Category { Id = 3, Type = ItemType.Clothing }
        };

        // 아이템과 카테고리를 Type 속성을 기준으로 조인
        // from : items 컬렉션에서 각 item을 선택
        // join : categories 컬렉션에서 각 category를 선택
        // on : item.Type과 category.Type이 일치하는 항목을 조인
        // equals : 일치 조건
        // select : 조인된 결과에서 새로운 익명 객체 생성

        var itemCategoryJoin = from item in items
                               join category in categories
                               on item.Type equals category.Type
                               select new
                               {
                                   ItemName = item.Name,
                                   CategoryId = category.Id,
                                   ItemType = item.Type
                               };

        // 메서드 체이닝을 사용한 Join
        // Joint 메서드를 사용하여 두 컬렉션을 조인
        // Catergory 컬렉션의 Type 속성과 Item 컬렉션의 Type 속성을 기준으로 조인
        // (item, category) => new { ... } 람다 식을 사용하여 조인된 결과에서 새로운 익명 객체 생성
        var itemCategoryJoin2 = items.Join(categories,
            item => item.Type,
            category =>  category.Type,
            (item, category) => new
            {
                ItemName = item.Name,
                CategoryId = category.Id,
                ItemType = item.Type
            });

        foreach (var entry in itemCategoryJoin)
        {
            WriteLine($"Item: {entry.ItemName}, Category ID: {entry.CategoryId}, Type: {entry.ItemType}");
        }
    }

    // 람다 식을 사용한 메서드
    public static int Add(int x, int y) => x + y;

    public static void TestFunction()
    {
        WriteLine("An event has occurred (from OnEventOccurred)!");
    }

    static void BasicLambda()
    {
        Func<int, int> doubleNumber = (x) => x * 2;

        int result = doubleNumber(5);
        WriteLine($"Double of 5 is: {result}");
    }
}

public enum ItemType
{
    Food,
    Electronics,
    Clothing,
    Furniture
}

public class Item
{
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public int Price { get; set; }
    public Item(string name, ItemType type, int price)
    {
        Name = name;
        Type = type;
        Price = price;
    }
}

public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }

    public int Score { get; set; }

    public Student() { }

    public Student(string name, int age, int score)
    {
        Name = name;
        Age = age;
        Score = score;
    }
}

public class Category
{
    public int Id { get; set; }

    public ItemType Type { get; set; }
}
