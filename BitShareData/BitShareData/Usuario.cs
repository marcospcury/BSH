//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BitShareData
{
    public partial class Usuario
    {
        #region Primitive Properties
    
        public virtual int IdUsuario
        {
            get;
            set;
        }
    
        public virtual string Nome
        {
            get;
            set;
        }
    
        public virtual string Senha
        {
            get;
            set;
        }
    
        public virtual string Categoria
        {
            get;
            set;
        }
    
        public virtual string PassKey
        {
            get;
            set;
        }
    
        public virtual double Ratio
        {
            get;
            set;
        }
    
        public virtual int IdUsuarioPadrinho
        {
            get;
            set;
        }
    
        public virtual bool Ativo
        {
            get;
            set;
        }
    
        public virtual double Downloaded
        {
            get;
            set;
        }
    
        public virtual double Uploaded
        {
            get;
            set;
        }
    
        public virtual System.DateTime DataCadastro
        {
            get;
            set;
        }
    
        public virtual bool Advertido
        {
            get;
            set;
        }
    
        public virtual string Email
        {
            get;
            set;
        }
    
        public virtual bool Admin
        {
            get;
            set;
        }
    
        public virtual double Bonus
        {
            get;
            set;
        }
    
        public virtual int ConvitesDisponiveis
        {
            get;
            set;
        }

        #endregion

        #region Navigation Properties
    
        public virtual ICollection<Torrent> Torrents
        {
            get
            {
                if (_torrents == null)
                {
                    var newCollection = new FixupCollection<Torrent>();
                    newCollection.CollectionChanged += FixupTorrents;
                    _torrents = newCollection;
                }
                return _torrents;
            }
            set
            {
                if (!ReferenceEquals(_torrents, value))
                {
                    var previousValue = _torrents as FixupCollection<Torrent>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTorrents;
                    }
                    _torrents = value;
                    var newValue = value as FixupCollection<Torrent>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTorrents;
                    }
                }
            }
        }
        private ICollection<Torrent> _torrents;
    
        public virtual ICollection<TorrentSeed> TorrentSeeds
        {
            get
            {
                if (_torrentSeeds == null)
                {
                    var newCollection = new FixupCollection<TorrentSeed>();
                    newCollection.CollectionChanged += FixupTorrentSeeds;
                    _torrentSeeds = newCollection;
                }
                return _torrentSeeds;
            }
            set
            {
                if (!ReferenceEquals(_torrentSeeds, value))
                {
                    var previousValue = _torrentSeeds as FixupCollection<TorrentSeed>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTorrentSeeds;
                    }
                    _torrentSeeds = value;
                    var newValue = value as FixupCollection<TorrentSeed>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTorrentSeeds;
                    }
                }
            }
        }
        private ICollection<TorrentSeed> _torrentSeeds;
    
        public virtual ICollection<TorrentLeech> TorrentLeeches
        {
            get
            {
                if (_torrentLeeches == null)
                {
                    var newCollection = new FixupCollection<TorrentLeech>();
                    newCollection.CollectionChanged += FixupTorrentLeeches;
                    _torrentLeeches = newCollection;
                }
                return _torrentLeeches;
            }
            set
            {
                if (!ReferenceEquals(_torrentLeeches, value))
                {
                    var previousValue = _torrentLeeches as FixupCollection<TorrentLeech>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTorrentLeeches;
                    }
                    _torrentLeeches = value;
                    var newValue = value as FixupCollection<TorrentLeech>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTorrentLeeches;
                    }
                }
            }
        }
        private ICollection<TorrentLeech> _torrentLeeches;
    
        public virtual ICollection<Convite> Convites
        {
            get
            {
                if (_convites == null)
                {
                    var newCollection = new FixupCollection<Convite>();
                    newCollection.CollectionChanged += FixupConvites;
                    _convites = newCollection;
                }
                return _convites;
            }
            set
            {
                if (!ReferenceEquals(_convites, value))
                {
                    var previousValue = _convites as FixupCollection<Convite>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupConvites;
                    }
                    _convites = value;
                    var newValue = value as FixupCollection<Convite>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupConvites;
                    }
                }
            }
        }
        private ICollection<Convite> _convites;
    
        public virtual ICollection<Comentario> Comentarios
        {
            get
            {
                if (_comentarios == null)
                {
                    var newCollection = new FixupCollection<Comentario>();
                    newCollection.CollectionChanged += FixupComentarios;
                    _comentarios = newCollection;
                }
                return _comentarios;
            }
            set
            {
                if (!ReferenceEquals(_comentarios, value))
                {
                    var previousValue = _comentarios as FixupCollection<Comentario>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupComentarios;
                    }
                    _comentarios = value;
                    var newValue = value as FixupCollection<Comentario>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupComentarios;
                    }
                }
            }
        }
        private ICollection<Comentario> _comentarios;
    
        public virtual ICollection<Mensagem> MensagensSaida
        {
            get
            {
                if (_mensagensSaida == null)
                {
                    var newCollection = new FixupCollection<Mensagem>();
                    newCollection.CollectionChanged += FixupMensagensSaida;
                    _mensagensSaida = newCollection;
                }
                return _mensagensSaida;
            }
            set
            {
                if (!ReferenceEquals(_mensagensSaida, value))
                {
                    var previousValue = _mensagensSaida as FixupCollection<Mensagem>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupMensagensSaida;
                    }
                    _mensagensSaida = value;
                    var newValue = value as FixupCollection<Mensagem>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupMensagensSaida;
                    }
                }
            }
        }
        private ICollection<Mensagem> _mensagensSaida;
    
        public virtual ICollection<Mensagem> MensagensEntrada
        {
            get
            {
                if (_mensagensEntrada == null)
                {
                    var newCollection = new FixupCollection<Mensagem>();
                    newCollection.CollectionChanged += FixupMensagensEntrada;
                    _mensagensEntrada = newCollection;
                }
                return _mensagensEntrada;
            }
            set
            {
                if (!ReferenceEquals(_mensagensEntrada, value))
                {
                    var previousValue = _mensagensEntrada as FixupCollection<Mensagem>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupMensagensEntrada;
                    }
                    _mensagensEntrada = value;
                    var newValue = value as FixupCollection<Mensagem>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupMensagensEntrada;
                    }
                }
            }
        }
        private ICollection<Mensagem> _mensagensEntrada;

        #endregion

        #region Association Fixup
    
        private void FixupTorrents(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Torrent item in e.NewItems)
                {
                    item.UsuarioLancamento = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Torrent item in e.OldItems)
                {
                    if (ReferenceEquals(item.UsuarioLancamento, this))
                    {
                        item.UsuarioLancamento = null;
                    }
                }
            }
        }
    
        private void FixupTorrentSeeds(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (TorrentSeed item in e.NewItems)
                {
                    item.Usuario = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TorrentSeed item in e.OldItems)
                {
                    if (ReferenceEquals(item.Usuario, this))
                    {
                        item.Usuario = null;
                    }
                }
            }
        }
    
        private void FixupTorrentLeeches(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (TorrentLeech item in e.NewItems)
                {
                    item.Usuario = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TorrentLeech item in e.OldItems)
                {
                    if (ReferenceEquals(item.Usuario, this))
                    {
                        item.Usuario = null;
                    }
                }
            }
        }
    
        private void FixupConvites(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Convite item in e.NewItems)
                {
                    item.Usuario = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Convite item in e.OldItems)
                {
                    if (ReferenceEquals(item.Usuario, this))
                    {
                        item.Usuario = null;
                    }
                }
            }
        }
    
        private void FixupComentarios(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Comentario item in e.NewItems)
                {
                    item.Usuario = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Comentario item in e.OldItems)
                {
                    if (ReferenceEquals(item.Usuario, this))
                    {
                        item.Usuario = null;
                    }
                }
            }
        }
    
        private void FixupMensagensSaida(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Mensagem item in e.NewItems)
                {
                    item.UsuarioDe = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Mensagem item in e.OldItems)
                {
                    if (ReferenceEquals(item.UsuarioDe, this))
                    {
                        item.UsuarioDe = null;
                    }
                }
            }
        }
    
        private void FixupMensagensEntrada(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Mensagem item in e.NewItems)
                {
                    item.UsuarioPara = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Mensagem item in e.OldItems)
                {
                    if (ReferenceEquals(item.UsuarioPara, this))
                    {
                        item.UsuarioPara = null;
                    }
                }
            }
        }

        #endregion

    }
}
