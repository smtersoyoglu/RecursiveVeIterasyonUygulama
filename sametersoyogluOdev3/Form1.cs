using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sametersoyogluOdev3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // standart sapma bulma adımları n tane girilen sayının ilk ortalamasını bulma, sonra varyansı bulma ortalama sonucunu girilen sayılardan tek tek cıkarma
        // cıkardıktan sonra her değerden gelen sonuçların karesini alma  karesi alındıktan sonra karesi alınan sayıları toplama = ortFarkKareTop, son olarak
        //Kareleri toplamını n-1'e bölme cıkan varyans değerinin karekök'ünü alma

        ArrayList sayiDizi = new ArrayList();

        private double girilenSayi;
        private double girSayiTop;
        private double ortalama;
        private double ortFarkKareTop;
        private double standartSapma;
        private double varyans;


        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtBox_Sayi.Text == "")
            {
                MessageBox.Show("Boş Bırakılamaz Sayı Girmeniz Gereklidir !");
            }
            else
            {
                sayiDizi.Add(int.Parse(txtBox_Sayi.Text));
                richTextBox_sayilar.Text += txtBox_Sayi.Text + " <---> ";
                txtBox_Sayi.Clear();
            }
        }

        public double recursiveOrtalamaBulma(int n)
        {
            if (n == 0)
            { return 0; }

            // n-1 sayı dizisinde ortalamayı bulma 
            double girilenSayi = double.Parse(sayiDizi[n - 1].ToString());
            return (recursiveOrtalamaBulma(n - 1) * (n - 1) + girilenSayi) / n;
        }

        public double recursiveToplamBulma(int n, double ortalama)
        {
            if (n == 0)
            { return 0; }

            double girilenSayi = double.Parse(sayiDizi[n - 1].ToString());
            //Math.Pow ile üs alma işlemini yapıyoruz (girilenSayi - ortalama) taban üst 2 oluyor karesini alma işlemi
            return Math.Pow(girilenSayi - ortalama, 2) + recursiveToplamBulma(n - 1, ortalama);

        }

        public double recursiveHesaplama()
        {

            int n = sayiDizi.Count;
            ortalama = recursiveOrtalamaBulma(n);
            ortFarkKareTop = recursiveToplamBulma(n, ortalama);
            varyans = 1 / (Convert.ToDouble(n) - 1) * ortFarkKareTop;
            //Math.Pow ile üs alma işlemini yapıyoruz (girilenSayi - ortalama) taban üst 2 oluyor karesini alma işlemi
            ortFarkKareTop += Math.Pow(girilenSayi - ortalama, 2);
            //Math.Sqrt ile son işlem olarak karakökünü alıyoruz. karakök hesaplama
            standartSapma = Math.Sqrt(varyans);
            standartSapma = Math.Round(standartSapma, 2);
            return standartSapma;

        }
        private void btnHesapla_Click(object sender, EventArgs e)
        {
            // iterasyon hesaplama
            int n = sayiDizi.Count;

            for (int i = 0; i < n; i++)
            {
                girilenSayi = double.Parse(sayiDizi[i].ToString());
                girSayiTop = girSayiTop + girilenSayi;
            }

            // ortalama bulma
            ortalama = girSayiTop / n;
            for (int i = 0; i < n; i++)
            {
                double girilenSayi = double.Parse(sayiDizi[i].ToString());
                //Math.Pow ile üs alma işlemini yapıyoruz (girilenSayi - ortalama) taban üst 2 oluyor karesini alma işlemi
                ortFarkKareTop = ortFarkKareTop + Math.Pow(girilenSayi - ortalama, 2);
            }
            //Math.Sqrt ile son işlem olarak karakökünü alıyoruz. karakök hesaplama
            standartSapma = Math.Sqrt(ortFarkKareTop / (n - 1));
            standartSapma = Math.Round(standartSapma, 2);
            txtBox_iterasyon.Text = standartSapma.ToString();

            // recursive hesaplama 
            txtBox_recursive.Text = recursiveHesaplama().ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            txtBox_Sayi.Clear();
            txtBox_recursive.Clear();
            txtBox_iterasyon.Clear();
            richTextBox_sayilar.Clear();

        }

    }
}
