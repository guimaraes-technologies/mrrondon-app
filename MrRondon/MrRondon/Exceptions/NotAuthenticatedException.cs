using System;  namespace MrRondon.Exceptions {
    public class NotAuthenticatedException : Exception
    {
        public string Title;          public             NotAuthenticatedException(string title = "Não Autenticado", string msg = "Você não está logado ou a sua sessão expirou\nÉ necessário realizar o login novamente no aplicativo") : base(msg)
        {             Title = title;         }
    } }