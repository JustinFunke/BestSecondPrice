//@Author = Justin Funke

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BestSecondPrice;

public abstract class WebseitenBase
{
    protected IWebDriver driver;

    public List<Ergebniss> Ergebnisse = new();

    protected WebseitenBase(string webseite)
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--window-size=1920,1080");

        var service = ChromeDriverService.CreateDefaultService();
        service.SuppressInitialDiagnosticInformation = true;
        service.HideCommandPromptWindow = true;

        this.driver = new ChromeDriver(service, options);
        this.VerbindeUndSetzeElemente(webseite);
    }
    protected abstract void SetzeCookies();

    public abstract void FuehreSucheDurch(List<string> sucheElemente);

    protected void SchreibeErgebnis(string element, string price, string angebot, string webseite)
    {
        Console.WriteLine($"Product: {element} | | Price: {price} € | | Angebot: {angebot} || Webseite: {webseite}");
        if (angebot.Equals("JA", StringComparison.OrdinalIgnoreCase))
        {
            this.Ergebnisse.Add(new Ergebniss()
            {
                Product = element,
                Price = price,
                Angebot = angebot,
                Webseite = webseite
            });
        }
    }

    protected static void SchreibeAbschnitt()
    {
        Console.WriteLine($"================================================================================================================");
    }

    protected void VerbindeUndSetzeElemente(string webseite)
    {
        this.driver.Navigate().GoToUrl(webseite);
        Thread.Sleep(1000);
        this.SetzeCookies();
    }

}