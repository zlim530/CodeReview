using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DailyLocalCode
{
    public class NetCoreExcel
    {
        public string ProcessName { get; set; }

        public string MachineNumber { get; set; }

        public string ProcessNo { get; set; }

        public string WorkOrder { get; set; }

        public string Model { get; set; }

        public string PrintDateTime { get; set; }

        public string Remark { get; set; }
    }

    public class ImportExcelInput
    {
        public IFormFile ExcelFile { get; set; }
    }

    public class ExcelDemoClass
    {
        [HttpPost]
        public List<NetCoreExcel> Import([FromForm] ImportExcelInput input)
        {
            var list = new List<NetCoreExcel>();

            using (var package = new ExcelPackage(input.ExcelFile.OpenReadStream()))
            {
                var sheet = package.Workbook.Worksheets.First();

                int startRowNumber = sheet.Dimension.Start.Row + 1;
                int endRowNumber = sheet.Dimension.End.Row;
                int startColumn = sheet.Dimension.Start.Column;
                int endColumn = sheet.Dimension.End.Column;

                //循环获取整个 Excel 数据表数据
                for (int currentRow = startRowNumber; currentRow <= endRowNumber; currentRow++)
                {
                    list.Add(new NetCoreExcel
                    { 
                        ProcessName = sheet.Cells[currentRow,1].Text,
                        MachineNumber = sheet.Cells[currentRow,1].Text,
                        ProcessNo = sheet.Cells[currentRow,1].Text,
                        WorkOrder = sheet.Cells[currentRow,1].Text,
                        Model = sheet.Cells[currentRow,1].Text,
                        PrintDateTime = sheet.Cells[currentRow,1].Text,
                        Remark = sheet.Cells[currentRow,1].Text,
                    });
                }
            }

            return list;
        }


        public async Task<string> Export()
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("sheet1");

            var headers = new string[] { "ProcessName","MachineNumber","ProcessNo","WorkOrder","Model","PrintDateTime","Remark"};
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = headers[i];
                worksheet.Cells[1, i + 1].Style.Font.Bold = true;
            }

            var list = new List<NetCoreExcel>();
            for (int i = 0; i <= 10; i++)
            {
                list.Add(new NetCoreExcel
                {
                    ProcessName = $"A{i}",
                    MachineNumber = $"B{i}",
                    ProcessNo = $"C{i}",
                    WorkOrder = $"D{i}",
                    Model = $"E{i}",
                    PrintDateTime = $"F{i}",
                    Remark = $"G{i}",
                });
            }

            int row = 2;
            foreach (var item in list)
            {
                worksheet.Cells[row, 1].Value = item.ProcessName;
                worksheet.Cells[row, 2].Value = item.MachineNumber;
                worksheet.Cells[row, 3].Value = item.ProcessNo;
                worksheet.Cells[row, 4].Value = item.WorkOrder;
                worksheet.Cells[row, 5].Value = item.Model;
                worksheet.Cells[row, 6].Value = item.PrintDateTime;
                worksheet.Cells[row, 7].Value = item.Remark;

                row++;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), $"excel.xlsx");
            //await package.GetAsByteArray().DownloadAsync(path);
            return path;
        }
    }
}