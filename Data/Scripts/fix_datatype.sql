-- fixes wrong datatype for Beitrag Inhalt

USE BuenzliTreff
GO

ALTER TABLE Beitrag
DROP COLUMN [Inhalt];

ALTER TABLE Beitrag
ADD [Inhalt] [varchar](max) NOT NULL;