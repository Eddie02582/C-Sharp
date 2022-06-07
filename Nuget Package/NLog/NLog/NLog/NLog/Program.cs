using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using NLog.Targets;
using NLog.Config;
namespace NLog
{
    class Program
    {      
        static void Main(string[] args)
        {
            //Logger logger = LogManager.GetCurrentClassLogger();
            //or
            Logger logger = LogManager.GetLogger("Example");
            CreateLogger();
            logger.Trace("Trace");
            logger.Debug("Debug");
            logger.Info("Info");
            logger.Warn("Warn");
            logger.Error("Error");
            logger.Fatal("Fatal");
            logger.Info(" Hello World 123");
            
            Console.ReadLine();
        }

        private static void CreateLogger()
        {
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget
            {
                FileName = "${basedir}/logs/${shortdate}.log",
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}][Test] ${message}",
            };

            var consoleTarget = new ColoredConsoleTarget
            {
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",             
            };

            consoleTarget.WordHighlightingRules.Add(
               new ConsoleWordHighlightingRule("Hello World",
                   ConsoleOutputColor.NoChange,
                   ConsoleOutputColor.DarkGreen));

            consoleTarget.RowHighlightingRules.Add(
                    new ConsoleRowHighlightingRule("level == LogLevel.Warn", ConsoleOutputColor.Yellow, ConsoleOutputColor.NoChange)
                );


            //設定config level
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, fileTarget);
            config.AddRule(LogLevel.Trace, LogLevel.Warn, consoleTarget);
            LogManager.Configuration = config;
        }
    }
}
