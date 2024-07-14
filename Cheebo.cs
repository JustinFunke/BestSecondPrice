//@Author = Justin Funke

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BestSecondPrice;

public class Cheebo : WebseitenBase
{
    private const string webseitenName = "Cheebo";

    public Cheebo()
        :base("https://cheaboo.de/")
    {
    }


    protected override void SetzeCookies()
    {
       var cookieButton = this.driver.FindElement(By.ClassName("js-offcanvas-cookie-submit"));
       cookieButton.Click();
    }

    public override void FuehreSucheDurch(List<string> sucheElemente)
    {
        if (sucheElemente.Count > 0)
        {
            Console.WriteLine($"Cheebo");
            this.SchreibeAbschnitt();

            var searchBox = this.driver.FindElement(By.Name("search"));
            if (searchBox == null)
            {
                Console.WriteLine($"Elemente auf der Webseite nicht gefunden");
                return;
            }

            try
            {
                

                foreach (var element in sucheElemente)
                {
                    var productFound = false;
                    searchBox.SendKeys(element);

                    Thread.Sleep(4000);

                    IReadOnlyCollection<IWebElement> vorschlaege =
                        this.driver.FindElements(By.CssSelector("a.search-suggest-product-link"));


                    foreach (var vorschlag in vorschlaege)
                    {
                        var productName = vorschlag.FindElement(By.ClassName("search-suggest-product-name")).Text;

                        var productPrice = vorschlag.FindElement(By.ClassName("search-suggest-product-price")).Text;
                        productPrice = productPrice[..productPrice.IndexOf(" ", StringComparison.Ordinal)];
                        
                        if (!productName.Equals(element, StringComparison.OrdinalIgnoreCase)) continue;
                        this.SchreibeErgebnis(element, productPrice, "JA", webseitenName);
                        productFound = true;
                        break;
                    }
                    if (productFound == false)
                    {
                        this.SchreibeErgebnis(element, "0", "NEIN", webseitenName);
                    }
                    searchBox.Clear();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.driver.Quit();
            }
        }
        else
        {
            Console.WriteLine("Keine Suchelemente vorhanden");
        }
        this.SchreibeAbschnitt();
    }

    //TODO [J.Funke] 11-07-2024: Consolenanwendung komplett verstecken wenn Email gesendet wurde
}