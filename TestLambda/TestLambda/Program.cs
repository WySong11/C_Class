using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

public enum ItemType
{
    Book,
    Food,
    Electronics
}

public class student
{
    public string Name { get; set; }
    public int Score { get; set; }
}

public class Item
{
    public string Name { get; set; }
    public ItemType Type { get; set; }
}

class Category
{
    public int Id { get; set; }
    public ItemType Type { get; set; }
}



public class Program
{

    static void Main(string[] args)
    {
        Func<int, int> doubleNumberr = (x) => x * 2;

        int result1 = doubleNumberr(3);
        int result2 = doubleNumberr(7);

        WriteLine($"Double of 3 is: {result1}");
        WriteLine($"Double of 7 is: {result2}");

        WriteLine();

        //////
        ///

        int[] numbers = { 0, 1, 2, 3, 4, 5, 6 };

        // 쿼리식
        var evenNumbersByQuery = from n in numbers
                                 where n % 2 == 0
                                 select n;

        foreach (var num in evenNumbersByQuery)
        {
            WriteLine($"Even number: {num}");
        }

        WriteLine();

        var evenNumbersByMethod = numbers.Where(n => n % 2 == 0);

        foreach (var num in evenNumbersByMethod)
        {
            WriteLine($"Even number: {num}");
        }

        // 주요 LINQ 메서드들

        // Where : 조건에 맞는 요소를 필터링
        // Select : 요소를 변환
        // OrderBy : 요소를 정렬 (오름차순)
        // OrderByDescending : 요소를 정렬 (내림차순)
        // GroupBy : 요소를 그룹화
        // Join : 두 컬렉션을 조인
        // ToList : 컬렉션을 리스트로 변환

        // Aggregate : 컬렉션의 요소들을 누적하여 단일 값으로 계산
        // Any : 컬렉션에 조건에 맞는 요소가 하나라도 있는지 확인
        // All : 컬렉션의 모든 요소가 조건에 맞는지 확인

        // Sum : 컬렉션의 모든 요소의 합을 계산
        // Average : 컬렉션의 모든 요소의 평균을 계산
        // Count : 컬렉션의 요소 개수를 셈

        // First : 컬렉션에서 첫 번째 요소를 반환
        // FirstOrDefault : 컬렉션에서 첫 번째 요소를 반환하거나, 없으면 기본값 반환

        // ToArray : 컬렉션을 배열로 변환
        // Distinct : 컬렉션에서 중복된 요소를 제거
        // ThenBy : 다중 정렬을 수행할 때 사용


        List<int> list = new List<int>() { 1, 11, 3, 14, 5, 16, 7, 18, 9, 10 };

        // OrderBy : 요소를 정렬 (오름차순)
        var orderList = list.OrderBy(n => n);

        WriteLine();

        foreach (var num in orderList)
        {
            WriteLine($"Ordered number: {num}");
        }

        // OrderByDescending : 요소를 정렬 (내림차순) 

        var orderDes = list.OrderByDescending(n => n);

        foreach (var num in orderDes)
        {
            WriteLine($"Ordered number: {num}");
        }

        WriteLine();

        // Count

        var count = list.Count(n => n % 2 == 0);

        WriteLine($"Count of even numbers: {count}");

        WriteLine();

        var count2 = list.Count(n => n % 2 != 0);

        WriteLine($"Count : {count2}");

        // GroupBy : 요소를 그룹화
        /*        var groupList = list.GroupBy(n => n % 2 == 0);

                foreach (var group in groupList)
                {
                    WriteLine(group.Key ? "Even numbers:" : "Odd numbers:");
                    foreach (var num in group)
                    {
                        WriteLine($"  {num}");
                    }
                }*/

        ///////////////////////////////////////////////
        ///

        List<student> students = new List<student>()
        {
            new student() { Name = "Alice", Score = 85 },
            new student() { Name = "Bob", Score = 92 },
            new student() { Name = "Charlie", Score = 78 },
            new student() { Name = "David", Score = 90 },
            new student() { Name = "Eve", Score = 88 }
        };

        // 90점 이상인 학생들의 이름을 점수 내림차순으로 출력
        var topStudents = students
            .Where(s => s.Score >= 90)
            .OrderByDescending(s => s.Score)
            .Select(s => s.Name);

        WriteLine("Students with scores 90 and above:");

        foreach (var name in topStudents)
        {
            WriteLine(name);
        }


        ////////////////////////////////////////////////////
        ///

        List<Item> items = new List<Item>()
        {
            new Item() { Name = "C# Programming", Type = ItemType.Book },
            new Item() { Name = "Apple", Type = ItemType.Food },
            new Item() { Name = "Laptop", Type = ItemType.Electronics },
            new Item() { Name = "Bread", Type = ItemType.Food },
            new Item() { Name = "Headphones", Type = ItemType.Electronics }
        };

        // Food 타입의 아이템 이름을 출력
        var foodItems = items
            .Where(i => i.Type == ItemType.Food)
            .Select(i => i.Name);

        WriteLine("Food items:");
        foreach (var itemName in foodItems)
        {
            WriteLine(itemName);
        }

        /////////////////////////////////////////////////////
        ///

        List<Category> categories = new List<Category>()
        {
            new Category() { Id = 1, Type = ItemType.Book },
            new Category() { Id = 2, Type = ItemType.Food },
            new Category() { Id = 3, Type = ItemType.Electronics }
        };

        // items 와 categories 를 조인하여 각 아이템의 이름과 카테고리 ID 출력

        var itemCategories = from item in items
                             join category in categories
                             on item.Type equals category.Type
                             select new { item.Name, category.Id };

        var itemCategoriesMethod = items.Join(
            categories,
            item => item.Type,
            category => category.Type,
            (item, category) => new { item.Name, category.Id });

        WriteLine("Item Categories (Query Syntax):");

        foreach (var ic in itemCategoriesMethod)
        {
            WriteLine($"Item: {ic.Name}, Category ID: {ic.Id}");
        }
    }
}



