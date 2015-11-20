using System.Collections.Generic;
using System.Linq;

namespace SphericalTrigonometry
{
    public class Route
    {
        public List<Point> RoutePoints { get; set; }

        public Route(string points)
        {
            RoutePoints = Parse(points);
        }

        public Route(List<Point> points)
        {
            RoutePoints = points;
        }

        public double GetLength()
        {
            double totalDistance = 0;
            // For each point except the last
            for (int i = 0; i < RoutePoints.Count - 1; i++)
                totalDistance += (Geometry.HaversineDistance(RoutePoints[i], RoutePoints[i + 1]) * Geometry.RadiusOfEarthInMeters);

            return totalDistance;
        }

        // Parses strings of x,y,z coordinates delimited by commas and spaces
        // Ex. -102.0345030095993,32.01404560474298,0 -102.0348333176769,32.01391900469123,0 -102.0350004718804,32.0146928768343,0
        public List<Point> Parse(string lineStringCoordinates)
        {
            return lineStringCoordinates.Split(' ').ToList()
                .Select(sub => sub.Split(','))
                    .Select(c => new Point
                    {
                        Latitude = Geometry.ConvertDegreesToRadians(double.Parse(c[0])),
                        Longitude = Geometry.ConvertDegreesToRadians(double.Parse(c[1]))
                        // Z coordinate is ignored
                    }).ToList();
        }
    }
}