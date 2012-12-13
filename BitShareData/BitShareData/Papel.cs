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
    public partial class Papel
    {
        #region Primitive Properties
    
        public virtual int IdPapel
        {
            get;
            set;
        }
    
        public virtual string NomePersonagem
        {
            get;
            set;
        }

        #endregion

        #region Navigation Properties
    
        public virtual Filme Filme
        {
            get { return _filme; }
            set
            {
                if (!ReferenceEquals(_filme, value))
                {
                    var previousValue = _filme;
                    _filme = value;
                    FixupFilme(previousValue);
                }
            }
        }
        private Filme _filme;
    
        public virtual Ator Ator
        {
            get { return _ator; }
            set
            {
                if (!ReferenceEquals(_ator, value))
                {
                    var previousValue = _ator;
                    _ator = value;
                    FixupAtor(previousValue);
                }
            }
        }
        private Ator _ator;

        #endregion

        #region Association Fixup
    
        private void FixupFilme(Filme previousValue)
        {
            if (previousValue != null && previousValue.Papels.Contains(this))
            {
                previousValue.Papels.Remove(this);
            }
    
            if (Filme != null)
            {
                if (!Filme.Papels.Contains(this))
                {
                    Filme.Papels.Add(this);
                }
            }
        }
    
        private void FixupAtor(Ator previousValue)
        {
            if (previousValue != null && previousValue.Papels.Contains(this))
            {
                previousValue.Papels.Remove(this);
            }
    
            if (Ator != null)
            {
                if (!Ator.Papels.Contains(this))
                {
                    Ator.Papels.Add(this);
                }
            }
        }

        #endregion

    }
}
