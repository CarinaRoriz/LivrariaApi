using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaApi.Modelos
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int IdCarrinhoCompras { get; set; }
        public int IdLivro { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }
}
