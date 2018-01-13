using System;

namespace MrRondon.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        public string Title;

        public InternalServerErrorException(string title = "Erro Interno",
            string msg = "Erro interno, estamos trabalhando para resolver") : base(msg)
        {
            Title = title;
        }
    }
}