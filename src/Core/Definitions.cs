using System.Diagnostics;
using static Automaton.src.Utils.Utility;

namespace Automaton.src.Core
{
    internal struct Chore
    {
        public readonly string Filepath;
        public readonly string args;
        public readonly DateTime time;
        private Process proc;

        public Chore(string xFilePath,DateTime xtime,string xargs)
        {
            Filepath = xFilePath;
            time = xtime;
            args = xargs;
            proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = xFilePath,
                    Arguments = xargs
                }
            };
        }

        public int procRun()
        {
            if (proc.Start())
            {
                proc.WaitForExit();
                proc.Dispose();

                return 0;
            }

            return 1;
        }

        
    };
}
