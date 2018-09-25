using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPresentationLayer.WEB
{
    public static class Haversine
    {
        public static double calculate(Ocorrencia ocorrencia1, Ocorrencia ocorrencia2)
        {
            double lat1 = Convert.ToDouble(ocorrencia1.Lat);
            double lat2 = Convert.ToDouble(ocorrencia2.Lat);
            double lon1 = Convert.ToDouble(ocorrencia1.Lng);
            double lon2 = Convert.ToDouble(ocorrencia2.Lng);
            var R = 6372.8; // In kilometers
            var dLat = toRadians(lat2 - lat1);
            var dLon = toRadians(lon2 - lon1);
            lat1 = toRadians(lat1);
            lat2 = toRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Asin(Math.Sqrt(a));
            var dist = R * 2 * Math.Asin(Math.Sqrt(a));
            return dist * 1000;
        }

        public static double toRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}