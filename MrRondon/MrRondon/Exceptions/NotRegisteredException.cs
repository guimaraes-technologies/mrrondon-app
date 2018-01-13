using System;

namespace MrRondon.Exceptions
{
	public class NotRegisteredException : Exception
	{
		public NotRegisteredException(string msg = "Desculpe, mas você ainda não está registrado") : base(msg) { }
	}
}