using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitShareData;
using BitSharePortal.Models;
using System.Net;

namespace BitShareTest
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void InclusaoRepositorio()
        {
            var repositorio = new DataRepository<Usuario>();

            var user = new Usuario();
            user.IdUsuario = 0;
            user.Nome = "marcospcury";
            user.Email = "marcospcury@gmail.com";
            user.PassKey = "54bfc949993a4c4188b500009d598972"; //Guid.NewGuid().ToString();
            user.Senha = "12345";
            user.Categoria = "Owner";
            user.Ratio = 0;
            user.Downloaded = 0;
            user.Uploaded = 0;
            user.Advertido = false;
            user.Ativo = true;
            user.Admin = true;
            user.DataCadastro = DateTime.Now;
            user.Bonus = 430;
            user.ConvitesDisponiveis = 3;

            repositorio.Add(user);
            repositorio.SaveChanges();
            
        }

        [TestMethod]
        public void DelecaoRepositorio()
        {
            //var repo = new DataRepository<Usuario>();
            //var user = repo.Single(u => u.IdUsuario == 1);
            //user.Advertido = true;
            //repo.SaveChanges();
            //MonoTorrent.Common.Torrent torr = MonoTorrent.Common.Torrent.Load("C:\\Users\\Cury\\Desktop\\Torrents To Go\\The Mentalist S05E09 HDTV XviD-SaM.torrent");
            
            var filme = new Imdb(false);
            filme.FilmePorUrl("http://www.imdb.com/title/tt0118583/");


            //filme.FilmePorId("0350804");
            var act = filme.Atores;
            //string a = filme.PosterURL;
            //WebClient client = new WebClient();
            //client.DownloadFile(a, "E:\\imagem.jpg");
        }
    }
}
