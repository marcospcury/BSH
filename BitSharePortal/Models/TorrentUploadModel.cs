using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitSharePortal.Models
{
    public class TorrentUploadModel
    {
        [Required(ErrorMessage = "Selecione um filme ou informe a URL do IMDB")]
        public string IdImdb { get; set; }

        public string Nome { get; set; }

        public string Categoria { get; set; }

        public string Observacoes { get; set; }

        [Required(ErrorMessage = "Informe a resolução do vídeo")]
        public string Resolucao { get; set; }

        [Required(ErrorMessage = "Informe os canais de áudio")]
        public string Audio { get; set; }

        [Required(ErrorMessage = "Informe o codec de vídeo")]
        public string CodecVideo { get; set; }

        [Required(ErrorMessage = "Informe o codec de áudio")]
        public string CodecAudio { get; set; }

        public string TrailerYoutube { get; set; }

        public string[] idiomaLegenda { get; set; }

        public bool Dublado { get; set; }

    }
}