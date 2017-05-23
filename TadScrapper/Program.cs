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


                }
            }
            catch (Exception e)
            {
                File.AppendAllLines("log.txt", new[] { String.Empty, DateTime.Now.ToString(), e.Message, e.StackTrace });
            }
        }
    }
}
