using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitSharePortal.Models
{
    public class TorrentUploadModel
    {
        [Required(ErrorMessage="Informe o Nome do Torrent")]
        public string Nome { get; set; }

        [Required(ErrorMessage="Selecione uma categoria")]
        public string Categoria { get; set; }

        // --------------- Detalhes do Torrent ---------------

        public string NomeFilme { get; set; }

        [Required(ErrorMessage = "O campo Descrição é necessário")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Inserir ao menos uma URL de imagen")]
        public string ImagensURL { get; set; }

        public string ImagemCapa { get; set; }

        public string TrailerYoutube { get; set; }

        public string Genero { get; set; }

        public string AnoLancamento { get; set; }

        public string Diretor { get; set; }



    }
}