using BitSharePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImdbRest.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult Search(string term)
        {
            var resultado = new List<PesquisaFilmesJsonResult>();
            var pesquisaFilmes = new Imdb(false);
            var filmesResult = pesquisaFilmes.PesquisarFilmes(term).Take(10);

            foreach (var filmeResult in filmesResult)
            {
                resultado.Add(new PesquisaFilmesJsonResult() { label = filmeResult.Nome, image = filmeResult.URLImagem, imdb = filmeResult.URLFilme });
            }

            return this.Jsonp(resultado);
        }

        public ActionResult Get(string id)
        {
            var resultado = new FilmeIMDBJsonResult();

            var filme = new Imdb(false);
            filme.FilmePorId(id);

            if (filme != null)
            {
                resultado.IDImdb = id;
                resultado.Titulo = filme.Nome;
                resultado.URLPoster = filme.PosterURL;
                resultado.URLImdb = filme.URLFilme;
                resultado.AnoLancamento = filme.AnoLancamento;
                resultado.Diretor = filme.Diretor;
                resultado.Generos = filme.Generos;
                resultado.Sinopse = filme.Sinopse;
                resultado.Duracao = filme.Duracao;

                foreach (var ator in filme.Atores)
                {
                    resultado.Cast += ator.ToString() + "<br />";
                }
            }
            return this.Jsonp(resultado);
        }

        public ActionResult GetComplete(string id)
        {
            var resultado = new FilmeIMDBJsonResult();

            var filme = new Imdb(true);
            filme.FilmePorId(id);

            if (filme != null)
            {
                resultado.IDImdb = id;
                resultado.Titulo = filme.Nome;
                resultado.URLPoster = filme.PosterURL;
                resultado.URLImdb = filme.URLFilme;
                resultado.AnoLancamento = filme.AnoLancamento;
                resultado.Diretor = filme.Diretor;
                resultado.Generos = filme.Generos;
                resultado.Sinopse = filme.Sinopse;
                resultado.Duracao = filme.Duracao;

                resultado.ListaAtores = new List<AtorIMDBJsonResult>();

                foreach (var ator in filme.Atores)
                {
                    resultado.ListaAtores.Add(new AtorIMDBJsonResult() { URLImdb = ator.URLImdb, IDImdb = ator.IdImdb, Nome = ator.Nome, Papel = ator.Papel, URLFoto = ator.URLFoto });
                }
            }

            return this.Jsonp(resultado);
        }
    }
}
