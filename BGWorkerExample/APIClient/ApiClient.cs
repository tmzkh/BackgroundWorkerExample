using BGWorkerExample.Data;
using BGWorkerExample.Delegates;
using BGWorkerExample.Enums;
using BGWorkerExample.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BGWorkerExample.APIClient
{
    public class ApiClient : IApiClient
    {
        public void Request(HTTPRequestType requestType, string url, ApiRequestCallback callback, JObject data = null) {
            switch (requestType) {
                case HTTPRequestType.GET:
                    this.GetRequest(url);
                    break;
                case HTTPRequestType.POST:
                    this.PostRequest(url, data);
                    break;
                case HTTPRequestType.PUT:
                    break;
                case HTTPRequestType.DELETE:
                    break;
                default:
                    break;
            }
        }

        private JObject GetRequest(string url) {
            using (var client = new HttpClient()) {
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode) {
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;

                    return JObject.Parse(responseString);
                } else {
                    return null;
                }
            }
        }

        private JObject PostRequest(string url, JObject data) {
            using (var client = new HttpClient()) {

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content).Result;

                if (response.IsSuccessStatusCode) {
                    string responseString = response.Content.ReadAsStringAsync().Result;

                    return JObject.Parse(responseString);
                } else {
                    return null;
                }
            }
        }
    }
}
