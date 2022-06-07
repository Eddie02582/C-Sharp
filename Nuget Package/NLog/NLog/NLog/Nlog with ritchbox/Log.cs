using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Windows.Forms;
using System.Threading;

namespace Nlog_with_ritchbox
{
    class Log
    {

        public Log(string logpath)
        {
            this.logpath = logpath;
            CreateLogger();

        }

        private string logpath;
        private Logger _logger = null;


        public Logger logger
        {
            get
            {
                return _logger;
            }
        }


        private void CreateLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
            string date = DateTime.Now.ToString("yyyy_MM_dd HH_mm_ss");

            RichTextBoxTarget target = new RichTextBoxTarget();
            target.Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}";
            target.ControlName = "richTextBox1";
            //target.ControlName = "richTextBox1";
            target.FormName = "Form1";
            target.UseDefaultRowColoringRules = true;

            var fileTarget = new FileTarget
            {
                FileName = logpath + "\\log.log",
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            };

            var config = new LoggingConfiguration();
            config.AddRule(LogLevel.Info, LogLevel.Fatal, target);  
            config.AddRule(LogLevel.Info, LogLevel.Fatal, fileTarget);
            LogManager.Configuration = config;

        }
    }
}
