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
            return Math.Asin(
                Math.Sin(HaversineDistance(A, C)) *
                Math.Sin(Course(A, C) - Course(A, B))
            );
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
    }
}
