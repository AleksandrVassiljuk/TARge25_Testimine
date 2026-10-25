
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

                IWebElement table = driver.FindElement(By.Id("IndexTableKindergarten"));

                IWebElement row = table.FindElement(
                    By.XPath(".//tr[td[contains(., '" + groupName + "')]]"));

                IWebElement detailsButton = row.FindElement(By.LinkText("Details"));
                detailsButton.Click();

                // Kontroll
                IWebElement detailsGroup = driver.FindElement(By.Id("Details_GroupName"));
                Assert.Equal(groupName, detailsGroup.Text);

                IWebElement detailsChildren = driver.FindElement(By.Id("Details_ChildrenCount"));
                Assert.Equal("20", detailsChildren.Text);

                IWebElement detailsKindergarten = driver.FindElement(By.Id("Details_KindergartenName"));
                Assert.Equal("Tapa Lasteaed", detailsKindergarten.Text);

                IWebElement detailsTeacher = driver.FindElement(By.Id("Details_TeacherName"));
                Assert.Equal("Mari", detailsTeacher.Text);
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
                driver.Url = "https://localhost:7227/Kindergarten";

                // Loome uue rühma
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

                // Tegevus
                IWebElement table = driver.FindElement(By.Id("IndexTableKindergarten"));

                IWebElement row = table.FindElement(
                    By.XPath(".//tr[td[contains(., '" + groupName + "')]]"));

                IWebElement updateButton = row.FindElement(By.LinkText("Update"));
                updateButton.Click();

                IWebElement updateGroup = driver.FindElement(By.Id("GroupName"));
                updateGroup.Clear();
                updateGroup.SendKeys(updatedName);

                IWebElement updateTeacher = driver.FindElement(By.Id("TeacherName"));
                updateTeacher.Clear();
                updateTeacher.SendKeys("Anna");

                IWebElement updateSaveButton = driver.FindElement(
                    By.CssSelector("input[type='submit'][value='Update']"));

                updateSaveButton.Click();

                driver.Navigate().GoToUrl("https://localhost:7227/Kindergarten");

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
                driver.Url = "https://localhost:7227/Kindergarten";

                // Loome uue rühma
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

                // Tegevus
                IWebElement table = driver.FindElement(By.Id("IndexTableKindergarten"));

                IWebElement row = table.FindElement(
                    By.XPath(".//tr[td[contains(., '" + groupName + "')]]"));

                Assert.Contains(groupName, row.Text);

                IWebElement deleteButton = row.FindElement(By.LinkText("Delete"));
                deleteButton.Click();

                Assert.Contains("/Kindergarten/Delete", driver.Url,
                    StringComparison.OrdinalIgnoreCase);

                IWebElement confirmButton = driver.FindElement(
                    By.CssSelector("input[type='submit'][value='Delete Confirmation']"));

                confirmButton.Click();

                driver.Navigate().GoToUrl("https://localhost:7227/Kindergarten");

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
                driver.Url = "https://localhost:7227/Kindergarten";

                // Tegevus
                IWebElement createButton = driver.FindElement(
                    By.Id("CreateInIndexKindergarten"));

                createButton.Click();

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
    }
}
