using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventosWebApp.Helper
{
    public class EventosWebAPI
    {

        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:61004/");
            return Client;

        }
    }
}
