using System;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace BitSharePortal.Models
{
    class Imdb
    {
        private Dictionary<string, string> GenerosImdb;

        private List<Ator> _atores;

        private bool PesquisaCompleta = false;

        private HtmlAgilityPack.HtmlDocument _markup;

        public Imdb(bool pesquisaCompleta)
        {
            GenerosImdb = new Dictionary<string, string>();
            GenerosImdb.Add("Action", "Ação");
            GenerosImdb.Add("Adventure", "Aventura");
            GenerosImdb.Add("Comedies", "Comédia");
            GenerosImdb.Add("Crime", "Policial");
            GenerosImdb.Add("Fantasy", "Fantasia");
            GenerosImdb.Add("Mystery", "Mistério");
            GenerosImdb.Add("Sport", "Esporte");
            GenerosImdb.Add("Westerns", "Faroeste");
            GenerosImdb.Add("Reality-TV", "Reality");
            GenerosImdb.Add("Biographies", "Biografia");
            GenerosImdb.Add("Game Shows", "Game Show");
            GenerosImdb.Add("Musicals", "Musical");
            GenerosImdb.Add("Adult", "Adulto");
            GenerosImdb.Add("Talk Shows", "Talk Show");
            GenerosImdb.Add("Drama", "Drama");
            GenerosImdb.Add("Animation", "Animação");
            GenerosImdb.Add("Sci-Fi", "Ficção Científica");
            GenerosImdb.Add("Thriller", "Suspense");
            GenerosImdb.Add("Family", "Familia");
            GenerosImdb.Add("Romance", "Romance");
            GenerosImdb.Add("Horror", "Terror");
            GenerosImdb.Add("War", "Guerra");
            GenerosImdb.Add("History", "História");
            GenerosImdb.Add("Documentaries", "Documentário");
            GenerosImdb.Add("Music", "Música");
            GenerosImdb.Add("News", "Notícia");
            GenerosImdb.Add("Film-Noir", "Noir");

            PesquisaCompleta = pesquisaCompleta;
        }

        public bool Pesquisar(string query)
        {
            HtmlWeb web = new HtmlWeb();

            _markup = web.Load("http://www.imdb.com/find?s=tt&q=" + query);

            if (!IsMoviePage())
            {
                string urlBestResult = FindBestMatch();
                if (!String.IsNullOrEmpty(urlBestResult))
                {
                    _markup = web.Load(urlBestResult);
                }
                else return false;
            }

            return IsMoviePage();
        }

        public List<FilmePesquisa> PesquisarFilmes(string query)
        {
            HtmlWeb web = new HtmlWeb();
            _markup = web.Load("http://www.imdb.com/find?s=tt&q=" + query);

            var retorno = new List<FilmePesquisa>();

            try
            {
                if (!IsMoviePage())
                {
                    var filmes = _markup.DocumentNode.SelectNodes("//td[contains(@class, 'result_text')]");
                    var linkFilmes = _markup.DocumentNode.SelectNodes("//td[contains(@class, 'result_text')]/a");
                    var thumbs = _markup.DocumentNode.SelectNodes("//td[contains(@class, 'primary_photo')]/a/img");

                    int indice = 0;
                    foreach (var filme in filmes)
                    {
                        retorno.Add(new FilmePesquisa() { Nome = filme.InnerText, URLImagem = thumbs[indice].Attributes["src"].Value, URLFilme = "http://www.imdb.com" + linkFilmes[indice].Attributes["href"].Value.Substring(0, linkFilmes[indice].Attributes["href"].Value.IndexOf("?ref")) });

                        if (thumbs.Count - 1 == indice)
                        {
                            break;
                        }

                        indice++;
                    }
                }
                else
                {
                    retorno.Add(new FilmePesquisa() { Nome = "Match" });
                }
            }
            catch (Exception)
            {

            }

            return retorno;
        }

        public void FilmePorId(string ID)
        {
            HtmlWeb web = new HtmlWeb();
            _markup = web.Load("http://www.imdb.com/title/tt" + ID);
        }

        public string URLFilme
        {
            get
            {
                return String.Format("http://www.imdb.com/title/tt{0}/", Id);
            }
        }

        public void FilmePorUrl(string URL)
        {
            HtmlWeb web = new HtmlWeb();
            _markup = web.Load(URL);
        }

        public List<Ator> Atores
        {
            get
            {
                if (_atores == null)
                {
                    _atores = new List<Ator>();
                    CarregarAtores();
                }
                return _atores;
            }
        }

        private bool IsMoviePage()
        {
            return (_markup.DocumentNode.SelectSingleNode("//td[contains(@class, 'castlist_label')]") != null);
        }

        private string FindBestMatch()
        {
            HtmlNodeCollection headers = _markup.DocumentNode.SelectNodes("//h3[contains(., 'Titles')]");
            if (headers != null) foreach (HtmlNode header in headers)
                {
                    HtmlNode link = header.ParentNode.SelectSingleNode(".//a[contains(@href, '/title/')]");
                    if (link != null)
                    {
                        return "http://www.imdb.com" + link.Attributes["href"].Value;
                    }
                }
            return String.Empty;
        }

        public string Nome
        {
            get
            {
                HtmlNode title = _markup.DocumentNode.SelectSingleNode("//title");
                return title == null ? String.Empty : HttpUtility.HtmlDecode(title.InnerText.Remove(title.InnerText.IndexOf("(")).Replace("&#x22;", String.Empty));
            }
        }

        public string AnoLancamento
        {
            get
            {
                HtmlNode year = _markup.DocumentNode.SelectSingleNode("//title");
                return year == null ? String.Empty : HttpUtility.HtmlDecode(Regex.Match(year.InnerText, @"\((.*?)\)").Groups[1].Value);
            }
        }

        public string LinkImdb
        {
            get
            {
                HtmlNode link = _markup.DocumentNode.SelectSingleNode("//link[contains(@rel, 'canonical')]");
                return link == null ? String.Empty : link.Attributes["href"].Value;
            }
        }

        public string Id
        {
            get
            {
                return this.LinkImdb.Replace("http://www.imdb.com/title/tt", String.Empty).TrimEnd('/');
            }
        }

        public string Diretor
        {
            get
            {
                HtmlNode director = _markup.DocumentNode.SelectSingleNode("//a[contains(@itemprop, 'director')]");
                return director == null ? String.Empty : HttpUtility.HtmlDecode(director.InnerText);
            }
        }

        public string Duracao
        {
            get
            {
                HtmlNode duracao = _markup.DocumentNode.SelectSingleNode("//time[contains(@itemprop, 'duration')]");
                return duracao == null ? String.Empty : duracao.InnerText;
            }
        }

        public string Generos
        {
            get
            {
                string retorno = "";
                var generos = _markup.DocumentNode.SelectNodes("//a[contains(@itemprop, 'genre')]");

                foreach (var genero in generos)
                {
                    retorno += GenerosImdb[genero.InnerText] + " | ";
                }

                if (retorno != "")
                {
                    retorno = retorno.Substring(0, retorno.Length - 3);
                }
                return retorno;
            }
        }

        private void CarregarAtores()
        {
            HtmlNodeCollection actors = _markup.DocumentNode.SelectNodes("//td[contains(@class, 'name')]/a");
            HtmlNodeCollection roles = _markup.DocumentNode.SelectNodes("//td[contains(@class, 'character')]/div");
            int indice = 0;
            foreach (var ator in actors)
            {
                string URLImdb = "http://www.imdb.com" + ator.Attributes["href"].Value;
                string IDImdb = ator.Attributes["href"].Value.Replace("/name/nm", "").Replace("/", "");

                if (indice < roles.Count)
                {
                    var personagem = roles[indice];
                    if (personagem == null)
                    {
                        personagem = roles[indice].SelectSingleNode("//a");
                    }

                    String URLPhoto = "";

                    if (PesquisaCompleta)
                    {
                        //------------------------------- Carrega a foto do ator
                        var webAtor = new HtmlWeb();
                        var _markupAtor = webAtor.Load(URLImdb);
                        var foto = _markupAtor.DocumentNode.SelectSingleNode("//td[contains(@id, 'img_primary')]/a/img");

                        if (foto != null)
                        {
                            URLPhoto = foto.Attributes["src"].Value;
                        }

                        //-------------------------------
                    }
                    
                    Atores.Add(new Ator() { Nome = HttpUtility.HtmlDecode(ator.InnerText.Replace("\n", "")), Papel = HttpUtility.HtmlDecode(personagem.InnerText.Replace("\n", "").Trim()), URLImdb = URLImdb, IdImdb = IDImdb, URLFoto = URLPhoto });
                }
                indice++;
            }
        }

        public string Sinopse
        {
            get
            {
                HtmlNode plot = _markup.DocumentNode.SelectSingleNode("//p/em[contains(@class, 'nobr')]").ParentNode;
                string retorno = plot == null ? String.Empty : plot.InnerText;

                if (retorno.Contains("Written by"))
                {
                    retorno = retorno.Substring(0, retorno.IndexOf("Written by"));
                }

                return HttpUtility.HtmlDecode(retorno);
            }
        }

        public string PosterURL
        {
            get
            {
                HtmlNode poster = _markup.DocumentNode.SelectSingleNode("//img[contains(@itemprop, 'image')]");
                return poster == null ? String.Empty : poster.Attributes["src"].Value;
            }
        }

        internal class Ator
        {
            public string IdImdb { get; set; }
            public string URLImdb { get; set; }
            public string URLFoto { get; set; }
            public string Nome { get; set; }
            public string Papel { get; set; }

            public override string ToString()
            {
                return String.Format("{0} - {1}", Nome, Papel);
            }
        }

        internal class FilmePesquisa
        {
            public string URLImagem { get; set; }
            public string Nome { get; set; }
            public string URLFilme { get; set; }
        }
    }
}
