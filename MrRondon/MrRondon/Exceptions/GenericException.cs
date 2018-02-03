using System;

namespace MrRondon.Exceptions
{
    public class GenericException : Exception
    {
        public GenericException(string msg = "Não foi possível concluir a requisição") : base(msg) { }
    }
}