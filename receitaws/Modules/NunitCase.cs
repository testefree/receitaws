using NUnit.Framework;
using System.Net;

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

                HttpWebRequest requestChangelog = null;
                HttpWebResponse changelogResponse = null;

                requestChangelog = (HttpWebRequest)HttpWebRequest.Create("http://localhost:51552/clients");
                requestChangelog.Method = "GET";

                changelogResponse = (HttpWebResponse)requestChangelog.GetResponse();

                var Code = (int)changelogResponse.StatusCode;

                Assert.That(Code, Is.EqualTo(Nancy.HttpStatusCode.OK));
            }

            [Test]
            public void ClienteGet1()
            {
                //var browser = new Browser(new Bootstrapper());

                //var response = browser.Get("/clients/1");

                //Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                HttpWebRequest requestChangelog = null;
                HttpWebResponse changelogResponse = null;

                requestChangelog = (HttpWebRequest)HttpWebRequest.Create("http://localhost:51552/clients/1");
                requestChangelog.Method = "GET";

                changelogResponse = (HttpWebResponse)requestChangelog.GetResponse();

                var Code = (int)changelogResponse.StatusCode;

                Assert.That(Code, Is.EqualTo(Nancy.HttpStatusCode.OK));
            }
        }
    }
}