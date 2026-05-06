/*
 * Automaton: A simple task orchestration tool
 * made by Cortez. Similar to crond in nature
 * but written in C#.
 */

using NetCoreAudio;
using static Automaton.src.Core.Shell;
using static Automaton.src.Utils.Utility;
using Automaton.src.Core;
using System.Runtime.CompilerServices;

//Shit to do: Get rid of the Shell
// Add a dedicated scripting engine for Automaton
// For now a shell will do.

namespace Automaton // Runs .bat and .ps1 files for automation,
{
    public static class Automaton
    {
        static List<Chore> Chores = new List<Chore>();
        static Action<string,bool> print = (x,y) => { // Simple Anonymous Function for printing
            

            if (!y)
            {
                Console.WriteLine(x);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {x}");
                Console.ForegroundColor = ConsoleColor.White;

            }
        };

        readonly static string AssetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
        


        static DateTime SetConfigs() // setting time: year, month, day, hour and minute
        {
            List<int> x = new List<int>();
            DateTime ntime;

            print("Enter target time(Military Time): [Year] [Month] [Day] [Hour] [Minute]",false);


            string ans = Console.ReadLine().ToString();


            x.AddRange(ans.Split(' ').Select(int.Parse));


            ntime = new DateTime(x[0], x[1], x[2], x[3], x[4], x[5]);
            return ntime;
        }

        private static void CheckChore()
        {
            foreach(Chore c in Chores)
            {
                if(c.time >= DateTime.Now)
                {
                    c.procRun();
                }

                continue;
            }
        }

        public static async Task Main()
        {

            
            var play = new Player();
            play.SetVolume(100);

            if(!File.Exists(Path.Combine(AssetPath, "beep.mp3")))
            {
                print("File: beep.mp3 does not exist!",true);
            }

            var time = new System.Timers.Timer // Our clock/timer
            {
                Interval = 1000,
                Enabled = true
            };

            int secs = 0;

            time.Elapsed += (s, e) => // simple function for the clock to increment our secs for counting how many seconds has elapsed
            {
                secs++;
                //print($"S: {secs}",false); // logging how much time has passed
            };

            bool ran = false,ran2 = false; // to stop over printing because of tick speed


            

            string final;

            

            while (true) { // when ijt starts we put a loop so it does not exit prematurely until the desired seconds is done.
               string input = Prompt<string>($"\e[41mAutomaton>\e[0m");
                string[] inputs = Tokenize(input,' ');
                string cmd = inputs[0].ToLower();
                


                if (cmd == "add") // add a new task to be executed: add <filename> <path> <args> <time-to-be-executed>
                {
                    string args = string.Join(' ',inputs.Skip(3));
                    Chores.Add(new Chore(inputs[2],SetConfigs(),args)); // process name, path and arguments
                } else if (cmd == "run")
                {
                    time.Start();
                    Debug("Automaton started!");
                } else if(cmd == "exit")
                {
                    print("Goodbye!",false);
                    Environment.Exit(0);
                }
                else
                {
                    print($"Command: {cmd} is not a command!",true);
                }


                CheckChore();
            }

            
        }
    }
}