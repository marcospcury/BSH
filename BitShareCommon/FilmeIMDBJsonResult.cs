using System.Collections.Generic;

namespace BitSharePortal.Models
{
    public class FilmeIMDBJsonResult
    {
        public string IDImdb { get; set; }
        public string URLImdb { get; set; }
        public string Titulo { get; set; }
        public string URLPoster { get; set; }
        public string Sinopse { get; set; }
        public string Diretor { get; set; }
        public string AnoLancamento { get; set; }
        public string Generos { get; set; }
        public string Duracao { get; set; }
        public string Cast { get; set; }
        public List<AtorIMDBJsonResult> ListaAtores { get; set; }
    }

    public class AtorIMDBJsonResult
    {
        public string IDImdb { get; set; }
        public string URLImdb { get; set; }
        public string URLFoto { get; set; }
        public string Nome { get; set; }
        public string Papel { get; set; }
    }
}