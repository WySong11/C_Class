using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SystemCollections
{
    public void OnArrayList()
    {
        // ArrayList : 크기가 동적으로 변할 수 있는 배열. 다양한 타입의 객체를 저장할 수 있음.
        System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
        arrayList.Add("Hello");
        arrayList.Add(123);
        arrayList.Add(true);
        foreach (var item in arrayList)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Count : ArrayList의 요소 개수
        Console.WriteLine($"Count: {arrayList.Count}");

        // Remove : 특정 요소를 제거
        arrayList.RemoveAt(1); // Remove the second item
        foreach (var item in arrayList)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Insert : 특정한 위치에 요소를 삽입
        arrayList.Insert(1, "World"); // Insert "World" at index 1
        foreach (var item in arrayList)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // AddRange : 다른 컬렉션의 요소를 추가
        System.Collections.ArrayList anotherList = new System.Collections.ArrayList() { "Apple", "Banana" };
        arrayList.AddRange(anotherList);
        foreach (var item in arrayList)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Contains : 특정 요소가 ArrayList에 포함되어 있는지 확인
        bool containsHello = arrayList.Contains("Hello");
        Console.WriteLine($"Contains 'Hello': {containsHello}");
        Console.WriteLine();

        // RemoveRange : 특정 범위의 요소를 제거
        arrayList.RemoveRange(2, 2); // Remove two items starting from index 2
        foreach (var item in arrayList)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Clear : ArrayList의 모든 요소를 제거
        arrayList.Clear();
        Console.WriteLine($"Count after Clear: {arrayList.Count}");
        Console.WriteLine();
    }

    public void OnHashtable()
    {
        // Hashtable : 키-값 쌍으로 데이터를 저장하는 컬렉션. 키는 고유해야 하며, 값은 중복될 수 있음.
        System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
        hashtable.Add("Key1", "Value1");
        hashtable.Add("Key2", "Value2");
        foreach (System.Collections.DictionaryEntry entry in hashtable)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
        Console.WriteLine();

        // Count : Hashtable의 요소 개수
        Console.WriteLine($"Count: {hashtable.Count}");
        Console.WriteLine();

        // ContainsKey : 특정 키가 Hashtable에 존재하는지 확인
        bool containsKey1 = hashtable.ContainsKey("Key1");
        Console.WriteLine($"Contains 'Key1': {containsKey1}");
        Console.WriteLine();

        // ContainsValue : 특정 값이 Hashtable에 존재하는지 확인
        bool containsValue2 = hashtable.ContainsValue("Value2");
        Console.WriteLine($"Contains 'Value2': {containsValue2}");
        Console.WriteLine();
            
         // Remove : 특정 키의 요소를 제거
         hashtable.Remove("Key1");
        foreach (System.Collections.DictionaryEntry entry in hashtable) 
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
        Console.WriteLine();

        // Clear : Hashtable의 모든 요소를 제거
        hashtable.Clear();
        Console.WriteLine($"Count after Clear: {hashtable.Count}");
        Console.WriteLine();
    }

    public void OnQueue()
    {
        // Queue : 선입선출(FIFO) 방식으로 데이터를 저장하는 컬렉션. 먼저 들어온 데이터가 먼저 나감.
        System.Collections.Queue queue = new System.Collections.Queue();
        queue.Enqueue("First");
        queue.Enqueue("Second");
        queue.Enqueue("Third");
        while (queue.Count > 0)
        {
            Console.WriteLine(queue.Dequeue());
        }
        Console.WriteLine();

        // Count : Queue의 요소 개수
        Console.WriteLine($"Count: {queue.Count}");
        Console.WriteLine();

        // Peek : Queue의 첫 번째 요소를 반환하지만 제거하지 않음
        queue.Enqueue("First");
        queue.Enqueue("Second");
        queue.Enqueue("Third");
        Console.WriteLine($"Peek: {queue.Peek()}");
        Console.WriteLine();

        // Contains : 특정 요소가 Queue에 포함되어 있는지 확인
        bool containsSecond = queue.Contains("Second");
        Console.WriteLine($"Contains 'Second': {containsSecond}");
        Console.WriteLine();

        // Clear : Queue의 모든 요소를 제거
        queue.Clear();
        Console.WriteLine($"Count after Clear: {queue.Count}");
        Console.WriteLine();
    }

    public void OnStack()
    {
        // Stack : 후입선출(LIFO) 방식으로 데이터를 저장하는 컬렉션. 마지막에 들어온 데이터가 먼저 나감.
        System.Collections.Stack stack = new System.Collections.Stack();
        stack.Push("First");
        stack.Push("Second");
        stack.Push("Third");
        while (stack.Count > 0)
        {
            Console.WriteLine(stack.Pop());
        }
        Console.WriteLine();

        // Count : Stack의 요소 개수
        Console.WriteLine($"Count: {stack.Count}");
        Console.WriteLine();

        // Peek : Stack의 마지막 요소를 반환하지만 제거하지 않음
        stack.Push("First");
        stack.Push("Second");
        stack.Push("Third");
        Console.WriteLine($"Peek: {stack.Peek()}");
        Console.WriteLine();

        // Contains : 특정 요소가 Stack에 포함되어 있는지 확인
        bool containsSecond = stack.Contains("Second");
        Console.WriteLine($"Contains 'Second': {containsSecond}");
        Console.WriteLine();

        // Clear : Stack의 모든 요소를 제거
        stack.Clear();
        Console.WriteLine($"Count after Clear: {stack.Count}");
        Console.WriteLine();
    }
}