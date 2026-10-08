using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // İzləmə rejimlərini Zoom edirik
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            // Yuxarıdakı PictureBox ilk açılışda boş (ağ) olur
            pictureBox2.Image = null;

            // Aşağıdakı PictureBox1-ə sabit tərəzi şəklini qoyuruq
            pictureBox1.Image = Properties.Resources.weight;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Mətn qutularından dəyərlərin oxunması
            string cekiMetn = textBox2.Text.Trim();
            string boyMetn = textBox3.Text.Trim();

            // Rəqəm olub-olmadığının yoxlanılması
            if (!double.TryParse(boyMetn, out double boySm) || !double.TryParse(cekiMetn, out double ceki))
            {
                MessageBox.Show("Lütfən boy və çəki göstəricilərini rəqəmlə daxil edin!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Boy məntiqi limit yoxlanışı (50 - 250 sm)
            if (boySm < 50 || boySm > 250)
            {
                MessageBox.Show("Boy göstəricisi reallığa uyğun deyil! Lütfən düzgün boy daxil edin (50 - 250 sm).", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Çəki məntiqi limit yoxlanışı (minimum 20 kg)
            if (ceki < 20)
            {
                MessageBox.Show("Çəki həddindən artıq azdır! Lütfən düzgün çəki daxil edin (Minimum 20 kg).", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. BKİ Hesablanması
            double boyM = boySm / 100.0;
            double bki = ceki / (boyM * boyM);

            // BKİ nəticəsini göstəririk
            label4.Text = $"{bki:F1}";

            // 3. Düyməyə basıldıqdan sonra yuxarıdakı pictureBox2-yə uyğun adam şəklini yükləyirik
            if (bki < 18.5)
            {
                label5.Text = "Çəki azlığınız var (Arıq).";
                label5.ForeColor = Color.Orange;
                pictureBox2.Image = Properties.Resources.Underweight;
            }
            else if (bki >= 18.5 && bki <= 24.9)
            {
                label5.Text = "Çəkiniz normaldır.";
                label5.ForeColor = Color.Green;
                pictureBox2.Image = Properties.Resources.normal;
            }
            else if (bki >= 25.0 && bki <= 32.0)
            {
                label5.Text = "Artıq çəkiniz var.";
                label5.ForeColor = Color.DarkOrange;
                pictureBox2.Image = Properties.Resources.Overweight;
            }
            else if (bki >= 33.0 && bki <= 39.0)
            {
                label5.Text = "1-ci dərəcəli piylənmə (Obez).";
                label5.ForeColor = Color.Red;
                pictureBox2.Image = Properties.Resources.obez;
            }
            else
            {
                label5.Text = "3-cü dərəcəli (Morbid) piylənmə.";
                label5.ForeColor = Color.DarkRed;
                pictureBox2.Image = Properties.Resources.morbid;
            }
        }
    }
}