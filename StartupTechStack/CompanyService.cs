using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;

namespace StartupTechStack
{

    [Route("/company/{Permalink}")]
    public class CompanyRequest
    {
        public string Permalink { get; set; }       
    }

     [Route("/company/{Permalink}", "POST")]
    public class UpdateTechStackRequest
    {
         public string Permalink { get; set; }
         public List<string> TechStack { get; set; }
    }
    public class CompanyService : ServiceStack.ServiceInterface.Service
    {

        public void Get(CompanyRequest adminRequest)
        {

        }

        public void Post(UpdateTechStackRequest adminRequest)
        {
        }
    }
}
