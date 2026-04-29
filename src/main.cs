/*
 * Automaton: A simple task orchestration tool
 * made by Cortez. Similar to crond in nature
 * but written in C#.
 */

using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Timers;

namespace Automaton
{
    public static class Automaton
    {
        static Action<string> print = (x) => { // Simple Anonymous Function for printing
            Console.WriteLine(x);
        };


        public static void Main()
        {
            var time = new System.Timers.Timer // Our clock/timer
            {
                Interval = 1000,
                Enabled = true
            };

            int secs = 0,limit = 20;

            time.Elapsed += (s, e) => // simple function for the clock to increment our secs for counting how many seconds has elapsed
            {
                secs++;
                print($"S: {secs}"); // logging how much time has passed
            };
            

            time.Start();

            while (true) { // when ijt starts we put a loop so it does not exit prematurely until the desired seconds is done.
            
                if(secs >= limit)
                {
                    time.Stop();
                    break;
                }

            }

            print($"--------------\nTotal Time Elapsed: {secs}"); // this is just a place holder (for now)
            // this will be the entry point for this project

            
        }
    }
}