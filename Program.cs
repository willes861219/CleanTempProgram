using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace CleanTempProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"
                ---------------------------------------------------------------------------
                                            -清除暫存程序-
                               ------------------------------------------------
                         說明： 自動清除使用者資料夾Temp的垃圾，等待5秒後自動開始清除
                ---------------------------------------------------------------------------");
            Thread.Sleep(5000); // 等候5秒


            /// 取得使用者名稱、Temp路徑
            string UserName = Environment.UserName;
            string pTempDir = $@"C:\Users\{UserName}\AppData\Local\Temp";

            /// 垃圾資料
            //string[] files = Directory.GetFiles(pTempDir);
            Count count = new Count();
            DirSearch(pTempDir, count);
            Console.WriteLine
               (
               "--------------------------------------------------------------------------- \n" +
               $"成功刪除 { count.nCountO.ToString()} 筆暫存資料 | { count.nCountX.ToString()} 筆還在使用中無法刪除 \n" +
               "--------------------------------------------------------------------------- "
               );
            Console.ReadKey();
        }
        private static void DirSearch(string sDir, Count count)
        {
            foreach (string d in Directory.GetDirectories(sDir))
            {
                foreach (string f in Directory.GetFiles(d))
                {
                    try
                    {
                        File.Delete(f);
                        Console.WriteLine($@"成功刪除 : {Path.GetFileName(f)}");
                        count.nCountO += 1;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($@"無法刪除 : {Path.GetFileName(f)}");
                        count.nCountX += 1;
                    }
                }
                DirSearch(d, count);
            }
        }

        private class Count
        {
            public int nCountO { get; set; }
            public int nCountX { get; set; }
        }
    }
}
