using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        bool xPlayerTurn = true;
        int turnCount = 0;
        int pictureCounter = 1;
        PictureBox pic;

        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeCells();
        }

        private void InitializeGrid()
        {
            Grid.BackColor = Color.DarkGray;
            Grid.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
        }

        private void RestartGame()
        {
            InitializeCells();
            turnCount = 0;
        }
        private void InitializeCells()
        {
            string labelName;
            for(int i = 1; i <= 9; i++)
            {
                labelName = "pictureBox" + i;
                Grid.Controls[labelName].Tag = String.Empty;
                Grid.Controls[labelName].BackColor = Color.Transparent;
            }
        }

        private void Player_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            if(pic.Tag != String.Empty)
            {
                return;
            }

            if (xPlayerTurn)
            {
                pic.Tag = "X";
            }
            else
            {
                pic.Tag = "O";
            }
            turnCount++;
            PlaySound("click_sound");
            CheckForWin();
            CheckForDraw();
            xPlayerTurn = !xPlayerTurn;            
        }

        private void CheckForWin()
        {
            if (
                (pictureBox1.Tag == pictureBox2.Tag && pictureBox2.Tag == pictureBox3.Tag && pictureBox1.Tag != string.Empty) ||
                (pictureBox4.Tag == pictureBox5.Tag && pictureBox5.Tag == pictureBox6.Tag && pictureBox4.Tag != string.Empty) ||
                (pictureBox7.Tag == pictureBox8.Tag && pictureBox8.Tag == pictureBox9.Tag && pictureBox7.Tag != string.Empty) ||
                (pictureBox1.Tag == pictureBox4.Tag && pictureBox4.Tag == pictureBox7.Tag && pictureBox1.Tag != string.Empty) ||
                (pictureBox2.Tag == pictureBox5.Tag && pictureBox5.Tag == pictureBox8.Tag && pictureBox2.Tag != string.Empty) ||
                (pictureBox3.Tag == pictureBox6.Tag && pictureBox6.Tag == pictureBox9.Tag && pictureBox3.Tag != string.Empty) ||
                (pictureBox1.Tag == pictureBox5.Tag && pictureBox5.Tag == pictureBox9.Tag && pictureBox1.Tag != string.Empty) ||
                (pictureBox3.Tag == pictureBox5.Tag && pictureBox5.Tag == pictureBox7.Tag && pictureBox3.Tag != string.Empty)
                )
            {
                GameOver();
            }
        }

        private void WinnerCellsChangeColor()
        {
            if (pictureBox1.Tag == pictureBox2.Tag && pictureBox1.Tag == pictureBox3.Tag && pictureBox1.Tag != "")
            {
                ChangeCellsColors(pictureBox1, pictureBox2, pictureBox3, Color.White);
            }
            else if (pictureBox4.Tag == pictureBox5.Tag && pictureBox4.Tag == pictureBox6.Tag && pictureBox4.Tag != "")
            {
                ChangeCellsColors(pictureBox4, pictureBox5, pictureBox6, Color.White);
            }
            else if (pictureBox7.Tag == pictureBox8.Tag && pictureBox7.Tag == pictureBox9.Tag && pictureBox7.Tag != "")
            {
                ChangeCellsColors(pictureBox7, pictureBox8, pictureBox9, Color.White);
            }
            else if (pictureBox1.Tag == pictureBox4.Tag && pictureBox1.Tag == pictureBox7.Tag && pictureBox1.Tag != "")
            {
                ChangeCellsColors(pictureBox1, pictureBox4, pictureBox7, Color.White);
            }
            else if (pictureBox2.Tag == pictureBox5.Tag && pictureBox2.Tag == pictureBox8.Tag && pictureBox2.Tag != "")
            {
                ChangeCellsColors(pictureBox2, pictureBox5, pictureBox8, Color.White);
            }
            else if (pictureBox3.Tag == pictureBox6.Tag && pictureBox3.Tag == pictureBox9.Tag && pictureBox3.Tag != "")
            {
                ChangeCellsColors(pictureBox3, pictureBox6, pictureBox9, Color.White);
            }
            else if (pictureBox1.Tag == pictureBox5.Tag && pictureBox1.Tag == pictureBox9.Tag && pictureBox1.Tag != "")
            {
                ChangeCellsColors(pictureBox1, pictureBox5, pictureBox9, Color.White);
            }
            else if (pictureBox3.Tag == pictureBox5.Tag && pictureBox3.Tag == pictureBox7.Tag && pictureBox3.Tag != "")
            {
                ChangeCellsColors(pictureBox3, pictureBox5, pictureBox7, Color.White);
            }
        }

        private void ChangeCellsColors(PictureBox firstLabel, PictureBox secondLabel, PictureBox thirdLabel, Color color)
        {
            firstLabel.BackColor = color;
            secondLabel.BackColor = color;
            thirdLabel.BackColor = color;
        }

        private void PlaySound(string soundName)
        {
           System.IO.Stream str = (System.IO.Stream)Properties.Resources.ResourceManager.GetObject(soundName);
           System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
           snd.Play();
        }

        private void CheckForDraw()
        {
            if(turnCount == 9)
            {
                MessageBox.Show("Draw!");
                RestartGame();
            }
        }

   
        private void GameOver()
        {
            string winner;
            if(xPlayerTurn)
            {
                winner = "X";
            }
            else
            {
                winner = "O";
            }
            WinnerCellsChangeColor();
            MessageBox.Show(winner + " wins!");
            RestartGame();
        }

        private void Animate()
        {
            string turn;
            string pictureName;
            

            turn = pic.Tag.ToString();
            turn = turn.ToLower();

            pictureName = "o_frame_0" + pictureCounter.ToString("00");
            pic.Image = (Image)Properties.Resources.ResourceManager.GetObject(pictureName);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureCounter += 1;
            if(pictureCounter > 20)
            {
                pictureCounter = 1;
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Animate();
        }
    }

}
