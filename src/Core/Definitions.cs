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
                StartInfo = new ProcessStartInfo(Filepath,args)
            };
        }

        public void procRun()
        {
            if (proc.Start())
            {
                proc.WaitForExit();
                proc.Dispose();
            }
               
        }

        
    };
}
