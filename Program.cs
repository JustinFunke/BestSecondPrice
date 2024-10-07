using BestSecondPrice;
using System.Text;
using TextReader = BestSecondPrice.TextReader;

Console.OutputEncoding = Encoding.UTF8;

if (args.Length < 5)
{
    Console.WriteLine(
        "Bitte geben Sie Ihre E-Mail-Adresse, Ihr Passwort, Ihr SMTPServer den Port und die Zieladresse als Argumente an.");
    Console.ReadLine();
    return;
}

if (int.TryParse(args[3], out var port) == false)
{
    Console.WriteLine(
        "Bitte geben Sie Ihre E-Mail-Adresse, Ihr Passwort, Ihr SMTPServer den Port und die Zieladresse als Argumente an.");
    Console.ReadLine();
    return;
}

if(Path.Exists("Suchbegriffe.txt")== false)
{
    Console.WriteLine("Bitte legen Sie eine Datei mit dem Namen Suchbegriffe.txt an.");
    Console.ReadLine();
    return;
}
if (IsInternetAvailable().Result == false)
{
    Console.WriteLine("Keine Internetverbindung vorhanden");
    Console.ReadLine();
    return;
}
var emailService = new MailService(args[0], args[1], args[2], port, args[4]);

var suchbegriffe = TextReader.HoleSuchbegriffe();
var cheeboWebsite = new Cheebo();

var arvelle = new Arvelle();

cheeboWebsite.FuehreSucheDurch(suchbegriffe);
arvelle.FuehreSucheDurch(suchbegriffe);

var aktiveAngebote = cheeboWebsite.Ergebnisse;
var aktiveAngeboteZusammen = aktiveAngebote.Concat(arvelle.Ergebnisse);

emailService.SendeErgebnissePerEmail(aktiveAngeboteZusammen.ToList());


static async Task<bool> IsInternetAvailable()
{
    try
    {
        using var client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(2);

        // Eine Anfrage an eine bekannte Website senden
        var response = await client.GetAsync("https://www.google.com");

        // Wenn die Antwort erfolgreich ist, ist eine Internetverbindung vorhanden
        return response.IsSuccessStatusCode;
    }
    catch
    {
        return false;
    }
    
}