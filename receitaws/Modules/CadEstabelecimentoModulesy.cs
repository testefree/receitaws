using Nancy;

namespace receitaws.Modules
{
    public class CadEstabelecimentoModule : NancyModule
    {
        public CadEstabelecimentoModule() : base("/TestCadEstabelecimento")
        {
            Get["/"] = parameters =>
            {
                return View["cadEstabelecimento"];
            };
        }
    }
}