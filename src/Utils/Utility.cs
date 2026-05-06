using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Automaton.src.Utils
{
    static internal class Utility
    {
        public static void Debug(string msg,bool isError = false)
        {
            if (isError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {msg}");
                Console.ForegroundColor = default;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"> {msg}");
                Console.ForegroundColor = default;
            }
        }

        private static bool isEmpty(string data)
        {
            return data != string.Empty;
        }

        public static string[] Tokenize(string data,char seperator)
        {
            StringBuilder str = new StringBuilder();
            List<string> nList = new List<string>();

            foreach(char c in data)
            {
                if(c == seperator)
                {
                    nList.Add(str.ToString());
                    str.Clear();
                    continue;
                }



                str.Append(c);
            }

            if(str.Length >= 1)
            {
                nList.Add(str.ToString());
            }

            return nList.ToArray();
        }
    }
}
