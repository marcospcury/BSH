using BitShareData;
using MonoTorrent;
using MonoTorrent.Tracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace ASPTracker
{
    public partial class register : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();

            if (Request.QueryString["accesstoken"] == null)
            {
                Response.Write("invalid access token");
            }
            else
            {
                using (var repositorio = new DataRepository<TokenRegistro>())
                {
                    try
                    {
                        var tokenGerado = repositorio.Single(t => t.Token == Request.QueryString["accesstoken"] && t.DataGeracao.AddMinutes(5) >= DateTime.Now);

                        // verificado que o token de fato existe, deleta-o e registra o torrent
                        repositorio.Delete(tokenGerado);
                        repositorio.SaveChanges();

                        lock (TrackerServer)
                        {
                            TrackerServer.Add(new CustomTrackable(Request.QueryString["name"], Request.QueryString["info"]));
                        }

                        Response.Write("torrent registered successfully");
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }
            }

            Response.End();
        }
    }
}