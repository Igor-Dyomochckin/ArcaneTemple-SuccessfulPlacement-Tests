using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace ArcaneTemple_SuccessfulPlacement_Tests
{
    //ѕоскольку была задача написать только один автотест, € не пользовалс€ паттерном 
    //PageObject дл€ вынесени€ XPath в отдельный класс
    public class Tests
    {
        private IWebDriver driver;

        private readonly By _enterNameInputButton = By.XPath("//input[@placeholder = 'Enter name']");
        private readonly By _enterDomainInputButton = By.XPath("//input[@placeholder ='Enter domain']");
        private readonly By _minFloorPriceInputButton = By.XPath("//input[@placeholder = 'Enter min floor price']");
        private readonly By _selecetOnePopUpButton = By.XPath("//select[@class='form-select mt-3']");
        private readonly By _bannerSelectedButton = By.XPath("//option[@value='Banner']");
        private readonly By _selectSizeButton = By.XPath("//label[@for='300x600']");
        private readonly By _saveButton = By.XPath("//button[@type='submit']");
        private readonly By _formHeader = By.XPath("//h3");

        private const string expectedHeader = "Ad placement";
        private const string name = "Form_1";
        private const string domain = "Google.com";
        private const string price = "1350";


        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://arcane-temple-53288.herokuapp.com/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            var enterName = driver.FindElement(_enterNameInputButton);
            enterName.SendKeys(name);

            var enterDomain = driver.FindElement(_enterDomainInputButton);
            enterDomain.SendKeys(domain);

            var minFloorPrice = driver.FindElement(_minFloorPriceInputButton);
            minFloorPrice.Clear();
            minFloorPrice.SendKeys(price);

            var selectOne = driver.FindElement(_selecetOnePopUpButton);
            selectOne.Click();

            Thread.Sleep(500);
            var bannerSelected = driver.FindElement(_bannerSelectedButton);
            bannerSelected.Click();
            selectOne.Click();//эта строка чтобы закрылось pop-up menu

            var selectSize = driver.FindElement(_selectSizeButton);
            selectSize.Click();

            var save = driver.FindElement(_saveButton);
            for (int i = 0; i < 10; i++)
            {
                save.Click();
                Thread.Sleep(300);
                var actualMessage = driver.FindElement(_formHeader).Text;
                Assert.AreEqual(expectedHeader,actualMessage,"Expected and actual headers are not equal");
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

    }
}