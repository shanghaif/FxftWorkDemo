using System;

namespace 随机数
{
    class Program
    {
        static void Main(string[] args)
        {
            Random ran = new Random();
            int firRandNum = ran.Next(100, 999);
            int secRandNum = ran.Next(100, 999);
            int fiveRandNum = ran.Next(10000, 99999);
        }

    }
}
