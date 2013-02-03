using BitShareData;
using BitSharePortal.Models;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace BitSharePortal.Services
{
    public class TorrentServices
    {
        /// <summary>
        /// Lança um novo torrent no portal, registrando os dados de filmes e atores caso não existam, e faz o upload dos devidos arquivos
        /// </summary>
        /// <param name="Request">Http Request gerado pela postagem das informações do torrent</param>
        /// <param name="model">Informações inseridas pelo usuário para o lançamento do torrent</param>
        /// <param name="usuarioLogado">Usuário que está fazendo o lançamento</param>
        /// <param name="pastaTorrents">Pasta onde são armazenados os arquivos .torrent</param>
        /// <param name="pastaLegendas">Pasta onde são armazenados os arquivos de legenda</param>
        public void LancarTorrent(HttpRequestBase Request, TorrentUploadModel model, Usuario usuarioLogado, string pastaTorrents, string pastaLegendas)
        {
            try
            {
                HttpPostedFileBase torrentFile = Request.Files["torrentFile"];

                var arquivoTorrent = String.Format("{0}\\{1}", pastaTorrents, torrentFile.FileName);
                torrentFile.SaveAs(arquivoTorrent);
                var dadosTorrent = MonoTorrent.Common.Torrent.Load(arquivoTorrent);

                var repositorioPrincipal = new DataRepository<BitShareData.Torrent>();
                var bitShareTorrent = new BitShareData.Torrent();

                var filmeImdb = CarregarFilmeImdb(model.IdImdb);

                var repositorioFilme = new DataRepository<Filme>(repositorioPrincipal.Context);
                var filmesEncontrados = from item in repositorioFilme.Fetch() where item.IdImdb == model.IdImdb select item;
                if (filmesEncontrados.Count() > 0)
                {
                    bitShareTorrent.Filme = filmesEncontrados.First();
                }
                else
                {
                    bitShareTorrent.Filme = CriarNovoFilme(repositorioFilme, filmeImdb, model);
                }

                var nomeTorrent = String.Format("[{0}] [{1} {2}] [{3} {4}]", filmeImdb.Titulo, model.CodecVideo, model.Resolucao, model.CodecAudio, model.Audio);

                if (model.Dublado)
                {
                    nomeTorrent += " [Dub]";
                }

                bitShareTorrent.IdTorrent = 0;
                bitShareTorrent.Nome = nomeTorrent;
                bitShareTorrent.Arquivo = torrentFile.FileName;
                bitShareTorrent.Categoria = "";//model.Categoria;
                bitShareTorrent.HashInfo = dadosTorrent.InfoHash.ToString();
                bitShareTorrent.Tamanho = dadosTorrent.Size;
                bitShareTorrent.Seeds = 0;
                bitShareTorrent.Leechers = 0;
                bitShareTorrent.DataLancamento = DateTime.Now;
                bitShareTorrent.FreeLeech = false;
                bitShareTorrent.Ativo = true;
                bitShareTorrent.PrimeiroSnatch = false;
                bitShareTorrent.Downloads = 0;
                bitShareTorrent.Resolucao = model.Resolucao;
                bitShareTorrent.Audio = model.Audio;
                bitShareTorrent.CodecAudio = model.CodecAudio;
                bitShareTorrent.CodecVideo = model.CodecVideo;
                bitShareTorrent.Observacoes = model.Observacoes == null ? "" : model.Observacoes;
                bitShareTorrent.Dublado = model.Dublado;

                // Informações de legenda enviadas no lançamento
                if (Request.Files.Count > 1)
                {
                    for (int indice = 1; indice < Request.Files.Count; indice++)
                    {
                        if (Request.Files[indice].FileName != "")
                        {
                            Request.Files[indice].SaveAs(String.Format("{0}\\{1}", pastaLegendas, Request.Files[indice].FileName));
                            var legenda = new Legenda();
                            legenda.IdLegenda = 0;
                            legenda.Idioma = model.idiomaLegenda[indice - 1];
                            legenda.Arquivo = Request.Files[indice].FileName;
                            bitShareTorrent.Filme.Legendas.Add(legenda);
                        }
                    }
                }

                // Informações dos arquivos presentes no torrent
                foreach (var file in dadosTorrent.Files)
                {
                    var arquivo = new ArquivoTorrent();
                    arquivo.Nome = file.Path;
                    arquivo.Tamanho = file.Length;

                    bitShareTorrent.ArquivoTorrents.Add(arquivo);
                }

                // recupera o usuário pela entity, para poder fazer o relacionamento com o Torrent
                var repositorioUsuario = new DataRepository<Usuario>(repositorioPrincipal.Context);
                var usuarioLancamento = (from item in repositorioUsuario.Fetch() where item.IdUsuario == usuarioLogado.IdUsuario select item).First();

                bitShareTorrent.UsuarioLancamento = usuarioLancamento;

                repositorioPrincipal.Add(bitShareTorrent);
                repositorioPrincipal.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                
            }
        }

        /// <summary>
        /// Envia um comentário para um torrent
        /// </summary>
        /// <param name="Request">Dados do comentário inseridos pelo usuário</param>
        /// <param name="usuarioLogado">Usuário que está postando o comentário</param>
        public void PostarComentario(HttpRequestBase Request, Usuario usuarioLogado)
        {
            int IdTorrent = Convert.ToInt32(Request.Form["IdTorrent"]);
            var repositorioPrincipal = new DataRepository<BitShareData.Torrent>();
            var torrent = (from item in repositorioPrincipal.Fetch() where item.IdTorrent == IdTorrent select item).First();
            var repositorioUsuario = new DataRepository<Usuario>(repositorioPrincipal.Context);
            var usuarioPostagem = (from item in repositorioUsuario.Fetch() where item.IdUsuario == usuarioLogado.IdUsuario select item).First();

            var comentario = new Comentario();
            comentario.TextoComentario = String.Format("[{0}]:<br /> {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"), Request.Form["Comentario"]); 
            comentario.Usuario = usuarioPostagem;

            torrent.Comentarios.Add(comentario);

            repositorioPrincipal.SaveChanges();
        }

        /// <summary>
        /// Carrega dados do IMDB de um filme via serviço REST
        /// </summary>
        /// <param name="id">ID do Imdb do filme</param>
        private FilmeIMDBJsonResult CarregarFilmeImdb(string id)
        {
            var filme = new FilmeIMDBJsonResult();

            try
            {
                var request = WebRequest.Create(String.Format("http://www.bit-share-rest.net/Movies/GetComplete/?id={0}&JsonCallback=callback", id)) as HttpWebRequest;
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).",
                        response.StatusCode,
                        response.StatusDescription));
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var responseText = streamReader.ReadToEnd();
                        filme = jsonSerializer.Deserialize<FilmeIMDBJsonResult>(responseText.Replace("callback(", "").Replace(");", ""));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return filme;
        }

        /// <summary>
        /// Insere um novo filme na base de dados, com base nas informações do IMDB e inseridas pelo usuário
        /// </summary>
        /// <param name="repositorioFilme">Repositório da base de dados com contexto já criado</param>
        /// <param name="filmeImdb">Informações do IMDB para o filme especificado</param>
        /// <param name="model">Informações inseridas pelo usuário para o lançamento do torrent</param>
        /// <returns>Filme criado na base de dados</returns>
        private Filme CriarNovoFilme(DataRepository<Filme> repositorioFilme, FilmeIMDBJsonResult filmeImdb, TorrentUploadModel model)
        {
            var filme = new Filme();
            filme.IdFilme = 0;
            filme.IdImdb = filmeImdb.IDImdb;
            filme.Nome = filmeImdb.Titulo;
            filme.Diretor = filmeImdb.Diretor;
            filme.AnoLancamento = filmeImdb.AnoLancamento;
            filme.Sinopse = filmeImdb.Sinopse;
            filme.URLPoster = filmeImdb.URLPoster;
            filme.Generos = filmeImdb.Generos;
            filme.TrailerYoutube = model.TrailerYoutube == null ? "" : model.TrailerYoutube;
            filme.ScreenShots = ""; //TODO
            filme.Duracao = filmeImdb.Duracao;

            foreach (var atorImdb in filmeImdb.ListaAtores)
            {
                var repositorioAtor = new DataRepository<Ator>(repositorioFilme.Context);
                var atores = from item in repositorioAtor.Fetch() where item.IdImdb == atorImdb.IDImdb select item; 
                Ator atorAtual;
                if (atores.Count() > 0)
                {
                    atorAtual = atores.First();
                }
                else
                {
                    atorAtual = new Ator();
                    atorAtual.IdAtor = 0;
                    atorAtual.Nome = atorImdb.Nome;
                    atorAtual.IdImdb = atorImdb.IDImdb;
                    atorAtual.URLFoto = atorImdb.URLFoto == null ? "" : atorImdb.URLFoto;
                    repositorioAtor.Add(atorAtual);
                }

                filme.Papels.Add(new Papel() { Ator = atorAtual, IdPapel = 0, NomePersonagem = atorImdb.Papel });
            }

            return filme;
        }

        /// <summary>
        /// Exibe uma quantidade de bytes com duas casas decimais e já calculado com a unidade de medida correta
        /// </summary>
        /// <param name="tamanho">Tamanho do torrent em Bytes</param>
        /// <returns>O tamanho formatado já com sufixo</returns>
        public static string TamanhoFormat(double tamanho)
        {
            double tamanhoCalculado = tamanho;
            string retorno = "";
            string sufixo = " B";

            if (tamanho.ToString().Length > 3)
            {
                tamanhoCalculado = tamanhoCalculado / 1024;
                sufixo = " KB";
            }

            if (tamanho.ToString().Length > 6)
            {
                tamanhoCalculado = tamanhoCalculado / 1024;
                sufixo = " MB";
            }

            if (tamanho.ToString().Length > 9)
            {
                tamanhoCalculado = tamanhoCalculado / 1024;
                sufixo = " GB";
            }

            if (tamanho.ToString().Length > 12)
            {
                tamanhoCalculado = tamanhoCalculado / 1024;
                sufixo = " TB";
            }

            retorno = tamanhoCalculado.ToString("0.00") + sufixo;

            return retorno;
        }
    }
}