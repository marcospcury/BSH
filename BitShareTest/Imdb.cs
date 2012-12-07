﻿using System;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace BitSharePortal.Models
{
    class Imdb
    {
        private List<Ator> _atores;

        public Imdb()
        {
            
        }

        public bool Pesquisar(string query)
        {
            HtmlWeb web = new HtmlWeb();
            _markup = web.Load("http://www.imdb.com/find?s=all&q=" + query);

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
            _markup = web.Load("http://www.imdb.com/find?s=all&q=" + query);

            var retorno = new List<FilmePesquisa>();

            if (!IsMoviePage())
            {
                var filmes = _markup.DocumentNode.SelectNodes("//td[contains(@class, 'result_text')]");
                var thumbs = _markup.DocumentNode.SelectNodes("//td[contains(@class, 'primary_photo')]/a/img");

                int indice = 0;
                foreach (var filme in filmes)
                {
                    retorno.Add(new FilmePesquisa() { Nome = filme.InnerText, URLImagem = thumbs[indice].Attributes["src"].Value });

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

            return retorno;
        }

        public void FilmePorId(string ID)
        {
            HtmlWeb web = new HtmlWeb();
            _markup = web.Load("http://www.imdb.com/title/tt" + ID);
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

        private HtmlAgilityPack.HtmlDocument _markup;

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
                return title == null ? String.Empty : title.InnerText.Remove(title.InnerText.IndexOf("(")).Replace("&#x22;", String.Empty);
            }
        }

        public string AnoLancamento
        {
            get
            {
                HtmlNode year = _markup.DocumentNode.SelectSingleNode("//title");
                return year == null ? String.Empty : Regex.Match(year.InnerText, @"\((.*?)\)").Groups[1].Value;
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
                return director == null ? String.Empty : director.InnerText;
            }
        }

        public void CarregarAtores()
        {
            HtmlNodeCollection actors = _markup.DocumentNode.SelectNodes("//td[contains(@class, 'name')]/a");
            HtmlNodeCollection roles = _markup.DocumentNode.SelectNodes("//td[contains(@class, 'character')]/div");
            int indice = 0;
            foreach (var ator in actors)
            {
                string URLImdb = "http://www.imdb.com/" + ator.Attributes["href"].Value;
                string IDImdb = ator.Attributes["href"].Value.Replace("/name/nm", "").Replace("/", "");

                var personagem = roles[indice];
                if (personagem == null)
                {
                    personagem = roles[indice].SelectSingleNode("//a");
                }

                Atores.Add(new Ator() { Nome = HttpUtility.HtmlDecode(ator.InnerText.Replace("\n", "")), Papel = HttpUtility.HtmlDecode(personagem.InnerText.Replace("\n", "").Trim()), URLImdb = URLImdb, IdImdb = IDImdb });
                
                indice++;
            }
        }

        public string Genre
        {
            get
            {
                HtmlNode header = _markup.DocumentNode.SelectSingleNode("//div[contains(@class, 'info')]/h5[contains(., 'Genre:')]");
                if (header != null)
                {
                    HtmlNode genre = header.ParentNode.SelectSingleNode(".//div[contains(@class, 'info-content')]");
                    if (genre != null) return genre.InnerText.Replace("See more&nbsp;&raquo;", String.Empty).Trim();
                }
                return String.Empty;
            }
        }

        public string Sinopse
        {
            get
            {
                HtmlNode plot = _markup.DocumentNode.SelectSingleNode("//p");
                return plot == null ? String.Empty : plot.InnerText;
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
        }

        internal class FilmePesquisa
        {
            public string URLImagem { get; set; }
            public string Nome { get; set; }
        }
    }
}
