using System;

namespace SphericalTrigonometry
{

    // Lots of math from http://williams.best.vwh.net/ftp/avsig/avform.txt
    // Also the classics http://www.movable-type.co.uk/scripts/latlong.html
    //                 & http://www.movable-type.co.uk/scripts/latlong-vincenty.html
    // Wikipedia is also great 



    class Geometry
    {
        public const double RadiusOfEarthInMeters = 6367444.7;
        public const double RadiusOfEarthInFeet = 20890566.601;

        // Find the great cirlce distance between Point A and Point B
        public static double HaversineDistance(Point A, Point B)
        {
            return 2 * Math.Asin(
                Math.Sqrt
                    (
                        Math.Pow(Math.Sin(Math.Abs(A.Latitude - B.Latitude) / 2), 2) +
                        Math.Cos(A.Latitude) * Math.Cos(B.Latitude) *
                        Math.Pow(Math.Sin(Math.Abs(A.Longitude - B.Longitude) / 2), 2)
                    )
                );
        }

        public static double Course(Point A, Point B)
        {
            return
                Math.Atan2(
                    (Math.Sin(Math.Abs(A.Longitude - B.Longitude)) * Math.Cos(B.Latitude)),
                    (
                        Math.Cos(A.Latitude) * Math.Sin(B.Latitude) -
                        Math.Sin(A.Latitude) * Math.Cos(B.Latitude) * Math.Cos(Math.Abs(A.Longitude - B.Longitude))
                        )
                    ) % (2 * Math.PI);
        }

        public static double CrossTrackDistance(Point A, Point B, Point C)
        {
            var aLong = RadianToDegree(A.Longitude);
            var bLong = RadianToDegree(B.Longitude);
            var cLOng = RadianToDegree(C.Longitude);
            //return Math.Asin(
            //    Math.Sin(HaversineDistance(A, C)) *
            //    Math.Sin(Course(A, C) - Course(A, B))
            //);

            //Point cn = new Point(c.X - a.X, c.Y - a.Y);
            //Point bn = new Point(b.X - a.X, b.Y - a.Y);

            //double angle = Math.Atan2(bn.Y, bn.X) - Math.Atan2(cn.Y, cn.X);
            //double abLength = Math.Sqrt(bn.X * bn.X + bn.Y * bn.Y);

            //return Math.Sin(angle) * abLength;

            // normalize points
            Point cn = new Point() { Longitude = (Math.Abs(C.Longitude) - Math.Abs(A.Longitude)), Latitude = (Math.Abs(C.Latitude) - Math.Abs(A.Latitude)) };
            var bn = new Point() { Longitude = (Math.Abs(B.Longitude) - Math.Abs(A.Longitude)), Latitude = (Math.Abs(B.Latitude) - Math.Abs(A.Latitude)) };

            double angle = Math.Atan2(bn.Latitude, bn.Longitude) - Math.Atan2(cn.Latitude, cn.Longitude);
            double abLength = Math.Sqrt((bn.Longitude * bn.Longitude) + (bn.Latitude * bn.Latitude));

            return Math.Sin(angle) * abLength;
        }

        public static double AlongTrackDistance(Point A, Point B, Point C)
        {
            return Math.Acos(
                Math.Cos(HaversineDistance(A, C)) /
                Math.Cos(CrossTrackDistance(A, B, C))
           );
        }

        public static double ConvertDegreesToRadians(double angleInDegrees)
        {
            return (Math.PI / 180) * angleInDegrees;
        }

        public static double RadianToDegree(double angle) { return angle * (180.0 / Math.PI); }

        public static double BearingTo(Point A, Point B)
        {
            double lat1 = A.Latitude;
            double lat2 = B.Latitude;
            double dLon = B.Longitude - A.Longitude;

            double y = Math.Sin(dLon) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);
            double brng = Math.Atan2(y, x);

            return (RadianToDegree(brng) + 360) % 360;
        }

        public static double bearing(Point A, Point B)
        {
            var dLon = B.Longitude - A.Longitude;
            var lat1 = A.Latitude;
            var lat2 = B.Latitude;
            var y = Math.Sin(dLon) * Math.Cos(lat2);
            var x = Math.Cos(lat1)* Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2)* Math.Cos(dLon);
            return ((RadianToDegree(Math.Atan2(y, x)) + 360) % 360);
        }

        public static double bearing_degrees(Point A, Point B) 
        {
            return RadianToDegree(bearing(A, B));
        }

        public static double crosstrack_error(Point A, Point B, Point C)
        {
            var dAp = HaversineDistance(A,C);
            var brngAp = ConvertDegreesToRadians (BearingTo(A, C));
            var brngAB = ConvertDegreesToRadians (BearingTo(A, B));
            var dXt = Math.Asin(Math.Sin(dAp) * Math.Sin(brngAp - brngAB));
            return Math.Abs(dXt);
        }
    
    }
}
