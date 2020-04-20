using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Сапер
{
    public partial class Form1 : Form
    {
        private int[,] field = new int[6, 6];
        private Button[,] btnList = new Button[6, 6];
        private int mineAmount = 0;
        private int targetCorrectClickAmount = 0;
        private int correctClickAmount = 0;
        public Form1()
        {
            InitializeComponent();

            btnList[0, 0] = b11;
            btnList[0, 1] = b12;
            btnList[0, 2] = b13;
            btnList[0, 3] = b14;
            btnList[0, 4] = b15;
            btnList[0, 5] = b16;

            btnList[1, 0] = b21;
            btnList[1, 1] = b22;
            btnList[1, 2] = b23;
            btnList[1, 3] = b24;
            btnList[1, 4] = b25;
            btnList[1, 5] = b26;

            btnList[2, 0] = b31;
            btnList[2, 1] = b32;
            btnList[2, 2] = b33;
            btnList[2, 3] = b34;
            btnList[2, 4] = b35;
            btnList[2, 5] = b36;

            btnList[3, 0] = b41;
            btnList[3, 1] = b42;
            btnList[3, 2] = b43;
            btnList[3, 3] = b44;
            btnList[3, 4] = b45;
            btnList[3, 5] = b46;

            btnList[4, 0] = b51;
            btnList[4, 1] = b52;
            btnList[4, 2] = b53;
            btnList[4, 3] = b54;
            btnList[4, 4] = b55;
            btnList[4, 5] = b56;

            btnList[5, 0] = b61;
            btnList[5, 1] = b62;
            btnList[5, 2] = b63;
            btnList[5, 3] = b64;
            btnList[5, 4] = b65;
            btnList[5, 5] = b66;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Coords[] coords = new Coords[10];
            for(int i = 0; i < 10; i++)
            {
                coords[i] = new Coords
                {
                    x = rnd.Next(0, 6),
                    y = rnd.Next(0, 6)
                };
                field[coords[i].x, coords[i].y] = 1;
                btnList[coords[i].x, coords[i].y].Text = "1";
            }

            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    if (field[i, j] == 1)
                        mineAmount++;
                    else
                    {
                        field[i, j] = 0;
                        btnList[i, j].Text = "0";
                    }
                    btnList[i, j].BackColor = Color.Gray;
                    btnList[i, j].ForeColor = Color.Gray;
                }
            }
            targetCorrectClickAmount = 36 - mineAmount;
            groupBoxField.Visible = true;
            buttonStop.Visible = true;
            buttonStart.Visible = false;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            groupBoxField.Visible = false;
            buttonStop.Visible = false;
            buttonStart.Visible = true;
        }

        
        private void b_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if (btn.Text == "1")
            {
                colorMinesRed();
                DialogResult dialogResult = MessageBox.Show("Начать заного?", "Вы проиграли", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    mineAmount = 0;
                    correctClickAmount = 0;
                    buttonStop.PerformClick();
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.Close();
                }
            }
            else
            {
                btn.Text = mineAroundCount(btn.Tag.ToString()).ToString();
                btn.ForeColor = Color.Black;
                btn.BackColor = Color.White;
                correctClickAmount++;
                if (correctClickAmount == targetCorrectClickAmount)
                {
                    win();
                    MessageBox.Show("Победа");
                    mineAmount = 0;
                    correctClickAmount = 0;
                    buttonStop.PerformClick();
                }
            }
        }

        private void win()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (field[i, j] == 1)
                    {
                        btnList[i, j].BackColor = Color.Gold;
                    }
                }
            }
        }
        private void colorMinesRed()
        {
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    if (field[i, j] == 1)
                    {
                        btnList[i, j].BackColor = Color.Red;
                        btnList[i, j].ForeColor = Color.Red;
                    }
                    field[i, j] = 0;
                }
            }
        }

        private int mineAroundCount(string tag)
        {
            int count = 0;
            string[] tags = tag.Split(',');
            int i = int.Parse(tags[0]);
            int j = int.Parse(tags[1]);

            if (i - 1 != -1 
                && j - 1 != -1)
            {
                if (field[i - 1, j] == 1)
                    count++;
                if (field[i, j - 1] == 1)
                    count++;
                if (field[i - 1, j - 1] == 1)
                    count++;
                if (j + 1 != 6 && i+1!=6)
                {
                    if (field[i - 1, j + 1] == 1)
                        count++;
                    if (field[i, j + 1] == 1)
                        count++;
                    if (field[i + 1, j + 1] == 1)
                        count++;
                    if (field[i + 1, j] == 1)
                        count++;
                    if (field[i + 1, j - 1] == 1)
                        count++;
                    return count;
                }
                else if (i + 1 == 6 && j + 1 != 6)
                {
                    if (field[i - 1, j + 1] == 1)
                        count++;
                    if (field[i, j + 1] == 1)
                        count++;
                    return count;
                }
                else if (i + 1 != 6 && j + 1 == 6)
                {
                    if (field[i + 1, j - 1] == 1)
                        count++;
                    if (field[i + 1, j] == 1)
                        count++;
                    return count;
                }
            }
            else if(i-1!=-1
                && j - 1 == -1)
            {
                if (field[i - 1, j] == 1)
                    count++;
                if (field[i - 1, j + 1] == 1)
                    count++;
                if (j + 1 != 6 && i + 1 != 6)
                {
                    if (field[i, j + 1] == 1)
                        count++;
                    if (field[i + 1, j + 1] == 1)
                        count++;
                    if (field[i + 1, j] == 1)
                        count++;
                    return count;
                }
                else if (i + 1 == 6 && j + 1 != 6)
                {
                    if (field[i, j + 1] == 1)
                        count++;
                    return count;
                }
                else if (i + 1 != 6 && j + 1 == 6)
                {
                    if (field[i + 1, j - 1] == 1)
                        count++;
                    if (field[i + 1, j] == 1)
                        count++;
                    return count;
                }
            }
            else if(i-1==-1
                && j - 1 != -1)
            {
                if (field[i, j - 1] == 1)
                    count++;
                if (field[i + 1, j - 1] == 1)
                    count++;
                if (field[i + 1, j] == 1)
                    count++;
                if (j + 1 != 6 && i + 1 != 6)
                {
                    if (field[i, j + 1] == 1)
                        count++;
                    if (field[i + 1, j + 1] == 1)
                        count++;
                    return count;
                }
                else if (i + 1 == 6 && j + 1 != 6)
                {
                    if (field[i, j + 1] == 1)
                        count++;
                    return count;
                }
            }
            else
            {
                if (field[i, j + 1] == 1)
                    count++;
                if (field[i + 1, j + 1] == 1)
                    count++;
                if (field[i + 1, j] == 1)
                    count++;
            }



            return count;
        }
    }
}
