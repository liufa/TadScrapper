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
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            this.Driver = new ChromeDriver(chromeDriverService, new ChromeOptions());
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
                    Account = cells[0].Text?.Trim(),
                    City = cells[2].Text?.Trim(),
                    Location = cells[1].Text?.Trim(),
                    MarketValue = int.Parse(cells[5].Text.Trim(new[] { '$' }).Replace(",", string.Empty)),
                    OwnerName = cells[3].Text?.Trim(),
                    Use = cells[4].Text?.Trim()
                };
            }
        }

        public void Dispose()
        {
            //this.Driver.Close();
            //this.Driver.Quit();
            this.Driver.Dispose();
        }
    }
}
