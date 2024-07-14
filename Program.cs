using BestSecondPrice;
using OpenQA.Selenium.Chrome;
using System.Text;
using TextReader = BestSecondPrice.TextReader;
Console.OutputEncoding = Encoding.UTF8;

if (args.Length < 5)
{
    Console.WriteLine("Bitte geben Sie Ihre E-Mail-Adresse, Ihr Passwort, Ihr SMTPServer den Port und die Zieladresse als Argumente an.");
    Console.ReadLine();
    return;
}
int port;
if (int.TryParse(args[3],out port)== false)
{
    Console.WriteLine("Bitte geben Sie Ihre E-Mail-Adresse, Ihr Passwort, Ihr SMTPServer den Port und die Zieladresse als Argumente an.");
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

