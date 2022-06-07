# NLog 

是一款主要用來寫log的package



## Nuget安裝
```
nuget.exe install NLog
```

## 使用NLog.config
```
nuget.exe install NLog.Config
```
安裝後會產生一個NLog.config,將NLog.config放置執行目錄,可以參考
<a href = "https://github.com/nlog/nlog/wiki/Configuration-file">Link1</a>
<a href = "https://github.com/NLog/NLog/tree/dev/examples/targets/Configuration%20File">Link2</a>



原始內容如下,主要分兩部分
<ul>
    <li>targets</li>
    <li>rules</li>    
</ul>

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      xsi:schemaLocation="http://www.nlog-project.org/schemas/NLog.xsd NLog.xsd"
      autoReload="true"
      throwExceptions="false"
      internalLogLevel="Off" internalLogFile="c:\temp\nlog-internal.log">

  <!-- optional, add some variables
  https://github.com/nlog/NLog/wiki/Configuration-file#variables
  -->
  <variable name="myvar" value="myvalue"/>

  <!--
  See https://github.com/nlog/nlog/wiki/Configuration-file
  for information on customizing logging rules and outputs.
   -->
  <targets>

    <!--
    add your targets here
    See https://github.com/nlog/NLog/wiki/Targets for possible targets.
    See https://github.com/nlog/NLog/wiki/Layout-Renderers for the possible layout renderers.
    -->

    <!--
    Write events to a file with the date in the filename.
    <target xsi:type="File" name="f" fileName="${basedir}/logs/${shortdate}.log"
            layout="${longdate} ${uppercase:${level}} ${message}" />
    -->
  </targets>

  <rules>
    <!-- add your logging rules here -->

    <!--
    Write all events with minimal level of Debug (So Debug, Info, Warn, Error and Fatal, but not Trace)  to "f"
    <logger name="*" minlevel="Debug" writeTo="f" />
    -->
  </rules>
</nlog>
```

### targets
用來寫各種輸出目標,主要有幾樣參數
<ul>
    <li>xsi:type:格式(File/Console/Network/ColoredConsole...)</li>
    <li>name:target name</li>  
    <li>fileName:輸出檔名(type 為File使用)</li>  
    <li>layout:log的顯示格式</li>  
    <li>keepFileOpen:檔案是否持續開啟</li>  
    <li>concurrentWrites:是否續寫</li>      
</ul>

還有幾個變數可以用
<ul>
    <li>${basedir}</li>
    <li>${shortdate}</li>  
    <li>${longdate}</li>  
    <li>${level}</li>  
    <li>${message}</li> 
</ul>


輸出.log的範例
```xml
<targets>
    <target xsi:type="File"
            name="fileTarget"
            fileName="${basedir}/logs/${shortdate}.log"
            layout="${longdate} [${uppercase:${level}}] ${message}"
</targets>
```

輸出到console
```xml
  <targets>
    ...
    <target name="consoleTarget"
            xsi:type="Console" 
            layout="${longdate} [${uppercase:${level}}] ${message}" />
</targets>
```

使用ColoredConsole
```xml
  <targets>
    ...
    <target name="consoleTarget"
            xsi:type="ColoredConsole" 
            layout="${longdate} [${uppercase:${level}}] ${message}" />
</targets>
```

可以自訂顏色
```xml
    <target name="consoleTarget"
            xsi:type="ColoredConsole" 
            layout="${longdate} [${uppercase:${level}}] ${message}">  
        <highlight-word foregroundColor="Green" regex="Hello World"/>
        <highlight-row condition="level == LogLevel.Info" foregroundColor="NoChange" />
        <highlight-row condition="level == LogLevel.Warn" foregroundColor="Yellow" />
        <highlight-row condition="level == LogLevel.Error" foregroundColor="NoChange" backgroundColor="DarkRed" />  
    </target>  
