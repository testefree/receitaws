using Nancy;
using NUnit.Framework;
using Nancy.Bootstrapper;
using Nancy.Helpers;
using Nancy.IO;

namespace receitaws.Modules
{
    public class NunitCase
    {
        [TestFixture]
        class MyTestCase
        {
            [Test]
            public void ClienteGet()
            {
                //var browser = new Browser(new Bootstrapper());

                //var response = browser.Get("/clients");

                //Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }

            [Test]
            public void ClienteGet1()
            {
                //var browser = new Browser(new Bootstrapper());

                //var response = browser.Get("/clients/1");

                //Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
        }
    }
}