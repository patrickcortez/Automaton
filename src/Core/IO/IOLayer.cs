using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace Automaton.src.Core.IO
{
    internal static class IOLayer // DO not use for now, this shit is unstable.
    {
        static StringBuilder buffer = new StringBuilder();
        static int curpos;
        public static string? ReadLine(string prompt)
        {
            curpos = 0;

            Console.Write(prompt);
            while (true)
            {
                ConsoleKeyInfo key;
                try
                {
                    key = Console.ReadKey(true);
                }
                catch
                {
                    return null;
                }

                if(key.Key == ConsoleKey.Backspace)
                {
                    HandleBackSpace();
                    continue;
                }

                if(key.Key == ConsoleKey.Enter)
                {
                    buffer.Insert(curpos, '\n');
                    curpos++;
                    if (Console.KeyAvailable)
                    {

                        while (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo nextkey = Console.ReadKey(true);
                            if (nextkey.Key == ConsoleKey.Enter)
                            {
                                buffer.Insert(curpos,'\n');
                                curpos++;
                            }else if (!char.IsControl(nextkey.KeyChar))
                            {
                                buffer.Insert(curpos, nextkey.KeyChar);
                                curpos++;
                            }

                        }
                    }

                    Console.WriteLine();
                    return buffer.ToString();
                }

                if ((key.Modifiers & ConsoleModifiers.Control) != 0)
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.C:
                            Console.WriteLine("\n");
                            Console.Out.Flush();
                            return "";
                        default:
                            Console.Error.WriteLine("CTRL not handled!");
                            return "";


                    }

 
                }

                if (!char.IsControl(key.KeyChar))
                {
                    HandleChar(key.KeyChar);
                }
            }
        }

        private static void DrawLine()
        {
            StringBuilder sb = buffer;

            Console.Write(sb.ToString());
        }

        private static void HandleChar(char c)
        {
            if (curpos == buffer.Length)
            {
                buffer.Append(c);
                curpos++;
            }
            else
            {
                buffer.Insert(curpos, c);
                curpos++;
            }

            DrawLine();
        }

        private static void HandleBackSpace()
        {
            if (curpos == 0)
                return;

            curpos--;
            buffer.Remove(curpos, 1);
            DrawLine();
        }
    }
}
