using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Threading;

namespace NLog_Muility_Output_Factory_
{
    class Log
    {
        public Log(string logpath,string name)
        {
            this.logpath = logpath;
            this.name = name;
            CreateLogger(); 
        }

        private string logpath;
        private Logger _logger = null;
        private string name = "";
        private string logName = "log.log";  
        public  Logger logger
        {
            get
            {
                return _logger;
            }
        }
        
        private void CreateLogger()
        {     

            var fileTarget = new FileTarget
            {
                FileName = logpath + "\\" + name + "_log.log",
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            };

            var debugfileTarget = new FileTarget
            {
                FileName = string.Format("{0}\\{1}_debug.log", logpath, name), 
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            };

            var consoleTarget = new ConsoleTarget();
            if(name != "")
            {
                consoleTarget.Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss}[${logger}] [${uppercase:${level}}] ${message}";
            }
            else{
                consoleTarget.Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss}[${uppercase:${level}}] ${message}";
            }       

            var config = new LoggingConfiguration();         
            config.AddRule(LogLevel.Info, LogLevel.Fatal, fileTarget);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, debugfileTarget);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, consoleTarget);  

            LogFactory Factory1 = new LogFactory(config);
            _logger = Factory1.GetLogger(name);
            //or use
            //_logger = Factory1.GetCurrentClassLogger();
        
            
        }

       

      
    }





}
