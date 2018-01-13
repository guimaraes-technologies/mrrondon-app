using System;

namespace MrRondon.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string msg = "Infelizmente o serviço não foi encontrado.\nVerifique sua conexão com a internet") : base(msg){}
	}
}