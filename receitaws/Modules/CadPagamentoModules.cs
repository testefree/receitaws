using MySql.Data.MySqlClient;
using Nancy;
using System;
using System.Collections.Generic;
using System.Data;
using receitaws.Entity;

namespace receitaws.Modules
{
    public class CadPagamentoModule : NancyModule
    {
        private List<Cliente> clientes = new List<Cliente>();
        private List<Estabelecimento> estabelecimentos = new List<Estabelecimento>();
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private String stringConexao = "Persist Security Info=False; server=localhost;database=receitaws;uid=root;Convert Zero Datetime=True";

        public CadPagamentoModule() : base("/TestCadPagamento")
        {
            Get["/"] = parameters =>
            {
                try
                {
                    mConn = new MySqlConnection(stringConexao);
                    mAdapter = new MySqlDataAdapter("SELECT * FROM cliente ORDER BY id", mConn);
                    DataSet myDataSet = new DataSet();
                    mAdapter.Fill(myDataSet, "cliente");

                    foreach (DataRow item in myDataSet.Tables[0].Rows)
                    {
                        Cliente cliente = new Cliente();
                        cliente.id = (Int64)item["id"];
                        cliente.nome = item["nome"].ToString();
                        cliente.cpf = item["cpf"].ToString();
                        cliente.dt_nascimento = DateTime.Parse(item["dt_nascimento"].ToString());
                        cliente.num_cartao = (int)item["num_cartao"];
                        clientes.Add(cliente);
                    }


                    mAdapter = new MySqlDataAdapter("SELECT * FROM estabelecimento ORDER BY id", mConn);
                    myDataSet = new DataSet();
                    mAdapter.Fill(myDataSet, "estabelecimento");

                    foreach (DataRow item in myDataSet.Tables[0].Rows)
                    {
                        Estabelecimento estabelecimento = new Estabelecimento();
                        estabelecimento.id = (Int64)item["id"];
                        estabelecimento.nome = item["nome"].ToString();
                        estabelecimento.cnpj = item["cnpj"].ToString();
                        estabelecimento.natureza_juridica = item["natureza_juridica"].ToString();
                        estabelecimento.situacao = item["situacao"].ToString();
                        estabelecimentos.Add(estabelecimento);
                    }
                    
                    mConn.Close();
                    
                    var model = new List<Object>();
                    model.Add(new { est = estabelecimentos, cli = clientes });

                    return View["cadPagamento", model];
                }
                catch (InvalidCastException)
                {
                    return HttpStatusCode.NoResponse;
                }
                catch (MySqlException)
                {
                    return HttpStatusCode.NotAcceptable;
                }
                
                
            };
        }
    }
}