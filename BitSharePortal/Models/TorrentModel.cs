using BitShareData;
using System;
using System.Collections.Generic;

namespace BitSharePortal.Models
{
    public class TorrentModel
    {
        public int IdTorrent { get; set; }
        public string NomeTorrent { get; set; }
        public double TamanhoTorrent { get; set; }
        public DateTime DataLancamento { get; set; }
        public int Downloads { get; set; }
        public int Seeders { get; set; }
        public int Leechers { get; set; }
        public string UsuarioLancamento { get; set; }
    }

    public class TorrentListModel : List<TorrentModel>
    {
        /// <summary>
        /// Lista todos os torrents cadastrados sem nenhum filtro
        /// </summary>
        public void CarregarTodosTorrents()
        {
            using (var repositorio = new DataRepository<Torrent>())
            {
                var torrents = repositorio.GetAll();

                foreach (var torrent in torrents)
                {
                    var torrentModel = new TorrentModel();
                    torrentModel.IdTorrent = torrent.IdTorrent;
                    torrentModel.NomeTorrent = torrent.Nome;
                    torrentModel.TamanhoTorrent = torrent.Tamanho;
                    torrentModel.DataLancamento = torrent.DataLancamento;
                    torrentModel.Downloads = 0; //TODO: trazer a quantidade de snatches
                    torrentModel.Seeders = torrent.Seeds;
                    torrentModel.Leechers = torrent.Leechers;
                    torrentModel.UsuarioLancamento = torrent.UsuarioLancamento.Nome;

                    Add(torrentModel);
                }
            }
        }
    }
}