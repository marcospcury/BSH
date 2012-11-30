using BitShareData;
using MonoTorrent.Tracker;
using MonoTorrent.Tracker.Listeners;
using System;
using System.Collections.Generic;
using System.Data.Objects;

namespace ASPTracker
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Manager do tracker
        /// </summary>
        public Tracker server;

        /// <summary>
        /// Objeto para processamento dos announces
        /// </summary>
        public AspNetListener listener;
        
        protected void Application_Start(object sender, EventArgs e)
        {
            server = new Tracker();
            server.AllowUnregisteredTorrents = false;
            server.AnnounceInterval = new TimeSpan(0, 5, 0); // 10 min de atualização

            // carrega os torrents ja registrados
            using (var repositorio = new DataRepository<Torrent>())
            {
                ObjectSet<Torrent> torrents = (ObjectSet<Torrent>)repositorio.GetAll();

                foreach (var torr in torrents)
                {
                    server.Add(new CustomTrackable(torr.Nome, torr.HashInfo));
                }
            }

            listener = new AspNetListener();
            listener.AnnounceReceived += listener_AnnounceReceived;
            server.RegisterListener(listener);

            Application.Add("server", server);
            Application.Add("listener", listener); 
        }

        
        void listener_AnnounceReceived(object sender, AnnounceParameters e)
        {
            //insere na base de dados para ser processado posteriormente pelo robô   
            using (var repositorio = new DataRepository<EventoAnnounce>())
            {
                var announce = new EventoAnnounce();
                announce.IdEventoAnnounce = 0;
                announce.HashInfoTorrent = e.InfoHash.ToString();
                announce.PasskeyUsuario = e.PassKey;
                announce.Evento = e.Event.ToString();
                announce.DataEvento = DateTime.Now;
                announce.Downloaded = e.Downloaded;
                announce.Uploaded = e.Uploaded;
                announce.Processado = false;
                announce.EnderecoIP = e.ClientAddress.Address.ToString();
                announce.PeerID = e.PeerId;

                repositorio.Add(announce);
                repositorio.SaveChanges();
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}