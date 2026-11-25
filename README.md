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
