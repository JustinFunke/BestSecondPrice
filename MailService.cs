//@Author = Justin Funke

using System.Net.Mail;

namespace BestSecondPrice;

public class MailService
{
    private readonly string _fromAddress;
    private readonly string _password;
    private readonly string _smtpServer;
    private readonly int _port;
    private readonly string _toAddress;


    public MailService(string fromAddress, string password, string smtpServer, int port, string toAddress)
    {
        this._fromAddress = fromAddress;
        this._password = password;
        this._smtpServer = smtpServer;
        this._port = port;
        this._toAddress = toAddress;
    }
    public void SendeErgebnissePerEmail(List<Ergebniss> aktiveAngebote)
    {
        if (aktiveAngebote.Count <= 0) return;
        var mailMessage = new MailMessage
        {
            From = new MailAddress(this._fromAddress),
            Subject = "Suchergebnisse",
            Body = this.ErstelleEmailBody(aktiveAngebote),
            IsBodyHtml = true
        };
        mailMessage.To.Add(this._toAddress);

        using var smtpClient = new SmtpClient(this._smtpServer);
        smtpClient.Port = this._port;
        smtpClient.Credentials = new System.Net.NetworkCredential(this._fromAddress, this._password);
        smtpClient.EnableSsl = true;
        smtpClient.Send(mailMessage);
    }

    private string ErstelleEmailBody(List<Ergebniss> ergebnisse)
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