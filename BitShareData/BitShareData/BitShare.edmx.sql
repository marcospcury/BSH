
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/20/2012 15:01:01
-- Generated from EDMX file: C:\Users\Cury\Documents\GitHub\BSH\BitShareData\BitShareData\BitShare.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BitShareAzure];
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
IF OBJECT_ID(N'[dbo].[FK_UsuarioConvite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Convites] DROP CONSTRAINT [FK_UsuarioConvite];
GO
IF OBJECT_ID(N'[dbo].[FK_FilmePapel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Papeis] DROP CONSTRAINT [FK_FilmePapel];
GO
IF OBJECT_ID(N'[dbo].[FK_AtorPapel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Papeis] DROP CONSTRAINT [FK_AtorPapel];
GO
IF OBJECT_ID(N'[dbo].[FK_FilmeTorrent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Torrents] DROP CONSTRAINT [FK_FilmeTorrent];
GO
IF OBJECT_ID(N'[dbo].[FK_FilmeLegenda]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Legendas] DROP CONSTRAINT [FK_FilmeLegenda];
GO
IF OBJECT_ID(N'[dbo].[FK_PackFilmeFilme]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Filmes] DROP CONSTRAINT [FK_PackFilmeFilme];
GO
IF OBJECT_ID(N'[dbo].[FK_TorrentComentario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comentarios] DROP CONSTRAINT [FK_TorrentComentario];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioComentario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comentarios] DROP CONSTRAINT [FK_UsuarioComentario];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioMensagem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mensagens] DROP CONSTRAINT [FK_UsuarioMensagem];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioMensagem1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mensagens] DROP CONSTRAINT [FK_UsuarioMensagem1];
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
IF OBJECT_ID(N'[dbo].[EventosAnnounce]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventosAnnounce];
GO
IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[Convites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Convites];
GO
IF OBJECT_ID(N'[dbo].[Legendas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Legendas];
GO
IF OBJECT_ID(N'[dbo].[Atores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Atores];
GO
IF OBJECT_ID(N'[dbo].[Filmes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Filmes];
GO
IF OBJECT_ID(N'[dbo].[Papeis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Papeis];
GO
IF OBJECT_ID(N'[dbo].[PackFilmes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PackFilmes];
GO
IF OBJECT_ID(N'[dbo].[Comentarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comentarios];
GO
IF OBJECT_ID(N'[dbo].[Mensagens]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mensagens];
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
    [Downloads] int  NOT NULL,
    [Categoria] nvarchar(max)  NOT NULL,
    [Arquivo] nvarchar(max)  NOT NULL,
    [Resolucao] nvarchar(max)  NOT NULL,
    [Audio] nvarchar(max)  NOT NULL,
    [CodecAudio] nvarchar(max)  NOT NULL,
    [CodecVideo] nvarchar(max)  NOT NULL,
    [Dublado] bit  NOT NULL,
    [Observacoes] nvarchar(max)  NOT NULL,
    [UsuarioLancamento_IdUsuario] int  NOT NULL,
    [Filme_IdFilme] int  NOT NULL
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

-- Creating table 'Legendas'
CREATE TABLE [dbo].[Legendas] (
    [IdLegenda] int IDENTITY(1,1) NOT NULL,
    [Arquivo] nvarchar(max)  NOT NULL,
    [Idioma] nvarchar(max)  NOT NULL,
    [Filme_IdFilme] int  NOT NULL
);
GO

-- Creating table 'Atores'
CREATE TABLE [dbo].[Atores] (
    [IdAtor] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [IdImdb] nvarchar(max)  NOT NULL,
    [URLFoto] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Filmes'
CREATE TABLE [dbo].[Filmes] (
    [IdFilme] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [Diretor] nvarchar(max)  NOT NULL,
    [Sinopse] nvarchar(max)  NOT NULL,
    [AnoLancamento] nvarchar(max)  NOT NULL,
    [IdImdb] nvarchar(max)  NOT NULL,
    [URLPoster] nvarchar(max)  NOT NULL,
    [Generos] nvarchar(max)  NOT NULL,
    [TrailerYoutube] nvarchar(max)  NOT NULL,
    [ScreenShots] nvarchar(max)  NOT NULL,
    [Duracao] nvarchar(max)  NOT NULL,
    [PackFilme_IdPack] int  NULL
);
GO

-- Creating table 'Papeis'
CREATE TABLE [dbo].[Papeis] (
    [IdPapel] int IDENTITY(1,1) NOT NULL,
    [NomePersonagem] nvarchar(max)  NOT NULL,
    [Filme_IdFilme] int  NOT NULL,
    [Ator_IdAtor] int  NOT NULL
);
GO

-- Creating table 'PackFilmes'
CREATE TABLE [dbo].[PackFilmes] (
    [IdPack] int IDENTITY(1,1) NOT NULL,
    [NomePack] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Comentarios'
CREATE TABLE [dbo].[Comentarios] (
    [IdComentario] int IDENTITY(1,1) NOT NULL,
    [TextoComentario] nvarchar(max)  NOT NULL,
    [Torrent_IdTorrent] int  NOT NULL,
    [Usuario_IdUsuario] int  NOT NULL
);
GO

-- Creating table 'Mensagens'
CREATE TABLE [dbo].[Mensagens] (
    [IdMensagem] int IDENTITY(1,1) NOT NULL,
    [AssuntoMensagem] nvarchar(max)  NOT NULL,
    [TextoMensagem] nvarchar(max)  NOT NULL,
    [Lida] bit  NOT NULL,
    [UsuarioDe_IdUsuario] int  NOT NULL,
    [UsuarioPara_IdUsuario] int  NOT NULL
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

-- Creating primary key on [IdLegenda] in table 'Legendas'
ALTER TABLE [dbo].[Legendas]
ADD CONSTRAINT [PK_Legendas]
    PRIMARY KEY CLUSTERED ([IdLegenda] ASC);
GO

-- Creating primary key on [IdAtor] in table 'Atores'
ALTER TABLE [dbo].[Atores]
ADD CONSTRAINT [PK_Atores]
    PRIMARY KEY CLUSTERED ([IdAtor] ASC);
GO

-- Creating primary key on [IdFilme] in table 'Filmes'
ALTER TABLE [dbo].[Filmes]
ADD CONSTRAINT [PK_Filmes]
    PRIMARY KEY CLUSTERED ([IdFilme] ASC);
GO

-- Creating primary key on [IdPapel] in table 'Papeis'
ALTER TABLE [dbo].[Papeis]
ADD CONSTRAINT [PK_Papeis]
    PRIMARY KEY CLUSTERED ([IdPapel] ASC);
GO

-- Creating primary key on [IdPack] in table 'PackFilmes'
ALTER TABLE [dbo].[PackFilmes]
ADD CONSTRAINT [PK_PackFilmes]
    PRIMARY KEY CLUSTERED ([IdPack] ASC);
GO

-- Creating primary key on [IdComentario] in table 'Comentarios'
ALTER TABLE [dbo].[Comentarios]
ADD CONSTRAINT [PK_Comentarios]
    PRIMARY KEY CLUSTERED ([IdComentario] ASC);
GO

-- Creating primary key on [IdMensagem] in table 'Mensagens'
ALTER TABLE [dbo].[Mensagens]
ADD CONSTRAINT [PK_Mensagens]
    PRIMARY KEY CLUSTERED ([IdMensagem] ASC);
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

-- Creating foreign key on [Filme_IdFilme] in table 'Papeis'
ALTER TABLE [dbo].[Papeis]
ADD CONSTRAINT [FK_FilmePapel]
    FOREIGN KEY ([Filme_IdFilme])
    REFERENCES [dbo].[Filmes]
        ([IdFilme])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FilmePapel'
CREATE INDEX [IX_FK_FilmePapel]
ON [dbo].[Papeis]
    ([Filme_IdFilme]);
GO

-- Creating foreign key on [Ator_IdAtor] in table 'Papeis'
ALTER TABLE [dbo].[Papeis]
ADD CONSTRAINT [FK_AtorPapel]
    FOREIGN KEY ([Ator_IdAtor])
    REFERENCES [dbo].[Atores]
        ([IdAtor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AtorPapel'
CREATE INDEX [IX_FK_AtorPapel]
ON [dbo].[Papeis]
    ([Ator_IdAtor]);
GO

-- Creating foreign key on [Filme_IdFilme] in table 'Torrents'
ALTER TABLE [dbo].[Torrents]
ADD CONSTRAINT [FK_FilmeTorrent]
    FOREIGN KEY ([Filme_IdFilme])
    REFERENCES [dbo].[Filmes]
        ([IdFilme])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FilmeTorrent'
CREATE INDEX [IX_FK_FilmeTorrent]
ON [dbo].[Torrents]
    ([Filme_IdFilme]);
GO

-- Creating foreign key on [Filme_IdFilme] in table 'Legendas'
ALTER TABLE [dbo].[Legendas]
ADD CONSTRAINT [FK_FilmeLegenda]
    FOREIGN KEY ([Filme_IdFilme])
    REFERENCES [dbo].[Filmes]
        ([IdFilme])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FilmeLegenda'
CREATE INDEX [IX_FK_FilmeLegenda]
ON [dbo].[Legendas]
    ([Filme_IdFilme]);
GO

-- Creating foreign key on [PackFilme_IdPack] in table 'Filmes'
ALTER TABLE [dbo].[Filmes]
ADD CONSTRAINT [FK_PackFilmeFilme]
    FOREIGN KEY ([PackFilme_IdPack])
    REFERENCES [dbo].[PackFilmes]
        ([IdPack])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PackFilmeFilme'
CREATE INDEX [IX_FK_PackFilmeFilme]
ON [dbo].[Filmes]
    ([PackFilme_IdPack]);
GO

-- Creating foreign key on [Torrent_IdTorrent] in table 'Comentarios'
ALTER TABLE [dbo].[Comentarios]
ADD CONSTRAINT [FK_TorrentComentario]
    FOREIGN KEY ([Torrent_IdTorrent])
    REFERENCES [dbo].[Torrents]
        ([IdTorrent])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TorrentComentario'
CREATE INDEX [IX_FK_TorrentComentario]
ON [dbo].[Comentarios]
    ([Torrent_IdTorrent]);
GO

-- Creating foreign key on [Usuario_IdUsuario] in table 'Comentarios'
ALTER TABLE [dbo].[Comentarios]
ADD CONSTRAINT [FK_UsuarioComentario]
    FOREIGN KEY ([Usuario_IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioComentario'
CREATE INDEX [IX_FK_UsuarioComentario]
ON [dbo].[Comentarios]
    ([Usuario_IdUsuario]);
GO

-- Creating foreign key on [UsuarioDe_IdUsuario] in table 'Mensagens'
ALTER TABLE [dbo].[Mensagens]
ADD CONSTRAINT [FK_UsuarioMensagem]
    FOREIGN KEY ([UsuarioDe_IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioMensagem'
CREATE INDEX [IX_FK_UsuarioMensagem]
ON [dbo].[Mensagens]
    ([UsuarioDe_IdUsuario]);
GO

-- Creating foreign key on [UsuarioPara_IdUsuario] in table 'Mensagens'
ALTER TABLE [dbo].[Mensagens]
ADD CONSTRAINT [FK_UsuarioMensagem1]
    FOREIGN KEY ([UsuarioPara_IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioMensagem1'
CREATE INDEX [IX_FK_UsuarioMensagem1]
ON [dbo].[Mensagens]
    ([UsuarioPara_IdUsuario]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------