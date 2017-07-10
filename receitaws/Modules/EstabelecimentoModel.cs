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
    public class EstabelecimentoModule : Nancy.NancyModule
    {

        //lista de estabelecimento que será manipulada 
        private List<Estabelecimento> estabelecimentos = new List<Estabelecimento>();
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private String stringConexao = "Persist Security Info=False; server=localhost;database=receitaws;uid=root;Convert Zero Datetime=True";

        public EstabelecimentoModule() : base("/establishments")
        {
            Get["/"] = parameter =>
            {
                try
                {
                    mConn = new MySqlConnection(stringConexao);
                    mAdapter = new MySqlDataAdapter("SELECT * FROM estabelecimento ORDER BY id", mConn);
                    DataSet myDataSet = new DataSet();
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
                    return Response.AsJson(estabelecimentos);
                }catch(InvalidCastException e)
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
                try
                {
                    mConn = new MySqlConnection(stringConexao);
                    mAdapter = new MySqlDataAdapter("SELECT * FROM estabelecimento WHERE id='" + parameter.id + "' ORDER BY id", mConn);
                    DataSet myDataSet = new DataSet();
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
                    return Response.AsJson(estabelecimentos);
                }catch(InvalidCastException e)
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
                    Estabelecimento estabelecimentotRequest = this.Bind();

                    if (estabelecimentotRequest == null)
                    {
                        return HttpStatusCode.Unauthorized;
                    }
                    //Console.WriteLine(Response.AsJson(jsonString));

                    mConn = new MySqlConnection(stringConexao);

                    mConn.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandText = "INSERT INTO estabelecimento(id, nome, cnpj, natureza_juridica, situacao)   VALUES(@param1,@param2,@param3,@param4,@param5)";

                    cmd.Parameters.AddWithValue("@param1", estabelecimentotRequest.id);
                    cmd.Parameters.AddWithValue("@param2", estabelecimentotRequest.nome);
                    cmd.Parameters.AddWithValue("@param3", estabelecimentotRequest.cnpj);
                    cmd.Parameters.AddWithValue("@param4", estabelecimentotRequest.natureza_juridica);
                    cmd.Parameters.AddWithValue("@param5", estabelecimentotRequest.situacao);

                    cmd.ExecuteNonQuery();

                    mConn.Close();
                    return Response.AsJson(estabelecimentotRequest, HttpStatusCode.OK);
                }catch(InvalidCastException e)
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
                return HttpStatusCode.NoResponse;
            };

            Delete["/{id}"] = parameter => 
            {
                try
                {
                    if (parameter.id == null)
                    {
                        return HttpStatusCode.Unauthorized;
                    }
                   
                    mConn = new MySqlConnection(stringConexao);

                    mConn.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandText = "DELETE FROM estabelecimento WHERE id=@param1";

                    cmd.Parameters.AddWithValue("@param1", parameter.id);
                  
                    cmd.ExecuteNonQuery();

                    mConn.Close();
                    return Response.AsJson(HttpStatusCode.OK);
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

    }
}