using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace receitaws.Entity
{
    [Serializable]
    class Cliente
    {
        public Int64 id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public DateTime dtNascimento { get; set; }
        public int numCartao { get; set; }
    }

}