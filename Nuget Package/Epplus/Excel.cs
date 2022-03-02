using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using System.IO;

namespace ExcelNamespace
{
    class Excel
    {
        ExcelPackage ep;
        ExcelWorksheet worksheet;

        public Excel(string excelName)
        {
            bool bFileExit = File.Exists(excelName);    
            FileInfo fi = new FileInfo(excelName);
            ep = new ExcelPackage(fi);

            if (!bFileExit)
            {
                ep.Workbook.Worksheets.Add("sheet1");
                ep.Save();¡@
            }            
        }

        public void setWorksheet(int sheetNunber)
        {
            worksheet = getSheet(sheetNunber);
        }
        public void setWorksheet(string worksheetName)
        {
            worksheet = getSheet(worksheetName);
        }



        public ExcelWorksheet getSheet(int sheetNunber)
        {
            if (sheetNunber == 0 || sheetNunber > ep.Workbook.Worksheets.Count)
            {
                throw new IndexOutOfRangeException("index out of range");
            }           
            return ep.Workbook.Worksheets[sheetNunber];
        }
        
        public ExcelWorksheet getSheet(string worksheetName)
        {
            ExcelWorksheet sheet = ep.Workbook.Worksheets[worksheetName];
            if (sheet == null)
            {
                throw new Exception("worksheet Name not found");
            }
            return sheet;
        }
		
		public static int columnNameToNumber(string columnName)
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

        public static string getExcelColumnName(int columnNumber)
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




        public bool Csv2Excel(string csvFilePath, string worksheetsName, bool bheader, string Loaction, OfficeOpenXml.Table.TableStyles TableStyle= OfficeOpenXml.Table.TableStyles.None)
        {
            try
            {
                bool firstRowIsHeader = false;
                var excelTextFormat = new ExcelTextFormat();
                excelTextFormat.Delimiter = ',';
                excelTextFormat.EOL = "\r";
                var csvFileInfo = new FileInfo(csvFilePath);

                ExcelWorksheet worksheet = ep.Workbook.Worksheets.Add(worksheetsName);
                worksheet.Cells["A1"].LoadFromText(csvFileInfo, excelTextFormat, TableStyle, firstRowIsHeader);
            }
            catch
            {
                return false;
            }
            return true;
        }  

        public Object getCellValue(int row, int col)
        {
            var value = worksheet.Cells[row, col].Value;
            return value = value == null ? "" : value;
        }

        public void writeCellValue(int row, int col,object value)
        {
            worksheet.Cells[row, col].Value = value;
        }


        public void writeCellFormula(int row, int col, string Formula)
        {
            worksheet.Cells[row, col].Formula = Formula;
        }

        public bool sheetAdd(string sheetName)
        {
            if (ep.Workbook.Worksheets[sheetName] == null )
            {
                ep.Workbook.Worksheets.Add(sheetName);
                return true;
            }           
            return false;
        }


        public void MergeCell(int RowStart, int ColStart, int RowEnd, int ColEnd)
        {
            var range = worksheet.Cells[RowStart, ColStart, RowEnd, ColEnd];
            range.Merge = true; 
        }

        public void FreezePanes(int row,int col)
        {
            worksheet.View.FreezePanes(row, col);
        }

        public void FilterRange(int RowStart, int ColStart, int RowEnd, int ColEnd)
        {
            var range = worksheet.Cells[RowStart, ColStart, RowEnd, ColEnd];
            range.AutoFilter = true;           
        }

        public void BorderRange(int RowStart, int ColStart, int RowEnd, int ColEnd, OfficeOpenXml.Style.ExcelBorderStyle style = OfficeOpenXml.Style.ExcelBorderStyle.Thin)
        {
            var range = worksheet.Cells[RowStart, ColStart, RowEnd, ColEnd];
            range.Style.Border.BorderAround(style);
        }

        public void FillRange(int RowStart, int ColStart, int RowEnd, int ColEnd,System.Drawing.Color color)
        {
            var range = worksheet.Cells[RowStart, ColStart, RowEnd, ColEnd];
            range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            range.Style.Fill.BackgroundColor.SetColor(color);
        }   

        public void Save()
        {
            ep.Save();
        }
       
       
          
    }
	
}