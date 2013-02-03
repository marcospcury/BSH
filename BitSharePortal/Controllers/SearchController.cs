using BitShareData;
using BitSharePortal.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
namespace BitSharePortal.Controllers
{
    public class SearchController : BitShareController
    {
        private int pageSize = 25;
        private IEnumerable<TorrentModel> ListaTorrents
        {
            get
            {
                return (IEnumerable<TorrentModel>)Session["ListaTorrents"];
            }
            set
            {
                Session["ListaTorrents"] = value;
            }
        }
        private string MensagemResultado
        {
            get
            {
                return (string)Session["MensagemResultado"];
            }
            set
            {
                Session["MensagemResultado"] = value;
            }
        }
        private string Pesquisa
        {
            get
            {
                return (string)Session["Pesquisa"];
            }
            set
            {
                Session["Pesquisa"] = value;
            }
        }
        private string ProcurarPor
        {
            get
            {
                return (string)Session["ProcurarPor"];
            }
            set
            {
                Session["ProcurarPor"] = value;
            }
        }

        public ActionResult Index(int page = 0)
        {
            if (page < 1)
            {
                page = 1;
                ListaTorrents = null;
                MensagemResultado = null;
                Pesquisa = null;
                ProcurarPor = null;
            }

            ViewBag.MensagemResultado = MensagemResultado;
            ViewBag.Pesquisa = Pesquisa;
            ViewBag.ProcurarPor = HttpUtility.HtmlEncode(ProcurarPor);

            if (ListaTorrents == null)
            {
                ListaTorrents = Connection.ExecuteQuery<TorrentModel>(queryBase);
            }

            var pagedListaTorrents = ListaTorrents.ToPagedList(page, pageSize);

            return View(pagedListaTorrents);
        }

        public ActionResult Director(string id)
        {
            var whereClause = String.Format(" where Filme_IdFilme in (Select IdFilme From Filmes Where Diretor = '{0}')", id);
            ListaTorrents = Connection.ExecuteQuery<TorrentModel>(queryBase + whereClause);
            MensagemResultado = "Filmes dirigidos por " + id;
            Pesquisa = null;
            ProcurarPor = null;
            return RedirectToAction("Index", new { page = 1 });
        }

        public ActionResult Actor(string id)
        {
            var whereClause = String.Format(" where Filme_IdFilme in (select Filme_IdFilme from Papeis where Ator_IdAtor in (select IdAtor from Atores Where Nome = '{0}'))", id);
            ListaTorrents = Connection.ExecuteQuery<TorrentModel>(queryBase + whereClause);
            MensagemResultado = "Filmes estrelados por " + id;
            Pesquisa = null;
            ProcurarPor = null;
            return RedirectToAction("Index", new { page = 1 });
        }

        public ActionResult Genre(string id)
        {
            var whereClause = String.Format( " where Filme_IdFilme in (Select IdFilme From Filmes Where Generos like '%{0}%')", id);
            ListaTorrents = Connection.ExecuteQuery<TorrentModel>(queryBase + whereClause);
            MensagemResultado = "Filmes do gênero " + id;
            Pesquisa = null;
            ProcurarPor = null;
            return RedirectToAction("Index", new { page = 1 });
        }

        public ActionResult Year(string id)
        {
            var whereClause = String.Format(" where Filme_IdFilme in (Select IdFilme From Filmes Where AnoLancamento = '{0}')", id);
            ListaTorrents = Connection.ExecuteQuery<TorrentModel>(queryBase + whereClause);
            MensagemResultado = "Filmes lançados no ano de " + id;
            Pesquisa = null;
            ProcurarPor = null;
            return RedirectToAction("Index", new { page = 1 });
        }

        public ActionResult Movie(string id)
        {
            var whereClause = String.Format(" where Filme_IdFilme in (Select IdFilme From Filmes Where Nome = '{0}')", id);
            ListaTorrents = Connection.ExecuteQuery<TorrentModel>(queryBase + whereClause);
            MensagemResultado = "Torrents lançados para o filme " + id;
            Pesquisa = null;
            ProcurarPor = null;
            return RedirectToAction("Index", new { page = 1 });
        }

