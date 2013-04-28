using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
            var data = await crunchbaseGateway.FillCompanyDetails(new Company() { Permalink = "general-assembly" });

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

        [Test]
        public async void SearchCompanyTest()
        {
            CrunchbaseGateway crunchbaseGateway = (new CrunchbaseGateway());
            var data = await crunchbaseGateway.SearchCompany( "meetup"  );

            data.PrintDump();
        }

        [Test]
        public async void FindPermalinksTest()
        {
            CrunchbaseGateway crunchbaseGateway = (new CrunchbaseGateway());
           
            var readAllLines = File.ReadLines("nytech.txt");

            Parallel.ForEach(readAllLines, async line =>
            {
                var data = await crunchbaseGateway.SearchCompany(line.Trim());

                "{0}: {1}".Fmt(line.Trim(), data.Join(", ")).PrintDump();
            });

        }

        [Test]
        public async void DownloadNYCompaniesTest()
        {
            CrunchbaseGateway crunchbaseGateway = (new CrunchbaseGateway());

            var readAllLines = File.ReadLines("nytech2.txt");

            Parallel.ForEach(readAllLines, async line =>
            {
                var data = await crunchbaseGateway.FillCompanyDetails(new Company() {Permalink = line}) ;

                data.PrintDump();
            });

        }

    }
}
