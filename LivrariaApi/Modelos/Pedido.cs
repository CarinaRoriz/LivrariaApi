using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaApi.Modelos
{
    public class Pedido
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
