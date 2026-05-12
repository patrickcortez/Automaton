using System;
using System.Collections.Generic;
using System.Diagnostics;
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

#nullable enable
        public static void WhichCTRL(ConsoleKeyInfo key)
        {
            if (char.IsControl(key.KeyChar))
            {
                switch(key.Key){
                    case ConsoleKey.D:
                        Console.WriteLine("CTRL D");
                        break;

                    default:
                        Console.WriteLine($"{key.KeyChar} is not a control");
                        break;
                }
            }
        }

        private static bool isEmpty(string data)
        {
            return data != string.Empty;
        }

        public static bool checkPowerShell(string path)
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = path,
                    Arguments = "--version",
                    UseShellExecute = true,
                    
                }
            };

           return proc.Start();

        }

        public static bool checkShell(string name)
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = name,
                    Arguments = "--version",
                    UseShellExecute = true
                }
            };

            return proc.Start();
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
