using System;
using System.Collections.Generic;

namespace TestIndexer
{
    public class Program
    {
        string[] baseballPostion = { "Pitcher", "Catcher", "First Base", "Second Base", "Third Base", "Shortstop", "Left Field", "Center Field", "Right Field" };

        public string this[int index]
        {
            get { return baseballPostion[index]; }
            set { baseballPostion[index] = value; }
        }

        static void Main(string[] args)
        {
            

            
        }
    }
}
