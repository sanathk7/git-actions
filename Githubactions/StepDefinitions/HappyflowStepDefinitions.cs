using System;
using System.Runtime.CompilerServices;
using Githubactions.Pages;
using TechTalk.SpecFlow;

namespace Githubactions.StepDefinitions
{
    [Binding]
    public class HappyflowStepDefinitions
    {

        LoginPage loginpage;
        AddCart addcart;
        ItemPage itempage;
        CheckOutPage checkout;
        CheckOverviewPage checkover;

        public HappyflowStepDefinitions()
        {

            this.loginpage = new LoginPage();
            this.addcart = new AddCart();
            this.checkout = new CheckOutPage();
            this.checkover = new CheckOverviewPage();
            this.itempage = new ItemPage();
        }

            [Given(@"User is on the Login Page")]
            public void GivenUserIsOnTheLoginPage()
            {
                loginpage.LaunchBrowser();
                Thread.Sleep(1000);
            }

            [When(@"User enters the Username ""([^""]*)""")]
            public void WhenUserEntersTheUsername(string p0)
            {
                loginpage.EnterUserNames(p0);
            }

            [When(@"User enters the Password ""([^""]*)""")]
            public void WhenUserEntersThePassword(string p0)
            {
                loginpage.EnterPassowrd(p0);
                Thread.Sleep(1000);
            }

            [When(@"Clicks the Login button")]
            public void WhenClicksTheLoginButton()
            {
                loginpage.Submit();

            }

            [Then(@"Homepage should open")]
            public void ThenHomepageShouldOpen()
            {
                loginpage.HomePageDisplay();
                Thread.Sleep(1000);
            }

            [When(@"User clicks on an Item")]
            public void WhenUserClicksOnAnItem()
            {
                itempage.Item();
            }

            [Then(@"Item details should be open")]
            public void ThenItemDetailsShouldBeOpen()
            {
                itempage.ItemDetail();
            }

            [Given(@"User is logged in")]
            public void GivenUserIsLoggedIn()
            {
                //loginpage.AddToCart();
                Console.WriteLine("Usre Logid in");
            }

            [When(@"User clicks on Add to Cart")]
            public void WhenUserClicksOnAddToCart()
            {
                addcart.AddToCart();
            }

            [When(@"User clicks on Cart")]
            public void WhenUserClicksOnCart()
            {
                addcart.Cart();
                Thread.Sleep(1000);
            }

            [Then(@"User Cart should open")]
            public void ThenUserCartShouldOpen()
            {
                addcart.UserCart();
                Thread.Sleep(1000);
            }


            [Given(@"User is on the Checkout page")]
            public void GivenUserIsOnTheCheckoutPage()
            {
                checkout.CheckOut();
            }

            [When(@"User enters First Name ""([^""]*)"", Last Name ""([^""]*)"", and Zip Code ""([^""]*)""")]
            public void WhenUserEntersFirstNameLastNameAndZipCode(string sanath, string kumar, string p2)
            {
                checkout.BuyerDetail(sanath, kumar, p2);
                Thread.Sleep(1000);
            }

            [Then(@"Clicks on Continue")]
            public void ThenClicksOnContinue()
            {
                checkout.Countniue();
                Thread.Sleep(1000);
            }

            [Given(@"User is on the Checkout Overview page")]
            public void GivenUserIsOnTheCheckoutOverviewPage()
            {
                checkover.CheckOutOverview();
                Thread.Sleep(1000);
            }

            [When(@"User clicks on Finish")]
            public void WhenUserClicksOnFinish()
            {
                checkover.Finish();
                Thread.Sleep(1000);
            }

            [Then(@"Order status should be visible")]
            public void ThenOrderStatusShouldBeVisible()
            {
                checkover.OrderConfirm();

            }


        }    

    }

