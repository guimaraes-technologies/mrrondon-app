using System;

namespace MrRondon.Exceptions
{
    public class WithOutInternetConnectionException : Exception
    {
        public WithOutInternetConnectionException(string msg = "Você está sem conexão com a internet") : base(msg) { }
    }
}