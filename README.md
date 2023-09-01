# projekat-bruto

# zadaci

Ogranicenja
 - Koristi .NET Web okruzenje C#, kreirati Web aplikaciju sa odgovarajućim frontend-om u skladu sa user stories
 - Koristi MSSQL bazu podataka

User stories:
- Kako korisnik želim da unesem - snimim osnovne informacije o radniku (ime, prezime, adresa, iznos neto plate, radnu poziciju).
- Kao korisnik želim da imam uvid, pregled u informacije o svim radnicima koji se nalaze u bazi.
- Kao korisnik želim da informacije o svim radnicima izvezem, exportujem u format: .xlsx, .csv.
- Kao korisnik želim da imam uvid u detaljan profil radnika koji obuhvata osnovne informacije o radniku i najbitnije detaljnu specifikaciju obračuna bruto plate, kao iznos neto plate.
- Kao korisnik želim da detalje o izračunavanju bruto iznosa, konvertujem u sledeće valute (EUR, USD), podrazumevana valuta je RSD, koristiti API (https://github.com/public-api-lists/public-api-lists#currency-exchange)
- Kao korisnik, želim da informacije koje se nalaze na profilu radnika sa kompletnim postupkom konverzije neto u bruto platu izvezem – exportujem u PDF format.

# databaza koriscena
CREATE TABLE [dbo].[clients] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [ime_prezime] VARCHAR (100) NOT NULL,
    [radno_mesto] VARCHAR (150) NOT NULL,
    [bruto_neto]  VARCHAR (20)  NULL,
    [prva_vrednost] VARCHAR (20) NULL,
    [adresa]      VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


INSERT INTO clients (ime_prezime, radno_mesto, bruto_neto, adresa)
VALUES
('Bill Gates', 'banka', '200000000', 'New York, USA'),
('Elon Musk', 'skupstina', '200000000', 'Florida, USA'),
('Will Smith', 'glumac', '200000000', 'California, USA'),
('Bob Marley', 'muzicar', '200000000', 'Texas, USA'),
('Cristiano Ronaldo', 'fudbaler', '200000000', 'Manchester, England')
