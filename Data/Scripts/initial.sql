USE BuenzliTreff 
GO

CREATE TABLE [dbo].[User](
	[Id] [int] IDENTiTY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	CONSTRAINT PK_User PRIMARY KEY CLUSTERED (Id)
);

CREATE TABLE [dbo].[Beitrag](
	[Id] [int] IDENTiTY(1,1) NOT NULL,
	[Titel] [varchar](50) NOT NULL,
	[Inhalt] [varbinary](max) NOT NULL,
	[ErstelltAm] [date] NOT NULL DEFAULT GetDate(),
	[AutorId] [int] NOT NULL,
	CONSTRAINT FK_Autor_Beitraege FOREIGN KEY ([AutorId]) REFERENCES [dbo].[User] ([Id]),
	CONSTRAINT PK_Beitrag PRIMARY KEY CLUSTERED (Id)
);

CREATE TABLE [dbo].[Kommentar](
	[Id] [int] IDENTiTY(1,1) NOT NULL,
	[AutorId] [int] NOT NULL,
	[BeitragId] [int] NOT NULL,
	[Titel] [varchar](50) NOT NULL,
	[Inhalt] [varchar](max) NOT NULL,
	CONSTRAINT PK_Kommentar PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT FK_Autor_Kommentare FOREIGN KEY ([AutorId]) REFERENCES [dbo].[User] ([Id]),
	CONSTRAINT FK_Beitrag_Kommentare FOREIGN KEY ([BeitragId]) REFERENCES [dbo].[Beitrag] ([Id])
);

CREATE TABLE [dbo].[BeitragLike](
	[Id] [int] IDENTiTY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[BeitragId] [int] NOT NULL,
	[IstDislike] [int] NULL,
	CONSTRAINT PK_BeitragLike PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT FK_User_BeitragLikes FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
	CONSTRAINT FK_Beitrag_Likes FOREIGN KEY ([BeitragId]) REFERENCES [dbo].[Beitrag] ([Id])
);

CREATE TABLE [dbo].[KommentarLike](
	[Id] [int] IDENTiTY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[KommentarId] [int] NOT NULL,
	[IstDislike] [int] NULL,
	CONSTRAINT PK_KommentarLike PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT FK_User_KommentarLikes FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
	CONSTRAINT FK_Kommentar_Likes FOREIGN KEY ([KommentarId]) REFERENCES [dbo].[Kommentar] ([Id])
);

CREATE TABLE Datei{
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar(50)] NULL,
	[File] [varbinary(max)] NOT NULL,
	CONSTRAINT PK_Datei PRIMARY KEY CLUSTERED (Id)
};

CREATE TABLE BeitragDatei{
	[Id] [int] IDENTITY (1,1) NOT NULL,
	[BeitragId] [int] NOT NULL,
	[DateiId] [int] NOT NULL,
	CONSTRAINT PK_BeitragDatei PRIMARY KEY CLUSTERED (Id)
};