using MonoTorrent.Tracker;
using MonoTorrent.Tracker.Listeners;

namespace ASPTracker
{
    public class PageBase : System.Web.UI.Page
    {
        /// <summary>
        /// Objeto responsável pelo processamento das requisições
        /// </summary>
        protected AspNetListener TrackerListener
        {
            get
            {
                return (AspNetListener)Application["listener"];
            }
        }

        /// <summary>
        /// Objeto responsável pelo gerenciamento dos torrents e peers
        /// </summary>
        protected Tracker TrackerServer
        {
            get
            {
                return (Tracker)Application["server"];
            }
        }
    }
}