using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace foobar4423
{
    public static class Utility
    {
        /// <summary>
        /// foobarのウィンドウタイトルを取得
        /// </summary>
        /// <returns></returns>
        public static string FoobarWindowTitle(string filePath, string processName = "foobar2000")
        {
            return Process.GetProcessesByName(processName)
                          .Where(p => p.MainModule.FileName == filePath)
                          .Select(p => p.MainWindowTitle)
                          .SingleOrDefault();
        }


        /// <summary>
        /// 連続する半角空白文字を一つの空白文字に集約します。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveContinuousWhiteSpace(string str)
        {
            int whitePos = 0;
            while ((whitePos = str.IndexOf("  ")) != -1)
            {
                str = str.Replace("  ", " ");
            }
            return str;
        }

    }
}