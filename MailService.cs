//@Author = Justin Funke

using System.Net.Mail;

namespace BestSecondPrice;

public class MailService(string fromAddress, string password, string smtpServer, int port, string toAddress)
{
    private readonly string _fromAddress = fromAddress;
    private readonly string _password = password;
    private readonly string _smtpServer = smtpServer;
    private readonly int _port = port;
    private readonly string _toAddress = toAddress;

    public void SendeErgebnissePerEmail(List<Ergebniss> aktiveAngebote)
    {
        if (aktiveAngebote.Count <= 0) return;
        var mailMessage = new MailMessage
        {
            From = new MailAddress(this._fromAddress),
            Subject = "Suchergebnisse",
            Body = ErstelleEmailBody(aktiveAngebote),
            IsBodyHtml = true
        };
        mailMessage.To.Add(this._toAddress);

        using var smtpClient = new SmtpClient(this._smtpServer);
        smtpClient.Port = this._port;
        smtpClient.Credentials = new System.Net.NetworkCredential(this._fromAddress, this._password);
        smtpClient.EnableSsl = true;
        smtpClient.Send(mailMessage);
    }

    private static string ErstelleEmailBody(List<Ergebniss> ergebnisse)
    {
        var body = "<h1>Angebote</h1><ul>";
        foreach (var ergebnis in ergebnisse)
        {
            body += $"<li>Produkt: {ergebnis.Product} | Preis: {ergebnis.Price} € | Angebot: {ergebnis.Angebot} | Webseite: {ergebnis.Webseite}</li>";
        }
        body += "</ul>";
        return body;
    }


}