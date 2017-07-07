using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace receitaws.Entity
{
    [Serializable]
    class Pagamento
    {
        public Int64 id { get; set; }
        public float Valor { get; set; }
        public DateTime dt_pagamento { get; set; }
        public Int64 id_cliente { get; set; }
        public Int64 id_estabelecimento { get; set; }

    }
}