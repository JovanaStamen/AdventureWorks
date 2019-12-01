using AdventureWorks.WebAPI.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AdventureWorks.WebAPI.AutoFacConfiguration
{
    public class Bootstrapper
    {
        public static void Run()
        {
            //Configure Automapper
            AutoMapperConfiguration.Configure();
            //Configure AutoFac  
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
        }
    }
}