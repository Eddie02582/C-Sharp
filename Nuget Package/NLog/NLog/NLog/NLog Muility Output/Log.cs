using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Threading;

namespace NLog_Muility_Output
{
    class Log
    {

        public Log(string logpath,string name)
        {
            this.logpath = logpath;
            this.name = name;
            _logger = LogManager.GetLogger(name); ;
            CreateConfig();

        }

        private string logpath;
        private string name;
        private Logger _logger = null;
        private LoggingConfiguration config = new LoggingConfiguration();

        public Logger logger
        {
            get
            {
                return _logger;
            }
        }

      
        private void CreateConfig()
        {
            _logger = LogManager.GetLogger(name); ;
            string date = DateTime.Now.ToString("yyyy_MM_dd HH_mm_ss");       

            var logFileTarget = new FileTarget
            {               
                //使用string format會有問題
                //FileName = string.Format("{0}\\{1}_log.log",logpath,name),   
                FileName = logpath + "\\" + name + "_log.log",
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            };

            var debugFileTarget = new FileTarget
            {              
                FileName = string.Format("{0}\\{1}_debug.log", logpath, name),      
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            };

            var consoleTarget = new ConsoleTarget
            {
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss}[${logger}] [${uppercase:${level}}] ${message}",   
            };
          
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logFileTarget);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, debugFileTarget);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, consoleTarget);  

            //LogManager.Configuration = config;   
        }

        //因為LogManager.Configuration為static 所以共用,當輸出時需要再設定一次
        public void Debug(string message)
        {
            LogManager.Configuration = config;
            _logger.Debug(message);
        }

        public void Info(string message)
        {
            LogManager.Configuration = config;
            _logger.Info(message);
        }
    }





}
