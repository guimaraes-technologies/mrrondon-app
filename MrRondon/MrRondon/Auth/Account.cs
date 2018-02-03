namespace MrRondon.Auth
{
    public abstract class Account
    {
        public static AccountManager Current => new AccountManager();
    }
}