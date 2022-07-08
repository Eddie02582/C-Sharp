# Struct vs Class




## Struct
<ul>
    <li>是一種實質型別（Value Type）</li>
    <li>存放在記憶體的Stack區。</li>
    <li>不需要使用new就可以產生一份新的Struct。</li>
    <li>欄位必須全部賦值，才能被使用</li>
    <li>不能有空參數的constructor 和destructor，若要寫constructor，則所有欄位必須全部賦值</li>
    <li>不能被繼承,所以不能使用abstract</li>
</ul>


### 基本架構
```csharp
public struct Coordinate
{
    public double x;
    public double y;

    public Coordinate(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public Coordinate(double x)
    {
        //必須賦值給y
        this.x = x;
        this.y = 5;
    }
}
```


### 建立object
```csharp

Coordinate p1 = new Coordinate() { x = 2, y = 5 };

Coordinate p2;
p.x = 2;
p.y = 5;  

Coordinate p3 = new Coordinate(7, 8);
Coordinate p4 =  new Coordinate(8);
```


## Class
<ul>
    <li>是一種參考型別（Reference Type）</li>
    <li>存放在記憶體的Heap區</li>
    <li>不需要使用new就可以產生一份新的Struct。</li>
    <li>欄位不必須全部賦值</li>
    <li>欄位可以使用初始化賦值</li>
</ul>


### 基本架構

```csharp
public class Coordinate
{
    public double x;
    public double y = 5;
    public Coordinate()
    {
       
    }
    public Coordinate(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
    public Coordinate(double x)
    {
        this.x = x;       
    }
}
```

### 建立object
這邊注意建立其他建構式,需再建立一個空的才能使用new Coordinate() { x = 2, y = 5 }

```csharp
Coordinate p1 = new Coordinate() { x = 2, y = 5 };

Coordinate p2 new Coordinate();
p.x = 2;
p.y = 5;  

Coordinate p3 = new Coordinate(7, 8);
Coordinate p4 =  new Coordinate(8);
```

















