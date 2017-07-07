using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace receitaws.Entity
{
    [Serializable]
    class PagamentoEstabelecimento
    {

        public Int64 id_cliente { get; set; }
        public string nome_cliente { get; set; }
        public string cpf { get; set; }
        public DateTime dt_nascimento { get; set; }
        public int num_cartao { get; set; }

        public Int64 id_estabelecimento { get; set; }
        public string nome_Estabelecimento { get; set; }
        public string cnpj { get; set; }
        public string natureza_juridica { get; set; }
        public string situacao { get; set; }

        public Int64 id_pagamento { get; set; }
        public float Valor { get; set; }
        public DateTime dt_pagamento { get; set; }
        public int cancelado { get; set; }

    }
}