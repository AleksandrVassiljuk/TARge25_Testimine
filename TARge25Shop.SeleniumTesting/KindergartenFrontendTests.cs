using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace TARge25Shop.SeleniumTesting
{
    public class KindergartenFrontendTests
    {
        /// <summary>
        /// Kontrollib, kas uut Kindergarten rühma saab luua
        /// ja kas sisestatud andmed on õigesti salvestatud.
        /// </summary>
        [Fact]
        public void Should_CreateKindergarten_WithCorrectData()
        {
            // Ülesseade
            IWebDriver driver = new FirefoxDriver();
            string groupName = "Mesilased_" + Guid.NewGuid().ToString("N").Substring(0, 8);

            try
            {
                driver.Url = "https://localhost:7227/Kindergarten";

                // Tegevus
                IWebElement createButton = driver.FindElement(By.Id("CreateInIndexKindergarten"));
                createButton.Click();

                IWebElement group = driver.FindElement(By.Id("GroupName"));
                group.SendKeys(groupName);

                IWebElement children = driver.FindElement(By.Id("ChildrenCount"));
                children.Clear();
                children.SendKeys("20");

                IWebElement kindergarten = driver.FindElement(By.Id("KindergartenName"));
                kindergarten.SendKeys("Tapa Lasteaed");

                IWebElement teacher = driver.FindElement(By.Id("TeacherName"));
                teacher.SendKeys("Mari");

                IWebElement saveButton = driver.FindElement(By.Id("CU_CreateKindergarten"));
                saveButton.Click();

                // Kontroll
                IWebElement table = driver.FindElement(By.Id("IndexTableKindergarten"));

                IWebElement row = table.FindElement(
                    By.XPath(".//tr[td[contains(., '" + groupName + "')]]"));

                Assert.Contains(groupName, row.Text);
                Assert.Contains("Tapa Lasteaed", row.Text);
                Assert.Contains("Mari", row.Text);
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
