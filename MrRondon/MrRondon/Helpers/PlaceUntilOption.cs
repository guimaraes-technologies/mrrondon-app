namespace MrRondon.Helpers
{
    public enum PlaceUntilOption
    {
        [EnumValueData(KeyValue = "100", Description = "100 metros")]
        Hundred,

        [EnumValueData(KeyValue = "300", Description = "300 metros")]
        ThreeHundred,

        [EnumValueData(KeyValue = "500", Description = "500 metros")]
        FiveHundred,

        [EnumValueData(KeyValue = "1000", Description = "1.000 metros")]
        Thousand,

        [EnumValueData(KeyValue = "3000", Description = "3.000 metros")]
        ThreeThousand,

        [EnumValueData(KeyValue = "5000", Description = "5.000 metros")]
        FiveThousand,

        [EnumValueData(KeyValue = "10000", Description = "10.000 metros")]
        TenThousand
    }
}