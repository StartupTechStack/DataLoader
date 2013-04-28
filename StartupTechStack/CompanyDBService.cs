using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupTechStack
{
    
    public class CompanyDBService : BaseDbService
    {
  
        public void SaveCompany(Company aCompany)
        {
            if (null == aCompany)
            {
                return;

            }
              
            GetCollection<Company>().Save(aCompany);
        }


        
    }
}