```
### Ruels
用來設定輸出規則

<ul>    
    <li>name:target name</li>  
    <li>minlevel:minimal level to log</li>  
    <li>maxlevel:maximum level to log</li>  
    <li>level:single level to log</li>  
    <li>levels:comma separated list of levels to log</li>  
    <li>writeTo:comma separated list of targets to write to</li>    
</ul>

輸出層級由低到高如下
<ul>
    <li>Trace</li>
    <li>Debug</li>
    <li>Info</li>
    <li>Warn</li>
    <li>Error</li>
    <li>Fatal</li>
</ul>



設定最小輸出level
```xml
<logger name="*" minlevel="Trace" writeTo="fileTarget"/>
```
設定輸出區間

```xml
<logger name="*" minlevel="Trace" maxlevel = "Info" writeTo="fileTarget"/>   
```
指定輸出level
```xml
<logger name="*" level="Debug" writeTo="consoleTarget" />  
```
指定輸出levels
```xml
 <logger name="*" levels="Debug,Trace" writeTo="consoleTarget" /> 
```


## 使用
```csharp
using System.Text;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace NLog
{
    class Program
    {
        private static void Main(string[] args)
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
```

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      xsi:schemaLocation="http://www.nlog-project.org/schemas/NLog.xsd NLog.xsd"
      autoReload="true"
      throwExceptions="false">

  <variable name="myvar" value="myvalue"/> 
  <targets>
  
    <target name="fileTarget"  xsi:type="File" fileName="${basedir}/logs/${shortdate}.log"
            layout="${longdate} ${uppercase:${level}} ${message}"/>
     
    <target name="consoleTarget" xsi:type="ColoredConsole" layout="${date:format=yyyy-MM-dd HH\:mm\:ss} [${uppercase:${level}}] ${message}">  
        <highlight-word foregroundColor="Green" regex="Hello World"/>   
        <highlight-word text="log" backgroundColor="DarkGreen" />       
        <highlight-row condition="level == LogLevel.Info" foregroundColor="NoChange" />
        <highlight-row condition="level == LogLevel.Warn" foregroundColor="Yellow" />
        <highlight-row condition="level == LogLevel.Error" foregroundColor="NoChange" backgroundColor="DarkRed" />  
    </target>    
  </targets>

  <rules>
        <logger name="*" minlevel="Trace" maxlevel = "Info" writeTo="fileTarget"/> 
        <logger name="*" minlevel="Trace" writeTo="consoleTarget" />
  </rules>
</nlog>
```



## 在程式裡面設定

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace NLog
{
    class Program
    {
        private static void Main(string[] args)
        {
            CreateLogger();
            Logger logger = LogManager.GetCurrentClassLogger();           
            logger.Trace("Trace");
            logger.Debug("Debug");
            logger.Info("Info");
            logger.Warn("Warn");
            logger.Error("Error");
            logger.Fatal("Fatal");       
            
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
```

## NLog with Winform RichTextBox

```
nuget.exe install NLog.Windows.Forms
```

這邊注意初始化log 不能放在Form1
```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nlog_with_ritchbox
{
    public partial class Form1 : Form
    {
        Log log =null;
        public Form1()
        {
            InitializeComponent();            
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            log.logger.Trace("trace log message");
            log.logger.Debug("debug log message");
            log.logger.Info("info log message");
            log.logger.Warn("warn log message");
            log.logger.Error("error log message");
            log.logger.Fatal("fatal log message");
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            log = new Log("");

        }
    }
   
}

```

Log.cs

```csharp
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
```


## NLog with Muility Output
這邊想將特定log 輸出到特定log,但是由於LogManager.Configuration屬於靜態的,一種方法可以將包起來每次輸出時在設定

Log.cs
```csharp
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
            var consoleTarget = new ConsoleTarget
            {
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss}[${logger}] [${uppercase:${level}}] ${message}",   
            };          
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logFileTarget);       
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, consoleTarget);  
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
```

而真正上使用Factory 

Log.cs
```csharp
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

            var consoleTarget = new ConsoleTarget();
            //用來區分console輸出格式
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
```

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLog_Muility_Output_Factory_
{
    class Program
    {
        static void Main(string[] args)
        {
            Log log1 = new Log("log", "output1");
            Log log2 = new Log("log", "output2");
            log1.logger.Debug("output1 => 1");
            log2.logger.Debug("output2 => ttt");
            log1.logger.Info("output1 => Hellow");
            log2.logger.Info("output2 =>2");
            log1.logger.Debug("output1 => World");
            log2.logger.Info("output2 =>3");
            log1.logger.Debug("output1 => ttt");

        }
    }
}
```










