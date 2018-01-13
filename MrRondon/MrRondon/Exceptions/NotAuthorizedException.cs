using System;

namespace MrRondon.Exceptions
{
	public class NotAuthorizedException: Exception
	{
	    public string Title { get; set; }

	    public NotAuthorizedException(string title="Não Autorizado", string msg = "Altere de conta para acessar esta funcionalidade ou realize o login novamente") : base(msg)
	    {
	        Title = title;
        }
	}
}