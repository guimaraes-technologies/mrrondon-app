using System;

namespace MrRondon.Exceptions
{
    public class BadGatewayRequestException : Exception
    {
        public BadGatewayRequestException(string msg = "Não foi possível obter as informações solicitadas no momento...") : base(msg) { }
    }
}