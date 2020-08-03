using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;

using _exc = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace AutoShop.api.Excel
{
    public class ExcelHandler
    {
        class VAL {
           public string title;
        }
        private _Application excelApp;
        private Workbook excelBook;
        private Worksheet excelSheet;
        public ExcelHandler() 
        {
            excelApp = new _exc.Application();
        }
        public string LoadPrice(string path) 
        {
            try
            {
                excelBook = excelApp.Workbooks.Open(path);
                excelSheet = excelBook.Worksheets[1];
            }
            catch (Exception er)
            { 
                return er.Message; 
            }

            int rows = excelSheet.Cells[excelSheet.Rows.Count, "A"].End[XlDirection.xlUp].Row;
            Range Rng = (Range)excelSheet.Range["A1", excelSheet.Cells[rows, 3]];
            var dataArr = (object[,])Rng.Value;
            List<VAL> result = new List<VAL>();
            for (int i = 2; i < rows; i++) 
            {
                VAL newItem;
                string dataName = dataArr[i, 2].ToString();
                if ((newItem = result.Where(x => x.title == dataName).FirstOrDefault()) == null) 
                {
                    newItem = new VAL();
                    newItem.title = dataName;
                    result.Add(newItem);
                }
            }
            string shopPriceString = string.Join("/", result.Select(x => x.title).ToArray());
            string currentShopItemsPath = "";
            try
            {
                currentShopItemsPath = System.AppDomain.CurrentDomain.BaseDirectory + "ShopPrice\\ShopItems.txt";
                using (FileStream fstream = new FileStream(currentShopItemsPath, FileMode.OpenOrCreate))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(shopPriceString);
                    fstream.Write(array, 0, array.Length);
                }
            }
            catch (Exception er) 
            {
                return er.Message + $"|{currentShopItemsPath}|";
            }
            return "";
        }
    }
}