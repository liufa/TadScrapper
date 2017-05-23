using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TadScrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var addressFileLines = File.ReadAllLines(ConfigurationManager.AppSettings["AddressFileName"]);

                using (var scrapper = new Scrapper())
                {
                    foreach (var address in addressFileLines.Skip(1))
                    {
                        var tadRecords = scrapper.ReadTadRecord(address.Split(new[] { ',' })[0]).ToList();
                        foreach (var tadRecord in tadRecords)
                        {
                            File.AppendAllLines("result.csv", new[]
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
