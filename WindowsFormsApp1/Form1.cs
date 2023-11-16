using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lbl_over.Hide();
        }

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    // Khởi tạo VlcControl
        //    VlcControl vlcControl = new VlcControl();
        //    vlcControl.BeginInit();
        //    vlcControl.VlcLibDirectory = new DirectoryInfo(@"E:\Bài Tập FPTS\Plane\WindowsFormsApp1\packages");
        //    vlcControl.EndInit();

        //    // Thêm VlcControl vào Form
        //    Controls.Add(vlcControl);

        //    // Đặt đường dẫn của video
        //    string videoPath = @"E:\Video\nebula_red_-_31420 (Original)"; 

        //    vlcControl.SetMedia(new FileInfo(videoPath));
        //    vlcControl.Play();
        //}


        bool right, left, space;
        int score;

        void Game_Retsult() 
        {
            foreach (Control x in this.Controls)
            {
                foreach (Control i in this.Controls)
                {
                    if (x is PictureBox && x.Tag == "bullet")
                    {
                        if (i is PictureBox && i.Tag=="enemy")
                        {
                            if (x.Bounds.IntersectsWith(i.Bounds))
                            {
                                i.Top = -100;
                                ((PictureBox)x).Image = Properties.Resources.explosion;
                                score++;
                                lbl_score.Text ="Score:"+score;






                            }
                        }
                    }
                }
            }
            if (player.Bounds.IntersectsWith(ship.Bounds) ||player.Bounds.IntersectsWith(alien.Bounds))
            {
                timer1.Stop();
                lbl_over.Show();
                lbl_over.BringToFront();
            }
        }

        void Add_Bullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.SizeMode = PictureBoxSizeMode.AutoSize;
            bullet.Image = Properties.Resources.bullet_img2;
            bullet.BackColor = System.Drawing.Color.Transparent;
            bullet.Tag = "bullet";
            bullet.Left = player.Left + 10;
            bullet.Top = player.Top -15;
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        void Buleet_Movement()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "bullet")
                {
                    x.Top -= 10;
                    if (x.Top<100)
                    {
                        this.Controls.Remove(x);
                    }
                }
            }
        }

        void Enemy_Movement()
        {
            Random rnd = new Random();
            int x, y;
            if (alien.Top>=500)
            {
                x = rnd.Next(0,300);
                alien.Location = new Point(x,0);
            }
            if (ship.Top>=500)
            {
                y = rnd.Next(0, 300);
                ship.Location = new Point(y,0);
            }
            else
            {
                alien.Top += 15;
                ship.Top += 10;
            }
        }

        void Arrow_key_Movement()
        {
            if (right == true)
            {
                if (player.Left<425) 
                {
                    player.Left += 20;
                }
            }
            if (left == true)
            {
                if (player.Left>10)
                {
                    player.Left -= 20;
                }
            }
        }  

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.Right)
            {
                right = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                left = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                space = true;
                Add_Bullet();
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                right = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                left = false;
            }
            if (e.KeyCode == Keys.Space)
            {
                space = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Arrow_key_Movement();
            Enemy_Movement();
            Buleet_Movement();
            Game_Retsult();
        }

    }
}
