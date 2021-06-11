
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class SendMail
    {
        //private readonly SendGridClient _sendGridClient;
        //private readonly IConfiguration _configuration;

        public SendMail()
        {
            //_configuration = configuration;
            //_sendGridClient = new SendGridClient(_configuration.GetSection("SendGrid:MailerAPIKey").Value);
        }

        //public async Task InviteTeamscan()
        //{
        //    var sendGridMessage = new SendGridMessage();
        //    // See if this is adjustable 
        //    sendGridMessage.SetFrom("matthias.hernalsteen@gmail.com", "gmail");
        //    sendGridMessage.AddTo("matthias.hernalsteen@euri.com", "euricom");

        //    var mailtemplate = new EmailLayout
        //    {
        //        Name = $"Matthias",
        //        TeamleaderName = "Hernalsteen",
        //        TeamName = "test"
        //    };

        //    sendGridMessage.SetTemplateData(mailtemplate);

        //    await _sendGridClient.SendEmailAsync(sendGridMessage);
        //}


        public async Task Execute(string email,List<Amounts> tableData)
        {
            //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient("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");
            var from = new EmailAddress("matthias.hernalsteen@euri.com", "Fridge Monitor");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(email, "Example User");
            var plainTextContent = "tester";
            var htmlContent = "<p>Het lijkt erop dat 1 van de dranken bijna op is.</p>";
            //"<p>De <strong>cola</strong> heeft nog <strong>" + tableData[ + "</strong> blikje(s) in de frigo</p>" +
            //"<p>De <strong>Fanta</strong> heeft nog <strong>" + FantaAmount + "</strong> blikje(s) in de frigo</p>" +
            //"<p>De <strong>SpriteLemon</strong> heeft nog <strong>" + SpriteLemonAmount + "</strong> blikje(s) in de frigo</p>";
            for(int i = 0; i < tableData.Count(); i++)
            {
                htmlContent += "<p>De <strong>" + tableData[i].Name + "</strong> heeft nog <strong>" + tableData[i].Amount + "</strong> blikje(s) in de frigo</p>";
            }
            
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        //public async Task CompletedTeamscan(string teamName, User teamleader, int teamscanId)
        //{
        //    var sendGridMessage = new SendGridMessage();
        //    sendGridMessage.SetFrom("yanu.szapinszky@euri.com", "Euricom");
        //    sendGridMessage.AddTo(teamleader.Email, $"{teamleader.Firstname} {teamleader.Lastname}");

        //    var mailtemplate = new MailTemplateCompletedTeamscan
        //    {
        //        TeamName = teamName,
        //        TeamleaderName = $"{teamleader.Firstname} {teamleader.Lastname}",
        //        Url = $"http://localhost:3000/scanresults/{teamscanId}"
        //    };

        //    sendGridMessage.SetTemplateId(mailtemplate.TemplateId);
        //    sendGridMessage.SetTemplateData(mailtemplate);

        //    await _sendGridClient.SendEmailAsync(sendGridMessage);
        //}
    }
}