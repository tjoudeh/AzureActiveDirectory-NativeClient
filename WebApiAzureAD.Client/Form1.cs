using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebApiAzureAD.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        Uri redirectUri = new Uri(ConfigurationManager.AppSettings["ida:RedirectUri"]);

        private static string authority = String.Format(aadInstance, tenant);

        private static string apiResourceId = ConfigurationManager.AppSettings["ApiResourceId"];
        private static string apiBaseAddress = ConfigurationManager.AppSettings["ApiBaseAddress"];

        private AuthenticationContext authContext = null;

        private async void button1_Click(object sender, EventArgs e)
        {

            authContext = new AuthenticationContext(authority);

            AuthenticationResult authResult = authContext.AcquireToken(apiResourceId, clientId, redirectUri);

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);

            HttpResponseMessage response = await client.GetAsync(apiBaseAddress + "api/orders");
          
            string responseString = await response.Content.ReadAsStringAsync();
           
            MessageBox.Show(responseString);

        }
    }
}
