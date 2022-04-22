# NLog 

是一款主要用來寫log的package



## Nuget安裝
```
nuget.exe install NLog
```

## 設定NLog.config
```
nuget.exe install NLog.Config
```
安裝後會產生一個NLog.config,將NLog.config放置執行目錄,設定可以<a href = "https://github.com/nlog/nlog/wiki/Configuration-file">參考</a>

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
            //CreateLogger();

            Logger logger = LogManager.GetCurrentClassLogger();
           
            logger.Trace("Trace");
            logger.Debug("Debug");
            logger.Info("Info");
            logger.Warn("Warn");
            logger.Error("Error");
            logger.Fatal("Fatal");
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
      throwExceptions="false"
      internalLogLevel="Off" internalLogFile="c:\temp\nlog-internal.log">

  <variable name="myvar" value="myvalue"/>
  <targets>
    <target xsi:type="File" name="fileTarget" fileName="${basedir}/logs/${shortdate}.log"
            layout="${longdate} ${uppercase:${level}} ${message}"    
            />
    <target name="consoleTarget"
            xsi:type="Console" 
            layout="${longdate} [${uppercase:${level}}] ${message}" />            
  </targets>
  <rules>
        <logger name="*" minlevel="Trace" maxlevel = "Info" writeTo="fileTarget"/> 
        <logger name="*" levels="Info" writeTo="consoleTarget" />
  </rules>
</nlog>

```



## 不使用NLog.Config
也可以不使用NLog.Config


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
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            };

            var consoleTarget = new ConsoleTarget
            {
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            };

            config.AddRule(LogLevel.Trace, LogLevel.Fatal, fileTarget);
            config.AddRule(LogLevel.Trace, LogLevel.Info, consoleTarget);
            LogManager.Configuration = config;
        }
    }
}
```

## NLog with Winform RichTextBox

```
nuget.exe install NLog.Windows.Forms
```

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Windows.Forms;

namespace NLog
{

    public partial class Form1 : Form
    {
        Logger logger = null;
        public Form1()
        {
            InitializeComponent();
            CreateLogger();
            
        }
        
        private static void CreateLogger()
        {
            logger = LogManager.GetCurrentClassLogger();
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget
            {
                FileName = "${basedir}/logs/${shortdate}.log",
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            };


            RichTextBoxTarget target = new RichTextBoxTarget();
            target.Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}";           
            target.ControlName = "richTextBox1";
            target.FormName = "Form1";
            target.UseDefaultRowColoringRules = true;


            config.AddRule(LogLevel.Trace, LogLevel.Fatal, fileTarget);
            config.AddRule(LogLevel.Trace, LogLevel.Info, target);
            LogManager.Configuration = config;
            
                        logger.Trace("Trace");
            logger.Debug("Debug");
            logger.Info("Info");
            logger.Warn("Warn");
            logger.Error("Error");
            logger.Fatal("Fatal");      
        }
    }
  
}
```

