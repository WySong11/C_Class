using System;
using System.Collections.Generic;

namespace PermutationGeneration
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = new List<int>();
            List<List<int>> result = new List<List<int>>();

            nums = GetPromprtNumbers();

            GeneratePermutations(nums, result);

        }

        public static List<int> GetPromprtNumbers()
        {
            List<int> numbers = new List<int>();
            Console.WriteLine("Enter numbers separated by spaces (e.g., 1 2 3):");
            string? input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                string[] parts = input.Split(' ');
                foreach (string part in parts)
                {
                    if (int.TryParse(part, out int number))
                    {
                        numbers.Add(number);
                    }
                    else
                    {
                        Console.WriteLine($"'{part}' is not a valid number and will be ignored.");
                    }
                }
            }
            return numbers;
        }

        public static void GeneratePermutations(List<int> nums, List<List<int>> result, int start = 0)
        {
            if (start >= nums.Count)
            {
                result.Add(new List<int>(nums));
                return;
            }

            var used = new HashSet<int>();
            for (int i = start; i < nums.Count; i++)
            {
                if (used.Contains(nums[i])) continue; // 이미 사용한 숫자면 건너뜀
                used.Add(nums[i]);

                Swap(nums, start, i);
                GeneratePermutations(nums, result, start + 1);
                Swap(nums, start, i); // backtrack
            }

            // 정렬 및 출력 (start == 0일 때만)
            if (start == 0)
            {
                result.Sort((a, b) =>
                {
                    for (int i = 0; i < a.Count && i < b.Count; i++)
                    {
                        int cmp = a[i].CompareTo(b[i]);
                        if (cmp != 0) return cmp;
                    }
                    return a.Count.CompareTo(b.Count);
                });
                Console.WriteLine("Generated Permutations (Sorted, No Duplicates):");
                foreach (var perm in result)
                {
                    Console.WriteLine(string.Join(", ", perm));
                }
            }
        }

        public static void Swap(List<int> nums, int i, int j)
        {
            (nums[i], nums[j]) = (nums[j], nums[i]);
        }
    }
}
