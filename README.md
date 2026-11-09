# ⚔️ Savaş Araçları Kart Oyunu (War Vehicles Card Game)

Bu proje, C# ve Windows Forms teknolojileri kullanılarak geliştirilmiş, nesne yönelimli programlama (OOP) prensiplerine dayanan stratejik bir kart savaşı oyunudur.

Kullanıcı (İnsan) ve Bilgisayar (Yapay Zeka) arasında geçen bu mücadelede, taraflar ellerindeki savaş aracı kartlarını sahaya sürerek birbirlerine üstünlük kurmaya çalışırlar. Oyun; şans, strateji ve sınıf avantajları üzerine kuruludur.

## 📖 Proje Hakkında

Oyun, Hava, Kara ve Deniz olmak üzere üç ana sınıfa ayrılmış savaş araçlarını temel alır. Her kartın kendine has vuruş gücü, dayanıklılığı ve sınıf avantajı bulunur. Proje, sadece bir oyun olmakla kalmayıp, arka planda **Kalıtım (Inheritance)**, **Çok Biçimlilik (Polymorphism)** ve **Kapsülleme (Encapsulation)** gibi temel yazılım mimarisi prensiplerini uygulamalı olarak göstermektedir.

## Proje Katılımcıları
<a href="https://github.com/numankhy28">
  <img src="https://avatars.githubusercontent.com/u/219421415?v=4" width="128" height="128" alt="numankhy28 GitHub Profile" />
</a>
<a href="https://github.com/Furkanaydn44">
  <img src="https://avatars.githubusercontent.com/u/174454978?v=4" width="128" height="128" alt="Furkanaydn44 GitHub Profile" />
</a>

## ✨ Temel Özellikler

* **Dinamik Kart Seçimi:**
    * **Kullanıcı:** Sürükle-bırak veya tıklama yöntemiyle interaktif seçim.
    * **Bilgisayar:** Rastgele (Random) seçim algoritması.
* **Sınıf Avantaj Sistemi:** Taş-Kağıt-Makas mantığına benzer bir üstünlük döngüsü (Hava > Kara > Deniz > Hava).
* **İlerlemeli Oyun Yapısı:**
    * Oyuncular 20 skoru geçtiğinde **"Elit Kartlar"** (SİHA, KFS, SİDA) desteye eklenir.
    * Tur sonunda kartı azalan oyuncuya ekstra kart çekme hakkı tanınır.
* **Detaylı Savaş Motoru:**
    * Vuruş gücü, dayanıklılık ve sınıf avantajı hesaplanarak hasar (damage) belirlenir.
    * Dayanıklılığı sıfırlanan kartlar "Empoze" olur ve elenir.
    * Kazanan kart, kaybeden kartın seviye puanını alarak güçlenir.
* **Görselleştirme:** Savaş sonuçları, kart durumları ve skorlar anlık olarak arayüzde güncellenir. Oyun sonunda detaylı bir log ekranı sunulur.

## 🎮 Oyun Mekanikleri ve Kurallar

Oyunun temel mantığı aşağıdaki hiyerarşiye dayanır:

### 1. Sınıf Üstünlükleri
* ✈️ **Hava Araçları** (Uçak, SİHA) -> 🚜 **Kara Araçlarına** (Obüs, KFS) karşı üstündür.
* 🚜 **Kara Araçları** (Obüs, KFS) -> 🚢 **Deniz Araçlarına** (Firkateyn, SİDA) karşı üstündür.
* 🚢 **Deniz Araçları** (Firkateyn, SİDA) -> ✈️ **Hava Araçlarına** (Uçak, SİHA) karşı üstündür.

### 2. Kart İstatistikleri
Her kartın aşağıdaki özellikleri bulunur:
* **Dayanıklılık:** Kartın can değeri. Hasar aldıkça azalır.
* **Vuruş:** Temel saldırı gücü (Varsayılan: 10).
* **Vuruş Avantajı:** Sınıf üstünlüğü durumunda eklenen ekstra hasar.
* **Seviye Puanı:** Kartın "değerini" belirleyen puan. Skor hesaplamasında kullanılır.

### 3. Oyun Akışı
1.  **Başlangıç:** Her oyuncuya 6 adet standart kart dağıtılır.
2.  **Seçim:** Kullanıcı ve Bilgisayar kartlarını seçer ve onaylar.
3.  **Savaş:** Kartlar karşılaştırılır, hasarlar hesaplanır ve dayanıklılıklar güncellenir.
4.  **Sonuç:** Dayanıklılığı biten kartlar elenir. Kalan kartlar desteye geri döner.
5.  **Skor Kontrolü:** Toplam skor 20'yi geçerse, sonraki turlarda daha güçlü kartlar gelmeye başlar.

## 🏗️ Yazılım Mimarisi (OOP Tasarımı)

Proje, genişletilebilir ve modüler bir yapıda tasarlanmıştır.

### Sınıf Hiyerarşisi
* **SavasAraclari (Abstract Base Class):** Tüm kartların atasıdır. Ortak özellikleri (Dayanıklılık, Vuruş vb.) tutar.
    * **HavaAraclari (Subclass):**
        * `Ucak`
        * `Siha` (Elit)
    * **KaraAraclari (Subclass):**
        * `Obus`
        * `KFS` (Elit)
    * **DenizAraclari (Subclass):**
        * `Firkateyn`
        * `Sida` (Elit)

* **Oyuncu (Base Class):**
    * `KullaniciOyuncu`: Kart seçimini arayüz üzerinden yapar.
    * `BilgisayarOyuncu`: Kart seçimini `Random` sınıfı ile yapar.

* **Oyun (Game Manager):** Oyun döngüsünü, deste yönetimini (`List<SavasAraclari>`) ve savaş algoritmalarını yöneten ana sınıftır.

## 🛠️ Teknik Altyapı

* **Dil:** C#
* **Framework:** .NET Framework (Windows Forms)
* **IDE:** Visual Studio / Visual Studio Code
* **Veri Yapıları:** `List<>` (Deste yönetimi), `Integer` (Skorlar), `String` (İsimlendirmeler).

## 🚀 Kurulum ve Çalıştırma

Projeyi yerel ortamınızda çalıştırmak için:

1.  Bu depoyu (repository) klonlayın.
2.  Projeyi **Visual Studio** ile açın (`.sln` dosyası).
3.  `Solution Explorer` üzerinden projeyi derleyin (Build).
4.  `Start` tuşuna basarak oyunu başlatın.
5.  Açılan ekranda tur sayısını belirleyip savaşa başlayın!

## 📄 Lisans

Bu proje açık kaynaklıdır ve eğitim amaçlı geliştirilmiştir. İstediğiniz gibi geliştirip katkıda bulunabilirsiniz.
