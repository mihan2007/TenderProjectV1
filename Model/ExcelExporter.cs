using ClosedXML.Excel;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TenderProject
{
    public static class ExcelExporter
    {
        public static void ExportToExcel(MainWindow mainWindow, ListBox listBox)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx|All Files|*.*",
                Title = "Выберите место сохранения файла",
                FileName = "exported_data.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                // Создание нового рабочего книги Excel
                using (var workbook = new XLWorkbook())
                {
                    // Добавление листа в книгу
                    var worksheet = workbook.Worksheets.Add("Sheet1");

                    // Заполнение данных
                    worksheet.Cell("A1").Value = "Имя";
                    worksheet.Cell("B1").Value = "Фамилия";
                    worksheet.Cell("A2").Value = "John";
                    worksheet.Cell("B2").Value = "Doe";

                    // Сохранение книги в выбранное место
                    workbook.SaveAs(filePath);
                }

                MessageBox.Show($"Данные экспортированы в Excel-файл: {filePath}", "Успех");
            }
        }

       
    }
}
