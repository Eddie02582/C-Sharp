# Thread
   


### No Parameter
假設有一個函數T()

```csharp
Thread thread = new Thread(T);
thread.Start();
```

### With One Parameter
此方法只適用一個參數,且參數型別必須為object,假設函數 T(object n)<br>

使用時只要將參數傳入Start裡面即可
```csharp
Thread thread = new Thread(T);
thread.Start(5);
```

### With One/More Parameter
假設有一個函數T(int n1,int n2)


可以使用以下方法Lambda/delegate/ThreadStart


```csharp
Thread thread = new Thread(() => T(5,8));
thread.Start();
```

```csharp
Thread thread = new Thread(delegate() { T(5,10);});
thread.Start();
```

```csharp
Thread thread = new Thread(new ThreadStart(() => T2(5, 8)));
thread.Start();
```


或者透過class傳遞參數 (不一定constructor)

```csharp
class T
{
    private int n;
    public T(int n){
        this.n = n;
    }
    public void run()
    {
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(i.ToString());
        }
    }
}
```
使用方法如下
```csharp
T t = new T(5)
Thread thread = new Thread(t.run);
thread.Start();
```



