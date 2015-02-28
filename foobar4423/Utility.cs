using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace foobar4423
{
    public static class Utility
    {
        /// <summary>
        /// foobarのウィンドウタイトルを取得します。
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


        /// <summary>
        /// この文字列に、指定された文字列が存在するかどうかを返します。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static public bool IsFind(this string target, string value)
        {
            return target.IndexOf(value) != -1;
        }
    }
}