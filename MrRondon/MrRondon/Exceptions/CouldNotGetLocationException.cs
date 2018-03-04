using System;

namespace MrRondon.Exceptions
{
    public class CouldNotGetLocationException : Exception
    {
        public CouldNotGetLocationException(string msg = "Não foi possível obter a localização atual.\n Verifique se você está conectado a internet e se o GPS está ativado") : base(msg) { }
    }
}