# projekat-bruto

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
