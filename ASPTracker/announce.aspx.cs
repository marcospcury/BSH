using BitShareData;
using System;

namespace ASPTracker
{
    public partial class announce : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var invalidPasseKey = false;
            try
            {
                // valida a passkey do usuário
                using (var repositorio = new DataRepository<Usuario>())
                {
                    var usuario = repositorio.Single(u => u.PassKey == Request.QueryString["passkey"]);
                    invalidPasseKey = (!usuario.Ativo);
                }
            }
            catch (Exception)
            {
                invalidPasseKey = true;
            }

            TrackerListener.HandleConnection(Context, invalidPasseKey);
        }
    }
}