using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Classes;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        private Dictionary<PictureBox, Point> eskikonumlar1 = new Dictionary<PictureBox, Point>();
        private Dictionary<PictureBox, Point> eskiKonumlar2 = new Dictionary<PictureBox, Point>();
        private Dictionary<string, Label> puanlabel = new Dictionary<string, Label>(); 
        List<int> secilenKartlar = new List<int>();

        private KullaniciOyuncu kullaniciOyuncu;
        private BilgisayarOyuncu bilgisayarOyuncu;
        List<SavasAraclari> deste;
        List<SavasAraclari> deste2;

        int tur = 1;
        int kartpuan;
        int maxtur;
        string gameid;
        public Form2(int a, int b, string c)
        {

            InitializeComponent();
            kartpuan = a; maxtur = b;
            gameid = c;

            kullaniciOyuncu = new KullaniciOyuncu(2, gameid);
            bilgisayarOyuncu = new BilgisayarOyuncu(1, "Bilgisayar");
            deste = new List<SavasAraclari> { };
            deste2 = new List<SavasAraclari> { };

    }

        private void KartlariOlusturK(SavasAraclari savasAraclari, int i)
        {
            int xKonum = 10;
            int yKonum = 700;
            int aralik = 10;

            for (int j = 0; j < i; j++)
            {
                xKonum += savasAraclari.pictureBox.Width + aralik;
            }

            savasAraclari.pictureBox.Location = new Point(xKonum, yKonum);



            eskikonumlar1[savasAraclari.pictureBox] = savasAraclari.pictureBox.Location;


            savasAraclari.pictureBox.MouseDown += PictureBox_MouseDown;
            savasAraclari.pictureBox.DragEnter += PictureBox_DragEnter;
           // savasAraclari.pictureBox.DragDrop += PictureBox_DragDrop;




            this.Controls.Add(savasAraclari.pictureBox);
  
            savasAraclari.KartPuaniGoster();
            

            Logger.Log($"{gameid} ye {savasAraclari.GetType().Name} kartı eklendi");

        }
        private void KartlariOlusturB(SavasAraclari savasAraclari, int i)
        {
            savasAraclari.xKoor = 10;
            savasAraclari.yKoor = 10;
            int aralik = 10;

            for (int j = 0; j < i; j++)
            {
                savasAraclari.xKoor += savasAraclari.pictureBox.Width + aralik;
            }



            savasAraclari.pictureBox.Image = Properties.Resources.bilinmeyenkart;

            savasAraclari.pictureBox.Location = new Point(savasAraclari.xKoor, savasAraclari.yKoor);






            this.Controls.Add(savasAraclari.pictureBox);
            Logger.Log($"Bilgisayara {savasAraclari.GetType().Name} kartı eklendi");


        }
        private void SilSavasAraci(SavasAraclari savasAraci)
        {
            string a = savasAraci.GetType().Name;

            if (savasAraci.pictureBox != null)
            {
                foreach (var panel in new Panel[] { panel1, panel2, panel3, panel4, panel5, panel6 })
                {
                    if (panel.Controls.Contains(savasAraci.pictureBox))
                    {
                        panel.Controls.Remove(savasAraci.pictureBox);
                        break;
                    }
                }
                this.Controls.Remove(savasAraci.pictureBox);
                savasAraci.pictureBox.Dispose();
                Logger.Log($"{a} kaldırıldı");
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        void SaldiriHesapla(SavasAraclari savasAraclari1, SavasAraclari savasAraclari2, Oyuncu kullanici, Oyuncu bilgisayar)
        {

            DayaniklilikHesapla(savasAraclari1, savasAraclari2, kullanici, bilgisayar);


            DayaniklilikHesapla(savasAraclari2, savasAraclari1, bilgisayar, kullanici);
        }

        void DayaniklilikHesapla(SavasAraclari saldiran, SavasAraclari savunma, Oyuncu oyuncu1, Oyuncu oyuncu2)
        {
            string p1 = null, p2 = null;
            p1 = oyuncu1.oyuncuAdi;
            p2 = oyuncu2.oyuncuAdi;
            string p1_card = saldiran.GetType().Name;
            string p2_card = savunma.GetType().Name;

            Logger.Log("=== Saldırı Durumu ===");
            Logger.Log($"Saldıran:{p1_card} - Dayanıklılık: {saldiran.dayaniklilik}, Seviye Puanı: {saldiran.seviyePuani}, Sınıf: {saldiran.sinif}");
            Logger.Log($"Savunan:{p2_card} - Dayanıklılık: {savunma.dayaniklilik}, Seviye Puanı: {savunma.seviyePuani}, Sınıf: {savunma.sinif}\n");

            int ekstraHasar = 0;
            

            switch (saldiran)
            {
                case HavaAraclari havaAraclari when savunma.sinif == "Kara":

                    ekstraHasar = havaAraclari.karaVurusAvantaji;
                    break;

                case HavaAraclari havaAraclari when savunma.sinif == "Deniz" && havaAraclari is Siha siha:

                    ekstraHasar = siha.denizVurusAvantaji;
                    break;

                case KaraAraclari karaAraclari when savunma.sinif == "Deniz":

                    ekstraHasar = karaAraclari.denizVurusAvantaji;
                    break;

                case KaraAraclari karaAraclari when savunma.sinif == "Hava" && karaAraclari is KFS kfs:

                    ekstraHasar = kfs.havaVurusAvantaji;
                    break;

                case DenizAraclari denizAraclari when savunma.sinif == "Hava":

                    ekstraHasar = denizAraclari.havaVurusAvantaji;
                    break;

                case DenizAraclari denizAraclari when savunma.sinif == "Kara" && denizAraclari is Sida sida:

                    ekstraHasar = sida.karaVurusAvantaji;
                    break;
            }


            int toplamHasar = saldiran.vurus + ekstraHasar;
            savunma.dayaniklilik -= toplamHasar;

            Logger.Log($"Toplam Hasar: {toplamHasar} (Ekstra Hasar: {ekstraHasar})");
            Logger.Log($"Savunan yeni dayanıklılık: {Math.Max(savunma.dayaniklilik, 0)}\n");


            if (savunma.dayaniklilik <= 0)
            {
                if (saldiran.dayaniklilik == 0)
                {
                    savunma.dayaniklilik = 0;
                    if (savunma.seviyePuani < 10)
                    {
                        saldiran.seviyePuani = saldiran.seviyePuani - savunma.seviyePuani;
                        saldiran.seviyePuani += 10;
                        oyuncu1.skor += 10;
                    }
                    else
                    {
                        saldiran.seviyePuani = saldiran.seviyePuani - savunma.seviyePuani;
                        saldiran.seviyePuani += savunma.seviyePuani;
                        oyuncu1.skor += savunma.seviyePuani;

                    }
                }
                else
                {
                    savunma.dayaniklilik = 0;
                    if (savunma.seviyePuani < 10)
                    {
                        saldiran.seviyePuani += 10;
                        oyuncu1.skor += 10;
                    }
                    else
                    {
                        saldiran.seviyePuani += savunma.seviyePuani;
                        oyuncu1.skor += savunma.seviyePuani;
                    }

                }
                saldiran.DurumGuncelle(saldiran.dayaniklilik, saldiran.seviyePuani);
                savunma.DurumGuncelle(savunma.dayaniklilik, savunma.seviyePuani);


                oyuncu2.kartListesi.Remove(savunma);
                SilSavasAraci(savunma);

                if (p2 != "Bilgisayar") 
                {
                    int kartID = GetKartID(savunma.pictureBox);
                    secilenKartlar.Remove(kartID);
                    savunma.label.Text = $"Puan.:0\nDayanıklılık.:0"; 
                }         


                Logger.Log("=== Sonuç ===");
                    Logger.Log($"Savunan {p2_card} kartı öldü!");
                    Logger.Log($"{p1_card} yeni seviye puanı: {saldiran.seviyePuani}");
                    Logger.Log($"{p1} yeni skoru: {oyuncu1.skor}\n");
                
            }
            else
            {
                Logger.Log("=== Sonuç ===");
                Logger.Log($"Savunan {p2_card} kartı hayatta!");
                Logger.Log($"{p2_card} kalan dayanıklılığı: {savunma.dayaniklilik}\n");
            }
        }





        private void Form2_Load(object sender, EventArgs e)
        {
            // 3 kullanıcı 4 bilgisayar
            label3.Text = $"Puan.: 0";
            label4.Text = $"Puan.: 0";
            label2.Text = gameid+":";
            for (int i = 0; i < 10; i++)
            {
                deste.Add(new Ucak(kartpuan, "Hava"));
                deste.Add(new Obus(kartpuan, "Kara"));
                deste.Add(new Firkateyn(kartpuan, "Deniz"));

            }


            for (int i = 0; i < 20; i++)
            {
                deste2.Add(new Ucak(kartpuan, "Hava"));
                deste2.Add(new Obus(kartpuan, "Kara"));
                deste2.Add(new Firkateyn(kartpuan, "Deniz"));
                deste2.Add(new Siha(kartpuan, "Hava"));
                deste2.Add(new KFS(kartpuan, "Kara"));
                deste2.Add(new Sida(kartpuan, "Deniz"));

            }

            Random random = new Random();

            for (int i = 0; i < deste.Count; i++)
            {
                int randomIndex = random.Next(deste.Count);
                var temp = deste[i];
                deste[i] = deste[randomIndex];
                deste[randomIndex] = temp;
            }

            for (int i = 0; i < deste.Count; i++)
            {
                int randomIndex = random.Next(deste2.Count);
                var temp = deste2[i];
                deste2[i] = deste2[randomIndex];
                deste2[randomIndex] = temp;
            }

            for (int i = 0; i < 6; i++)
            {
                kullaniciOyuncu.kartListesi.Add(deste[i]);
                KartlariOlusturK(kullaniciOyuncu.kartListesi[i], i);
            }
            for (int i = 0; i < 6; i++) 
            {
                bilgisayarOyuncu.kartListesi.Add(deste[i + 6]);
                 KartlariOlusturB(bilgisayarOyuncu.kartListesi[i], i);

            }


            Logger.Log("");

        }
        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.DoDragDrop(pictureBox, DragDropEffects.Move);

                if (kullaniciOyuncu.kartListesi.Count - secilenKartlar.Count <= 0)
                    for (int i = 0; i < kullaniciOyuncu.kartListesi.Count; i++)
                    {
                        if (kullaniciOyuncu.kartListesi[i].pictureBox.Location.Y == 700)
                        { 
                        int kartID = GetKartID(kullaniciOyuncu.kartListesi[i].pictureBox);
                        secilenKartlar.Remove(kartID);
                        kullaniciOyuncu.kartListesi[i].pictureBox.Enabled = true;
                        kullaniciOyuncu.kartListesi[i].pictureBox.BorderStyle = BorderStyle.None;
                        }
                    }
            }
        }
        private void PictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void PictureBox_DragDrop(object sender, DragEventArgs e)
        {
            var draggedPictureBox = e.Data.GetData(typeof(PictureBox)) as PictureBox;
            var targetPictureBox = sender as PictureBox;

            
            if (draggedPictureBox != null && targetPictureBox != null)
            {
                
                int kartID = GetKartID(draggedPictureBox);
                MessageBox.Show($"{kartID}");

                if (secilenKartlar.Contains(kartID))
                {
                    MessageBox.Show("Bu kart zaten seçildi ve tekrar seçilemez.");
                }
                else
                {
                    MessageBox.Show("a");

                    secilenKartlar.Add(kartID);

                    
                    draggedPictureBox.Enabled = false;

                    
                    draggedPictureBox.BorderStyle = BorderStyle.Fixed3D;

                    
                    draggedPictureBox.Location = targetPictureBox.Location;

                }

                if (secilenKartlar.Count >= kullaniciOyuncu.kartListesi.Count || (kullaniciOyuncu.kartListesi.Count- secilenKartlar.Count)<=2)
                {
                    secilenKartlar.Clear();
                    for(int i=0;i<kullaniciOyuncu.kartListesi.Count;i++)
                    {
                        kullaniciOyuncu.kartListesi[i].pictureBox.Enabled = true;
                        kullaniciOyuncu.kartListesi[i].pictureBox.BorderStyle = BorderStyle.None;
                    }
                }
            }
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            if (deckbutton.Enabled)
            {
                PictureBox draggedPictureBox = e.Data.GetData(typeof(PictureBox)) as PictureBox;

                if (draggedPictureBox != null)
                {
                    int kartID = GetKartID(draggedPictureBox);

                    if (secilenKartlar.Contains(kartID))
                    {              
                        MessageBox.Show("Bu kart zaten seçildi ve tekrar seçilemez.");
                        return;
                    }

                    secilenKartlar.Add(kartID);
                    draggedPictureBox.Enabled = false;
                    draggedPictureBox.BorderStyle = BorderStyle.Fixed3D;
                    if (panel1.Controls.Count > 0)
                    {
                        PictureBox existingPictureBox = panel1.Controls[0] as PictureBox;

                        if (existingPictureBox != null && eskikonumlar1.ContainsKey(existingPictureBox))
                        {
                            kartID = GetKartID(existingPictureBox);
                            secilenKartlar.Remove(kartID);
                            existingPictureBox.Enabled = true;
                            existingPictureBox.BorderStyle = BorderStyle.None;
                            Point eskiKonum = eskikonumlar1[existingPictureBox];
                            existingPictureBox.Location = eskiKonum;

                            panel1.Controls.Remove(existingPictureBox);

                            this.Controls.Add(existingPictureBox);
                        }
                    }

                    panel1.Controls.Add(draggedPictureBox);

                    draggedPictureBox.Location = new Point(0, 0);
                }
            }
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rebutton1_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count > 0)
            {
                
                PictureBox pictureBox = panel1.Controls[0] as PictureBox;
                int kartID = GetKartID(pictureBox);
                if (pictureBox != null && eskikonumlar1.ContainsKey(pictureBox))
                {
                    panel1.Controls.Remove(pictureBox);

                    Point eskiKonum = eskikonumlar1[pictureBox];
                    pictureBox.Location = eskiKonum;

                    this.Controls.Add(pictureBox);

                    secilenKartlar.Remove(kartID);
                    pictureBox.Enabled = true;
                    pictureBox.BorderStyle = BorderStyle.None;
                }
            }
        }



        private void panel2_DragDrop(object sender, DragEventArgs e)
        {
            if (deckbutton.Enabled)
            {
                PictureBox draggedPictureBox = e.Data.GetData(typeof(PictureBox)) as PictureBox;

                if (draggedPictureBox != null)
                {
                    int kartID = GetKartID(draggedPictureBox);

                    if (secilenKartlar.Contains(kartID))
                    {
                        MessageBox.Show("Bu kart zaten seçildi ve tekrar seçilemez.");
                        return;
                    }
                    secilenKartlar.Add(kartID);

                    // Kartı devre dışı bırak
                    draggedPictureBox.Enabled = false;
                    draggedPictureBox.BorderStyle = BorderStyle.Fixed3D;

                    if (panel2.Controls.Count > 0)
                    {
                        PictureBox existingPictureBox = panel2.Controls[0] as PictureBox;

                        if (existingPictureBox != null && eskikonumlar1.ContainsKey(existingPictureBox))
                        {
                            kartID = GetKartID(existingPictureBox);
                            secilenKartlar.Remove(kartID);
                            existingPictureBox.Enabled = true;
                            existingPictureBox.BorderStyle = BorderStyle.None;
                            Point eskiKonum = eskikonumlar1[existingPictureBox];
                            existingPictureBox.Location = eskiKonum;

                            panel2.Controls.Remove(existingPictureBox);

                            this.Controls.Add(existingPictureBox);
                        }
                    }

                    panel2.Controls.Add(draggedPictureBox);

                    draggedPictureBox.Location = new Point(0, 0);
                }
            }
        }
        private void panel2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void rebutton2_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = panel2.Controls[0] as PictureBox;
            int kartID = GetKartID(pictureBox);
            if (pictureBox != null && eskikonumlar1.ContainsKey(pictureBox))
            {
                panel2.Controls.Remove(pictureBox);

                Point eskiKonum = eskikonumlar1[pictureBox];
                pictureBox.Location = eskiKonum;

                this.Controls.Add(pictureBox);

                secilenKartlar.Remove(kartID);
                pictureBox.Enabled = true;
                pictureBox.BorderStyle = BorderStyle.None;
            }
        }
        private void panel3_DragDrop(object sender, DragEventArgs e)
        {
            if (deckbutton.Enabled)
            {
                PictureBox draggedPictureBox = e.Data.GetData(typeof(PictureBox)) as PictureBox;

                if (draggedPictureBox != null)
                {
                    int kartID = GetKartID(draggedPictureBox);

                    if (secilenKartlar.Contains(kartID))
                    {
                        MessageBox.Show("Bu kart zaten seçildi ve tekrar seçilemez.");
                        return;
                    }

                    secilenKartlar.Add(kartID);

                    draggedPictureBox.Enabled = false;

                    draggedPictureBox.BorderStyle = BorderStyle.Fixed3D;

                    if (panel3.Controls.Count > 0)
                    {
                        PictureBox existingPictureBox = panel3.Controls[0] as PictureBox;

                        if (existingPictureBox != null && eskikonumlar1.ContainsKey(existingPictureBox))
                        {
                            int eskiKartID = GetKartID(existingPictureBox);

                            if(secilenKartlar.Remove(eskiKartID)==false)
                                MessageBox.Show("A");

                            existingPictureBox.Enabled = true;
                            existingPictureBox.BorderStyle = BorderStyle.None;

                            Point eskiKonum = eskikonumlar1[existingPictureBox];
                            existingPictureBox.Location = eskiKonum;

                            panel3.Controls.Remove(existingPictureBox);

                            this.Controls.Add(existingPictureBox);
                        }
                    }

                    panel3.Controls.Add(draggedPictureBox);

                    draggedPictureBox.Location = new Point(0, 0);
                }
            }
        }

        private void panel3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void rebutton3_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = panel3.Controls[0] as PictureBox;
            int kartID = GetKartID(pictureBox);
            if (pictureBox != null && eskikonumlar1.ContainsKey(pictureBox))
            {
                panel3.Controls.Remove(pictureBox);

                Point eskiKonum = eskikonumlar1[pictureBox];
                pictureBox.Location = eskiKonum;

                this.Controls.Add(pictureBox);

                secilenKartlar.Remove(kartID);
                pictureBox.Enabled = true;
                pictureBox.BorderStyle = BorderStyle.None;
            }
        }
        private void deckbutton_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count > 0 && panel2.Controls.Count > 0 && panel3.Controls.Count > 0)
            {
                rebutton1.Enabled = false;
                rebutton2.Enabled = false;
                rebutton3.Enabled = false;
                deckbutton.Enabled = false;
                startbutton.Enabled = true;

                
                Random random = new Random();
                HashSet<int> secilenKartlar = new HashSet<int>();

                while (secilenKartlar.Count < 3)
                {
                    int kartIndex = random.Next(bilgisayarOyuncu.kartListesi.Count);
                    secilenKartlar.Add(kartIndex);
                }

                Panel[] paneller = { panel4, panel5, panel6 }; 

                int i = 0;
                foreach (int kartIndex in secilenKartlar)
                {
                    var kart = bilgisayarOyuncu.kartListesi[kartIndex];
                    if (!eskiKonumlar2.ContainsKey(kart.pictureBox))
                    {
                        eskiKonumlar2[kart.pictureBox] = kart.pictureBox.Location;
                    }

                    
                    if (kart.GetType().Name == "KFS")
                        kart.pictureBox.Image = Properties.Resources.kfskart;
                    if (kart.GetType().Name == "Obus")
                        kart.pictureBox.Image = Properties.Resources.obuskart;
                    if (kart.GetType().Name == "Sida")
                        kart.pictureBox.Image = Properties.Resources.Sidakart;
                    if (kart.GetType().Name == "Siha")
                        kart.pictureBox.Image = Properties.Resources.sihakart;
                    if (kart.GetType().Name == "Ucak")
                        kart.pictureBox.Image = Properties.Resources.ucakkart;
                    if (kart.GetType().Name == "Firkateyn")
                        kart.pictureBox.Image = Properties.Resources.firkateynkart;

                    kart.pictureBox.Parent = paneller[i]; 
                    kart.pictureBox.Location = new Point(0, 0); 
                    kart.pictureBox.Dock = DockStyle.Fill; 
                    kart.pictureBox.Refresh();
                    i++;
                }

            }
            else
            {
                MessageBox.Show("Kullanıcının kartları seçilmedi !");
                return;

            }
        }

        private void startbutton_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count > 0 && panel2.Controls.Count > 0 && panel3.Controls.Count > 0 &&
                panel4.Controls.Count > 0 && panel5.Controls.Count > 0 && panel6.Controls.Count > 0)
            {
                SavasAraclari kullaniciKart = KartBul(panel1, kullaniciOyuncu.kartListesi);
                SavasAraclari bilgisayarKart = KartBul(panel4, bilgisayarOyuncu.kartListesi);

                if (kullaniciKart != null && bilgisayarKart != null)
                {
                    // Saldırı hesapla fonksiyonunu çağır
                    SaldiriHesapla(kullaniciKart, bilgisayarKart, kullaniciOyuncu, bilgisayarOyuncu);
                }
                else
                {
                    MessageBox.Show("Her iki panelde de bir kart bulunmalıdır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                

                kullaniciKart = KartBul(panel2, kullaniciOyuncu.kartListesi);
                bilgisayarKart = KartBul(panel5, bilgisayarOyuncu.kartListesi);

                if (kullaniciKart != null && bilgisayarKart != null)
                {
                    // Saldırı hesapla fonksiyonunu çağır
                    SaldiriHesapla(kullaniciKart, bilgisayarKart, kullaniciOyuncu, bilgisayarOyuncu);
                }
                else
                {
                    MessageBox.Show("Her iki panelde de bir kart bulunmalıdır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                

                kullaniciKart = KartBul(panel3, kullaniciOyuncu.kartListesi);
                bilgisayarKart = KartBul(panel6, bilgisayarOyuncu.kartListesi);

                if (kullaniciKart != null && bilgisayarKart != null)
                {
                    SaldiriHesapla(kullaniciKart, bilgisayarKart, kullaniciOyuncu, bilgisayarOyuncu);
                }
                else
                {
                    MessageBox.Show("Her iki panelde de bir kart bulunmalıdır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                

                geriTasimak(panel1);                
                geriTasimak(panel2);
                geriTasimak(panel3);
                geriTasimab(panel4);
                geriTasimab(panel5);
                geriTasimab(panel6);

                label3.Text = $"Puan.: {kullaniciOyuncu.skor}";
                label4.Text = $"Puan.: {bilgisayarOyuncu.skor}";



                for (int i = 0; i < kullaniciOyuncu.kartListesi.Count; i++) 
                {
                    kullaniciOyuncu.kartListesi[i].label.Text = $"Puan.: {kullaniciOyuncu.kartListesi[i].seviyePuani}\nDayanıklılık.:{kullaniciOyuncu.kartListesi[i].dayaniklilik}";
                }

                this.Refresh();

                endbutton.Enabled = true;
                startbutton.Enabled = false;
            }



        }

        private SavasAraclari KartBul(Panel panel, List<SavasAraclari> kartListesi)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    return kartListesi.FirstOrDefault(kart => kart.pictureBox == pictureBox);
                }
            }
            return null;
        }

        private void geriTasimab(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    if (eskiKonumlar2.ContainsKey(pictureBox))
                    {
                        Point eskiKonum = eskiKonumlar2[pictureBox];
                        Size eskiBoyut = pictureBox.Size; 
                       
                        panel.Controls.Remove(pictureBox);
                        pictureBox.Image = Properties.Resources.bilinmeyenkart;
                        pictureBox.Refresh();
                        pictureBox.Dock = DockStyle.None;
                        pictureBox.Location = eskiKonum;
                        pictureBox.Width = 100;
                        pictureBox.Height = 150;

                        
                        this.Controls.Add(pictureBox);
                    }
                }
            }
        }
        private void geriTasimak(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    if (eskikonumlar1.ContainsKey(pictureBox))
                    {
                        Point eskiKonum = eskikonumlar1[pictureBox];
                        Size eskiBoyut = pictureBox.Size;

                        panel.Controls.Remove(pictureBox);

                        pictureBox.Dock = DockStyle.None;
                        pictureBox.Location = eskiKonum;
                        pictureBox.Width = 100;
                        pictureBox.Height = 150;
                        

                        this.Controls.Add(pictureBox);
                       
                    }
                }
            }
        }
        private int j = 0;
        private bool lastcard = false;
        private void endbutton_Click(object sender, EventArgs e)
        {
            if (kullaniciOyuncu.kartListesi.Count > 0 && bilgisayarOyuncu.kartListesi.Count > 0 && maxtur > tur && !lastcard)
            {


                if (kullaniciOyuncu.kartListesi.Count == 1)
                {
                    lastcard = true;
                    if (kullaniciOyuncu.skor >= 20)
                    {
                        MessageBox.Show($"{gameid} tek kartı kaldı. Son tur oynanacak.");
                        kullaniciOyuncu.kartListesi.Add(deste2[0 + j]);
                        KartlariOlusturK(kullaniciOyuncu.kartListesi[kullaniciOyuncu.kartListesi.Count - 1], 6 + j);
                        kullaniciOyuncu.kartListesi.Add(deste2[1 + j]);
                        KartlariOlusturK(kullaniciOyuncu.kartListesi[kullaniciOyuncu.kartListesi.Count - 1], 7 + j);


                    }
                    else
                    {
                        MessageBox.Show($"{gameid} tek kartı kaldı. Son tur oynanacak.");
                        kullaniciOyuncu.kartListesi.Add(deste[12 + j]);
                        KartlariOlusturK(kullaniciOyuncu.kartListesi[kullaniciOyuncu.kartListesi.Count - 1], 6 + j);

                        kullaniciOyuncu.kartListesi.Add(deste[13 + j]);
                        KartlariOlusturK(kullaniciOyuncu.kartListesi[kullaniciOyuncu.kartListesi.Count - 1], 7 + j);
                    }
                }
                else
                {

                    if (kullaniciOyuncu.skor >= 20)
                    {
                        kullaniciOyuncu.kartListesi.Add(deste2[0 + j]);
                        KartlariOlusturK(kullaniciOyuncu.kartListesi[kullaniciOyuncu.kartListesi.Count - 1], 6 + j);
                    }
                    else
                    {
                        kullaniciOyuncu.kartListesi.Add(deste[12 + j]);
                        KartlariOlusturK(kullaniciOyuncu.kartListesi[kullaniciOyuncu.kartListesi.Count - 1], 6 + j);
                    }
                }


                if (bilgisayarOyuncu.kartListesi.Count == 1)
                {
                    lastcard = true;
                    if (bilgisayarOyuncu.skor >= 20)
                    {
                        MessageBox.Show("Bilgisayarın tek kartı kaldı. Son tur oynanacak.");
                        bilgisayarOyuncu.kartListesi.Add(deste2[5 + j]);
                        KartlariOlusturB(bilgisayarOyuncu.kartListesi[bilgisayarOyuncu.kartListesi.Count - 1], 6 + j);
                        bilgisayarOyuncu.kartListesi.Add(deste2[6 + j]);
                        KartlariOlusturB(bilgisayarOyuncu.kartListesi[bilgisayarOyuncu.kartListesi.Count - 1], 7 + j);
                    }
                    else
                    {
                        MessageBox.Show("Bilgisayarın tek kartı kaldı. Son tur oynanacak.");
                        bilgisayarOyuncu.kartListesi.Add(deste[17 + j]);
                        KartlariOlusturB(bilgisayarOyuncu.kartListesi[bilgisayarOyuncu.kartListesi.Count - 1], 6 + j);
                        bilgisayarOyuncu.kartListesi.Add(deste[18 + j]);
                        KartlariOlusturB(bilgisayarOyuncu.kartListesi[bilgisayarOyuncu.kartListesi.Count - 1], 7 + j);
                    }
                }
                else
                {

                    if (bilgisayarOyuncu.skor >= 20)
                    {
                        bilgisayarOyuncu.kartListesi.Add(deste2[5 + j]);
                        KartlariOlusturB(bilgisayarOyuncu.kartListesi[bilgisayarOyuncu.kartListesi.Count - 1], 6 + j);
                    }
                    else
                    {
                        bilgisayarOyuncu.kartListesi.Add(deste[17 + j]);
                        KartlariOlusturB(bilgisayarOyuncu.kartListesi[bilgisayarOyuncu.kartListesi.Count - 1], 6 + j);
                    }
                }
                rebutton1.Enabled = true;
                rebutton2.Enabled = true;
                rebutton3.Enabled = true;
                deckbutton.Enabled = true;
                endbutton.Enabled = false;


                j++;
            }
            else
            {
                if (kullaniciOyuncu.skor == bilgisayarOyuncu.skor)
                {
                    Logger.Log($"{kullaniciOyuncu.skor}:{bilgisayarOyuncu.skor} Oyun bitti. Beraberlik !");
                    MessageBox.Show(kullaniciOyuncu.skor + ":" + bilgisayarOyuncu.skor + " Oyun bitti. Beraberlik");
                }
                else if (kullaniciOyuncu.skor > bilgisayarOyuncu.skor)
                {
                    Logger.Log($"{kullaniciOyuncu.skor}:{bilgisayarOyuncu.skor} Oyun bitti. {gameid} kazandı!");
                    MessageBox.Show(kullaniciOyuncu.skor + ":" + bilgisayarOyuncu.skor + " Oyun bitti. " + gameid + " kazandı");
                }
                else 
                {
                    Logger.Log($"{kullaniciOyuncu.skor}:{bilgisayarOyuncu.skor} Oyun bitti. Bilgisayar kazandı!");
                    MessageBox.Show(kullaniciOyuncu.skor + ":" + bilgisayarOyuncu.skor + "Oyun bitti. Bigisayar kazandı");
                }

                

                string logs = Logger.GetLogs();
                LogForm logForm = new LogForm(logs);
                logForm.ShowDialog();
                this.Hide();


            }
            tur++;
        }

        public static class Logger
        {
            private static List<string> logEntries = new List<string>();

            public static void Log(string message)
            {
                logEntries.Add($"{DateTime.Now}: {message}");
            }

            public static string GetLogs()
            {
                return string.Join(Environment.NewLine, logEntries);
            }

            public static void ClearLogs()
            {
                logEntries.Clear();
            }
        }

        private int GetKartID(PictureBox pictureBox)
        {
            int id = 1;
            if (eskikonumlar1[pictureBox].X <110)
                return 0;

            id = (eskikonumlar1[pictureBox].X - 10) / 110;
                return id;           
        }

    }
}
