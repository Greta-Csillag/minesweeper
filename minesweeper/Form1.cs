using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesweeper
{
    
    struct Cella
    {
        public int Line;
        public int Row;
        public Cella(int line, int row)
        {
            Line = line;
            Row = row;
        }
    }
    public partial class Form1 : Form
    {
        public int hossz;
        public int akna;
        public PictureBox tiz = new PictureBox();
        public PictureBox zaszlo = new PictureBox();
        public PictureBox bomba = new PictureBox();
        public bool vesztett = false;
        public bool nyertes = false;
        public int mezok = 0;
        public int min = 0;
        public int sec = 0;
        public int[,] aknamező;
        public PictureBox[,] kepek;
        public int zaszlo_szam;
        public PictureBox button2;
        public Label label4;
        public Label label5;
        public Form1()
        {
            InitializeComponent();
     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hossz = Convert.ToInt32(numericUpDown1.Value);
            akna = Convert.ToInt32(numericUpDown2.Value);
            if (akna < hossz * hossz)
            {
                alap();
                timer1.Start();
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                numericUpDown1.Visible = false;
                numericUpDown2.Visible = false;
                button1.Visible = false;
                label6.Visible = false;
                
            }
            else
            {
                label6.Visible = true;
            }
            
        }


        private void alap()
        {
            aknamező = new int[hossz, hossz];
            kepek = new PictureBox[hossz, hossz];
            zaszlo_szam = akna;

            //felső sor létrehozása
            button2 = new PictureBox();
            button2.SizeMode = PictureBoxSizeMode.StretchImage;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            Bitmap emoji1 = new Bitmap("emoji_1.png");
            button2.BackgroundImage = (Image)emoji1;
            button2.Left = 300;
            button2.Top = 20;
            button2.Width = 25;
            button2.Height = 25;
            button2.Click += button2_Click;
            this.Controls.Add(button2);

            PictureBox picturebox1 = new PictureBox();
            picturebox1.SizeMode = PictureBoxSizeMode.StretchImage;
            picturebox1.BackgroundImageLayout = ImageLayout.Stretch;
            Bitmap bitmap4 = new Bitmap("12.png");
            zaszlo.Image = (Image)bitmap4;
            picturebox1.BackgroundImage =zaszlo.Image;
            picturebox1.Left = 50;
            picturebox1.Top = 20;
            picturebox1.Width = 25;
            picturebox1.Height = 25;
            this.Controls.Add(picturebox1);

            label4 = new Label();
            label4.Text = ($"{akna}");
            label4.Left = 80;
            label4.Top = 20;
            label4.Width = 25;
            label4.Height = 25;
            this.Controls.Add(label4);

            PictureBox picturebox2 = new PictureBox();
            picturebox2.SizeMode = PictureBoxSizeMode.StretchImage;
            picturebox2.BackgroundImageLayout = ImageLayout.Stretch;
            Bitmap bitmap5 = new Bitmap("11.png");
            picturebox2.BackgroundImage = (Image)bitmap5;
            picturebox2.Left = 450;
            picturebox2.Top = 20;
            picturebox2.Width = 25;
            picturebox2.Height = 25;
            this.Controls.Add(picturebox2);


            label5 = new Label();
            label5.Text = "";
            label5.Left = 475;
            label5.Top = 20;
            label5.Width = 50;
            label5.Height = 25;
            this.Controls.Add(label5);




            int sor;
            int oszlop;
            Random r = new Random();
            for (int i = 0; i < akna; i++)
            {
                sor = r.Next(0, hossz);
                oszlop = r.Next(0, hossz);
                while (aknamező[sor, oszlop] == -1)
                {
                    sor = r.Next(0, hossz);
                    oszlop = r.Next(0, hossz);
                }
                aknamező[sor, oszlop] = -1;
            }
            for (int i = 0; i < hossz; i++)
            {
                for (int j = 0; j < hossz; j++)
                {
                    if (aknamező[i, j] == -1)
                    {
                        for (int k = -1; k < 2; k++)
                        {
                            for (int l = -1; l < 2; l++)
                            {
                                if (i - k >= 0 && i - k <= hossz - 1 && j - l >= 0 && j - l <= hossz - 1)
                                {
                                    if (aknamező[i - k, j - l] != -1)
                                    {
                                        aknamező[i - k, j - l]++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            kepbeolvaso();
        }
        private void kepbeolvaso()
        {

            Bitmap bitmap2 = new Bitmap("10.png");
            tiz.Image = (Image)bitmap2;
            Bitmap akna = new Bitmap("0.png");
            bomba.Image = (Image)akna;
            for (int i = 0; i < hossz; i++)
            {
                for (int j = 0; j < hossz; j++)
                {

                    PictureBox picture = new PictureBox();
                    picture.SizeMode = PictureBoxSizeMode.StretchImage;
                    if (hossz > 15)
                    {
                        picture.Left = 50 + j * 25;
                        picture.Top = 50 + i * 25;
                        picture.Width = 25;
                        picture.Height = 25;
                    }
                    else
                    {
                        picture.Left = 50 + j * 50;
                        picture.Top = 50 + i * 50;
                        picture.Width = 50;
                        picture.Height = 50;
                    }
                    Cella cella = new Cella(i, j);
                    picture.Tag = cella;
                    kepek[i, j] = picture;


                    picture.Image = (Image)bitmap2;


                    this.Controls.Add(picture);
                    picture.Click += kep_klikk;
                }
            }

        }
        public void kep_klikk(object sender, System.EventArgs e)
        {
            if (vesztett == false && nyertes == false)
            {
                PictureBox clicked_pd = (PictureBox)sender;
                Cella c = (Cella)(clicked_pd.Tag);
                MouseEventArgs me = (MouseEventArgs)e;
                if (me.Button == MouseButtons.Right)
                {
                    Zaszlo(c.Line, c.Row);
                }
                if (me.Button == MouseButtons.Left)
                {
                    Felfedes(c.Line, c.Row);
                }
            }
        }
        public void Felfedes(int sor, int oszlop)
        {
            if (kepek[sor, oszlop].Image == tiz.Image)
            {
                int a = aknamező[sor, oszlop];
                if (a == -1)
                {
                    for (int i = 0; i < hossz; i++)
                    {
                        for (int j = 0; j < hossz; j++)
                        {
                            if (aknamező[i, j] == -1)
                            {
                                kepek[i, j].Image = bomba.Image;
                                vesztett = true;
                                Bitmap emoji3 = new Bitmap("emoji_3.png");
                                button2.BackgroundImage = (Image)emoji3;
                                timer1.Stop();
                            }
                        }
                    }
                }
                else
                {
                    Bitmap bitmap1 = new Bitmap($"{a + 1}.png");
                    kepek[sor, oszlop].Image = (Image)bitmap1;
                    mezok++;
                    if (mezok ==(hossz*hossz)-akna)
                    {
                        nyertes = true;
                        Bitmap emoji2 = new Bitmap("emoji_2.png");
                        button2.BackgroundImage = (Image)emoji2;
                        timer1.Stop();
                    }
                }
                if (aknamező[sor, oszlop] == 0)
                {
                    for (int k = -1; k < 2; k++)
                    {
                        for (int l = -1; l < 2; l++)
                        {
                            if (sor - k >= 0 && sor - k <= hossz - 1 && oszlop - l >= 0 && oszlop - l <= hossz - 1)
                            {
                                if (aknamező[sor - k, oszlop - l] != -1)
                                {
                                    Felfedes(sor - k, oszlop - l);
                                }
                            }
                        }
                    }
                }
            }
        }
        public void Zaszlo(int sor, int oszlop)
        {
            if (kepek[sor, oszlop].Image == tiz.Image)
            {
                kepek[sor, oszlop].Image = zaszlo.Image;
                zaszlo_szam--;
                label4.Text = ($"{zaszlo_szam}");
            }
            else if (kepek[sor, oszlop].Image == zaszlo.Image)
            {
                kepek[sor, oszlop].Image = tiz.Image;
                zaszlo_szam++;
                label4.Text = ($"{zaszlo_szam}");
            }
        }


        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            min = 0;
            sec = 0;
            timer1.Start();
            nyertes = false;
            vesztett = false;
            mezok = 0;
            alap();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sec++;
            if (sec == 60)
            {
                min++;
                sec = 0;
            }
            label5.Text = ($"{min}:{sec}");
        }
        
    }
}


// az első csak üres lehet