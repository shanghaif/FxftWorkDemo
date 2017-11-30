using System;

namespace 随机数
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random(Environment.TickCount);
            int firRandNum = rnd.Next(100, 999);
            int secRandNum = rnd.Next(100, 999);
            int fiveRandNum = rnd.Next(10000, 99999);
        }

    }
}
