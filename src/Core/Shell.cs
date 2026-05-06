using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Automaton.src.Core
{
    static internal class Shell
    {
        
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


    }
}
