using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyramid.Test
{
    public class TestDelegate
    {
        // Delegate 선언
        public delegate void PyramidDelegate(int height, bool reverse);
        public PyramidDelegate? pyramidMethod;

        // Delegate를 사용하는 메서드
        public void ExecutePyramid(PyramidDelegate pyramidMethod, int height, bool reverse)
        {
            if (pyramidMethod == null)
            {
                Console.WriteLine("No method provided to execute.");
                return;
            }
            pyramidMethod(height, reverse);
        }
    }
}

// Action delegate 사용 예시
// public void ExecutePyramid(Action<int, bool> pyramidMethod, int height, bool reverse)
// {    
//     if (pyramidMethod == null)
//     {
//         Console.WriteLine("No method provided to execute.");
//         return;
//     }
//     pyramidMethod(height, reverse);
// }
// 사용 예시
// ExecutePyramid(pyramid.PrintPyramid, Height, eTypePyamid == ETypePyamid.Increase || eTypePyamid == ETypePyamid.ReverseIncrease ? false : true);
// ExecutePyramid(pyramidMethod, Height, eTypePyamid == ETypePyamid.Increase || eTypePyamid == ETypePyamid.ReverseIncrease ? false : true);
// pyramidMethod?.Invoke(Height, eTypePyamid == ETypePyamid.Increase || eTypePyamid == ETypePyamid.ReverseIncrease ? false : true);    

// Func delegate 사용 예시
// public void ExecutePyramid(Func<int, bool, string> pyramidMethod, int height, bool reverse)
// {
//     if (pyramidMethod == null)
//     {
//         Console.WriteLine("No method provided to execute.");
//         return;
//     }
//     string result = pyramidMethod(height, reverse);
//     Console.WriteLine(result);
// }
// 사용 예시
// ExecutePyramid(pyramid.PrintPyramid, Height, eTypePyamid == ETypePyamid.Increase || eTypePyamid == ETypePyamid.ReverseIncrease ? false : true);
// string result = pyramidMethod?.Invoke(Height, eTypePyamid == ETypePyamid.Increase || eTypePyamid == ETypePyamid.ReverseIncrease ? false : true);
// Console.WriteLine(result); // 결과 출력
// pyramidMethod?.Invoke(Height, eTypePyamid == ETypePyamid.Increase || eTypePyamid == ETypePyamid.ReverseIncrease ? false : true); // 대리자 호출

// Predicate delegate 사용 예시
// public void ExecutePyramid(Predicate<int> pyramidMethod, int height)
// {
//     if (pyramidMethod == null)
//     {
//         Console.WriteLine("No method provided to execute.");
//         return;
//     }
//     bool result = pyramidMethod(height);
//     Console.WriteLine($"Pyramid height {height} is valid: {result}");
// }
// 사용 예시
// ExecutePyramid(height => height > 0, Height); // 높이가 0보다 큰지 확인하는 Predicate 사용
// bool isValid = pyramidMethod?.Invoke(Height); // 높이가 유효한지 확인
// Console.WriteLine($"Pyramid height {Height} is valid: {isValid}"); // 결과 출력
// pyramidMethod?.Invoke(Height); // 대리자 호출

// 멀티캐스트 대리자 사용 예시
// public void ExecutePyramid(PyramidDelegate pyramidMethod, int height, bool reverse)
// {
//     if (pyramidMethod == null)
//     {
//         Console.WriteLine("No method provided to execute.");
//         return;
//     }
//     foreach (PyramidDelegate method in pyramidMethod.GetInvocationList())
//     {
//         method(height, reverse); // 각 메서드를 호출합니다.
//     }
// }
// 사용 예시
// pyramidMethod += pyramid.PrintPyramid; // 여러 메서드를 추가할 수 있습니다.
// pyramidMethod += anotherPyramidMethod; // 다른 메서드 추가
// ExecutePyramid(pyramidMethod, Height, eTypePyamid == ETypePyamid.Increase || eTypePyamid == ETypePyamid.ReverseIncrease ? false : true); // 멀티캐스트 대리자 호출
// foreach (var method in pyramidMethod.GetInvocationList())
// {
//     method(Height, eTypePyamid == ETypePyamid.Increase || eTypePyamid == ETypePyamid.ReverseIncrease ? false : true); // 각 메서드를 호출합니다.
// }
// pyramidMethod?.Invoke(Height, eTypePyamid == ETypePyamid.Increase || eTypePyamid == ETypePyamid.ReverseIncrease ? false : true); // 대리자 호출

// 람다식 사용 예시
// public void ExecutePyramid(Action<int, bool> pyramidMethod, int height, bool reverse)
// {
//     if (pyramidMethod == null)
//     {
//         Console.WriteLine("No method provided to execute.");
//         return;
//     }
//     pyramidMethod(height, reverse); // 람다식을 호출합니다.
// }
// 사용 예시
// ExecutePyramid((h, r) => pyramid.PrintPyramid(h, r), Height, eTypePyamid == ETypePyamid.Increase || eTypePyamid == ETypePyamid.ReverseIncrease ? false : true); // 람다식 사용

// 이벤트  사용 예시
// public event PyramidDelegate PyramidEvent;
// public void ExecutePyramid(int height, bool reverse)
// {
//     if (PyramidEvent == null)
//     {
//         Console.WriteLine("No event handlers registered.");
//         return;
//     }
//     PyramidEvent(height, reverse); // 이벤트를 호출합니다.
// }
// 사용 예시
// PyramidEvent += pyramid.PrintPyramid; // 이벤트 핸들러 등록
// ExecutePyramid(Height, eTypePyamid == ETypePyamid.Increase || eTypePyamid == ETypePyamid.ReverseIncrease ? false : true); // 이벤트 호출
