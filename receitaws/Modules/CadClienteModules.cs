using Nancy;

namespace receitaws.Modules
{
    public class CadClienteModule : NancyModule
    {
        public CadClienteModule() : base("/TestCadCliente")
        {
            Get["/"] = parameters =>
            {
                return View["cadCliente"];
            };
        }
    }
}