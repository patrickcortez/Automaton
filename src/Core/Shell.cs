using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Automaton.src.Core
{
    static internal class Shell
    {

        static public async Task<ConsoleKeyInfo> ReadKey()
        {
            return await Task.Run(() => Console.ReadKey(true));
        }
        
        static public T Prompt<T>(string text = "")
        {
            try
            {
                Console.Write(text);
                object input = Console.ReadLine();
                return (T)Convert.ChangeType(input, typeof(T));
            }catch(InvalidCastException)
            {
                throw new InvalidCastException($"Cannot convert data!");
            }
        }

        static public void PrintBanner()
        {
            string banner = @"
                _                        _              
     /\        | |                      | |             
    /  \  _   _| |_ ___  _ __ ___   __ _| |_ ___  _ __  
   / /\ \| | | | __/ _ \| '_ ` _ \ / _` | __/ _ \| '_ \ 
  / ____ \ |_| | || (_) | | | | | | (_| | || (_) | | | |
 /_/    \_\__,_|\__\___/|_| |_| |_|\__,_|\__\___/|_| |_|
                                                        
            ";
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(banner);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Type 'help' for commands!\n");
        }


    }
}
