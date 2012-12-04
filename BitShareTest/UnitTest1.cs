﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitShareData;

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
            user.Nome = "Marcos Cury";
            user.Email = "marcospcury@gmail.com";
            user.PassKey = "54bfc949-993a-4c41-88b5-00009d598972"; //Guid.NewGuid().ToString();
            user.Senha = "214196";
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
            
        }
    }
}
