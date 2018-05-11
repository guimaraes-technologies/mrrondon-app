namespace MrRondon.ViewModels
{
    public class LocationVm
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Address { get; set; }
        public byte[] Image { get; set; }
        public PinType PinType { get; set; }
        public LocationType LocationType { get; set; }
        public Position Position { get; set; }
    }

    public class Position
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}