using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLog_Muility_Output
{
    class Program
    {
        static void Main(string[] args)
        {
            Log log1 = new Log("log", "output1");
            Log log2 = new Log("log", "output2");
            log1.Debug("output1 => 1");
            log2.Debug("output2 => ttt");
            log1.Info("output1 => Hellow");
            log2.Info("output2 =>2");
            log1.Debug("output1 => World");
            log2.logger.Info("output2 =>3");
            log1.Debug("output1 => ttt");

        }
    }
}
