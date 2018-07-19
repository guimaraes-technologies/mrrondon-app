using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MrRondon.ViewModels;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace MrRondon.Services.Rest
{
    public class ContactRest : BaseRest
    {
        public async Task<bool> SendAsync(ContactMessageVm contactMessage)
        {
            if (!CrossConnectivity.Current.IsConnected) throw new Exception("Você está sem conexão com a internet");

            var jsonObject = JsonConvert.SerializeObject(contactMessage);

            var response = await HttpClient.PostAsync($"{UrlService}/contact/send", new StringContent(jsonObject, Encoding.UTF8, "application/json"));
            
            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var isEmailSend = JsonConvert.DeserializeObject<bool>(json);
                if (isEmailSend) return true;
            }

            var errors = await response.Content.ReadAsStringAsync();
            throw new Exception(errors);
        }

    }
}