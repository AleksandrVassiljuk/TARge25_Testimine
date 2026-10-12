
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace TARge25Shop.SeleniumTesting
{
    public class SpaceshipFrontendTests
    {
        [Fact]
        public void Should_NavigateToCreate_AddSpaceShip_WithCorrectData_ReturnToIndex()
        {
            //ülesseade
            IWebDriver driver = SetupAndNavigateSIndex();

            //tegevus
            IWebElement createInIndex = driver.FindElement(By.Id("CreateInIndex"));
            createInIndex.Click();

            //sisestatavad andmed
            InsertSpaceShipData(driver);

            IWebElement cu_CreateSpaceship = driver.FindElement(By.Id("CU_CreateSpaceship"));
            cu_CreateSpaceship.Click();

            //arvuti tudub
            Thread.Sleep(1000);

            //andmete kogumine pärast reloadi
            IWebElement indexNameSpaceship = driver.FindElement(By.Id("IndexNameSpaceship"));
            var spaceShipNameData = indexNameSpaceship.Text;

            IWebElement indexTypeSpaceship = driver.FindElement(By.Id("IndexTypeSpaceship"));
            var spaceShipTypeData = indexTypeSpaceship.Text;

            IWebElement indexCrewSpaceship = driver.FindElement(By.Id("IndexCrewSpaceship"));
            var spaceShipCrewData = indexCrewSpaceship.Text;

            //kontroll
            Assert.Equal("i add name for spaceship", spaceShipNameData);
            Assert.Equal("i add ship type for spaceship", spaceShipTypeData);
            Assert.Equal("12345", spaceShipCrewData);

            driver.Quit();
        }

        [Fact]
        public void Should_NavigateToDetails_OfASpaceShip_WithPreviouslyCorrectData()
        {
            //ülesseade
            IWebDriver driver = SetupAndNavigateSIndex();

            //otsime üles kõik tabeli read
            var table = driver.FindElement(By.Id("IndexTable"));
            List<IWebElement> rows = table.FindElements(By.TagName("tr")).ToList();
            rows.RemoveAt(0);

            foreach (var row in rows)
            {
                var locatedelement = row.FindElement(By.Id("IndexNameSpaceship"));

                if (locatedelement.Text == "i add name for spaceship")
                {
                    var rowelement = row.FindElement(By.Id("IndexActionsSpaceship"));
                    var button = rowelement.FindElement(By.Id("IndexSpaceshipDetails"));
                    button.Click();

                    Thread.Sleep(500);
                    break;
                }
            }

            //andmete kogumine pärast reloadi
            IWebElement details_SpaceshipId =
                driver.FindElement(By.Id("Details_SpaceshipId"));
            var details_id = details_SpaceshipId.Text;

            IWebElement details_SpaceshipName =
                driver.FindElement(By.Id("Details_SpaceshipName"));
            var details_name = details_SpaceshipName.Text;

            IWebElement details_SpaceshipType =
                driver.FindElement(By.Id("Details_SpaceshipType"));
            var details_type = details_SpaceshipType.Text;

            IWebElement details_SpaceshipCrew =
                driver.FindElement(By.Id("Details_SpaceshipCrew"));
            var details_crew = details_SpaceshipCrew.Text;

            IWebElement details_SpaceshipPower =
                driver.FindElement(By.Id("Details_SpaceshipPower"));
            var details_power = details_SpaceshipPower.Text;

            //kontroll
            Assert.NotNull(details_id);
            Assert.Equal("i add name for spaceship", details_name);
            Assert.Equal("i add ship type for spaceship", details_type);
            Assert.Equal("12345", details_crew);
            Assert.Equal("666667", details_power);

            driver.Quit();
        }

        [Fact]
        public void Should_UpdateDetails_OfASpaceShip_WithNewData()
        {
            //ülesseade
            IWebDriver driver = SetupAndNavigateSIndex();

            //otsime üles kõik tabeli read
            var table = driver.FindElement(By.Id("IndexTable"));
            List<IWebElement> rows = table.FindElements(By.TagName("tr")).ToList();
            rows.RemoveAt(0);

            foreach (var row in rows)
            {
                var locatedelement = row.FindElement(By.Id("IndexNameSpaceship"));

                if (locatedelement.Text == "i add name for spaceship")
                {
                    var rowelement = row.FindElement(By.Id("IndexActionsSpaceship"));
                    var button = rowelement.FindElement(By.Id("IndexSpaceshipUpdate"));
                    button.Click();

                    Thread.Sleep(500);
                    break;
                }
            }

            //sisestatavad andmed
            InsertSpaceShipData(driver, true);

            IWebElement cu_CreateSpaceship =
                driver.FindElement(By.Id("CU_CreateSpaceship"));
            cu_CreateSpaceship.Click();

            Thread.Sleep(1000);

            driver.Quit();
        }

        private static IWebDriver SetupAndNavigateSIndex()
        {
            //firefoxi draiver
            IWebDriver driver = new FirefoxDriver();

            //aadress
            driver.Url = "https://localhost:7227/";

            //navigeerimine Spaceship lehele
            IWebElement navigateToSpaceship =
                driver.FindElement(By.LinkText("Spaceship"));

            navigateToSpaceship.Click();

            return driver;
        }

        private void InsertSpaceShipData(IWebDriver driver, bool isUpdate = false)
        {
            IWebElement cu_NameEntrySpaceship =
                driver.FindElement(By.Id("CU_NameEntrySpaceship"));

            cu_NameEntrySpaceship.Clear();
            cu_NameEntrySpaceship.SendKeys(
                isUpdate ? "updated spaceship name" : "i add name for spaceship");

            IWebElement cu_ShipTypeEntrySpaceship =
                driver.FindElement(By.Id("CU_ShipTypeEntrySpaceship"));

            cu_ShipTypeEntrySpaceship.Clear();
            cu_ShipTypeEntrySpaceship.SendKeys(
                isUpdate ? "updated ship type" : "i add ship type for spaceship");

            IWebElement cu_CrewEntrySpaceship =
                driver.FindElement(By.Id("CU_CrewEntrySpaceship"));

            cu_CrewEntrySpaceship.Clear();
            cu_CrewEntrySpaceship.SendKeys(
                isUpdate ? "54321" : "12345");

            IWebElement cu_EnginePowerEntrySpaceship =
                driver.FindElement(By.Id("CU_EnginePowerEntrySpaceship"));

            cu_EnginePowerEntrySpaceship.Clear();
            cu_EnginePowerEntrySpaceship.SendKeys(
                isUpdate ? "777778" : "666667");
        }
    }
}
