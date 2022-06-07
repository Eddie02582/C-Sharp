using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using NLog.Targets;
using NLog.Config;

namespace NLog_with_config
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = LogManager.GetCurrentClassLogger();           
            logger.Trace("Trace");
            logger.Debug("Debug");
            logger.Info("Info");
            logger.Warn("Warn");
            logger.Error("Error");
            logger.Fatal("Fatal");

            logger.Warn("Hello World");
            logger.Warn("Example 1");
            logger.Debug("log1");
        }
    }
}
