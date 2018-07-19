using MrRondon.Exceptions;
using MrRondon.Helpers;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
            if (json?.ToLower().Contains("usuário ou senha incorreta") ?? false)
                return new Exception("Usuário ou Senha incorreta");

            switch (response.StatusCode)
            {
                case HttpStatusCode.Forbidden: return new Exception("Acesso Negado\nNão foi possível concluir a requisição.");
                case HttpStatusCode.Unauthorized: return new NotAuthorizedException();
                case HttpStatusCode.NotFound: return new NotFoundException();
                case HttpStatusCode.BadGateway: return new BadGatewayRequestException();
                case HttpStatusCode.BadRequest: throw new Exception("Não foi possível concluir a requisição");
                case HttpStatusCode.InternalServerError: return new InternalServerErrorException();
                default: return new GenericException();
            }
        }

        protected async Task<TObject> GetObjectAsync<TObject>(string url) where TObject : class
        {
            return await GetAsync<TObject>(url);
        }

        protected async Task<TObject> GetAsync<TObject>(string url)
        {
            ValidateConnection();

            var httpResponse = await HttpClient.GetAsync(url);
            
            if (httpResponse.IsSuccessStatusCode && httpResponse.StatusCode == HttpStatusCode.OK)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TObject>(json);
                return result;
            }

            var error = await GenerateError(httpResponse);
            throw error;
        }

        public async Task<TObject> PostObjectAsync<TObject>(string url, StringContent content) where TObject : class
        {
            return await PostAsync<TObject>(url, content);
        }

        public async Task<TObject> PostAsync<TObject>(string url, StringContent content)
        {
            ValidateConnection();

            var httpResponse = await HttpClient.PostAsync(url, content);
            
            if (httpResponse.IsSuccessStatusCode && httpResponse.StatusCode == HttpStatusCode.OK)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TObject>(json);
                return result;
            }

            var error = await GenerateError(httpResponse);
            throw error;
        }

        public async Task<bool> PostObjectAsync(string url, StringContent content)
        {
            ValidateConnection();

            var httpResponse = await HttpClient.PostAsync(url, content);
            
            if (httpResponse.IsSuccessStatusCode && httpResponse.StatusCode == HttpStatusCode.OK)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<bool>(json);
                return result;
            }

            var error = await GenerateError(httpResponse);
            throw error;
        }

        protected async void ValidateConnection()
        {
            if (!CrossConnectivity.Current.IsConnected) throw new WithOutInternetConnectionException();
            if (!await CrossConnectivity.Current.IsRemoteReachable(Constants.Host)) throw new ServiceUnavailableException();
        }
    }
}