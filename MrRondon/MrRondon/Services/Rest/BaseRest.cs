using MrRondon.Exceptions;
using MrRondon.Helpers;
using MrRondon.Services.Interfaces;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        public static async Task<CustomError> GetError(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            if (json?.ToLower().Contains("usuário ou senha incorreta") ?? false) return new CustomError("Usuário ou Senha incorreta");

            switch (response.StatusCode)
            {
                case HttpStatusCode.Forbidden: return new CustomError("Acesso Negado", "Não foi possível concluir a requisição.");
                case HttpStatusCode.Unauthorized: return new CustomError("Acesso não permitido", "Não foi possível concluir a requisição.");
                case HttpStatusCode.NotFound: return new CustomError("Recurso não encontrado", "Não foi possível concluir a requisição.");
                case HttpStatusCode.BadGateway: return new CustomError("Não foi possível concluir a requisição.");
                case HttpStatusCode.BadRequest: return new CustomError("Não foi possível concluir a requisição");
                case HttpStatusCode.InternalServerError: return new CustomError("Erro no servidor", "Não foi possível concluir a requisição.");
                default: return new CustomError("Não foi possível concluir a requisição.");
            }
        }

        protected async Task<CustomReturn<TObject>> GetObjectAsync<TObject>(string url) where TObject : class
        {
            return await GetAsync<TObject>(url);
        }

        protected async Task<CustomReturn<TObject>> GetAsync<TObject>(string url)
        {
            var json = string.Empty;
            try
            {
                var resultConnection = await ValidateConnection();
                if (!resultConnection.IsValid) return new CustomReturn<TObject>(resultConnection.Error);

                var httpResponse = await HttpClient.GetAsync(url);

                if (httpResponse.IsSuccessStatusCode && httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    json = await httpResponse.Content.ReadAsStringAsync();
                    var result = ValidateJson<TObject>(json);
                    return new CustomReturn<TObject>(result);
                }

                var error = await GetError(httpResponse);
                return new CustomReturn<TObject>(error);
            }
            catch (Exception ex)
            {
                var exceptionService = DependencyService.Get<IExceptionService>();

                exceptionService.TrackError(ex, new Dictionary<string, string>
                {
                    {"Date", $"{DateTime.UtcNow}"},
                    {"Url", url},
                    {"Object", json.Length > 125 ? json.Substring(0, 124) : json}
                });

                return new CustomReturn<TObject>(Constants.AppName, ex.Message);
            }
        }

        public async Task<CustomReturn<TObject>> PostObjectAsync<TObject>(string url, StringContent content) where TObject : class
        {
            var result = await PostAsync<TObject>(url, content);

            return result;
        }

        public async Task<CustomReturn<TObject>> PostAsync<TObject>(string url, StringContent content)
        {
            var json = string.Empty;
            try
            {
                var resultConnection = await ValidateConnection();
                if (!resultConnection.IsValid) return new CustomReturn<TObject>(resultConnection.Error);

                var httpResponse = await HttpClient.PostAsync(url, content);

                if (httpResponse.IsSuccessStatusCode && httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    json = await httpResponse.Content.ReadAsStringAsync();

                    var result = ValidateJson<TObject>(json);
                    return new CustomReturn<TObject>(result);
                }

                var error = await GetError(httpResponse);
                return new CustomReturn<TObject>(error);
            }
            catch (Exception ex)
            {
                var exceptionService = DependencyService.Get<IExceptionService>();

                exceptionService.TrackError(ex, new Dictionary<string, string>
                {
                    {"Date", $"{DateTime.UtcNow}"},
                    {"Url", url},
                    {"Object", json.Length > 125 ? json.Substring(0, 124) : json}
                });

                return new CustomReturn<TObject>(Constants.AppName, ex.Message);
            }
        }

        public async Task<CustomReturn<bool>> PostObjectAsync(string url, StringContent content)
        {
            var json = string.Empty;
            try
            {
                var resultConnection = await ValidateConnection();
                if (!resultConnection.IsValid) return new CustomReturn<bool>(resultConnection.Error);

                var httpResponse = await HttpClient.PostAsync(url, content);

                if (httpResponse.IsSuccessStatusCode && httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    json = await httpResponse.Content.ReadAsStringAsync();
                    var result = ValidateJson<bool>(json);
                    return new CustomReturn<bool>(result);
                }

                var error = await GetError(httpResponse);
                return new CustomReturn<bool>(error);
            }
            catch (Exception ex)
            {
                var exceptionService = DependencyService.Get<IExceptionService>();

                exceptionService.TrackError(ex, new Dictionary<string, string>
                {
                    {"Date", $"{DateTime.UtcNow}"},
                    {"Url", url},
                    {"Object", json.Length > 125 ? json.Substring(0, 124) : json}
                });

                return new CustomReturn<bool>(Constants.AppName, ex.Message);
            }
        }

        protected async Task<CustomReturn<bool>> ValidateConnection()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected) return new CustomReturn<bool>(false, "Conectividade", "Você está sem conexão com a internet");
                if (!await CrossConnectivity.Current.IsRemoteReachable(Constants.Host))
                {
                    var exceptionService = DependencyService.Get<IExceptionService>();
                    exceptionService?.TrackError($"SERVIÇO INDISPONÍVEL \nHorário: {DateTime.UtcNow}");

                    return new CustomReturn<bool>(false, "Conectividade", $"Serviço do { Constants.AppName} não está disponível.");
                }

                return new CustomReturn<bool>(true);
            }
            catch (Exception ex)
            {
                var exceptionService = DependencyService.Get<IExceptionService>();
                exceptionService?.TrackError(ex, $"IMPOSSÍVEL VALIDAR CONEXÃO \nHorário: {DateTime.UtcNow}");

                return new CustomReturn<bool>(false);
            }
        }

        private static TObject ValidateJson<TObject>(string json)
        {
            if (json.Contains("<html") || json.Contains("<body"))
                throw new Exception("Houve um erro ao tentar conectar com o servidor");

            var result = JsonConvert.DeserializeObject<TObject>(json);

            return result;
        }
    }
}