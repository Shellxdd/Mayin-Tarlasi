using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mayın_Tarlası
{
    public partial class Form1 : Form
    {
        Button[,] butonlar;

        Random rnd = new Random();

        Label lblBayrak;

        MenuStrip menuStrip;
        ToolStripMenuItem ayarlarMenu;

        bool ilkTiklama = true;
        bool oyunBitti = false;

        int satirSayisi = 10;
        int sutunSayisi = 10;
        int mayinSayisi = 10;

        int acilanKareSayisi = 0;
        int bayrakSayisi = 0;

        public Form1()
        {
            InitializeComponent();

            btnSmile.Click += btnSmile_Click;

            ArayuzuHazirla();
            OyunuBaslat();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void ArayuzuHazirla()
        {
            this.Text = "Mayın Tarlası";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            menuStrip = new MenuStrip();

            ayarlarMenu = new ToolStripMenuItem("Ayarlar");
            ToolStripMenuItem yeniOyunMenu = new ToolStripMenuItem("Yeni Oyun");
            ToolStripMenuItem cikisMenu = new ToolStripMenuItem("Çıkış");

            yeniOyunMenu.Click += (s, e) => OyunuSifirla();
            ayarlarMenu.Click += AyarlarMenu_Click;
            cikisMenu.Click += (s, e) => Application.Exit();

            menuStrip.Items.Add(ayarlarMenu);
            menuStrip.Items.Add(yeniOyunMenu);
            menuStrip.Items.Add(cikisMenu);

            this.Controls.Add(menuStrip);

            lblBayrak = new Label();
            lblBayrak.AutoSize = true;
            lblBayrak.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblBayrak.Location = new Point(10, 35);
            this.Controls.Add(lblBayrak);

            btnSmile.Text = "😄";
            btnSmile.Font = new Font("Segoe UI Emoji", 14);
            btnSmile.Size = new Size(55, 40);
            btnSmile.Location = new Point(150, 28);
        }

        private void OyunuBaslat()
        {
            ilkTiklama = true;
            oyunBitti = false;

            acilanKareSayisi = 0;
            bayrakSayisi = 0;

            btnSmile.Text = "😄";

            if (butonlar != null)
            {
                for (int i = 0; i < butonlar.GetLength(0); i++)
                {
                    for (int j = 0; j < butonlar.GetLength(1); j++)
                    {
                        if (butonlar[i, j] != null)
                        {
                            this.Controls.Remove(butonlar[i, j]);
                            butonlar[i, j].Dispose();
                        }
                    }
                }
            }

            butonlar = new Button[satirSayisi, sutunSayisi];

            int baslangicX = 10;
            int baslangicY = 75;

            for (int i = 0; i < satirSayisi; i++)
            {
                for (int j = 0; j < sutunSayisi; j++)
                {
                    Button btn = new Button();

                    btn.Location = new Point(
                        baslangicX + j * 40,
                        baslangicY + i * 40
                    );

                    btn.Size = new Size(40, 40);
                    btn.Tag = false;
                    btn.Text = "";
                    btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    btn.UseCompatibleTextRendering = true;

                    btn.MouseDown += Btn_MouseDown;

                    this.Controls.Add(btn);

                    butonlar[i, j] = btn;
                }
            }

            int genislik = sutunSayisi * 40 + 20;
            int yukseklik = satirSayisi * 40 + 110;

            this.ClientSize = new Size(
                Math.Max(genislik, 300),
                Math.Max(yukseklik, 180)
            );

            lblBayrak.Text = "Bayrak: " + mayinSayisi;
        }

        private void Btn_MouseDown(object? sender, MouseEventArgs e)
        {
            if (oyunBitti)
            {
                return;
            }

            if (sender is not Button tiklananButon)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                BayrakDegistir(tiklananButon);
                return;
            }

            if (tiklananButon.Text == "🚩")
            {
                return;
            }

            if (ilkTiklama)
            {
                ilkTiklama = false;

                int satir = -1;
                int sutun = -1;

                for (int i = 0; i < satirSayisi; i++)
                {
                    for (int j = 0; j < sutunSayisi; j++)
                    {
                        if (butonlar[i, j] == tiklananButon)
                        {
                            satir = i;
                            sutun = j;
                            break;
                        }
                    }

                    if (satir != -1)
                    {
                        break;
                    }
                }

                MayinlariOlustur(satir, sutun);
            }

            if (tiklananButon.Tag is bool mayinMi && mayinMi)
            {
                OyunBitti();
                return;
            }

            KareAc(tiklananButon);

            KazanmaKontrol();
        }

        private void MayinlariOlustur(int guvenliSatir, int guvenliSutun)
        {
            int olusturulanMayin = 0;

            while (olusturulanMayin < mayinSayisi)
            {
                int satir = rnd.Next(0, satirSayisi);
                int sutun = rnd.Next(0, sutunSayisi);

                if (satir >= guvenliSatir - 1 &&
                    satir <= guvenliSatir + 1 &&
                    sutun >= guvenliSutun - 1 &&
                    sutun <= guvenliSutun + 1)
                {
                    continue;
                }

                if (butonlar[satir, sutun].Tag is bool mayin && mayin)
                {
                    continue;
                }

                butonlar[satir, sutun].Tag = true;
                olusturulanMayin++;
            }
        }

        private void BayrakDegistir(Button buton)
        {
            if (buton.BackColor == Color.Green ||
                buton.BackColor == Color.Yellow)
            {
                return;
            }

            if (buton.Text == "🚩")
            {
                buton.Text = "";
                bayrakSayisi--;
            }
            else
            {
                if (bayrakSayisi >= mayinSayisi)
                {
                    return;
                }

                buton.Text = "🚩";
                bayrakSayisi++;
            }

            lblBayrak.Text =
                "Bayrak: " + (mayinSayisi - bayrakSayisi);
        }

        private void KareAc(Button buton)
        {
            if (buton.BackColor == Color.Green ||
                buton.BackColor == Color.Yellow)
            {
                return;
            }

            if (buton.Text == "🚩")
            {
                return;
            }

            int satir = -1;
            int sutun = -1;

            for (int i = 0; i < satirSayisi; i++)
            {
                for (int j = 0; j < sutunSayisi; j++)
                {
                    if (butonlar[i, j] == buton)
                    {
                        satir = i;
                        sutun = j;
                        break;
                    }
                }

                if (satir != -1)
                {
                    break;
                }
            }

            int komsuMayinSayisi = 0;

            for (int i = satir - 1; i <= satir + 1; i++)
            {
                for (int j = sutun - 1; j <= sutun + 1; j++)
                {
                    if (i >= 0 && i < satirSayisi &&
                        j >= 0 && j < sutunSayisi)
                    {
                        if (butonlar[i, j].Tag is bool mayinMi &&
                            mayinMi)
                        {
                            komsuMayinSayisi++;
                        }
                    }
                }
            }

            acilanKareSayisi++;

            if (komsuMayinSayisi == 0)
            {
                buton.BackColor = Color.Green;
                buton.Text = "";

                for (int i = satir - 1; i <= satir + 1; i++)
                {
                    for (int j = sutun - 1; j <= sutun + 1; j++)
                    {
                        if (i >= 0 && i < satirSayisi &&
                            j >= 0 && j < sutunSayisi)
                        {
                            Button komsu = butonlar[i, j];

                            if (komsu.Tag is bool komsuMayin &&
                                !komsuMayin &&
                                komsu.Text != "🚩" &&
                                komsu.BackColor != Color.Green &&
                                komsu.BackColor != Color.Yellow)
                            {
                                KareAc(komsu);
                            }
                        }
                    }
                }
            }
            else
            {
                buton.BackColor = Color.Yellow;
                buton.Text = komsuMayinSayisi.ToString();
            }
        }

        private void OyunBitti()
        {
            oyunBitti = true;

            btnSmile.Text = "💀";

            for (int i = 0; i < satirSayisi; i++)
            {
                for (int j = 0; j < sutunSayisi; j++)
                {
                    Button buton = butonlar[i, j];

                    if (buton.Tag is bool mayinMi && mayinMi)
                    {
                        buton.BackColor = Color.Red;
                        buton.Text = "💣";
                    }
                }
            }

            MessageBox.Show(
                "Mayına bastın!",
                "Oyun Bitti"
            );
        }

        private void KazanmaKontrol()
        {
            int toplamKare = satirSayisi * sutunSayisi;
            int guvenliKareSayisi = toplamKare - mayinSayisi;

            if (acilanKareSayisi >= guvenliKareSayisi)
            {
                oyunBitti = true;

                btnSmile.Text = "😎";

                MessageBox.Show(
                    "Tebrikler! Oyunu kazandın!",
                    "Kazandın!"
                );
            }
        }

        private void btnSmile_Click(object sender, EventArgs e)
        {
            OyunuSifirla();
        }

        private void OyunuSifirla()
        {
            OyunuBaslat();
        }

        private void AyarlarMenu_Click(object? sender, EventArgs e)
        {
            using Form ayarlar = new Form();

            ayarlar.Text = "Oyun Ayarları";
            ayarlar.StartPosition = FormStartPosition.CenterParent;
            ayarlar.FormBorderStyle = FormBorderStyle.FixedDialog;
            ayarlar.MaximizeBox = false;
            ayarlar.MinimizeBox = false;
            ayarlar.ClientSize = new Size(300, 260);

            Label lblSatir = new Label();
            lblSatir.Text = "Satır sayısı:";
            lblSatir.Location = new Point(20, 20);
            lblSatir.AutoSize = true;

            NumericUpDown numSatir = new NumericUpDown();
            numSatir.Minimum = 5;
            numSatir.Maximum = 30;
            numSatir.Value = satirSayisi;
            numSatir.Location = new Point(160, 18);

            Label lblSutun = new Label();
            lblSutun.Text = "Sütun sayısı:";
            lblSutun.Location = new Point(20, 65);
            lblSutun.AutoSize = true;

            NumericUpDown numSutun = new NumericUpDown();
            numSutun.Minimum = 5;
            numSutun.Maximum = 30;
            numSutun.Value = sutunSayisi;
            numSutun.Location = new Point(160, 63);

            Label lblMayinSayisi = new Label();
            lblMayinSayisi.Text = "Mayın sayısı:";
            lblMayinSayisi.Location = new Point(20, 110);
            lblMayinSayisi.AutoSize = true;

            NumericUpDown numMayin = new NumericUpDown();
            numMayin.Minimum = 1;
            numMayin.Maximum = 100;
            numMayin.Value = mayinSayisi;
            numMayin.Location = new Point(160, 108);

            Button btnUygula = new Button();
            btnUygula.Text = "Uygula";
            btnUygula.Size = new Size(100, 40);
            btnUygula.Location = new Point(100, 165);

            btnUygula.Click += (s, args) =>
            {
                int yeniSatir = (int)numSatir.Value;
                int yeniSutun = (int)numSutun.Value;
                int yeniMayin = (int)numMayin.Value;

                int toplamKare = yeniSatir * yeniSutun;

                if (yeniMayin >= toplamKare - 9)
                {
                    MessageBox.Show(
                        "Mayın sayısı çok fazla.\n" +
                        "İlk tıklama ve çevresindeki kareler güvenli olmalıdır."
                    );

                    return;
                }

                satirSayisi = yeniSatir;
                sutunSayisi = yeniSutun;
                mayinSayisi = yeniMayin;

                ayarlar.Close();

                OyunuBaslat();
            };

            ayarlar.Controls.Add(lblSatir);
            ayarlar.Controls.Add(numSatir);

            ayarlar.Controls.Add(lblSutun);
            ayarlar.Controls.Add(numSutun);

            ayarlar.Controls.Add(lblMayinSayisi);
            ayarlar.Controls.Add(numMayin);

            ayarlar.Controls.Add(btnUygula);

            ayarlar.ShowDialog(this);
        }
    }
}