using System;

namespace MemCachedDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //清除所有的缓存数据
            MemCachedManager.cache.FlushAll();

            var v1 = MemCachedManager.cache.Add("a", "123");
            var res1 = MemCachedManager.cache.Get("a");
            Console.WriteLine("第一次Add返回值{0},结果{1}", v1, res1);

            var v2 = MemCachedManager.cache.Add("a", "456");
            var res2 = MemCachedManager.cache.Get("a");
            Console.WriteLine("第二次Add返回值{0},结果{1}", v2, res2);


            var v3 = MemCachedManager.cache.Set("a", "789");
            var res3 = MemCachedManager.cache.Get("a");
            Console.WriteLine("Set返回值{0}，结果{0}", res3);


            var v4 = MemCachedManager.cache.Replace("a", "123");
            var res4 = MemCachedManager.cache.Get("a");
            Console.WriteLine("replace返回值{0}，结果{0}", res4);

            var v5 = MemCachedManager.cache.Replace("b", "123");
            Console.WriteLine("replace返回值{0}", v5);

            MemCachedManager.cache.Set("b", "456");
            MemCachedManager.cache.Delete("b");
            var v6 = MemCachedManager.cache.KeyExists("b");
            Console.WriteLine("Delete之后返回值{0}", v6);
        }
    }
}
