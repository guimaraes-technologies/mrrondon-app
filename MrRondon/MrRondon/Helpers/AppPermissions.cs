namespace MrRondon.Helpers
{
    public class AppPermissions
    {
        public AppPermissions(bool locationGranted)
        {
            LocationGranted = locationGranted;
        }

        public bool LocationGranted { get; private set; }
    }
}