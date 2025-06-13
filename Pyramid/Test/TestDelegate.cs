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