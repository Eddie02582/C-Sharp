# TCP
這邊介紹兩種方式一種是用TcpListener,另一種是用Socket 建立TCP連線

## Server

### TcpListener

```csharp
using System.Net;
using System.Net.Sockets;

public static void Server1()
{    
    string ip = "127.0.0.1";
    int iPort = 3600;

    IPAddress IPA = IPAddress.Parse(ip);
    TcpListener tcplistener = new TcpListener(IPA, iPort);
    tcplistener.Start();

    Socket newsock = null;
    byte[] myBufferBytes;
    int bufferSize;
    int dataLength;
    newsock = tcplistener.AcceptSocket();
    while (true)
    {                
        if (newsock.Connected)
        {
            //取得receive buffersize 
            bufferSize = newsock.ReceiveBufferSize;
            myBufferBytes = new byte[bufferSize];
            // 接收資料 
            dataLength = newsock.Receive(myBufferBytes);
            //將byte to string 
            string strRecieve = Encoding.ASCII.GetString(myBufferBytes, 0, dataLength);
            Console.WriteLine(string.Format("讀取訊息:{0}", strRecieve));

            // 發送訊息            
            myBufferBytes = Encoding.ASCII.GetBytes("OK");
            newsock.Send(myBufferBytes, myBufferBytes.Length, 0);                  
        }
    }
}
```

### Socket
傳輸與接受與TcpListener一樣差別在連線寫法
```csharp
public static void Server3()
{
   
    string ip = "127.0.0.1";
    int iPort = 3600;
    IPAddress IPA = IPAddress.Parse(ip);
    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);  //建立Socket 連線  
    IPEndPoint ipep = new IPEndPoint(IPA, iPort);
    socket.Bind(ipep);//建立連線
    socket.Listen(10);

    Socket newsock = null;
    newsock = socket.Accept();

    byte[] myBufferBytes;
    int bufferSize;
    int dataLength;

    while (true)
    {  
        if (newsock.Connected)
        {
            //取得receive buffersize 
            bufferSize = newsock.ReceiveBufferSize;
            myBufferBytes = new byte[bufferSize];
            // 接收資料 
            dataLength = newsock.Receive(myBufferBytes);

            //將byte to string 
            string strRecieve = Encoding.ASCII.GetString(myBufferBytes, 0, dataLength);
            Console.WriteLine(string.Format("讀取訊息:{0}", strRecieve));

            myBufferBytes = Encoding.ASCII.GetBytes("OK");

            newsock.Send(myBufferBytes, myBufferBytes.Length, 0);
        }
    }

}

```

也可以透過直接接收傳送字串
```csharp
    NetworkStream network = new NetworkStream(newsock);
    StreamReader reader = new StreamReader(network);
    StreamWriter writer = new StreamWriter(network);
    
    writer.WriteLine(strSendData);
    writer.Flush();   

    strRecieve = reader.ReadLine();    
    
```



## Client

### TCPClient
   

```csharp
    public static void Client1()
    {
        string ip = "127.0.0.1";
        int iPort = 3600;
        TcpClient tcpclient = new TcpClient(ip, iPort);            
        NetworkStream network = tcpclient.GetStream();

        Byte[] myBytes;
        int bufferSize;
        byte[] myBufferBytes;
        string strSendData = "";

        while (true)
        {
            strSendData = Console.ReadLine();
            myBytes = Encoding.ASCII.GetBytes(strSendData); //將字串轉成byte
            network.Write(myBytes, 0, myBytes.Length);      //寫入訊息                
            Console.WriteLine(string.Format("寫入訊息:{0}",strSendData));

            // 從網路資料流讀取資料
            bufferSize = tcpclient.ReceiveBufferSize; //取得buffer 長度
            myBufferBytes = new byte[bufferSize];
            int dataLength = network.Read(myBufferBytes, 0, bufferSize); //讀取
            
            string strRecieve = Encoding.ASCII.GetString(myBufferBytes, 0, dataLength);
            Console.WriteLine(string.Format("讀訊息:{0}", strRecieve));
           
        }

    }
```           
      
      
### Sockets
```csharp
using System.Net;
using System.Net.Sockets;
using System.IO;
public static void Client()
{
    string ip = "127.0.0.1";
    int iPort = 3600;
    IPAddress IPA = IPAddress.Parse(ip);
    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);  //建立Socket 連線  
    socket.Connect(ip, iPort);
    Byte[] myBytes;
    int bufferSize;
    byte[] myBufferBytes;
    string strSendData = "";


    while (true)
    {
        if (socket.Connected)
        {

            strSendData = Console.ReadLine();
            myBytes = Encoding.ASCII.GetBytes(strSendData); //將字串轉成byte
            socket.Send(myBytes, 0, myBytes.Length,0);      //寫入訊息

            Console.WriteLine(string.Format("寫入訊息:{0}", strSendData));


            //bufferSize = socket.ReceiveBufferSize;
            bufferSize = 1024;
            myBufferBytes = new byte[bufferSize];
            int dataLength = socket.Receive(myBufferBytes, bufferSize,0);

            string strRecieve = Encoding.ASCII.GetString(myBufferBytes, 0, dataLength);
            Console.WriteLine(string.Format("讀訊息:{0}", strRecieve));
        }

    }

}
```


