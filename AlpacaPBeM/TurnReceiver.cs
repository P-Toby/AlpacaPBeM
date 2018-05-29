using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlpacaPBeM.Properties;
//using OpenPop.Mime;
//using OpenPop.Pop3;
using MailKit.Net.Imap;
using MailKit;
using MimeKit;

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

                    Console.WriteLine("Total messages: {0}", inbox.Count);
                    Console.WriteLine("Recent messages: {0}", inbox.Recent);

                    for (int i = 0; i < inbox.Count; i++)
                    {
                        var message = inbox.GetMessage(i);
                        Console.WriteLine("Subject: {0}", message.Subject);
                    }

                    client.Disconnect(true);
                }

                //using (var client = new Pop3Client())
                //{
                //    client.Connect(Settings.Default["UsrSMTPServer"].ToString(), 995, true); //Hardcoded port, fix, different ports for out and in in the settings should be added
                //    client.Authenticate(Settings.Default["Email"].ToString(), Settings.Default["Password"].ToString());

                //    //int count = client.Count();
                //    //bool foundGame = false;
                //    //for (int i = 0; i < count && i < 50; ++i) //Will at most search through 50 emails atm
                //    //{
                //    //    MimeMessage message = client.GetMessage(i);

                //    //    if ((message.From.ToString() == Settings.Default["TurnEmail"].ToString()) && (message.Subject.ToString().Contains(gameName)))
                //    //    {
                //    //        Console.WriteLine("Message from llamaserver: " + message.Subject.ToString() + " " + message.From.ToString());


                //    //        //MessagePart trnFile = message.FindAllAttachments()[0]; //Assume there is only one attachment as per llama server email format
                //    //        //Console.WriteLine("Found trn file: " + trnFile.FileName);

                //    //        //Create folder and save the turn file
                //    //        string savePath = System.IO.Path.Combine(Settings.Default["Savedgames"].ToString(), gameName);
                //    //        System.IO.Directory.CreateDirectory(savePath);

                //    //        //trnFile.Save(new System.IO.FileInfo(System.IO.Path.Combine(savePath, trnFile.FileName)));

                //    //        foundGame = true;
                //    //        break;
                //    //    }
                //    //}

                //    //if (!foundGame)
                //    //{
                //    //    Console.WriteLine("No game found with the name " + gameName);
                //    //}

                //    for (int i = 0; i < client.Count; i++)
                //    {
                //        var message = client.GetMessage(i);

                //        Console.WriteLine("Subject: {0}", message.Subject);
                //    }
                //    client.Disconnect(true);
                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            

            //Pop3Client client = new Pop3Client();
            //client.Connect(Settings.Default["UsrSMTPServer"].ToString(), int.Parse(Settings.Default["UsrSMTPServerPort"].ToString()), true);
            //client.Authenticate(Settings.Default["Email"].ToString(), Settings.Default["Password"].ToString());

            //int count = client.GetMessageCount();

            //bool foundGame = false;
            //for (int i = 0; i < count && i < 50; ++i) //Will at most search through 50 emails atm
            //{
            //    Message message = client.GetMessage(i + 1);
                
            //    if ((message.Headers.From.Address == Settings.Default["TurnEmail"].ToString()) && (message.Headers.Subject.ToString().Contains(gameName))) 
            //    {
            //        Console.WriteLine("Message from llamaserver: " + message.Headers.Subject + " " + message.Headers.From);
            //        MessagePart trnFile = message.FindAllAttachments()[0]; //Assume there is only one attachment as per llama server email format
            //        Console.WriteLine("Found trn file: " + trnFile.FileName);

            //        //Create folder and save the turn file
            //        string savePath = System.IO.Path.Combine(Settings.Default["Savedgames"].ToString(), gameName);
            //        System.IO.Directory.CreateDirectory(savePath);
                    
            //        trnFile.Save(new System.IO.FileInfo(System.IO.Path.Combine(savePath, trnFile.FileName)));

            //        foundGame = true;
            //        break;
            //    }
            //}

            //if(!foundGame)
            //{
            //    Console.WriteLine("No game found with the name " + gameName);
            //}

        }
    }
}
