using BitShareData;
using BitSharePortal.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BitSharePortal.Models
{
    public class TorrentModel
    {
        public int IdTorrent { get; set; }
        public string Nome { get; set; }
        public double Tamanho { get; set; }
        public string TamanhoTorrent
        {
            get
            {
                return TorrentServices.TamanhoFormat(Tamanho);
            }
        }
        public DateTime DataLancamento { get; set; }
        public int Downloads { get; set; }
        public int Seeds { get; set; }
        public int Leechers { get; set; }
        public string UsuarioLancamento { get; set; }

        public string RelevanciaResultado { get; set; }
    }
}