        [HttpPost]
        public ActionResult Pesquisar()
        {
            Pesquisa = Request.Form["Pesquisa"];
            ProcurarPor = Request.Form["ProcurarPor"];
            ListaTorrents = null;
            MensagemResultado = null;
            var query = "select '{0}' as RelevanciaResultado, t.IdTorrent, t.Nome, t.Tamanho, t.Seeds, t.Leechers, t.DataLancamento, 0 as Downloads, u.Nome as UsuarioLancamento From Torrents t Inner Join Usuarios u on u.IdUsuario = t.UsuarioLancamento_IdUsuario"; 
            var whereClause = "";
            IEnumerable<TorrentModel> resultadoTemporario = new List<TorrentModel>();

            switch (ProcurarPor)
            {
                case "Filme":
                    query = String.Format(query, "");
                    whereClause = String.Format(" where Filme_IdFilme in (Select IdFilme From Filmes Where Nome like '%{0}%')", Pesquisa);
                    resultadoTemporario = Connection.ExecuteQuery<TorrentModel>(query + whereClause);
                    break;

                case "Ator":
                    query = String.Format(query, "");
                    whereClause = String.Format(" where Filme_IdFilme in (select Filme_IdFilme from Papeis where Ator_IdAtor in (select IdAtor from Atores Where Nome like '%{0}%'))", Pesquisa);
                    resultadoTemporario = Connection.ExecuteQuery<TorrentModel>(query + whereClause);
                    break;

                case "Diretor":
                    query = String.Format(query, "");
                    whereClause = String.Format(" where Filme_IdFilme in (Select IdFilme From Filmes Where Diretor like '%{0}%')", Pesquisa);
                    resultadoTemporario = Connection.ExecuteQuery<TorrentModel>(query + whereClause);
                    break;

                case "Personagem":
                    query = String.Format(query, "");
                    whereClause = String.Format(" where Filme_IdFilme in (select Filme_IdFilme from Papeis where NomePersonagem like '%{0}%')", Pesquisa);
                    resultadoTemporario = Connection.ExecuteQuery<TorrentModel>(query + whereClause);
                    break;

                case "Ano":
                    query = String.Format(query, "");
                    whereClause = String.Format(" where Filme_IdFilme in (Select IdFilme From Filmes Where AnoLancamento = '{0}')", Pesquisa);
                    resultadoTemporario = Connection.ExecuteQuery<TorrentModel>(query + whereClause);
                    break;

                case "Genero":
                    query = String.Format(query, "");
                    whereClause = String.Format(" where Filme_IdFilme in (Select IdFilme From Filmes Where Generos like '%{0}%')", Pesquisa);
                    resultadoTemporario = Connection.ExecuteQuery<TorrentModel>(query + whereClause);
                    break;

                case "Tudo":
                    whereClause = String.Format(" where (Filme_IdFilme in (Select IdFilme From Filmes Where Nome like '%{0}%'))", Pesquisa);
                    whereClause += String.Format(" or (Filme_IdFilme in (select Filme_IdFilme from Papeis where Ator_IdAtor in (select IdAtor from Atores Where Nome like '%{0}%')))", Pesquisa);
                    whereClause += String.Format(" or (Filme_IdFilme in (Select IdFilme From Filmes Where Diretor like '%{0}%'))", Pesquisa);
                    whereClause += String.Format(" or (Filme_IdFilme in (select Filme_IdFilme from Papeis where NomePersonagem like '%{0}%'))", Pesquisa);
                    whereClause += String.Format(" or (Filme_IdFilme in (Select IdFilme From Filmes Where AnoLancamento = '{0}'))", Pesquisa);
                    whereClause += String.Format(" or (Filme_IdFilme in (Select IdFilme From Filmes Where Generos like '%{0}%'))", Pesquisa);
                    break;
            }

            ListaTorrents = resultadoTemporario;

            return RedirectToAction("Index", new { page = 1 });
        }
    }
}
