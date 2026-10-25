
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace TARge25Shop.SeleniumTesting
{
    public class KindergartenFrontendTests
    {
        private const string BaseUrl = "https://localhost:7227/Kindergarten";

        /// <summary>
        /// Kontrollib, kas uut Kindergarten rühma saab luua
        /// ja kas sisestatud andmed salvestatakse õigesti.
        /// </summary>
        [Fact]
        public void Should_CreateKindergarten_WithCorrectData()
        {
            // Ülesseade
            IWebDriver driver = new FirefoxDriver();
            string groupName = "Mesilased_" + Guid.NewGuid().ToString("N").Substring(0, 8);

            try
            {
                driver.Url = BaseUrl;

                // Tegevus
                driver.FindElement(By.Id("CreateInIndexKindergarten")).Click();

                driver.FindElement(By.Id("GroupName")).SendKeys(groupName);

                IWebElement children = driver.FindElement(By.Id("ChildrenCount"));
                children.Clear();
                children.SendKeys("20");

                driver.FindElement(By.Id("KindergartenName")).SendKeys("Tapa Lasteaed");
                driver.FindElement(By.Id("TeacherName")).SendKeys("Mari");
                driver.FindElement(By.Id("CU_CreateKindergarten")).Click();

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

        /// <summary>
        /// Kontrollib, kas Kindergarten Details lehel
        /// kuvatakse loodud rühma õiged andmed.
        /// </summary>
        [Fact]
        public void Should_ViewKindergarten_Details()
        {
            // Ülesseade
            IWebDriver driver = new FirefoxDriver();
            string groupName = "Mesilased_" + Guid.NewGuid().ToString("N").Substring(0, 8);

            try
            {
                driver.Url = BaseUrl;

                // Tegevus
                driver.FindElement(By.Id("CreateInIndexKindergarten")).Click();

                driver.FindElement(By.Id("GroupName")).SendKeys(groupName);

                IWebElement children = driver.FindElement(By.Id("ChildrenCount"));
                children.Clear();
                children.SendKeys("20");

                driver.FindElement(By.Id("KindergartenName")).SendKeys("Tapa Lasteaed");
                driver.FindElement(By.Id("TeacherName")).SendKeys("Mari");
                driver.FindElement(By.Id("CU_CreateKindergarten")).Click();

                IWebElement table = driver.FindElement(By.Id("IndexTableKindergarten"));

                IWebElement row = table.FindElement(
                    By.XPath(".//tr[td[contains(., '" + groupName + "')]]"));

                row.FindElement(By.LinkText("Details")).Click();

                // Kontroll
                Assert.Equal(groupName,
                    driver.FindElement(By.Id("Details_GroupName")).Text);

                Assert.Equal("20",
                    driver.FindElement(By.Id("Details_ChildrenCount")).Text);

                Assert.Equal("Tapa Lasteaed",
                    driver.FindElement(By.Id("Details_KindergartenName")).Text);

                Assert.Equal("Mari",
                    driver.FindElement(By.Id("Details_TeacherName")).Text);
            }
            finally
            {
                driver.Quit();
            }
        }

        /// <summary>
        /// Kontrollib, kas Kindergarten rühma andmeid saab muuta
        /// ja kas muudatused salvestatakse õigesti.
        /// </summary>
        [Fact]
        public void Should_UpdateKindergarten_WithCorrectData()
        {
            // Ülesseade
            IWebDriver driver = new FirefoxDriver();
            string groupName = "Mesilased_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            string updatedName = "Liblikad_" + Guid.NewGuid().ToString("N").Substring(0, 8);

            try
            {
                driver.Url = BaseUrl;

                // Loome uue rühma
                driver.FindElement(By.Id("CreateInIndexKindergarten")).Click();

                driver.FindElement(By.Id("GroupName")).SendKeys(groupName);

                IWebElement children = driver.FindElement(By.Id("ChildrenCount"));
                children.Clear();
                children.SendKeys("20");

                driver.FindElement(By.Id("KindergartenName")).SendKeys("Tapa Lasteaed");
                driver.FindElement(By.Id("TeacherName")).SendKeys("Mari");
                driver.FindElement(By.Id("CU_CreateKindergarten")).Click();

                // Tegevus
                IWebElement table = driver.FindElement(By.Id("IndexTableKindergarten"));

                IWebElement row = table.FindElement(
                    By.XPath(".//tr[td[contains(., '" + groupName + "')]]"));

                row.FindElement(By.LinkText("Update")).Click();

                IWebElement updateGroup = driver.FindElement(By.Id("GroupName"));
                updateGroup.Clear();
                updateGroup.SendKeys(updatedName);

                IWebElement updateTeacher = driver.FindElement(By.Id("TeacherName"));
                updateTeacher.Clear();
                updateTeacher.SendKeys("Anna");

                driver.FindElement(
                    By.CssSelector("input[type='submit'][value='Update']")).Click();

                driver.Navigate().GoToUrl(BaseUrl);

                // Kontroll
                IWebElement updatedTable = driver.FindElement(By.Id("IndexTableKindergarten"));

                IWebElement updatedRow = updatedTable.FindElement(
                    By.XPath(".//tr[td[contains(., '" + updatedName + "')]]"));

                Assert.Contains(updatedName, updatedRow.Text);
                Assert.Contains("Anna", updatedRow.Text);
                Assert.Contains("Tapa Lasteaed", updatedRow.Text);
            }
            finally
            {
                driver.Quit();
            }
        }

        /// <summary>
        /// Kontrollib, kas Kindergarten rühma saab kustutada
        /// ja kas kustutatud rühm kaob nimekirjast.
        /// </summary>
        [Fact]
        public void Should_DeleteKindergarten_WithCorrectData()
        {
            // Ülesseade
            IWebDriver driver = new FirefoxDriver();
            string groupName = "Mesilased_" + Guid.NewGuid().ToString("N").Substring(0, 8);

            try
            {
                driver.Url = BaseUrl;

                // Loome uue rühma
                driver.FindElement(By.Id("CreateInIndexKindergarten")).Click();

                driver.FindElement(By.Id("GroupName")).SendKeys(groupName);

                IWebElement children = driver.FindElement(By.Id("ChildrenCount"));
                children.Clear();
                children.SendKeys("20");

                driver.FindElement(By.Id("KindergartenName")).SendKeys("Tapa Lasteaed");
                driver.FindElement(By.Id("TeacherName")).SendKeys("Mari");
                driver.FindElement(By.Id("CU_CreateKindergarten")).Click();

                // Tegevus
                IWebElement table = driver.FindElement(By.Id("IndexTableKindergarten"));

                IWebElement row = table.FindElement(
                    By.XPath(".//tr[td[contains(., '" + groupName + "')]]"));

                Assert.Contains(groupName, row.Text);

                row.FindElement(By.LinkText("Delete")).Click();

                Assert.Contains("/Kindergarten/Delete", driver.Url,
                    StringComparison.OrdinalIgnoreCase);

                driver.FindElement(
                    By.CssSelector("input[type='submit'][value='Delete Confirmation']")).Click();

                driver.Navigate().GoToUrl(BaseUrl);

                // Kontroll
                IWebElement updatedTable = driver.FindElement(By.Id("IndexTableKindergarten"));

                var deletedRows = updatedTable.FindElements(
                    By.XPath(".//tr[td[contains(., '" + groupName + "')]]"));

                Assert.Empty(deletedRows);
            }
            finally
            {
                driver.Quit();
            }
        }

        /// <summary>
        /// Kontrollib, kas Kindergarten Create leht avaneb
        /// ja kas kõik vajalikud sisestusväljad on olemas.
        /// </summary>
        [Fact]
        public void Should_OpenKindergarten_CreatePage()
        {
            // Ülesseade
            IWebDriver driver = new FirefoxDriver();

            try
            {
                driver.Url = BaseUrl;

                // Tegevus
                driver.FindElement(By.Id("CreateInIndexKindergarten")).Click();

                // Kontroll
                Assert.Contains("/Kindergarten/Create", driver.Url,
                    StringComparison.OrdinalIgnoreCase);

                Assert.True(driver.FindElement(By.Id("GroupName")).Displayed);
                Assert.True(driver.FindElement(By.Id("ChildrenCount")).Displayed);
                Assert.True(driver.FindElement(By.Id("KindergartenName")).Displayed);
                Assert.True(driver.FindElement(By.Id("TeacherName")).Displayed);
            }
            finally
            {
                driver.Quit();
            }
        }

        /// <summary>
        /// Kontrollib, kas Kindergarten Index lehel
        /// kuvatakse tabel ja vajalikud veerud.
        /// </summary>
        [Fact]
        public void Should_DisplayKindergarten_IndexTable()
        {
            // Ülesseade
            IWebDriver driver = new FirefoxDriver();

            try
            {
                driver.Url = BaseUrl;

                // Tegevus
                IWebElement table = driver.FindElement(
                    By.Id("IndexTableKindergarten"));

                // Kontroll
                Assert.True(table.Displayed);

                IWebElement header = table.FindElement(By.TagName("thead"));

                Assert.Contains("Nr", header.Text);
                Assert.Contains("Group", header.Text);
                Assert.Contains("Kindergarten", header.Text);
                Assert.Contains("Teacher", header.Text);
                Assert.Contains("Created", header.Text);

                Assert.True(driver.FindElement(
                    By.Id("CreateInIndexKindergarten")).Displayed);
            }
            finally
            {
                driver.Quit();
            }
        }

        /// <summary>
        /// Kontrollib, kas kasutaja saab Kindergarten Details lehelt
        /// brauseri Back nupuga tagasi Index lehele.
        /// </summary>
        [Fact]
        public void Should_NavigateBackToKindergarten_Index()
        {
            // Ülesseade
            IWebDriver driver = new FirefoxDriver();
            string groupName = "Mesilased_" + Guid.NewGuid().ToString("N").Substring(0, 8);

            try
            {
                driver.Url = BaseUrl;

                // Loome uue rühma
                driver.FindElement(By.Id("CreateInIndexKindergarten")).Click();

                driver.FindElement(By.Id("GroupName")).SendKeys(groupName);

                IWebElement children = driver.FindElement(By.Id("ChildrenCount"));
                children.Clear();
                children.SendKeys("20");

                driver.FindElement(By.Id("KindergartenName")).SendKeys("Tapa Lasteaed");
                driver.FindElement(By.Id("TeacherName")).SendKeys("Mari");
                driver.FindElement(By.Id("CU_CreateKindergarten")).Click();

                // Tegevus
                IWebElement table = driver.FindElement(By.Id("IndexTableKindergarten"));

                IWebElement row = table.FindElement(
                    By.XPath(".//tr[td[contains(., '" + groupName + "')]]"));

                row.FindElement(By.LinkText("Details")).Click();

                Assert.Equal(groupName,
                    driver.FindElement(By.Id("Details_GroupName")).Text);

                driver.Navigate().Back();

                // Kontroll
                IWebElement indexTable = driver.FindElement(
                    By.Id("IndexTableKindergarten"));

                Assert.True(indexTable.Displayed);
                Assert.Contains(groupName, indexTable.Text);
            }
            finally
            {
                driver.Quit();
            }
        }

        /// <summary>
        /// Kontrollib, kas Kindergarten Create lehelt saab
        /// Back to List nupuga tagasi Index lehele.
        /// </summary>
        [Fact]
        public void Should_ReturnToKindergarten_IndexFromCreate()
        {
            // Ülesseade
            IWebDriver driver = new FirefoxDriver();

            try
            {
                driver.Url = BaseUrl;

                // Tegevus
                IWebElement createButton = driver.FindElement(
                    By.Id("CreateInIndexKindergarten"));

                createButton.Click();

                Assert.Contains("/Kindergarten/Create", driver.Url,
                    StringComparison.OrdinalIgnoreCase);

                IWebElement backButton = driver.FindElement(
                    By.LinkText("Back to List"));

                backButton.Click();

                // Kontroll
                IWebElement table = driver.FindElement(
                    By.Id("IndexTableKindergarten"));

                Assert.True(table.Displayed);

                Assert.True(driver.FindElement(
                    By.Id("CreateInIndexKindergarten")).Displayed);
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
