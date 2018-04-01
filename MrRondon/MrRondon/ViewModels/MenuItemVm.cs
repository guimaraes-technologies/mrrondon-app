namespace MrRondon.ViewModels
{
  public  class MenuItemVm
    {
        public MenuItemVm(string title, string icon, MenuType type)
        {
            Title = title;
            Icon = icon;
            Type = type;
        }

        public string Title { get; private set; }
        public string Icon { get; private set; }
        public MenuType Type { get; private set; }
    }
}

public enum MenuType
{
    Home,
    FavoriteEvent,
    ContactUs,
    Configurations,
    Logout
}