using System;
using System.Collections.Generic;
using System.Text;

namespace UsefulExtensions
{
    public static class Utils
    {
        public static double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6372.8; // In kilometers
            var dLat = Posicao.toRadians(lat2 - lat1);
            var dLon = Posicao.toRadians(lon2 - lon1);
            double p1Latitude = Posicao.toRadians(lat1);
            double p2Latitude = Posicao.toRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(p1Latitude) * Math.Cos(p2Latitude);
            var c = 2 * Math.Asin(Math.Sqrt(a));
            return R * 2 * Math.Asin(Math.Sqrt(a));
        }
    }

    public class Posicao
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Posicao()
        {

        }

        public Posicao(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        public Posicao(string latitude, string longitude)
        {
            Latitude = Convert.ToDouble(latitude);
            Longitude = Convert.ToDouble(longitude);
        }

        public static double toRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}
