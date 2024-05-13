namespace ProductWebAPI.Services.Background
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Quartz;

    public class BackMail : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.atb.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("emailfromabt555@gmail.com", "atbatb123123"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("emailfromabt555@gmail.com"),
                    Subject = "Test Email",
                    Body = "This is a test email sent from ATB job.",
                    IsBodyHtml = true,
                };

                mailMessage.To.Add("kovalevvanya50@gmail.com");

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }
    }


}
