﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivrariaApi.Modelos;

namespace LivrariaApi.Controllers
{
    [Route("v1/carrinhos")]
    [ApiController]
    public class CarrinhoComprasController : ControllerBase
    {
        List<CarrinhoCompras> listaCarrinhos;
        List<ItemCarrinhoCompras> listaItensCarrinhos;

        public CarrinhoComprasController()
        {
            listaItensCarrinhos = new List<ItemCarrinhoCompras>
            {
                new ItemCarrinhoCompras { Id = 1, IdCarrinhoCompras = 1, IdLivro = 1, Quantidade = 2, Valor = 20 },
                new ItemCarrinhoCompras { Id = 2, IdCarrinhoCompras = 1, IdLivro = 2, Quantidade = 1, Valor = 10 }
            };

            listaCarrinhos = new List<CarrinhoCompras>() {
                new CarrinhoCompras { Id = 1, IdUsuario = 1, listaItensCarrinho = listaItensCarrinhos },
                new CarrinhoCompras { Id = 2, IdUsuario = 2 }
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
        public async Task<ActionResult<List<ItemCarrinhoCompras>>> GetItensCarrinho(int id)
        {
            List<ItemCarrinhoCompras> itens = listaItensCarrinhos.Where(l => l.IdCarrinhoCompras == id).ToList();

            if (itens.Count() == 0)
            {
                return NotFound();
            }

            return itens;
        }

        [HttpPost]
        public async Task<ActionResult<List<CarrinhoCompras>>> CadastrarCarrinho(CarrinhoCompras carrinho)
        {
            CarrinhoCompras novoCarrinho = new CarrinhoCompras() { Id = ((listaCarrinhos.Count() == 0) ? 1 : (listaCarrinhos.Max(l => l.Id) + 1)), IdUsuario = carrinho.IdUsuario, listaItensCarrinho = new List<ItemCarrinhoCompras>() };

            foreach (ItemCarrinhoCompras item in carrinho.listaItensCarrinho)
            {
                ItemCarrinhoCompras itemCarrinho = new ItemCarrinhoCompras() { Id = ((listaItensCarrinhos.Count() == 0) ? 1 : (listaItensCarrinhos.Max(l => l.Id) + 1)), IdLivro = item.IdLivro, Quantidade = item.Quantidade, Valor = item.Valor, IdCarrinhoCompras = novoCarrinho.Id };
                novoCarrinho.listaItensCarrinho.Add(itemCarrinho);
            }

            listaCarrinhos.Add(novoCarrinho);

            return listaCarrinhos;

        }

        [HttpPost("{id}/itens")]
        public async Task<ActionResult<List<ItemCarrinhoCompras>>> AdicionarItensCarrinho(long id, ItemCarrinhoCompras itemCarrinhoCompras)
        {
            ItemCarrinhoCompras novoItemCarrinho = new ItemCarrinhoCompras() { Id = ((listaItensCarrinhos.Count() == 0) ? 1 : (listaItensCarrinhos.Max(l => l.Id) + 1)), IdLivro= itemCarrinhoCompras.IdLivro, Quantidade = itemCarrinhoCompras.Quantidade, Valor = itemCarrinhoCompras.Valor, IdCarrinhoCompras = id };
            listaItensCarrinhos.Add(novoItemCarrinho);

            return listaItensCarrinhos.Where(c=>c.IdCarrinhoCompras == id).ToList();
        }

    }
}