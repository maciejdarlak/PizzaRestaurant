using System.Net;
using System.Net.Mail;
using System.Text;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.Domain.Entities;



//Implementacja interfejsu "IOrderProcessor" będzie przetwarzała zamówienia przez ich przesłanie pocztą elektroniczną do administratora witryny.
//Plik zawiera dwie klasy: 1."EmailSettings" (dane mailowe) oraz 2."EmailOrderProcessor" - w jej konstruktorze dodajemy 1."EmailSettings" oraz implementujemy interface "IOrderProcessor" 
//(koszyk i szczegóły zamówienia z innymi danymi). 
//W "EmailOrderProcessor" następuje sklejenie powyższego w całość, czyli ZAMÓWIENIE.
namespace PizzaRestaurant.Domain.Concrete
{
    //Obiekt tej klasy zawiera wszystkie ustawienia wymagane do skonfigurowania klas e-mail.NET i jest oczekiwany przez konstruktor EmailOrderProcessor.
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

        //Obiekt "EmailSettings" (utworzony potem w "NinjectDepedencyResolver") wstrzykujemy do konstruktora "EmailOrderProcessor" w momencie tworzenia nowego egzemplarza 
        //w odpowiedzi na żądanie interfejsu IOrderProcessor.
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            //Umożliwia aplikacji wysyłanie wiadomości e-mail przy użyciu transferu protokół SMTP (Simple Mail).
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                //Podaje poświadczenia dla schematów uwierzytelniania opartego na hasłach, np. basic, digest, NTLM oraz uwierzytelnianie Kerberos.
                = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    // Określa, jak wiadomości e-mail są dostarczane.
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    //Pobiera lub ustawia folderu oszczędzić wiadomości e-mail do przetworzenia przez lokalny serwer SMTP w aplikacji.
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
                emailSettings.MailFromAddress, // od
                emailSettings.MailToAddress, // do
                "Otrzymano nowe zamówienie!", // temat
                body.ToString()); // treść

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}