using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivrariaApi.Modelos;

namespace LivrariaApi.Controllers
{
    [Route("livro")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        List<Livro> listaLivros;

        public LivroController()
        {
            CriaTabelas();
        }
        
        public void CriaTabelas()
        {
            listaLivros = new List<Livro>() {
                new Livro { Id = 1, Nome = "Livro 1", Descricao = "Livro 1", Preco = 10, QuantPaginas = 10, CodCategoria = 1 },
                new Livro { Id = 2, Nome = "Livro 2", Descricao = "Livro 2", Preco = 20, QuantPaginas = 40, CodCategoria = 2 },
                new Livro { Id = 3, Nome = "Livro 3", Descricao = "Livro 3", Preco = 25, QuantPaginas = 350, CodCategoria = 2 }
            };

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetLivros()
        {
            return listaLivros;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> GetLivros(int id)
        {
            var livro = listaLivros.Where(l=>l.Id == id).FirstOrDefault();

            if (livro == null)
            {
                return NotFound();
            }

            return livro;
        }

        [HttpGet("categoria/{id}")]
        public async Task<ActionResult<List<Livro>>> GetLivrosPorCategoria(int id)
        {
            List<Livro> livros = listaLivros.Where(l => l.CodCategoria == id).ToList();

            if (livros.Count() == 0)
            {
                return NotFound();
            }

            return livros;
        }
        
        [HttpPost]
        public async Task<ActionResult<List<Livro>>> CadastrarLivro(Livro livro)
        {
            Livro novoLivro = new Livro() { Id = (listaLivros.Max(l=>l.Id) + 1), Nome = livro.Nome, Descricao = livro.Descricao, CodCategoria = livro.CodCategoria, Preco = livro.Preco, QuantPaginas = livro.QuantPaginas };
            listaLivros.Add(novoLivro);

            return listaLivros;
        }
    }
}