using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MrRondon.Exceptions;
using MrRondon.Helpers;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace MrRondon.Services.Rest
{
    public class BaseRest
    {
        protected static readonly Uri UrlService = new Uri($"{Constants.Host}/v1");
        protected readonly HttpClient HttpClient = new HttpClient { BaseAddress = UrlService };

        public BaseRest()
        {
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<Exception> GenerateError(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            //var error = JsonConvert.DeserializeObject<ErrorMessageRest>(json);

            switch (response.StatusCode)
            {
                case HttpStatusCode.Forbidden: throw new Exception("Acesso Negado\nNão foi possível concluir a requisição.");
                case HttpStatusCode.Unauthorized: throw new NotAuthorizedException();
                case HttpStatusCode.NotFound: throw new NotFoundException();
                case HttpStatusCode.BadGateway: throw new BadGatewayRequestException();
                case HttpStatusCode.BadRequest: throw new Exception("Não foi possível concluir a requisição");
                case HttpStatusCode.InternalServerError: throw new InternalServerErrorException();
                default: throw new GenericException();
            }
        }

        protected async Task<TObject> GetObjectAsync<TObject>(string url) where TObject : class
        {
            return await GetAsync<TObject>(url);
        }

        protected async Task<TObject> GetAsync<TObject>(string url)
        {
            if (!CrossConnectivity.Current.IsConnected) throw new WithOutInternetConnectionException();

            var response = await HttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TObject>(json);
            }

            await GenerateError(response);
            throw new GenericException();
        }

        public async Task<TObject> PostObjectAsync<TObject>(string url, StringContent content) where TObject : class
        {
            return await PostAsync<TObject>(url, content);
        }

        public async Task<TObject> PostAsync<TObject>(string url, StringContent content)
        {
            if (!CrossConnectivity.Current.IsConnected) throw new WithOutInternetConnectionException();

            var response = await HttpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TObject>(json);
            }

            await GenerateError(response);
            throw new GenericException();
        }

        public async Task<bool> PostObjectAsync(string url, StringContent content)
        {
            if (!CrossConnectivity.Current.IsConnected) throw new WithOutInternetConnectionException();

            var response = await HttpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(json);
            }

            await GenerateError(response);
            throw new GenericException();
        }
    }
}