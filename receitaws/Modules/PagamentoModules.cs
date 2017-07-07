using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using MySql.Data.MySqlClient;
using System.Data;
using Nancy.ModelBinding;
using receitaws.Entity;

namespace receitaws.Modules
{
    public class PagamentoModule : Nancy.NancyModule
    {

        //lista de cliente que será manipulada 
        private List<Pagamento> pagamentos = new List<Pagamento>();
        private List<PagamentoEstabelecimento> pagamentoEstabelecimentos = new List<PagamentoEstabelecimento>();
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private String stringConexao = "Persist Security Info=False; server=localhost;database=receitaws;uid=root;Convert Zero Datetime=True";

     
        public PagamentoModule() : base("/payments")
        {
            Get["/"] = parameter =>
            {
                try
                {

                    mConn = new MySqlConnection(stringConexao);
                    string sql = " SELECT  ";
                    sql += "e.id as id_estabelecimento, ";
                    sql += "e.nome as nome_estabelecimento,";
                    sql += "e.cnpj, ";
                    sql += "e.natureza_juridica, ";
                    sql += "e.situacao, ";
                    sql += "c.id as id_cliente, ";
                    sql += "c.nome as nome_cliente, ";
                    sql += "c.cpf, ";
                    sql += "c.dt_nascimento, ";
                    sql += "c.num_cartao, ";
                    sql += "p.id as id_pagamento, ";
                    sql += "p.valor, ";
                    sql += "p.dt_pagamento, ";
                    sql += "p.cancelado ";
                    sql += "from pagamento as p ";
                    sql += "inner join cliente as c ";
                    sql += "    on c.id = p.id_cliente ";
                    sql += "inner join estabelecimento as e ";
                    sql += "    on e.id = p.id_estabelecimento where not p.cancelado=1";
                    mAdapter = new MySqlDataAdapter(sql, mConn);
                    DataSet myDataSet = new DataSet();
                    mAdapter.Fill(myDataSet, "cliente");

                    foreach (DataRow item in myDataSet.Tables[0].Rows)
                    {
                        PagamentoEstabelecimento pagamentoEstabelecimento = new PagamentoEstabelecimento();
                        pagamentoEstabelecimento.id_estabelecimento = (Int64)item["id_estabelecimento"];
                        pagamentoEstabelecimento.nome_Estabelecimento = item["nome_estabelecimento"].ToString();
                        pagamentoEstabelecimento.cnpj = item["cnpj"].ToString();
                        pagamentoEstabelecimento.natureza_juridica = item["natureza_juridica"].ToString();
                        pagamentoEstabelecimento.situacao = item["situacao"].ToString();

                        pagamentoEstabelecimento.id_cliente = (Int64)item["id_cliente"];
                        pagamentoEstabelecimento.nome_cliente = item["nome_cliente"].ToString();
                        pagamentoEstabelecimento.dt_nascimento = DateTime.Parse(item["dt_nascimento"].ToString());
                        pagamentoEstabelecimento.num_cartao = (int)item["num_cartao"];
           
                        pagamentoEstabelecimento.id_pagamento = (Int64)item["id_pagamento"];
                        pagamentoEstabelecimento.Valor = (float)item["valor"];
                        pagamentoEstabelecimento.dt_pagamento = DateTime.Parse(item["dt_pagamento"].ToString());
                        pagamentoEstabelecimento.cancelado = (int)item["cancelado"];
                        pagamentoEstabelecimentos.Add(pagamentoEstabelecimento);

                    }
                    mConn.Close();
                    return Response.AsJson(pagamentoEstabelecimentos);
                }catch (InvalidCastException e)
                {
                    return HttpStatusCode.NoResponse;
                }
                catch (MySqlException e)
                {
                    return HttpStatusCode.NotAcceptable;
                }
        };
            
            Get["/{id}"] = parameter => 
            {
                try { 
                    mConn = new MySqlConnection(stringConexao);
                    string sql = " SELECT  ";
                    sql += "e.id as id_estabelecimento, ";
                    sql += "e.nome as nome_estabelecimento,";
                    sql += "e.cnpj, ";
                    sql += "e.natureza_juridica, ";
                    sql += "e.situacao, ";
                    sql += "c.id as id_cliente, ";
                    sql += "c.nome as nome_cliente, ";
                    sql += "c.cpf, ";
                    sql += "c.dt_nascimento, ";
                    sql += "c.num_cartao, ";
                    sql += "p.id as id_pagamento, ";
                    sql += "p.valor, ";
                    sql += "p.dt_pagamento, ";
                    sql += "p.cancelado ";
                    sql += "from pagamento as p ";
                    sql += "inner join cliente as c ";
                    sql += "    on c.id = p.id_cliente ";
                    sql += "inner join estabelecimento as e ";
                    sql += "    on e.id = p.id_estabelecimento where p.id='"+ parameter .id + "'";
                    mAdapter = new MySqlDataAdapter(sql, mConn);
                    DataSet myDataSet = new DataSet();
                    mAdapter.Fill(myDataSet, "cliente");

                    foreach (DataRow item in myDataSet.Tables[0].Rows)
                    {
                        PagamentoEstabelecimento pagamentoEstabelecimento = new PagamentoEstabelecimento();
                        pagamentoEstabelecimento.id_estabelecimento = (Int64)item["id_estabelecimento"];
                        pagamentoEstabelecimento.nome_Estabelecimento = item["nome_estabelecimento"].ToString();
                        pagamentoEstabelecimento.cnpj = item["cnpj"].ToString();
                        pagamentoEstabelecimento.natureza_juridica = item["natureza_juridica"].ToString();
                        pagamentoEstabelecimento.situacao = item["situacao"].ToString();

                        pagamentoEstabelecimento.id_cliente = (Int64)item["id_cliente"];
                        pagamentoEstabelecimento.nome_cliente = item["nome_cliente"].ToString();
                        pagamentoEstabelecimento.dt_nascimento = DateTime.Parse(item["dt_nascimento"].ToString());
                        pagamentoEstabelecimento.num_cartao = (int)item["num_cartao"];
                        
                        pagamentoEstabelecimento.id_pagamento = (Int64)item["id_pagamento"];
                        pagamentoEstabelecimento.Valor = (float)item["valor"];
                        pagamentoEstabelecimento.dt_pagamento = DateTime.Parse(item["dt_pagamento"].ToString());
                        pagamentoEstabelecimento.cancelado = (int)item["cancelado"];
                        pagamentoEstabelecimentos.Add(pagamentoEstabelecimento);

                    }
                    mConn.Close();
                    return Response.AsJson(pagamentoEstabelecimentos);
                }
                catch (InvalidCastException e)
                {
                    return HttpStatusCode.NoResponse;
                }
                catch (MySqlException e)
                {
                    return HttpStatusCode.NotAcceptable;
                }

            };

            Post["/"] = parameters =>
            {
                try
                {
                    Pagamento pagamentoRequest = this.Bind();

                    if (pagamentoRequest == null)
                    {
                        return HttpStatusCode.Unauthorized;
                    }
                    
                    mConn = new MySqlConnection(stringConexao);

                    mConn.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandText = "INSERT INTO pagamento(id, valor, dt_pagamento, id_cliente, id_estabelecimento)   VALUES(@param1,@param2,@param3,@param4,@param5)";

                    cmd.Parameters.AddWithValue("@param1", pagamentoRequest.id);
                    cmd.Parameters.AddWithValue("@param2", pagamentoRequest.Valor);
                    // se o pagamento vier do post, não foi especificado no projeto
                    cmd.Parameters.AddWithValue("@param3", pagamentoRequest.dt_pagamento.ToString());
                    // se a data do pagamento for introduzida na hora, não foi especificado no projeto
                    //DateTime dateValue = DateTime.Parse(DateTime.Now.ToShortTimeString());
                    //cmd.Parameters.AddWithValue("@param3", dateValue.ToString("yyyy-MM-dd HH:mm"));
                    cmd.Parameters.AddWithValue("@param4", (Int64)pagamentoRequest.id_cliente);
                    cmd.Parameters.AddWithValue("@param5", (Int64)pagamentoRequest.id_estabelecimento);

                    cmd.ExecuteNonQuery();

                    mConn.Close();
                    return Response.AsJson(pagamentoRequest, HttpStatusCode.OK );
                }
                catch (InvalidCastException e)
                {
                    return HttpStatusCode.NoResponse;
                }
                catch (MySqlException e)
                {
                    return HttpStatusCode.NotAcceptable;
                }
            };

            Put["/{id}"] = parameter => 
            {
                return HttpStatusCode.NoResponse; ; 
            };

            Delete["/{id}"] = parameter => 
            {
                try
                {
                    if (parameter.id == null || parameter.id == "")
                    {
                        return HttpStatusCode.Unauthorized;
                    }

                    mConn = new MySqlConnection(stringConexao);

                    mConn.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandText = "UPDATE pagamento set cancelado=1 where id = @param1";

                    cmd.Parameters.AddWithValue("@param1", (string)parameter.id);

                    cmd.ExecuteNonQuery();

                    mConn.Close();
                    return HttpStatusCode.OK;
                }
                catch (InvalidCastException e)
                {
                    return HttpStatusCode.NoResponse;
                }
                catch (MySqlException e)
                {
                    return HttpStatusCode.NotAcceptable;
                }
            };
        }

       
        private object GetById(int id)
        {
            // fake a return
            return new { Id = id, Title = "Site Admin", Level = 2 };
        }


    }
}