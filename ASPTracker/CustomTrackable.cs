using MonoTorrent;
using MonoTorrent.Common;
using MonoTorrent.Tracker;

namespace ASPTracker
{
    /// <summary>
    /// Objeto simples para armazenar os dados necessários do torrent para o tracking
    /// </summary>
    public class CustomTrackable : ITrackable
    {
        private InfoHash infoHash;
        private string name;

        public CustomTrackable(Torrent t)
        {
            infoHash = t.InfoHash;
            name = t.Name;
        }

        public CustomTrackable(string pName, string pInfoHash)
        {
            name = pName;
            var hash = InfoHash.FromHex(pInfoHash.Replace("-", ""));
            infoHash = hash;
        }

        /// <summary> 
        /// The infohash of the torrent 
        /// </summary> 
        public InfoHash InfoHash
        {
            get { return infoHash; }
        }

        /// <summary> 
        /// The name of the torrent 
        /// </summary> 
        public string Name
        {
            get { return name; }
        }

        public byte[] StrToByteArray(string str)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(str);
        }
    } 
}