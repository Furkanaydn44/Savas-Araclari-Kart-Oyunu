using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2.Classes
{
        abstract class SavasAraclari
        {

            public int seviyePuani { get; set; }

            public abstract int dayaniklilik { get; set; }
            public abstract int vurus { get; set; }
            public abstract string sinif { get; }


            public int xKoor;
            public int yKoor;
            public Label label {  get; set; }
            public PictureBox pictureBox { get; set; }


            public SavasAraclari(int seviyePuani)
            {
                this.seviyePuani = seviyePuani;
            }


            public virtual void KartPuaniGoster()
            {
            label = new Label
            {
                Text = $"Puan.: {seviyePuani}\nDayanıklılık.:{dayaniklilik}", 
                AutoSize = false,
                Width = pictureBox.Width+10,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            // Label'ı PictureBox'ın alt kısmına yerleştiriyoruz
            label.Location = new Point(pictureBox.Left, pictureBox.Bottom);

            // Form'un kontrol listesine eklenmesi gerekiyor
            if (pictureBox.Parent != null)
            {
                pictureBox.Parent.Controls.Add(label);
            }

            label.BringToFront(); // Label'ı öne getir

            // puanlabel sözlüğüne ekle
            

            label.BringToFront(); // Label'ı öne getiriyoruz
            }


            public abstract void DurumGuncelle(int dayaniklilik, int seviyePuani);



        }

        abstract class HavaAraclari : SavasAraclari
        {
            public HavaAraclari(int seviyePuani, string sinif) : base(seviyePuani)
            { }

            public abstract string altsinif { get; }
            public abstract int karaVurusAvantaji { get; }

            public override string sinif => "Hava";


            public override void KartPuaniGoster()
            {
                base.KartPuaniGoster();
            }


            public override void DurumGuncelle(int dayaniklilik, int seviyePuani)
            {
                this.dayaniklilik = dayaniklilik;
            
                this.seviyePuani = seviyePuani;

                this.label.Text = $"Puan.:0\nDayanıklılık.:{this.dayaniklilik}";
        }
            
        }


        abstract class KaraAraclari : SavasAraclari
        {

            public KaraAraclari(int seviyePuani, string sinif) : base(seviyePuani) { }



            public abstract string altsinif { get; }
            public abstract int denizVurusAvantaji { get; }

            public override string sinif => "Kara";

            public override void KartPuaniGoster()
            {
                base.KartPuaniGoster();

            }

            public override void DurumGuncelle(int dayaniklilik, int seviyePuani)
            {
                this.dayaniklilik = dayaniklilik;
                this.seviyePuani = seviyePuani;
            }



        }

        abstract class DenizAraclari : SavasAraclari
        {
            public abstract string altsinif { get; }

            public abstract int havaVurusAvantaji { get; }

            public override string sinif => "Deniz";
            public DenizAraclari(int seviyePuani, string sinif) : base(seviyePuani)
            { }


            public override void KartPuaniGoster()
            {
                base.KartPuaniGoster();

            }
            public override void DurumGuncelle(int dayaniklilik, int seviyePuani)
            {
                this.dayaniklilik = dayaniklilik;
                this.seviyePuani = seviyePuani;
            }
        }
        class Ucak : HavaAraclari
        {
            public override int dayaniklilik { get; set; } = 20;
            public override int vurus { get; set; } = 10;

            public string image = "ucakkart.png";

            public override int karaVurusAvantaji => 10;
            public override string altsinif => "Ucak";
            
            public Ucak(int seviyePuani, string sinif) : base(seviyePuani, sinif)
            {
            pictureBox = new PictureBox
            {
                Width = 100,
                Height = 150,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.ucakkart
                
            };
        }


            public override void KartPuaniGoster()
            {
                base.KartPuaniGoster();

            }

            public override void DurumGuncelle(int dayaniklilik, int seviyePuani)
            {
                this.dayaniklilik = dayaniklilik;
                this.seviyePuani = seviyePuani;
            
        }

        }
        class Obus : KaraAraclari
        {
            public override int dayaniklilik { get; set; } = 20;
            public override int vurus { get; set; } = 10;

            public string image = "obuskart.png";
            public override int denizVurusAvantaji => 5;

            public override string altsinif => "Obus";
            
            public Obus(int seviyePuani, string sinif) : base(seviyePuani, sinif)
            {
            pictureBox = new PictureBox
            {
                Width = 100,
                Height = 150,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.obuskart
            };

        }
            public override void KartPuaniGoster()
            {
                base.KartPuaniGoster();
                


        }

        public override void DurumGuncelle( int dayaniklilik, int seviyePuani)
            {
                this.dayaniklilik = dayaniklilik;
                this.seviyePuani = seviyePuani;
        }

        }
        class Firkateyn : DenizAraclari
        {
            public override int dayaniklilik { get; set; } = 25;
            public override int vurus { get; set; } = 10;

            public string image = "firkateyn.png";
            public override int havaVurusAvantaji => 5;

            public override string altsinif => "Firkateyn";
            
            public Firkateyn(int seviyePuani, string sinif) : base(seviyePuani, sinif)
            {
            pictureBox = new PictureBox
            {
                Width = 100,
                Height = 150,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.firkateynkart
            };
        }

            public override void KartPuaniGoster()
            {
                base.KartPuaniGoster();
            }
            public override void DurumGuncelle( int dayaniklilik, int seviyePuani)
            {
                this.dayaniklilik = dayaniklilik;
                
                this.seviyePuani = seviyePuani;
                
            }

        }
        class Siha : HavaAraclari
        {
            public override int dayaniklilik { get; set; } = 15;
            public override int vurus { get; set; } = 10;

            public string image = "sihakart.png";
            public override int karaVurusAvantaji => 10;

            public int denizVurusAvantaji = 10;


            public override string altsinif => "Siha";

            
            public Siha(int seviyePuani, string sinif) : base(seviyePuani, sinif)
            {
            pictureBox = new PictureBox
            {
                Width = 100,
                Height = 150,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.sihakart
            };
        }

            public override void KartPuaniGoster()
            {
                base.KartPuaniGoster();
            }
            public override void DurumGuncelle(int dayaniklilik, int seviyePuani)
            {
                this.dayaniklilik = dayaniklilik;
                this.seviyePuani = seviyePuani;
                
        }
        }

        class KFS : KaraAraclari
        {
            public override int dayaniklilik { get; set; } = 10;

            public override int vurus { get; set; } = 10;

            public string image = "kfskart.png";
            public override int denizVurusAvantaji => 10;
            public int havaVurusAvantaji = 20;

            public override string altsinif => "KFS";
            
            public KFS(int seviyePuani, string sinif) : base(seviyePuani, sinif)
            {
            pictureBox = new PictureBox
            {
                Width = 100,
                Height = 150,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.kfskart
            };
        }

            public override void KartPuaniGoster()
            {
                base.KartPuaniGoster();
            }
            public override void DurumGuncelle(int dayaniklilik, int seviyePuani)
            {
                this.dayaniklilik = dayaniklilik;
                this.seviyePuani = seviyePuani;
               
        }

        }

        class Sida : DenizAraclari
        {
            public override int dayaniklilik { get; set; } = 15;
            public override int vurus { get; set; } = 10;

            public string image = "sidakart.png";
            public override int havaVurusAvantaji => 10;

            public int karaVurusAvantaji = 10;

            public override string altsinif => "Sida";
            
            
            public Sida(int seviyePuani, string sinif) : base(seviyePuani, sinif)
            {
            pictureBox = new PictureBox
            {
                Width = 100,
                Height = 150,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.Sidakart
            };
        }

            public override void KartPuaniGoster()
            {
                base.KartPuaniGoster();
            }
            public override void DurumGuncelle(int dayaniklilik, int seviyePuani)
            {
                this.dayaniklilik = dayaniklilik;
                this.seviyePuani = seviyePuani;
             

        }
    }
        class Oyuncu
        {

            public int oyuncuID { get; set; }
            public string oyuncuAdi { get; set; }
            public int skor { get; set; }

            public List<SavasAraclari> kartListesi { get; set; }

            public Oyuncu()
            {
                oyuncuID = 0;
                oyuncuAdi = "Bilinmeyen";
                skor = 0;
                kartListesi = new List<SavasAraclari>();
            }


            public Oyuncu(int oyuncuID, string oyuncuAdi, int skor)
            {
                this.oyuncuID = oyuncuID;
                this.oyuncuAdi = oyuncuAdi;
                this.skor = skor;
                kartListesi = new List<SavasAraclari>();
            }
            public virtual void SkorGoster()
            {
                Console.WriteLine($"{oyuncuAdi} Skor: {skor}");
            }


            public virtual SavasAraclari KartSec()
            {
                return null;
            }
        }


        class BilgisayarOyuncu : Oyuncu
        {
            private Random random = new Random();

            public BilgisayarOyuncu(int oyuncuID, string oyuncuAdi) : base(oyuncuID, oyuncuAdi, 0)
            {
            }


            public override SavasAraclari KartSec()
            {
                if (kartListesi.Count > 0)
                {
                    int index = random.Next(kartListesi.Count);
                    SavasAraclari secilenKart = kartListesi[index];
                    return secilenKart;
                }
                return null;
            }
        }


        class KullaniciOyuncu : Oyuncu
        {

            public KullaniciOyuncu(int oyuncuID, string oyuncuAdi) : base(oyuncuID, oyuncuAdi, 0)
            {
            }


            public override SavasAraclari KartSec()
            {
                if (kartListesi.Count > 0)
                {
                    Console.WriteLine("Elinizdeki kartlar:");
                    for (int i = 0; i < kartListesi.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {kartListesi[i]}");
                    }

                    Console.Write("Bir kart numarasi seçin: ");
                    int secim = int.Parse(Console.ReadLine());

                    if (secim > 0 && secim <= kartListesi.Count)
                    {
                        SavasAraclari secilenKart = kartListesi[secim - 1];

                        return secilenKart;
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz seçim!");
                    }
                }
                return null;
            }
        }
    }


