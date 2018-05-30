using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlpacaPBeM.Properties;
using System.IO;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace AlpacaPBeM
{
    class TurnSender
    {
        public TurnSender()
        {

        }

        public void SendTurn(string gameName, string pwd)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(Settings.Default["Email"].ToString()));
            message.To.Add(new MailboxAddress(Settings.Default["TurnEmail"].ToString()));
            message.Subject = gameName + ": Turn from AlpacaPBeM";

            BodyBuilder builder = new BodyBuilder();
            builder.TextBody = @"Turn from AlpacaPBeM attached.";
            try
            {
                string ordersPath = System.IO.Path.Combine(Settings.Default["Savedgames"].ToString(), gameName);
                string[] dirFiles = Directory.GetFiles(ordersPath);
                bool fileFound = false;
                foreach (var file in dirFiles)
                {
                    if (file.Contains(".2h"))
                    {
                        Console.WriteLine("Found 2h file " + file);
                        builder.Attachments.Add(file);
                        message.Body = builder.ToMessageBody();

                        fileFound = true;
                        break;
                    }
                }
                if (!fileFound)
                {
                    Console.WriteLine("2h file not found!");
                }
                else
                {
                    using (var client = new SmtpClient())
                    {
                        client.Connect(Settings.Default["UsrSMTPServer"].ToString(), int.Parse(Settings.Default["UsrSMTPServerPort"].ToString()), true);
                        client.Authenticate(Settings.Default["Email"].ToString(), pwd);
                        client.Send(message);
                        client.Disconnect(true);
                    }
                    Console.WriteLine("Sending mail.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
