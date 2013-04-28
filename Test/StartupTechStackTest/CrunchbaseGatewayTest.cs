using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ServiceStack.Text;
using StartupTechStack;

namespace StartupTechStackTest
{
    [TestFixture]
    public class CrunchbaseGatewayTest
    {

        [Test]
        public async void LoadCompaniesTest()
        {
            CrunchbaseGateway crunchbaseGateway = (new CrunchbaseGateway());
            var data = await crunchbaseGateway.LoadCompanies(0);

            data.PrintDump();
        }


        [Test]
        public async void FillCompanyDetailsTest()
        {
            CrunchbaseGateway crunchbaseGateway = (new CrunchbaseGateway());
            var data = await crunchbaseGateway.FillCompanyDetails(new Company() { Permalink = "meetup"}) ;

            data.PrintDump();
        }


        [Test]
        public async void LoadCompaniesAndFillDetailsTest()
        {
            CrunchbaseGateway crunchbaseGateway = (new CrunchbaseGateway());
            var data = await crunchbaseGateway.LoadCompanies(0);


            Parallel.ForEach(data, async company =>
             {
               await crunchbaseGateway.FillCompanyDetails(company);
            });
        }

    }
}
