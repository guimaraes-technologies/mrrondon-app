using System;

namespace MrRondon.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string msg = "Serviço não encontrado.\nJá estamos trabalhando pra resolver") : base(msg){}
	}
}