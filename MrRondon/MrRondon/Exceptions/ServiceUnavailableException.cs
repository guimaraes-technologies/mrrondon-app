using System;
using MrRondon.Helpers;
using MrRondon.Services.Interfaces;
using Xamarin.Forms;

namespace MrRondon.Exceptions
{
    public sealed class ServiceUnavailableException : Exception
    {
        public ServiceUnavailableException() : base($"Serviço do {Constants.AppName} não está disponível.")
        {
            var exceptionService = DependencyService.Get<IExceptionService>();
            exceptionService.TrackError($"{Message} \nHorário: {DateTime.UtcNow}");
        }
    }
}