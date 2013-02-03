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
    public partial class Comentario
    {
        #region Primitive Properties
    
        public virtual int IdComentario
        {
            get;
            set;
        }
    
        public virtual string TextoComentario
        {
            get;
            set;
        }

        #endregion

        #region Navigation Properties
    
        public virtual Torrent Torrent
        {
            get { return _torrent; }
            set
            {
                if (!ReferenceEquals(_torrent, value))
                {
                    var previousValue = _torrent;
                    _torrent = value;
                    FixupTorrent(previousValue);
                }
            }
        }
        private Torrent _torrent;
    
        public virtual Usuario Usuario
        {
            get { return _usuario; }
            set
            {
                if (!ReferenceEquals(_usuario, value))
                {
                    var previousValue = _usuario;
                    _usuario = value;
                    FixupUsuario(previousValue);
                }
            }
        }
        private Usuario _usuario;

        #endregion

        #region Association Fixup
    
        private void FixupTorrent(Torrent previousValue)
        {
            if (previousValue != null && previousValue.Comentarios.Contains(this))
            {
                previousValue.Comentarios.Remove(this);
            }
    
            if (Torrent != null)
            {
                if (!Torrent.Comentarios.Contains(this))
                {
                    Torrent.Comentarios.Add(this);
                }
            }
        }
    
        private void FixupUsuario(Usuario previousValue)
        {
            if (previousValue != null && previousValue.Comentarios.Contains(this))
            {
                previousValue.Comentarios.Remove(this);
            }
    
            if (Usuario != null)
            {
                if (!Usuario.Comentarios.Contains(this))
                {
                    Usuario.Comentarios.Add(this);
                }
            }
        }

        #endregion

    }
}