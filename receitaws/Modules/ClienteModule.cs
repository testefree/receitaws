using System;
using System.Collections.Generic;
using Nancy;
using MySql.Data.MySqlClient;
using System.Data;
using Nancy.ModelBinding;
using receitaws.Entity;

namespace receitaws.Modules
{
    public class ClienteModule : Nancy.NancyModule
    {

        //lista de cliente que será manipulada 
        private List<Cliente> clientes = new List<Cliente>();
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private String stringConexao = "Persist Security Info=False; server=localhost;database=receitaws;uid=root;Convert Zero Datetime=True";

        public ClienteModule() : base("/clients")
        {
            Get["/"] = parameter =>
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
                    mConn.Close();
                    return Response.AsJson(clientes);
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
                //return GetById(parameter.id);
                /*
                clientes
                .Where(e => e.ID.Equals(parameter.id))
                .SingleOrDefault();
                */
                try
                {
                    mConn = new MySqlConnection(stringConexao);
                    mAdapter = new MySqlDataAdapter("SELECT * FROM cliente WHERE id='" + parameter.id + "' ORDER BY id", mConn);
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
                    mConn.Close();
                    return Response.AsJson(clientes);
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
                    Cliente clientetRequest = this.Bind();

                    if (clientetRequest == null || clientetRequest.dt_nascimento == DateTime.MinValue)
                    {
                        return HttpStatusCode.Unauthorized;
                    }
                    //Console.WriteLine(Response.AsJson(jsonString));

                    mConn = new MySqlConnection(stringConexao);

                    mConn.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandText = "INSERT INTO cliente(id, nome, cpf, dt_nascimento, num_cartao)   VALUES(@param1,@param2,@param3,@param4,@param5)";

                    cmd.Parameters.AddWithValue("@param1", clientetRequest.id);
                    cmd.Parameters.AddWithValue("@param2", clientetRequest.nome);
                    cmd.Parameters.AddWithValue("@param3", clientetRequest.cpf);
                    cmd.Parameters.AddWithValue("@param4", clientetRequest.dt_nascimento.ToString());
                    cmd.Parameters.AddWithValue("@param5", clientetRequest.num_cartao);

                    cmd.ExecuteNonQuery();

                    mConn.Close();
                    return Response.AsJson(clientetRequest, HttpStatusCode.OK);
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
                    cmd.CommandText = "DELETE FROM cliente WHERE id=@param1";

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

        /*
        private object GetById(int id)
        {
            // fake a return
            return new { Id = id, Title = "Site Admin", Level = 2 };
        }
        */


    }
}