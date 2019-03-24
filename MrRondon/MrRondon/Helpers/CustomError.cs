namespace MrRondon.Helpers
{
   public class CustomError
    {
        public CustomError(string message)
        {
            Title = Constants.AppName;
            Message = message;
        }

        public CustomError(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public string Title { get; private set; }
        public string Message { get; private set; }
    }
}