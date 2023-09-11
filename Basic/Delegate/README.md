# Delegate
<ul>
　<li>Delegate:最通用</li>
　<li>Action:無回傳值</li>
　<li>Func:有回傳值</li>
　<li>Predicate:回傳boolean</li>
</ul>

簡單來說Action/Func/Predicate 為Delegate的特定使用情況<br>

分別針對以下情況

### 無回傳值,無參數

這時候可以使用delegate/Action
#### Delegate
宣告deleage
```csharp
    delegate void WithoutParaReturnVoid();
```
假設有個function Show(),回傳void

```csharp
 WithoutParaReturnVoid d1 = Show;
 d1();
```
也可透過lambda/delegate
```csharp
    WithoutParaReturnVoid d2 = delegate() { Console.WriteLine("123"); };
    WithoutParaReturnVoid d3 = (() =>  Console.WriteLine("123") );
    d2();
    d3();
```

#### Action
```
    Action a1 = Show;
    Action a2 = delegate() { Console.WriteLine("123"); };
    Action a3 = (() => Console.WriteLine("123")); 
``` 

### 無回傳值,有參數
這時候可以使用delegate/Action<br>
函數如下
```csharp
    public static void Show(string message)
    {
        Console.WriteLine(message);
    }
```

#### Delegate
```csharp
    WithParaReturnVoid d1 = Show;
    d1("hellow");
```

#### Action
```csharp
    Action<string> a1 = Show;           
    Action<string> a2 = delegate(string s) { Console.WriteLine(s); };
    Action<string> a3 = ((s) => Console.WriteLine(s));
    Action<string> a4 = new Action<string>(Show);
```





