using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlpacaPBeM.Properties;
using OpenPop.Mime;
using OpenPop.Pop3;

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
            Pop3Client client = new Pop3Client();
            client.Connect(Settings.Default["UsrEmailServer"].ToString(), int.Parse(Settings.Default["UsrEmailServerPort"].ToString()), true);
            client.Authenticate(Settings.Default["Email"].ToString(), Settings.Default["Password"].ToString());

            int count = client.GetMessageCount();

            bool foundGame = false;
            for (int i = 0; i < count && i < 50; ++i) //Will at most search through 50 emails atm
            {
                Message message = client.GetMessage(i + 1);
                
                if ((message.Headers.From.Address == Settings.Default["TurnEmail"].ToString()) && (message.Headers.Subject.ToString().Contains(gameName))) 
                {
                    Console.WriteLine("Message from llamaserver: " + message.Headers.Subject + " " + message.Headers.From);
                    MessagePart trnFile = message.FindAllAttachments()[0]; //Assume there is only one attachment as per llama server email format
                    Console.WriteLine("Found trn file: " + trnFile.FileName);

                    //Create folder and save the turn file
                    string savePath = System.IO.Path.Combine(Settings.Default["Savedgames"].ToString(), gameName);
                    System.IO.Directory.CreateDirectory(savePath);
                    
                    trnFile.Save(new System.IO.FileInfo(System.IO.Path.Combine(savePath, trnFile.FileName)));

                    foundGame = true;
                    break;
                }
            }

            if(!foundGame)
            {
                Console.WriteLine("No game found with the name " + gameName);
            }

        }
    }
}
