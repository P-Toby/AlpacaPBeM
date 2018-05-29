using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlpacaPBeM.Properties;
using MailKit.Net.Imap;
using MailKit;
using MimeKit;
using System.IO;

namespace AlpacaPBeM
{
    class TurnReceiver
    {
        public TurnReceiver()
        {

        }

        /*
        * Currently only fetches the last turn file matching the game name
        * TODO:
        * TEST EMAIL USED
        * Distinguish between different types of emails later
        * Check and remember turn number for each game
        */

        public void GetTurn(string gameName)
        {
            try
            {
                using (var client = new ImapClient())
                {
                    client.Connect(Settings.Default["UsrIMAPServer"].ToString(), int.Parse(Settings.Default["UsrIMAPServerPort"].ToString()), true);
                    client.Authenticate(Settings.Default["Email"].ToString(), Settings.Default["Password"].ToString());

                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);

                    int count = inbox.Count();
                    bool foundGame = false;
                    for (int i = 0; i < count && i < 50; ++i) //Will at most search through 50 emails atm
                    {
                        MimeMessage message = inbox.GetMessage(i);

                        if ((message.From.ToString().Contains(Settings.Default["TurnEmail"].ToString())) && (message.Subject.ToString().Contains(gameName)))
                        {
                            Console.WriteLine("Message from llamaserver: " + message.Subject.ToString() + " " + message.From.ToString());
                            string savePath = System.IO.Path.Combine(Settings.Default["Savedgames"].ToString(), gameName);
                            System.IO.Directory.CreateDirectory(savePath);

                            foreach (var attachment in message.Attachments)
                            {
                                if (attachment is MimeEntity)
                                {
                                    var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
                                    fileName = System.IO.Path.Combine(Settings.Default["Savedgames"].ToString(), gameName, fileName);
                                    System.IO.Directory.CreateDirectory(savePath); //Check if folder exists first?
                                    using (var stream = File.Create(fileName))
                                    {
                                        var part = (MimePart)attachment;
                                        part.Content.DecodeTo(stream);
                                    }
                                }
                            }

                            foundGame = true;
                            break;
                        }
                    }

                    if (!foundGame)
                    {
                        Console.WriteLine("No game found with the name " + gameName);
                    }

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
