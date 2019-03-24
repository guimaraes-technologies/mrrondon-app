namespace MrRondon.Helpers
{
    public class CustomReturn<T>
    {
        public CustomReturn(T value)
        {
            Value = value;
            IsValid = true;
        }

        public CustomReturn(CustomError error)
        {
            Error = error;
            IsValid = false;
        }

        public CustomReturn(string title, string error)
        {
            Error = new CustomError(Constants.AppName, error);
            IsValid = false;
        }

        public CustomReturn(T value, string title, string error)
        {
            Value = value;
            Error = new CustomError(title, error);
            IsValid = false;
        }

        public bool IsValid { get; private set; }
        public CustomError Error { get; private set; }
        public T Value { get; private set; }
    }
}