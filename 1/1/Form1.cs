using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Button> buttons = new List<Button>();
        int turn = 0;
        Random rnd;
        int size = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            rnd = new Random();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox1.Text, out int result) && result % 2 == 1 && comboBox1.Text != "" && result > 3 )
            {
                panel1.Enabled = true;
                panel1.Visible = true;
                button1.Enabled = false;
                button1.Visible = false;
                textBox1.Visible = false;
                label1.Visible = false;
                comboBox1.Visible = false;
                int x = 0;
                int y = 0;
                for (int i = 0; i < result; i++)
                {
                    x = 0;
                    for (int j = 0; j < result; j++)
                    {
                        Button myButton = new Button();
                        myButton.Size = new Size(50, 50);
                        myButton.Location = new Point(x, y);
                        buttons.Add(myButton);
                        myButton.Click += MyButton_Click;
                        panel1.Controls.Add(myButton);
                        x = x + 60;
                    }
                    y = y + 60;
                }
                panel1.Width = result * 50 + result * 10 - 10;
                panel1.Height = result * 50 + result * 10 - 10;
                panel1.Location = new Point((this.Width - panel1.Width) / 2, (this.Height - panel1.Height) / 2);
                size = buttons.Count();
            }
            else
            {
                label1.Text = "Ο αριθμος πρεπει να ειναι περριτος και πρεπει να επιλεχθει ειδος παιχνιδιου";
            }
            turn = 0;
        }

        private void MyButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == "" && comboBox1.Text == "vs Player")
            {
                if (turn % 2 == 0)
                {
                b.Text = "X";
                }
                if (turn % 2 != 0)
                {
                b.Text = "O";
                }
                turn += 1;
                checkGame();
            }
            if (b.Text == "" && comboBox1.Text == "vs CPU")
            {
                b.Text = "X";
                int temp = rnd.Next(0, size);
                checkGame();
                if (buttons.Count() > 0)
                {
                    while (buttons[temp].Text != "")
                    {
                        temp = rnd.Next(0, size);
                    }
                    buttons[temp].Text = "O";
                    checkGame();
                }
            }
        }
        public void checkGame()//Elegxos ean exei kerdisei kapoios
        {
            int imp = Int32.Parse(textBox1.Text);
            int offset;
            int counter = 0;
            int counter0 = 0;
            int counter1 = 0;
            int counter2 = 0;
            int counter3 = 0;
            int tiecounter = 0;
            for (int i = 0; i < imp * imp; i++)//Elegxos grammhs
            {
                if (counter == imp)
                {
                    counter = 0; counter0 = 0; counter1 = 0;
                }
                if (buttons[i].Text == "X")
                {
                    counter0 += 1;
                }
                if (buttons[i].Text == "O")
                {
                    counter1 += 1;
                }
                if (i < imp)
                {
                    for (int j = 0; j < imp; j++)//Elegxos sthlhs
                    {
                        offset = j * imp;//Offset = repetition * the distance between the elements of the list we need
                        if (buttons[i + offset].Text == "X")
                        {
                            counter2 += 1;
                        }
                        if (buttons[i + offset].Text == "O")
                        {
                            counter3 += 1;
                        }
                    }
                }
                if (counter0 == imp || counter2 == imp)
                {
                    endGame(0, imp);
                    return;
                }
                else if (counter1 == imp || counter3 == imp)
                {
                    endGame(1, imp);
                    return;
                }
                counter += 1;

                counter2 = 0; counter3 = 0;
            }
            int of1 = 0;
            int of2 = imp - 1;
            counter0 = 0;
            counter1 = 0;
            for (int i = 0; i < imp; i++)//Elegxos diagwniwn
            {

                if (buttons[of1].Text == "X")
                {
                    counter0 += 1;
                }
                if (buttons[of1].Text == "O")
                {
                    counter1 += 1;
                }
                if (buttons[of2].Text == "X")
                {
                    counter2 += 1;
                }
                if (buttons[of2].Text == "O")
                {
                    counter3 += 1;
                }
                of1 = of1 + imp + 1;
                of2 = of2 + imp - 1;
            }
            if (counter0 == imp || counter2 == imp)
            {
                endGame(0, imp);
                return;
            }
            if (counter1 == imp || counter3 == imp)
            {
                endGame(1, imp);
                return;
            }
            for (int i = 0; i < imp * imp; i++)//Elegxos isopalias
            {
                if (buttons[i].Text != "")
                {
                    tiecounter += 1;
                }
            }
            if (tiecounter == (imp * imp))//Elegxos isopalias
            {
                endGame(2, imp);
            }

        }
        public void endGame(int p,int im)//Telos paixnidiou
        {
            if (p == 0)
            {
                MessageBox.Show("Player1 Has Won");
            }
            if (p == 1)
            {
                MessageBox.Show("Player2 Has Won");
            }
            if (p == 2)
            {
                MessageBox.Show("Tie");
            }
            panel1.Visible = false;
            panel1.Enabled = false;
            button1.Enabled = true;
            button1.Visible = true;
            label1.Visible = true;
            textBox1.Visible = true;
            comboBox1.Visible = true;
            for (int l = 0; l < im * im; l++)
            {
                panel1.Controls.Remove(buttons[l]);
            }
            buttons.Clear();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox1.Text, out int temp))
            {
                panel1.Width = temp * 50 + temp * 10 - 10;
                panel1.Height = temp * 50 + temp * 10 - 10;
                panel1.Location = new Point((this.Width - panel1.Width) / 2, (this.Height - panel1.Height) / 2);
            }
            label1.Location = new Point((this.Width - label1.Width) / 2, ((this.Height - label1.Height) / 2) - 40);
            button1.Location = new Point((this.Width - button1.Width) / 2, ((this.Height - button1.Height) / 2) + 60);
            textBox1.Location = new Point((this.Width - textBox1.Width) / 2, ((this.Height - textBox1.Height) / 2));
            comboBox1.Location = new Point((this.Width - comboBox1.Width) / 2, ((this.Height - comboBox1.Height) / 2)+30);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "vs Player";
            button1_Click(sender, e);
        }

        private void startVsCPUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "vs CPU";
            button1_Click(sender, e);
        }

        private void startVsPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "vs Player";
            button1_Click(sender, e);
        }
    }
}
