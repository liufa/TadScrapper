using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TadScrapper
{
    public class Scrapper : IDisposable
    {
        public IWebDriver Driver;

        public Scrapper()
        {
            this.Driver = new ChromeDriver();
        }



        public void Dispose()
        {
            this.Driver.Close();
        }
    }
}
