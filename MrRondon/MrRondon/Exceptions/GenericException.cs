using System;

namespace MrRondon.Exceptions
{
    public class GenericException : Exception
    {
        public GenericException(string msg = "") 
            : base($"Não foi possível concluir a requisição {(string.IsNullOrWhiteSpace(msg) ? "" : $"\n{msg}")}") { }
    }
}