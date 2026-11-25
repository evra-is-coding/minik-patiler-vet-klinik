# minik-patiler-vet-klinik
# WEB PROGRAMLAMA 2026 - 2027 PROJE ÖDEVİ 
# Veteriner Kliniği Randevu ve Hasta Takip Sistemi 

## Proje Künyesi
* **Geliştiriciler:**
  * Evra Durmaz - B241200072
  * Khadra Khoder - B231200576
* **GitHub Deposu:** https://github.com/evra-is-coding/minik-patiler-vet-klinik

## Proje Tanımı
Bu proje, **ASP.NET Core MVC (.NET 10 LTS)** kullanılarak geliştirilen web tabanlı bir **Veteriner Kliniği Randevu ve Hasta Takip Sistemi**dir. Uygulama; veteriner kliniklerinin sunduğu hizmetleri, veteriner hekimlerin uzmanlık alanlarını ve müsaitlik saatlerini, hayvan sahiplerinin evcil hayvan ve aşı kayıtlarını ile randevu süreçlerini uçtan uca yönetmeyi amaçlamaktadır.

## Temel Özellikler (Planlanan)
* **Klinik ve Hizmet Yönetimi:** Klinik çalışma saatleri, sunulan hizmetler, süre ve ücret tanımlamaları.
* **Veteriner Hekim Yönetimi:** Hekim uzmanlık alanları, verebildikleri hizmetler ve çalışma/müsaitlik saatleri.
* **Hayvan Sahibi ve Evcil Hayvan Takibi:** Üye kaydı, evcil hayvan profilleri, aşı takvimi kayıtları ve hekim muayene notları.
* **Randevu Sistemi:** Hekim müsaitliği ve hizmet süresine göre çakışma kontrollü randevu oluşturma ve onay/red mekanizması.
* **REST API & Raporlama:** LINQ sorguları ile filtrelenmiş hekim, aşı geçmişi ve ciro raporlama uçları.
* **Yapay Zekâ (MCP) Entegrasyonu:** uygun_saatler, asi_takvimi ve randevu_olustur araçlarını sunan yetkilendirilmiş MCP sunucusu.

## Kullanılan Teknolojiler
* **Backend:** ASP.NET Core MVC (.NET 10 LTS), C#, Entity Framework Core (Code First, Migration), LINQ
* **Veritabanı:** SQL Server 
* **Kimlik Doğrulama & Yetkilendirme:** ASP.NET Core Identity (Admin , Veteriner , Üye rolleri)
* **Yapay Zekâ Entegrasyonu:** ModelContextProtocol.AspNetCore (MCP C# SDK)
* **Frontend:** Bootstrap 5, HTML5, CSS3, JavaScript, jQuery

*Not: Veritabanı ER diyagramı, uygulama ekran görüntüleri, REST API ve MCP test çıktıları geliştirme süreci tamamlandıkça bu rapora eklenecektir.*

## Veritabanı Modeli (ER Diyagramı) 
<img width="5927" height="5960" alt="Veterinary Clinic Management Model-2026-11-25-093321" src="https://github.com/user-attachments/assets/8f5ea8cf-abb9-4001-8b5e-738fb2fc68ba" /> 
Sistemin veritabanı modeli; klinik yönetimi, hekim müsaitlik ve hizmet eşleştirmeleri, evcil hayvan/aşı takibi ve randevu süreçlerini ilişkisel olarak kurgulayacak şekilde tasarlanmıştır.
**Temel Varlıklar (Entities)**
ApplicationUser: ASP.NET Core Identity yapısına dayanan kullanıcı hesabıdır (Admin, Veteriner, Üye rollerini ve kullanıcı bilgilerini tutar).
Clinic: Veteriner kliniğinin adı, adres, telefon ve günlük çalışma saatleri (OpeningTime, ClosingTime) bilgilerini saklar.
ClinicService: Kliniğin sunduğu hizmetleri (muayene, aşı, kısırlaştırma vb.), hizmet süresini (DurationMinutes) ve ücretini (Price) tutar.
Veterinarian: Hekim bilgilerini, uzmanlık alanını (Specialty) ve ilgili klinik / kullanıcı hesabını ilişkilendirir.
VeterinarianService: Veteriner hekim ile verebildiği hizmetler arasındaki çoktan çoka ilişkiyi kuran bağlantı tablosudur .
VeterinarianAvailability: Hekimin randevu verilebilecek uygun zaman aralıklarını (StartTime, EndTime) tanımlar.
Pet: Üyeye (hayvan sahibine) ait evcil hayvanın ad, tür, ırk, doğum tarihi, kilo ve isteğe bağlı fotoğraf bilgilerini tutar.
VaccineRecord: Evcil hayvanın uygulanan aşılarını (VaccineName, AdministeredDate) ve bir sonraki doz tarihini (NextDueDate) kaydeder.  
Appointment: Evcil hayvan, hekim ve hizmet bazında oluşturulan randevu zamanını, onay durumunu (Status) ve tamamlanan randevulara girilen muayene notunu (ExaminationNote) saklar.   
**Başlıca İlişkiler**
Bir klinik (Clinic) birden fazla hizmet (ClinicService) sunabilir ve birden fazla veteriner hekim (Veterinarian) çalıştırabilir. 
Veteriner hekimler ve hizmetler VeterinarianService üzerinden eşleşir; her hekimin kendi VeterinarianAvailability kayıtları bulunur.  
Bir üye (ApplicationUser) birden fazla evcil hayvana (Pet) sahip olabilir; her evcil hayvanın birden fazla aşı kaydı (VaccineRecord) ve randevusu (Appointment) olabilir. 
Her randevu (Appointment) bir evcil hayvan (Pet), bir hekim (Veterinarian) ve bir hizmet (ClinicService) ile ilişkilidir.   


