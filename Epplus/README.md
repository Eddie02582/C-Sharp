# Epplus 

好用的讀寫Excel 程式

##　讀取excel
```csharp
var fileName = "888.xlsx";
var file = new FileInfo(fileName);
using (var excel = new ExcelPackage(file))
{    
   

}
```

##　建立excel
與讀取方式相同,但需要需要新增sheet,否則會暴錯


```csharp
var fileName = "888.xlsx";
var file = new FileInfo(fileName);
using (var excel = new ExcelPackage(file))
{    
    excel.Workbook.Worksheets.Add("sheet1");
    excel.Save();　

}
```
亦可以下列方式使用儲
```csharp
    Byte[] bin = excel.GetAsByteArray();
    File.WriteAllBytes(fileName, bin);
```

## excel 儲存格操作

### 取得sheet

```csharp
 
   ExcelWorksheet sheet = excel.Workbook.Worksheets[1]; //取得第一個分頁，注意起始為置從1開始     
   ExcelWorksheet sheet = ep.Workbook.Worksheets["MySheet"] //取得sheet名字為MySheet  
``` 

### 取得sheet row col 範圍
```csharp
int startRowNumber = header+1;//起始列編號，從1算起
int endRowNumber = worksheet.Dimension.End.Row;//結束列編號，從1算起
int startColumn = worksheet.Dimension.Start.Column;//開始欄編號，從1算起
int endColumn = worksheet.Dimension.End.Column;//結束欄編號，從1算起
  ```
### 寫値

```csharp 
   sheet.Cells[1, 2].Value = "Hellow";  //寫値在 row = 1 ,col = 2  
　 sheet.Cells["B1"].Value = "Hellow"  

   sheet.Cells[1, 1, 2, 3].Value = "測試"; // [rowStart,colStart,rowEnd,colEnd]
   sheet.Cells["A1:J3"].Value = "測試";
   
   var numbers = new List<object> { 1.1, 2.2, 3.3 };
   sheet.Cells["B1:D1"].LoadFromArrays(new List<object[]> {numbers.ToArray()}); ／／在B1-D1 填入[ 1.1, 2.2, 3.3]
   
   var colours = new object[] { "red", "green", "blue" };
   sheet.Cells["B1:D2"].LoadFromArrays(new List<object[]> { numbers.ToArray(), colours });
``` 

### 讀値

```csharp 
   var value = sheet.Cells[1, 2].Value";  //讀値在 row = 1 ,col = 2

   value = sheet.Cells[1, 1, 1, 10].Value; 
   value = sheet.Cells["B1:D2"].Value;　
``` 

### 合併儲存格


```csharp 
   var range = worksheet.Cells[RowStart, ColStart, RowEnd, ColEnd];
   range.Merge = true; 
``` 
### 凍結視窗

```csharp 
  worksheet.View.FreezePanes(row, col);
``` 
### 公式

```csharp 
  worksheet.Cells[row, col].Formula = Formula;
``` 
### Filter

```csharp 
  worksheet.Cells[RowStart, ColStart, RowEnd, ColEnd].AutoFilter =true
``` 

### 外框

```csharp 
  OfficeOpenXml.Style.ExcelBorderStyle style = OfficeOpenXml.Style.ExcelBorderStyle.Thin  
  worksheet.Cells[RowStart, ColStart, RowEnd, ColEnd].Style.Border.BorderAround(style)
``` 

####樣式設定
```csharp 
sheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; 
sheet.Cells.Style.Font.Name = "Arial";
sheet.Cells.Style.Font.Size = 8;
```
### 填色

```csharp 
   var range = worksheet.Cells[RowStart, ColStart, RowEnd, ColEnd];
   range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
   range.Style.Fill.BackgroundColor.SetColor(color);
``` 

### simple example　
```csharp
  var fileName = "888.xlsx";
  var file = new FileInfo(fileName);
  using (var excel = new ExcelPackage(file))
  {                
      excel.Workbook.Worksheets.Add("sheet1");
      ExcelWorksheet sheet = excel.Workbook.Worksheets[1]; //注意起始為置從1開始
      sheet.Cells[1, 2].Value = "Hellow";  //在 row = 1 ,col = 2 寫?
      var value = sheet.Cells[1, 2].Value; //在 row = 1 ,col = 2 讀?
      Console.WriteLine(value);
      excel.Save();

  }
```     

## 其它好用的

### ColumnNameToNumber
```csharp
public static int ColumnNameToNumber(string columnName)
{
    columnName = columnName.ToUpper();
    int sum = 0;
    for (int i=0 ;i<columnName.Length;i++)
    {
        sum *= 26;
        sum += ( columnName[i]) - 'A' + 1;
    }
    return sum;
}
``` 

### ColumnNameToNumber
```csharp
public static string GetExcelColumnName(int columnNumber)
{
    int dividend = columnNumber;
    string columnName = String.Empty;
    int modulo;

    while (dividend > 0)
    {
        modulo = (dividend - 1) % 26;
        columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
        dividend = (int)((dividend - modulo) / 26);
    }

    return columnName;
}
```













       
            
            
            
            
            
            
            
            