using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ServiceStack.Text;
using StartupTechStack;

namespace StartupTechStackTest
{
    [TestFixture]
    public class CompanyDBServiceTest
    {

        CompanyDBService _companyDbService = new CompanyDBService();

        [Test]
        public async void SaveCompanyTest()
        {
            CrunchbaseGateway crunchbaseGateway = (new CrunchbaseGateway());
            var data = await crunchbaseGateway.FillCompanyDetails(new Company() { Permalink = "meetup" });

            _companyDbService.SaveCompany(data);
        }


        [Test]
        public async void SaveNYCompaniesTest()
        {
          

            CrunchbaseGateway crunchbaseGateway = (new CrunchbaseGateway());

            var readAllLines = File.ReadLines("nytech2.txt");

            Parallel.ForEach(readAllLines, async line =>
            {
                var data = await crunchbaseGateway.FillCompanyDetails(new Company() { Permalink = line });

                _companyDbService.SaveCompany(data);
            });


            "got here".PrintDump();
        }

    }
}