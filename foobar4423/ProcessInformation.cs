using System;
using System.Diagnostics;

namespace foobar4423
{
    class ProcessInformation
    {

        /// <summary>
        /// foobarのタイトルを取得
        /// </summary>
        /// <returns></returns>
        internal string GetFoobarTitle(string filePath)
        {
            string windowTitle = string.Empty;

            //全てのプロセスを列挙
            //→ プロセス名=foobar2000 のみ列挙に変更(β)
            foreach (Process p in Process.GetProcessesByName("foobar2000"))
            {
                try
                {
                    if (p.MainModule.FileName == filePath)
                    {
                        if ((windowTitle = p.MainWindowTitle) != string.Empty)
                        {
                            return p.MainWindowTitle;
                        }

                        break;
                    }
                }
                catch(Exception ex){
                    //何もしないんだよよ
                }
            }

            return "not found";
        }
    }
}


