using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Threading;

namespace TadScrapper
{
    public class Scrapper : IDisposable
    {
        public IWebDriver Driver;

        public Uri UriBase;

        public Scrapper()
        {
            this.Driver = new ChromeDriver();
            this.UriBase = new Uri(ConfigurationManager.AppSettings["UriBase"]);
        }

        public IEnumerable<TadRecord> ReadTadRecord(string address)
        {
            var addressesWithPluses = address.Replace(" ", "+");
            this.Driver.Url = $"{this.UriBase}?keyword={addressesWithPluses}&count=2&city=all&DepartmentCd=";
            this.Driver.Navigate();
            Thread.Sleep(1000);

            var rows = this.Driver.FindElements(By.CssSelector(".desktop-ver .tablelong tr"));

            foreach (var row in rows.Skip(1))
            {
                var cells = row.FindElements(By.CssSelector("td"));
                yield return new TadRecord
                {
                    Account = cells[0].Text,
                    City = cells[2].Text,
                    Location = cells[1].Text,
                    MarketValue = int.Parse(cells[5].Text.Trim(new[] { '$' }).Replace(",", string.Empty)),
                    OwnerName = cells[3].Text,
                    Use = cells[4].Text
                };
            }
        }

        public void Dispose()
        {
            this.Driver.Close();
        }
    }
}
