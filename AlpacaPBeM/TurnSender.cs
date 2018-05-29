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

        public void SendTurn(string gameName)
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
                        client.Authenticate(Settings.Default["Email"].ToString(), Settings.Default["Password"].ToString());
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

        //public void SendTurn(string gameName)
        //{
        //    try
        //    {
        //        MailMessage Mail = new MailMessage();
        //        SmtpClient Server = new SmtpClient(Settings.Default["UsrSMTPServer"].ToString());
        //        Mail.From = new MailAddress(Settings.Default["Email"].ToString());
        //        Mail.To.Add(Settings.Default["TurnEmail"].ToString());
        //        Mail.Subject = "Turn from AlpacaPBeM";
        //        Mail.Body = "Sending turn from AlpacaPBeM!";

        //        string ordersPath = System.IO.Path.Combine(Settings.Default["Savedgames"].ToString(), gameName);
        //        string []dirFiles = Directory.GetFiles(ordersPath);
        //        bool fileFound = false;
        //        foreach (var file in dirFiles)
        //        {
        //            if(file.Contains(".2h"))
        //            {
        //                Console.WriteLine("Found 2h file " + file);
        //                Mail.Attachments.Add(new Attachment(file));
        //                fileFound = true;
        //                break;
        //            }
        //        }
        //        if (!fileFound)
        //        {
        //            Console.WriteLine("2h file not found!");
        //        }
        //        else
        //        {
        //            Server.Port = 587;
        //            Server.Credentials = new System.Net.NetworkCredential(Settings.Default["Email"].ToString(), Settings.Default["Password"].ToString());
        //            Server.EnableSsl = true;
        //            Server.Send(Mail);

        //            Console.WriteLine("Attempting to send mail");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}
    }
}
