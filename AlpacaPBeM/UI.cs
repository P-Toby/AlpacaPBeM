using AlpacaPBeM.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlpacaPBeM
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
        }

        private void UI_Load(object sender, EventArgs e)
        {
            label2.Text = Settings.Default["Email"].ToString() + "\n";
            ConsoleOutput.Text = "Turn email: " + Settings.Default["TurnEmail"].ToString() + "\n";
            ConsoleOutput.Text += "Dominions save directory: " + Settings.Default["Savedgames"].ToString() + "\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsoleOutput.Text = "Fetching latest " + textBox2.Text.ToString() + " turn.\n";
            bool success = Alpaca.TurnReceiverManager.GetTurn(textBox2.Text.ToString(), PasswordField.Text.ToString());
            
            if(success)
                ConsoleOutput.Text += "Done fetching " + textBox2.Text.ToString() + " turn.\n";
            else
                ConsoleOutput.Text = "Could not find latest " + textBox2.Text.ToString() + " turn.\n";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void ConsoleOutput_TextChanged(object sender, EventArgs e)
        {
        }

        private void PasswordField_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConsoleOutput.Text = "Sending latest " + textBox2.Text.ToString() + " turn.\n";
            bool success = Alpaca.TurnSenderManager.SendTurn(textBox2.Text.ToString(), PasswordField.Text.ToString());

            if (success)
                ConsoleOutput.Text += "Done sending " + textBox2.Text.ToString() + " turn to " + Settings.Default["TurnEmail"].ToString();
            else
                ConsoleOutput.Text = "Could not find a .2h file in a game called " + textBox2.Text.ToString() + ".\n";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
