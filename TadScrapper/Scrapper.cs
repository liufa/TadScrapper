using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

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

        public List<TadRecord> ReadAddress(string address)
        {
            var addressesWithPluses = address.Replace(" ", "+");
            this.Driver.Url = $"{this.UriBase}?keyword={addressesWithPluses}&count=2&city=all&DepartmentCd=";
            this.Driver.Navigate();

            return null;
        }

        public void Dispose()
        {
            this.Driver.Close();
        }
    }
}
