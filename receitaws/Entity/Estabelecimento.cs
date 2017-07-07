using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace receitaws.Entity
{
    [Serializable]
    public class Estabelecimento
    {
        public Int64 id { get; set; }
        public string nome { get; set; }
        public string cnpj { get; set; }
        public string naturezaJuridica { get; set; }
        public string situacao { get; set; }
    }
}