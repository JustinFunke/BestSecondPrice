//@Author = Justin Funke

using OpenQA.Selenium;

namespace BestSecondPrice;

public class Arvelle : WebseitenBase
{

    private const string webseitenName = "Arvelle";
    public Arvelle() : base("https://www.arvelle.de/")
    {
    }

    protected override void SetzeCookies()
    {
    }

    public override void FuehreSucheDurch(List<string> sucheElemente)
    {
        if (sucheElemente.Count > 0)
        {
            Console.WriteLine($"Arvelle");
            this.SchreibeAbschnitt();

            var searchBox = this.driver.FindElement(By.Id("searchfield"));
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
                    //TODO [J.Funke] 11-07-2024: Ändern
                    IWebElement suggestionsContainer = driver.FindElement(By.CssSelector("li.interim.autocomplete-article > ul"));

                    IReadOnlyCollection<IWebElement> vorschlaege = suggestionsContainer.FindElements(By.CssSelector("li.ui-menu-item"));


                    foreach (var vorschlag in vorschlaege)
                    {
                        IWebElement productNameElement;
                        var oldPrice = string.Empty;
                        try
                        { 
                            productNameElement = vorschlag.FindElement(By.CssSelector("div.autocomplete-item"));
                        }
                        catch (Exception e)
                        {
                            break;
                        }
                        if (productNameElement == null)
                        {
                            break;
                        }
                        var productName = productNameElement.Text.Split(new[] { "<br>" }, StringSplitOptions.None)[0].Trim();
                        var trennpunkt = productName.IndexOf('\r');
                        
                        if (trennpunkt > 0)
                        {
                            if (productName.Length > 0) productName = productName[..trennpunkt];
                            var oldPriceElement = vorschlag.FindElements(By.TagName("s"));
                            if (oldPriceElement.Count == 0)
                            {
                                break;
                            }
                            oldPrice = oldPriceElement[0].Text;
                        }
                        
                        
                        if (oldPrice.Length == 0)
                        {
                            break;
                        }

                        //weil 2 Preise angezeigt werden
                        var newPriceElement = vorschlag.FindElements(By.CssSelector("span[style='color:#F06F0F;font-weight:bold;']"));
                        var newPrice = newPriceElement[0].Text;

                        if (!productName.Contains(element, StringComparison.OrdinalIgnoreCase)) continue;
                        this.SchreibeErgebnis(element, newPrice, "JA", webseitenName);
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
                Console.WriteLine(e.StackTrace);
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
}