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
    public partial class Convite
    {
        #region Primitive Properties
    
        public virtual int IdConvite
        {
            get;
            set;
        }
    
        public virtual string HashConvite
        {
            get;
            set;
        }

        #endregion

        #region Navigation Properties
    
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
    
        private void FixupUsuario(Usuario previousValue)
        {
            if (previousValue != null && previousValue.Convites.Contains(this))
            {
                previousValue.Convites.Remove(this);
            }
    
            if (Usuario != null)
            {
                if (!Usuario.Convites.Contains(this))
                {
                    Usuario.Convites.Add(this);
                }
            }
        }

        #endregion

    }
}
