using Microsoft.Owin;
using Microsoft.Owin.Security.ActiveDirectory;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApiAzureAD.Api.App_Start;

[assembly: OwinStartup(typeof(WebApiAzureAD.Api.Startup))]
namespace WebApiAzureAD.Api
{

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
 
            ConfigureAuth(app);

            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                new WindowsAzureActiveDirectoryBearerAuthenticationOptions
                {
                    TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters()
                    { 
                        ValidAudience =  ConfigurationManager.AppSettings["Audience"] ,
                    },
                    Tenant = ConfigurationManager.AppSettings["Tenant"]
                });
        }
    }

}
