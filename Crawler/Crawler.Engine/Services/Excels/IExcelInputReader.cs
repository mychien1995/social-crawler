using Crawler.Core;
using Crawler.Providers.FacebookCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Engine.Services.Excels
{
    public interface IExcelInputReader
    {
        List<DataInputBase> Read(ExcelWorkbook workbook);
    }

    public class DefaultExcelInputReader : IExcelInputReader
    {
        public List<DataInputBase> Read(ExcelWorkbook workbook)
        {
            var rowStart = 2;
            var result = new List<DataInputBase>();
            var xmlSheets = workbook.GetSheets();
            foreach (var sheet in xmlSheets)
            {
                if (sheet.Worksheet.Visibility == ClosedXML.Excel.XLWorksheetVisibility.Visible)
                {
                    var colTotal = sheet.Worksheet.ColumnsUsed().Count();
                    var rowTotal = sheet.Worksheet.LastRowUsed().RowNumber();
                    for (int i = 0; i < rowTotal - rowStart + 1; i++)
                    {
                        var rowNumber = i + rowStart;
                        var row = sheet.GetRange(rowNumber, 1, rowTotal, colTotal);
                        var accountName = row.Range.Cell(1, 1).Value.ToString();
                        var url = row.Range.Cell(1, 2).Value.ToString();
                        var platForm = row.Range.Cell(1, 3).Value.ToString();
                        var region = row.Range.Cell(1, 4).Value.ToString();
                        var owner = row.Range.Cell(1, 5).Value.ToString();
                        if (!IsValidUrl(url)) continue;
                        if (platForm.Equals(SocialPlatform.Facebook, StringComparison.OrdinalIgnoreCase))
                        {
                            var facebookInput = new FacebookCrawlerDataInput();
                            facebookInput.AccountName = accountName;
                            facebookInput.Platform = platForm;
                            facebookInput.Region = region;
                            facebookInput.Owner = owner;
                            facebookInput.PageUrl = url;
                            result.Add(facebookInput);
                        }
                    }
                }
            }
            return result;
        }

        private bool IsValidUrl(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult);
        }
    }
}