## 接收檔案

### 接收方

```csharp

    int receivedBytesLen = 0;
    int recieve_data_size = 0;
    int fileNameLen = 0;
    string fileName = "";
    int first = 1;                  
    byte[] clientData = new byte[1024 * 5000];
    //5000 = 5MB 50000 = 50MB //定義傳輸每段資料大小，值越大傳越快  
    //byte[] clientData = new byte[8192];  
    string receivedPath = "D:/";
    BinaryWriter bWrite = null;
    MemoryStream ms = null;
    string file_type = "";
    string display_data = "";
    string content = "";
    double cal_size = 0;
 do
 {
     receivedBytesLen = Sockets.Receive(clientData);
     //接收資料 (receivedBytesLen = 資料長度)  

     if (first == 1) //第一筆資料為檔名  
     {
         fileNameLen = BitConverter.ToInt32(clientData, 0);
         //轉換檔名的位元組為整數 (檔名長度)  
         fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);
         // 1 int = 4 byte  轉換Byte為字串  
         file_type = fileName.Substring(fileName.Length - 3, 3);
         //取得檔名  
         //-----------  
         content = Encoding.ASCII.GetString(clientData, 4 +
         fileNameLen, receivedBytesLen - 4 - fileNameLen);
         //取得檔案內容 起始(檔名以後) 長度(扣除檔名長度)  
         display_data += content;
         //-----------  
         bWrite = new BinaryWriter(File.Open(receivedPath +
         fileName, FileMode.Create));
         //CREATE 覆蓋舊檔 APPEND 延續舊檔  
         ms = new MemoryStream();
         bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 -
         fileNameLen);
         //寫入資料 ，跳過起始檔名長度，接收長度減掉檔名長度  
         ms.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 -
         fileNameLen);
         //寫入資料 ，呈現於BITMAP用  
     }
     else //第二筆接收為資料  
     {
         //-----------  
         content = Encoding.ASCII.GetString(clientData, 0,
         receivedBytesLen);
         display_data += content;
         //-----------  
         bWrite.Write(clientData/*, 4 + fileNameLen, receivedBytesLen - 4 -
     fileNameLen*/, 0, receivedBytesLen);
         //每筆接收起始 0 結束為當次Receive長度  
         ms.Write(clientData, 0, receivedBytesLen);
         //寫入資料 ，呈現於BITMAP用  
     }
     recieve_data_size += receivedBytesLen;
     //計算資料每筆資料長度並累加，後面可以輸出總值看是否有完整接收  
     cal_size = recieve_data_size;
     cal_size /= 1024;
     cal_size = Math.Round(cal_size, 2);
     first++;
     Thread.Sleep(10); //每次接收不能太快，否則會資料遺失  
 } while (Sockets.Available != 0); //如果還沒接收完則繼續接收  
 bWrite.Close();
 Console.WriteLine("Recieve File:" + fileName);
 Console.WriteLine("SavePath" + receivedPath);

```
### 傳輸方

```csharp
 
 string sReplace = sCommand.Replace("\\", ",");
 string[] sArray = Regex.Split(sReplace, ",");
 string fileName = sArray[sArray.Count() - 1];
 string filePath = sCommand.Replace(fileName, "");   
 byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
 byte[] fileData = File.ReadAllBytes(filePath + fileName);
 byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
 byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length); 
 fileNameLen.CopyTo(clientData, 0);
 fileNameByte.CopyTo(clientData, 4);
 fileData.CopyTo(clientData, 4 + fileNameByte.Length);
 socket.Send(clientData);   

```


            
            