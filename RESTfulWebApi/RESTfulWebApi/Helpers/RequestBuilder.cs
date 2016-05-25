using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RESTfulWebApi.Helpers
{
    public class RequestBuilder
    {
        private string _object;
        private RestClient _client;
        public RequestBuilder(string relatedObject)
        {
            _object = relatedObject;
           _client = new RestClient(ConfigurationManager.AppSettings["ApiEndpoint"]);
        }

        public TClass GetObject<TClass>(object id = null)
        {
            string path = string.Empty;

            if (id != null)
                path = string.Format("/{0}", id.ToString());

            var request = new RestRequest(_object + path, Method.GET);
            request.AddHeader("User-Agent", "RESTfulWebApp");

            var response = _client.Execute(request) as RestResponse;

            return JsonConvert.DeserializeObject<TClass>(response.Content);
        }

        public TClass PostObject<TClass>(TClass obj)
        {
            var request = new RestRequest(_object, Method.POST);
            request.AddHeader("User-Agent", "RESTfulWebApp");
            request.AddJsonBody(obj);

            var response = _client.Execute(request) as RestResponse;

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new HttpException((int)response.StatusCode, "Ocorreu um erro ao executar a chamada.");
            }

            return JsonConvert.DeserializeObject<TClass>(response.Content);
        }

        public TClass PutObject<TClass>(TClass obj, object id)
        {
            var request = new RestRequest(string.Format("{0}/{1}",_object, id), Method.PUT);
            request.AddHeader("User-Agent", "RESTfulWebApp");
            request.AddJsonBody(obj);

            var response = _client.Execute(request) as RestResponse;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new HttpException((int)response.StatusCode, "Ocorreu um erro ao executar a chamada.");
            }

            return JsonConvert.DeserializeObject<TClass>(response.Content);
        }

        public void DeleteObject(object id)
        {
            var request = new RestRequest(string.Format("{0}/{1}", _object, id), Method.DELETE);
            request.AddHeader("User-Agent", "RESTfulWebApp");

            var response = _client.Execute(request) as RestResponse;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new HttpException((int)response.StatusCode, "Ocorreu um erro ao executar a chamada.");
            }
        }

    }
}