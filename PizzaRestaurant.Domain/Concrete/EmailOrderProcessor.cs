using System.Net;
using System.Net.Mail;
using System.Text;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.Domain.Entities;



// The implementation of the "IOrderProcessor" interface will process orders by sending them via email to the site administrator.
// The file contains two classes: 1. "EmailSettings" (email data) and 2. "EmailOrderProcessor" - in its constructor added 1. "EmailSettings" and implement the interface "IOrderProcessor"
// (cart and order details with other details).
// In "EmailOrderProcessor" the above is glued together, that is ORDER.
namespace PizzaRestaurant.Domain.Concrete
{
    //The object of this class contains all the settings required to configure e-mail.NET classes and is expected by the EmailOrderProcessor constructor
    public class EmailSettings
    {
        public string MailToAddress = "zamowienia@przyklad.pl";
        public string MailFromAddress = "pizzarestaurant@przyklad.pl";
        public bool UseSsl = true;
        public string Username = "UżytkownikSmtp";
        public string Password = "HasłoSmtp";
        public string ServerName = "smtp.przyklad.pl";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"C:\Users\mdarlak\source\repos\PizzaRestaurant\emails";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        // The "EmailSettings" object (created later in "NinjectDepedencyResolver") is injected into the "EmailOrderProcessor" constructor 
        //when creating a new instance in response to an IOrderProcessor interface request.
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            //Allows the app to send email using the SMTP (Simple Mail) transfer protocol.
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                //Provides credentials for password-based authentication schemes, e.g. basic, digest, NTLM
                = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    //Specifies how email is delivered.
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                .AppendLine("Nowe zamówienie")
                .AppendLine("---")
                .AppendLine("Produkty:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (wartość: {2:c}", line.Quantity,
                    line.Product.Name, subtotal);
                }

                body.AppendFormat("Wartość całkowita: {0:c}", cart.ComputeTotalValue())
                .AppendLine("---")
                .AppendLine("Wysyłka dla:")
                .AppendLine(shippingInfo.Name)
                .AppendLine(shippingInfo.Line1)
                .AppendLine(shippingInfo.Line2 ?? "")
                .AppendLine(shippingInfo.Line3 ?? "")
                .AppendLine(shippingInfo.City)
                .AppendLine(shippingInfo.State ?? "")
                .AppendLine(shippingInfo.Country)
                .AppendLine(shippingInfo.Zip)
                .AppendLine("---")
                .AppendFormat("Pakowanie prezentu: {0}", shippingInfo.GiftWrap ? "Tak" : "Nie");

                MailMessage mailMessage = new MailMessage(
                emailSettings.MailFromAddress, // from
                emailSettings.MailToAddress, // to
                "Otrzymano nowe zamówienie!", // subject
                body.ToString()); // contents

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}