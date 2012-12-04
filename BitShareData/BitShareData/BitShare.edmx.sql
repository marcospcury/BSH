
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/04/2012 00:13:17
-- Generated from EDMX file: C:\Users\Cury\Documents\GitHub\BSH\BitShareData\BitShareData\BitShare.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BitShareDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UsuarioTorrent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Torrents] DROP CONSTRAINT [FK_UsuarioTorrent];
GO
IF OBJECT_ID(N'[dbo].[FK_TorrentArquivoTorrent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ArquivosTorrents] DROP CONSTRAINT [FK_TorrentArquivoTorrent];
GO
IF OBJECT_ID(N'[dbo].[FK_TorrentTorrentSeed]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TorrentSeeds] DROP CONSTRAINT [FK_TorrentTorrentSeed];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioTorrentSeed]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TorrentSeeds] DROP CONSTRAINT [FK_UsuarioTorrentSeed];
GO
IF OBJECT_ID(N'[dbo].[FK_TorrentTorrentLeech]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TorrentLeeches] DROP CONSTRAINT [FK_TorrentTorrentLeech];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioTorrentLeech]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TorrentLeeches] DROP CONSTRAINT [FK_UsuarioTorrentLeech];
GO
IF OBJECT_ID(N'[dbo].[FK_TorrentDetalheTorrent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Torrents] DROP CONSTRAINT [FK_TorrentDetalheTorrent];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioConvite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Convites] DROP CONSTRAINT [FK_UsuarioConvite];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuarios];
GO
IF OBJECT_ID(N'[dbo].[Torrents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Torrents];
GO
IF OBJECT_ID(N'[dbo].[ArquivosTorrents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArquivosTorrents];
GO
IF OBJECT_ID(N'[dbo].[TorrentSeeds]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TorrentSeeds];
GO
IF OBJECT_ID(N'[dbo].[TorrentLeeches]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TorrentLeeches];
GO
IF OBJECT_ID(N'[dbo].[TokensRegistro]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TokensRegistro];
GO
IF OBJECT_ID(N'[dbo].[DetalheTorrents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DetalheTorrents];
GO
IF OBJECT_ID(N'[dbo].[EventosAnnounce]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventosAnnounce];
GO
IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[Convites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Convites];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [IdUsuario] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [Senha] nvarchar(max)  NOT NULL,
    [Categoria] nvarchar(max)  NOT NULL,
    [PassKey] nvarchar(max)  NOT NULL,
    [Ratio] float  NOT NULL,
    [IdUsuarioPadrinho] int  NOT NULL,
    [Ativo] bit  NOT NULL,
    [Downloaded] float  NOT NULL,
    [Uploaded] float  NOT NULL,
    [DataCadastro] datetime  NOT NULL,
    [Advertido] bit  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Admin] bit  NOT NULL,
    [Bonus] float  NOT NULL,
    [ConvitesDisponiveis] int  NOT NULL
);
GO

-- Creating table 'Torrents'
CREATE TABLE [dbo].[Torrents] (
    [IdTorrent] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [HashInfo] nvarchar(max)  NOT NULL,
    [Tamanho] float  NOT NULL,
    [Seeds] int  NOT NULL,
    [Leechers] int  NOT NULL,
    [DataLancamento] datetime  NOT NULL,
    [FreeLeech] bit  NOT NULL,
    [Ativo] bit  NOT NULL,
    [PrimeiroSnatch] bit  NOT NULL,
    [UsuarioLancamento_IdUsuario] int  NOT NULL,
    [DetalheTorrent_IdDetalheTorrent] int  NOT NULL
);
GO

-- Creating table 'ArquivosTorrents'
CREATE TABLE [dbo].[ArquivosTorrents] (
    [IdArquivoTorrent] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [Tamanho] float  NOT NULL,
    [Torrent_IdTorrent] int  NOT NULL
);
GO

-- Creating table 'TorrentSeeds'
CREATE TABLE [dbo].[TorrentSeeds] (
    [IdTorrentSeed] int IDENTITY(1,1) NOT NULL,
    [ClienteTorrent] nvarchar(max)  NOT NULL,
    [Uploaded] float  NOT NULL,
    [Downloaded] float  NOT NULL,
    [Torrent_IdTorrent] int  NOT NULL,
    [Usuario_IdUsuario] int  NOT NULL
);
GO

-- Creating table 'TorrentLeeches'
CREATE TABLE [dbo].[TorrentLeeches] (
    [IdTorrentLeech] int IDENTITY(1,1) NOT NULL,
    [ClienteTorrent] nvarchar(max)  NOT NULL,
    [Uploaded] float  NOT NULL,
    [Downloaded] float  NOT NULL,
    [Torrent_IdTorrent] int  NOT NULL,
    [Usuario_IdUsuario] int  NOT NULL
);
GO

-- Creating table 'TokensRegistro'
CREATE TABLE [dbo].[TokensRegistro] (
    [IdTokenRegistro] int IDENTITY(1,1) NOT NULL,
    [Token] nvarchar(max)  NOT NULL,
    [DataGeracao] datetime  NOT NULL
);
GO

-- Creating table 'DetalheTorrents'
CREATE TABLE [dbo].[DetalheTorrents] (
    [IdDetalheTorrent] int IDENTITY(1,1) NOT NULL,
    [Descricao] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EventosAnnounce'
CREATE TABLE [dbo].[EventosAnnounce] (
    [IdEventoAnnounce] int IDENTITY(1,1) NOT NULL,
    [HashInfoTorrent] nvarchar(max)  NOT NULL,
    [PasskeyUsuario] nvarchar(max)  NOT NULL,
    [Uploaded] float  NOT NULL,
    [Downloaded] float  NOT NULL,
    [Evento] nvarchar(max)  NOT NULL,
    [DataEvento] datetime  NOT NULL,
    [Processado] bit  NOT NULL,
    [EnderecoIP] nvarchar(max)  NOT NULL,
    [PeerID] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [IdClient] int IDENTITY(1,1) NOT NULL,
    [PeerID] nvarchar(max)  NOT NULL,
    [Nome] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Convites'
CREATE TABLE [dbo].[Convites] (
    [IdConvite] int IDENTITY(1,1) NOT NULL,
    [HashConvite] nvarchar(max)  NOT NULL,
    [Usuario_IdUsuario] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdUsuario] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([IdUsuario] ASC);
GO

-- Creating primary key on [IdTorrent] in table 'Torrents'
ALTER TABLE [dbo].[Torrents]
ADD CONSTRAINT [PK_Torrents]
    PRIMARY KEY CLUSTERED ([IdTorrent] ASC);
GO

-- Creating primary key on [IdArquivoTorrent] in table 'ArquivosTorrents'
ALTER TABLE [dbo].[ArquivosTorrents]
ADD CONSTRAINT [PK_ArquivosTorrents]
    PRIMARY KEY CLUSTERED ([IdArquivoTorrent] ASC);
GO

-- Creating primary key on [IdTorrentSeed] in table 'TorrentSeeds'
ALTER TABLE [dbo].[TorrentSeeds]
ADD CONSTRAINT [PK_TorrentSeeds]
    PRIMARY KEY CLUSTERED ([IdTorrentSeed] ASC);
GO

-- Creating primary key on [IdTorrentLeech] in table 'TorrentLeeches'
ALTER TABLE [dbo].[TorrentLeeches]
ADD CONSTRAINT [PK_TorrentLeeches]
    PRIMARY KEY CLUSTERED ([IdTorrentLeech] ASC);
GO

-- Creating primary key on [IdTokenRegistro] in table 'TokensRegistro'
ALTER TABLE [dbo].[TokensRegistro]
ADD CONSTRAINT [PK_TokensRegistro]
    PRIMARY KEY CLUSTERED ([IdTokenRegistro] ASC);
GO

-- Creating primary key on [IdDetalheTorrent] in table 'DetalheTorrents'
ALTER TABLE [dbo].[DetalheTorrents]
ADD CONSTRAINT [PK_DetalheTorrents]
    PRIMARY KEY CLUSTERED ([IdDetalheTorrent] ASC);
GO

-- Creating primary key on [IdEventoAnnounce] in table 'EventosAnnounce'
ALTER TABLE [dbo].[EventosAnnounce]
ADD CONSTRAINT [PK_EventosAnnounce]
    PRIMARY KEY CLUSTERED ([IdEventoAnnounce] ASC);
GO

-- Creating primary key on [IdClient] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([IdClient] ASC);
GO

-- Creating primary key on [IdConvite] in table 'Convites'
ALTER TABLE [dbo].[Convites]
ADD CONSTRAINT [PK_Convites]
    PRIMARY KEY CLUSTERED ([IdConvite] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UsuarioLancamento_IdUsuario] in table 'Torrents'
ALTER TABLE [dbo].[Torrents]
ADD CONSTRAINT [FK_UsuarioTorrent]
    FOREIGN KEY ([UsuarioLancamento_IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioTorrent'
CREATE INDEX [IX_FK_UsuarioTorrent]
ON [dbo].[Torrents]
    ([UsuarioLancamento_IdUsuario]);
GO

-- Creating foreign key on [Torrent_IdTorrent] in table 'ArquivosTorrents'
ALTER TABLE [dbo].[ArquivosTorrents]
ADD CONSTRAINT [FK_TorrentArquivoTorrent]
    FOREIGN KEY ([Torrent_IdTorrent])
    REFERENCES [dbo].[Torrents]
        ([IdTorrent])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TorrentArquivoTorrent'
CREATE INDEX [IX_FK_TorrentArquivoTorrent]
ON [dbo].[ArquivosTorrents]
    ([Torrent_IdTorrent]);
GO

-- Creating foreign key on [Torrent_IdTorrent] in table 'TorrentSeeds'
ALTER TABLE [dbo].[TorrentSeeds]
ADD CONSTRAINT [FK_TorrentTorrentSeed]
    FOREIGN KEY ([Torrent_IdTorrent])
    REFERENCES [dbo].[Torrents]
        ([IdTorrent])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TorrentTorrentSeed'
CREATE INDEX [IX_FK_TorrentTorrentSeed]
ON [dbo].[TorrentSeeds]
    ([Torrent_IdTorrent]);
GO

-- Creating foreign key on [Usuario_IdUsuario] in table 'TorrentSeeds'
ALTER TABLE [dbo].[TorrentSeeds]
ADD CONSTRAINT [FK_UsuarioTorrentSeed]
    FOREIGN KEY ([Usuario_IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioTorrentSeed'
CREATE INDEX [IX_FK_UsuarioTorrentSeed]
ON [dbo].[TorrentSeeds]
    ([Usuario_IdUsuario]);
GO

-- Creating foreign key on [Torrent_IdTorrent] in table 'TorrentLeeches'
ALTER TABLE [dbo].[TorrentLeeches]
ADD CONSTRAINT [FK_TorrentTorrentLeech]
    FOREIGN KEY ([Torrent_IdTorrent])
    REFERENCES [dbo].[Torrents]
        ([IdTorrent])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TorrentTorrentLeech'
CREATE INDEX [IX_FK_TorrentTorrentLeech]
ON [dbo].[TorrentLeeches]
    ([Torrent_IdTorrent]);
GO

-- Creating foreign key on [Usuario_IdUsuario] in table 'TorrentLeeches'
ALTER TABLE [dbo].[TorrentLeeches]
ADD CONSTRAINT [FK_UsuarioTorrentLeech]
    FOREIGN KEY ([Usuario_IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioTorrentLeech'
CREATE INDEX [IX_FK_UsuarioTorrentLeech]
ON [dbo].[TorrentLeeches]
    ([Usuario_IdUsuario]);
GO

-- Creating foreign key on [DetalheTorrent_IdDetalheTorrent] in table 'Torrents'
ALTER TABLE [dbo].[Torrents]
ADD CONSTRAINT [FK_TorrentDetalheTorrent]
    FOREIGN KEY ([DetalheTorrent_IdDetalheTorrent])
    REFERENCES [dbo].[DetalheTorrents]
        ([IdDetalheTorrent])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TorrentDetalheTorrent'
CREATE INDEX [IX_FK_TorrentDetalheTorrent]
ON [dbo].[Torrents]
    ([DetalheTorrent_IdDetalheTorrent]);
GO

-- Creating foreign key on [Usuario_IdUsuario] in table 'Convites'
ALTER TABLE [dbo].[Convites]
ADD CONSTRAINT [FK_UsuarioConvite]
    FOREIGN KEY ([Usuario_IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioConvite'
CREATE INDEX [IX_FK_UsuarioConvite]
ON [dbo].[Convites]
    ([Usuario_IdUsuario]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------