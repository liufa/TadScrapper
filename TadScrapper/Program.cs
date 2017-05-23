using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace TadScrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var addressFileLines = File.ReadAllLines(ConfigurationManager.AppSettings["AddressFileName"]);
                var resultFileName = ConfigurationManager.AppSettings["ResultsFileName"];
                if (File.Exists(resultFileName))
                {
                    var filenameParts = resultFileName.Split('.');
                    resultFileName = filenameParts[0] + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + "." + filenameParts[1];
                }

                File.AppendAllLines(resultFileName,
                    new[] {
                                 "Date,Account,Location,City,Owner Name,Use,Market Value $"
                            });

                using (var scrapper = new Scrapper())
                {
                    foreach (var address in addressFileLines.Skip(1))
                    {
                        var tadRecords = scrapper.ReadTadRecord(address.Split(new[] { ',' })[0]).ToList();
                        foreach (var tadRecord in tadRecords)
                        {
                            File.AppendAllLines(resultFileName, new[]
                            {
                                $"{DateTime.Now},{tadRecord.Account},{tadRecord.Location},{tadRecord.City},{tadRecord.OwnerName},{tadRecord.Use},{tadRecord.MarketValue}"
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                File.AppendAllLines("log.txt", new[] { String.Empty, DateTime.Now.ToString(), e.Message, e.StackTrace });
            }
        }
    }
}
