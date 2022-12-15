using CapaEntidad.Paypal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Paypal
    {
        //Obtener información para usarla en el API
        private static string urlPaypal = ConfigurationManager.AppSettings["UrlPaypal"];
        private static string clientId = ConfigurationManager.AppSettings["ClientId"];
        private static string secret = ConfigurationManager.AppSettings["Secret"];

        //Método que nos permite crear una Solicitud de cobro en Paypal
        public async Task<Response_Paypal<Response_Checkout>> CrearSolicitud(Checkout_Order orden)
        {
            Response_Paypal<Response_Checkout> responsePaypal = new Response_Paypal<Response_Checkout>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlPaypal);

                //Creamos una autenticacion a traves de las crendenciales que le pasamos
                var authToken = Encoding.ASCII.GetBytes($"{clientId}:{secret}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                var json = JsonConvert.SerializeObject(orden);

                //Convertimos en tipo de contenido que necesita el API
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutamos la API (utilizando el cliente mediante una solicitud de tipo POST)
                //Utilizamos la API (la cual nos permite crear solicitud) y le enviamos la data
                HttpResponseMessage response = await client.PostAsync("/v2/checkout/orders", data);

                //leemos la respuesta
                responsePaypal.Status = response.IsSuccessStatusCode;

                if (response.IsSuccessStatusCode)
                {
                    string jsonrespuesta = response.Content.ReadAsStringAsync().Result;

                    Response_Checkout checkout = JsonConvert.DeserializeObject<Response_Checkout>(jsonrespuesta);

                    responsePaypal.Response = checkout;
                }

                return responsePaypal;
            }
        }

        public async Task<Response_Paypal<Response_Capture>> AprobarPago(string token)
        {
            Response_Paypal<Response_Capture> responsePaypal = new Response_Paypal<Response_Capture>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlPaypal);

                //Creamos una autenticacion a traves de las crendenciales que le pasamos
                var authToken = Encoding.ASCII.GetBytes($"{clientId}:{secret}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                //Convertimos en tipo de contenido que necesita el API
                var data = new StringContent("{}", Encoding.UTF8, "application/json");

                //Ejecutamos la API (utilizando el cliente mediante una solicitud de tipo POST)
                //Utilizamos la API (la cual nos permite crear solicitud) y le enviamos la data
                HttpResponseMessage response = await client.PostAsync($"/v2/checkout/orders/{token}/capture", data);

                //leemos la respuesta
                responsePaypal.Status = response.IsSuccessStatusCode;

                if (response.IsSuccessStatusCode)
                {
                    string jsonrespuesta = response.Content.ReadAsStringAsync().Result;

                    Response_Capture capture = JsonConvert.DeserializeObject<Response_Capture>(jsonrespuesta);

                    responsePaypal.Response = capture;
                }

                return responsePaypal;
            }
        }
    }
}
