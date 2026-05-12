/*
 * Automaton: A simple task orchestration tool
 * made by Cortez. Similar to crond in nature
 * but written in C#.
 */

using Automaton.src.Core;
using NetCoreAudio;
using static Automaton.src.Core.Shell;
using static Automaton.src.Utils.Utility;
using static Automaton.src.Core.IO.IOLayer;
using System.Reflection.Metadata.Ecma335;

//Shit to do: Get rid of the Shell
// Add a dedicated scripting engine for Automaton
// For now a shell will do.

namespace Automaton // Runs .bat and .ps1 files for automation,
{
    
    public static class Automaton
    {
        static List<Chore> Chores = new List<Chore>();
        static string PowshPath = "C:\\WINDOWS\\System32\\WindowsPowerShell\\v1.0\\powershell.exe";
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
        static bool powsOK = false;

        readonly static string AssetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
        


        static DateTime SetConfigs() // setting time: year, month, day, hour and minute
        {
            List<int> x = new List<int>();
            DateTime ntime;

            print("Enter target time(Military Time): [Year] [Month] [Day] [Hour] [Minute]",false);


            string ans = Console.ReadLine().ToString();


            x.AddRange(ans.Split(' ').Select(int.Parse));


            ntime = new DateTime(x[0], x[1], x[2], x[3], x[4], 0);
            return ntime;
        }

        static void Help()
        {
            print("\nAutomaton Commands:",false);
            print("add <scriptpath> <time to exec: year month day hour minute second>",false);
            print("run", false);
            print("pause", false);
            print("check", false);
            print("exit\n", false);
        }

        static void initShell()
        {
            if (checkPowerShell("powershell"))
            {
                print("PowerShell: OK", false);
                powsOK = true;
            }
            else
            {
                print("PowerShell: OFFLINE", false);
            }
        }

        static void timecheck(int x)
        {

            string outp = $@"
               Current Time: {DateTime.Now}
               Seconds Elapsed: {x}
            ";

            print(outp, false);
        }

        private static void CheckChore()
        {
            if(Chores.Count < 1)
            {
                Debug("There are no chores!",true);
            }

            foreach(Chore c in Chores)
            {
                if(DateTime.Now >= c.time)
                {
                  if(c.procRun().Equals(0))
                    {
                        print("Task Run Successfully!", false);
                    }
                    else
                    {
                        print("Task failed to run!", true);
                    }
                }

                continue;
            }
        }

        public static void Main() // Main entry point
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
                Enabled = false
            };

            int secs = 0;

            time.Elapsed += (s, e) => // simple function for the clock to increment our secs for counting how many seconds has elapsed
            {
                secs++;
                //print($"S: {secs}",false); // logging how much time has passed
            };

            bool ran = false;

            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true; // we Disable Sigint
                //Console.WriteLine(Environment.NewLine);
                
            };

            initShell();
            PrintBanner();
            while (true) { // when ijt starts we put a loop so it does not exit prematurely until the desired seconds is done.

                string? input = Prompt<string>($"\n\e[41mAutomaton>\e[0m");
             //   ConsoleKeyInfo key = await ReadKey();

               // WhichCTRL(key);

                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                if(string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                string[] inputs = Tokenize(input,' ');
                string cmd = inputs[0].ToLower();
                


                if (cmd == "add") // add a new task to be executed: add <script-path> <args> <time-to-be-executed>
                {
                    if(inputs.Count() < 2)
                    {
                        print("No Scripts added!", true);
                        print("Usage: add <script-path>", false);
                        continue;
                    }

                    string args = string.Join(' ',inputs.Skip(1));
                    Chores.Add(new Chore(PowshPath,SetConfigs(),args)); // process name, path and arguments
                } else if (cmd == "run")
                {
                    if (!ran)
                    {
                        time.Start();
                        Debug("Automaton started!");
                        ran = true;
                    }
                    else
                    {
                        Debug("Automaton Already Ran!");
                    }

                } else if(cmd == "exit")
                {
                    print("Goodbye!",false);
                    Environment.Exit(0);
                } else if(cmd == "help")
                {
                    Help();
                } else if (cmd.Equals("check"))
                {
                    timecheck(secs);
                } else if (cmd.Equals("pause"))
                {

                    if (!ran)
                        print("Automaton hasn't started yet!", true);
                    else
                        time.Stop();
                }else if (cmd.Equals("chsh"))
                {
                    if(inputs.Count() < 2)
                    {
                        print("No shell name!", true);
                        continue;
                    }

                    if (!powsOK)
                    {
                        PowshPath = inputs[1];
                        if (checkShell(PowshPath))
                            print($"Shell changed to {PowshPath}",false);
                        else
                        {
                            PowshPath = "C:\\WINDOWS\\System32\\WindowsPowerShell\\v1.0\\powershell.exe";
                        }
                    }
                    else
                    {
                        print("PowerShell is ok, No need to change shell",true);
                    }

                }
                else
                {
                    print($"{cmd} is not a command!",true);
                }

                if((secs%60).Equals(0) && secs > 0)
                {
                    print("A minute has passed!", false);
                }

                if (ran)
                {
                    CheckChore();
                }
            }

            
        }
    }
}