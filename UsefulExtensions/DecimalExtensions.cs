using System;

namespace UsefulExtensions
{
    public static class DecimalExtensions
    {
        public static decimal CeilingWithPlaces(this decimal instance, int places)
        {
            var scale = (decimal)Math.Pow(10, places);
            var multiplied = instance * scale;
            var ceiling = Math.Ceiling(multiplied);
            return ceiling / scale;
        }
    }
}
