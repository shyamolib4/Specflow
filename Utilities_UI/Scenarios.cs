using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace Utilities_UI
{
    public class Scenarios
    {
        static String SignInText;
        public static void Login()
        {
            IWebElement SigninGrid = Utilities.WebDriver.FindElement(By.XPath("//div[@class='nav-signin-tt nav-flyout']"));

            try
            {
                if (SigninGrid.Displayed)
                {
                    Utilities.WebDriver.FindElement(By.Id("nav-signin-tooltip")).Click();
                    Utilities.Log("Clicked on Sign-In through the popup grid");
                    SignIn();
                }
                else
                {
                    Utilities.WebDriver.FindElement(By.Id("nav-link-accountList")).Click();
                    Utilities.Log("Clicked on signin through the Hyperlink");
                    SignIn();
                }

            }
            catch (Exception e)
            {
                Utilities.Log("Error occurred : " + e.Message);
            }
            finally
            {
                Thread.Sleep(5000);
                // WebDriver.Quit();
            }
        }

        public static void SignIn()
        {
            SignInText = Utilities.SignInText().Text;

            String actUser;
            String[] user;

            if (SignInText.Contains("Sign-In"))
            {

                Utilities.Log("We are on the Sign-In Page");

                Utilities.WebDriver.FindElement(By.Id("ap_email")).SendKeys(ConfigurationManager.AppSettings["UserEmail"]);
                Utilities.WebDriver.FindElement(By.Id("continue")).Click();
                Utilities.WebDriver.FindElement(By.Id("ap_password")).SendKeys(ConfigurationManager.AppSettings["Password"]);
                Utilities.WebDriver.FindElement(By.Id("signInSubmit")).Click();

                Thread.Sleep(3000);

                user = Utilities.FetchUserName().Text.Split(',');

                actUser = user[1].Trim();

                if (ConfigurationManager.AppSettings["UserName"].Equals(actUser))
                {
                    Utilities.Log("User " + actUser + " Successfully Signed-In");

                }
                else
                {
                    Utilities.Log("User " + actUser + " Not Successfully Signed-In");
                }
            }
            else
            {
                Utilities.Log("We are not on the Sign-In page");
            }
        }

        public static void Search()
        {
            Utilities.WebDriver.FindElement(By.Id("twotabsearchtextbox")).SendKeys("television");
            Utilities.Log("Searched for Television");
            Thread.Sleep(3000);
            Utilities.WebDriver.FindElement(By.Id("nav-search-submit-button")).Click();
            Thread.Sleep(3000);
            Utilities.WebDriver.FindElement(By.XPath("//div[@class='a-section aok-relative s-image-fixed-height']//img[@data-image-index='1']")).Click();
            Thread.Sleep(3000);
        }
        public static void AddToCart()
        {
            try
            {
                String windowHandle = Utilities.WebDriver.WindowHandles[1];
                Utilities.WebDriver.SwitchTo().Window(windowHandle);

                String telName = Utilities.WebDriver.FindElement(By.Id("productTitle")).Text;
                Utilities.Log("Element to be added in the cart:" + telName);

                Utilities.WebDriver.FindElement(By.XPath("//input[@value='Add to Cart']")).Click();
                Thread.Sleep(3000);

                if (Utilities.WebDriver.FindElement(By.Id("attach-close_sideSheet-link")).Displayed)
                {
                    Utilities.WebDriver.FindElement(By.Id("attach-close_sideSheet-link")).Click();
                    Utilities.WebDriver.FindElement(By.Id("nav-cart-count-container")).Click();
                    Thread.Sleep(3000);

                    IList<IWebElement> list = Utilities.WebDriver.FindElements(By.XPath("//span[@class='a-truncate-cut']"));

                    foreach (IWebElement el in list)
                    {
                        //Program.Log("In the cart:" + el.Text);

                        if (el.Text.Contains(telName))
                        {
                            //Assert.AreEqual(telName, el.Text);
                            Utilities.Log("Verified item in the cart is:" + el.Text);
                            break;
                        }
                        else
                        {
                            //Assert.AreNotEqual(telName, el.Text);
                        }
                    }
                }
                else
                {
                    // Utilities.WebDriver.FindElement(By.Id("attach-close_sideSheet-link")).Click();
                    Utilities.WebDriver.FindElement(By.Id("nav-cart-count-container")).Click();
                    Thread.Sleep(3000);

                    IList<IWebElement> list = Utilities.WebDriver.FindElements(By.XPath("//span[@class='a-truncate-cut']"));

                    foreach (IWebElement el in list)
                    {
                        //Program.Log("In the cart:" + el.Text);

                        if (el.Text.Contains(telName))
                        {
                            //Assert.AreEqual(telName, el.Text);
                            Utilities.Log("Verified item in the cart is:" + el.Text);
                            break;
                        }
                        else
                        {
                            //Assert.AreNotEqual(telName, el.Text);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Utilities.Log("Error occurred : " + e.Message);
            }
            finally
            {
                Thread.Sleep(5000);


            }
        }

        public static void Logout()
        {
            Actions act = new Actions(Utilities.WebDriver);

            String AppUser = Utilities.FetchUserName().Text;

            if (AppUser.Contains(ConfigurationManager.AppSettings["UserName"]))
            {
                IWebElement el = Utilities.WebDriver.FindElement(By.Id("nav-link-accountList"));
                act.MoveToElement(el).Build().Perform();
                IWebElement Signout = Utilities.WebDriver.FindElement(By.Id("nav-item-signout"));
                Signout.Click();
                SignInText = Utilities.SignInText().Text;
                if (SignInText.Contains("Sign-In"))
                {
                    Utilities.Log("User has logged out successfully");
                }
                else
                {
                    Utilities.Log("User has not logged out successfully");
                }
            }
            else
            {
                Utilities.Log("User is not logged in");

            }
        }
    }
}
