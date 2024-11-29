
using System.Net;
using System.Net.Mail;

namespace IdentityApp.Models;

public class SmtpEmailSender : IEmailSender
{   

    private string? _host;
    private int _port;
    private string? _username;
    private string? _password;

    private bool _enableSsl;

    public SmtpEmailSender(string? host, int port, bool enableSsl,string? username, string? password)
    {
        _host = host;
        _port = port;
        _username = username;
        _password = password;
        _enableSsl = enableSsl;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {   
        
        var client = new SmtpClient(_host, _port)
        {
            Credentials = new NetworkCredential(_username, _password),
            EnableSsl = _enableSsl
        };
        return client.SendMailAsync(new MailMessage(_username, email, subject, htmlMessage){IsBodyHtml = true});
    }
}