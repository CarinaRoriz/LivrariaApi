using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivrariaApi.Modelos;

namespace LivrariaApi.Controllers
{
    [Route("carrinho")]
    [ApiController]
    public class CarrinhoComprasController : ControllerBase
    {
        List<CarrinhoCompras> listaCarrinhos;
        List<ItemCarrinhoCompras> listaItensCarrinhos;

        public CarrinhoComprasController()
        {
            CriaTabelas();
        }
        
        public void CriaTabelas()
        {
            listaCarrinhos = new List<CarrinhoCompras>() {
                new CarrinhoCompras { Id = 1, IdUsuario = 1 },
                new CarrinhoCompras { Id = 2, IdUsuario = 2 }
            };

            listaItensCarrinhos = new List<ItemCarrinhoCompras>
            {
                new ItemCarrinhoCompras { Id = 1, IdCarrinhoCompras = 1, IdLivro = 1, Quantidade = 2, Valor = 20 },
                new ItemCarrinhoCompras { Id = 2, IdCarrinhoCompras = 1, IdLivro = 2, Quantidade = 1, Valor = 10 }
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarrinhoCompras>>> GetCarrinhos()
        {
            return listaCarrinhos;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<CarrinhoCompras>> GetCarrinhos(int id)
        {
            var carrinho = listaCarrinhos.Where(l=>l.Id == id).FirstOrDefault();

            if (carrinho == null)
            {
                return NotFound();
            }

            return carrinho;
        }

        [HttpGet("itens/{id}")]
        public async Task<ActionResult<List<ItemCarrinhoCompras>>> GetItensCarrinho(int idCarrinho)
        {
            List<ItemCarrinhoCompras> itens = listaItensCarrinhos.Where(l => l.IdCarrinhoCompras == idCarrinho).ToList();

            if (itens.Count() == 0)
            {
                return NotFound();
            }

            return itens;
        }
        
    }
}