using Crawler.Core;
using Crawler.Engine.Services;
using Crawler.Engine.Services.Excels;
using Crawler.Providers.FacebookCrawler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Presesentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-100);
            //IAgentFactory agentLoader = new FileBasedAgentFactory();
            //var input = new FacebookCrawlerDataInput();
            //input.EndDate = DateTime.Now;
            //input.StartDate = DateTime.Now.AddDays(-365);
            //input.PageUrl = "https://www.facebook.com/grantthorntonspain/";
            //var agent = agentLoader.GetAgent("FacebookCrawlerAgent1");
            //agent.RetrieveData(input);


            var excelFilePath = "../../Sample/ExcelSample.xlsx";
            ExcelWorkbook workbook;
            using (var fileStream = File.OpenRead(excelFilePath))
            {
                workbook = ExcelWorkbook.OpenWorkbook(fileStream);
            }
            IExcelInputReader excelReader = new DefaultExcelInputReader();
            var inputList = excelReader.Read(workbook);
            inputList = inputList.Take(10).ToList();
            var bulkRequest = new BulkRequest();
            inputList.ForEach(x =>
            {
                if (x is FacebookCrawlerDataInput)
                {
                    x.Agent = "DefaultFacebookCrawlerAgent";
                    ((FacebookCrawlerDataInput)x).StartDate = startDate;
                    ((FacebookCrawlerDataInput)x).EndDate = endDate;
                    bulkRequest.Inputs.Add(x);
                }
            });
            IBulkRequestProcessor bulkProcessor = new DefaultBulkRequestProcessor();
            var response = bulkProcessor.Process(bulkRequest);
            Console.ReadLine();
        }
    }
}
