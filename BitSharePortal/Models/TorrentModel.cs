using BitShareData;
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
                double tamanhoCalculado = Tamanho;
                string retorno = "";
                string sufixo = " B";

                if (Tamanho.ToString().Length > 3)
                {
                    tamanhoCalculado = tamanhoCalculado / 1024;
                    sufixo = " KB";
                }

                if (Tamanho.ToString().Length > 6)
                {
                    tamanhoCalculado = tamanhoCalculado / 1024;
                    sufixo = " MB";
                }

                if (Tamanho.ToString().Length > 9)
                {
                    tamanhoCalculado = tamanhoCalculado / 1024;
                    sufixo = " GB";
                }

                if (Tamanho.ToString().Length > 12)
                {
                    tamanhoCalculado = tamanhoCalculado / 1024;
                    sufixo = " TB";
                }

                retorno = tamanhoCalculado.ToString("0.00") + sufixo;

                return retorno;
            }
        }
        public DateTime DataLancamento { get; set; }
        public int Downloads { get; set; }
        public int Seeds { get; set; }
        public int Leechers { get; set; }
        public string UsuarioLancamento { get; set; }
    }
}