using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Hubs;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace SignalRDemo.Helpers;

public class ImportExecutor
{
    private readonly IHubContext<ImportHub> hubContext;

    public ImportExecutor(IHubContext<ImportHub> hubContext)
    {
        this.hubContext = hubContext;
    }

    public async Task ExecuteAsync(string connectionId)
    {
        string[] lines = await File.ReadAllLinesAsync($"dicLocalFilePath");
        int totalCount = lines.Length - 1;// 总行数，跳过表头
        string connStr = "Server=127.0.0.1;Database=demoone;User ID=sa;Pwd=q1w2e3R4;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;";
        int counter = 0;
        using SqlBulkCopy bulkCopy = new SqlBulkCopy(connStr);
        bulkCopy.DestinationTableName = "T_WordItems";
        bulkCopy.ColumnMappings.Add("Word", "Word");
        bulkCopy.ColumnMappings.Add("Phonetic", "Phonetic");
        bulkCopy.ColumnMappings.Add("Definition", "Definition");
        bulkCopy.ColumnMappings.Add("Translation", "Translation");

        using DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Word");
        dataTable.Columns.Add("Phonetic");
        dataTable.Columns.Add("Definition");
        dataTable.Columns.Add("Translation");

        foreach (string line in lines)
        {
            string[] strs = line.Split(',');
            string word = strs[0];
            string? Phonetic = strs[1];
            string? Definition = strs[2];
            string? Translation = strs[3];

            DataRow row = dataTable.NewRow();
            row["Word"] = word;
            row["Phonetic"] = Phonetic;
            row["Definition"] = Definition;
            row["Translation"] = Translation;
            dataTable.Rows.Add(row);
            counter++;
            // 每100条批量提交一次
            if (dataTable.Rows.Count == 100)
            {
                await bulkCopy.WriteToServerAsync(dataTable);
                dataTable.Clear();
                await hubContext.Clients.Client(connectionId).SendAsync("ImportProgess", totalCount, counter);
                Console.WriteLine($"已经导入了{counter}条");
            }
        }


        await bulkCopy.WriteToServerAsync(dataTable);
    }
}