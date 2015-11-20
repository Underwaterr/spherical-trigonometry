using System;
using System.Xml.Linq;

namespace SphericalTrigonometry
{
    class Program
    {
        static void Main()
        {

            // Started doing some XML stuff but that's on hold meow
            //XDocument kml = XDocument.Load("../../meow.xml");
            //XNamespace kmlNamespace = "http://www.opengis.net/kml/2.2"; // is this helpful?

            const string data =
                "-93.99999738163126,29.9921445767328,0 -94.00347221032369,29.99228341335801,0 " +
                "-94.0059349094318,29.99263601204909,0 -94.00672062872219,29.99292772502246,0 " +
                "-94.00730519344832,29.99337663014395,0 -94.00921273810748,29.99476902202765,0 " +
                "-94.00997907053092,29.99538118364479,0 -94.01072039901113,29.99599456586107,0 " +
                "-94.01353446366707,29.99841208652437,0 -94.01533340638456,30.00021173697568,0 " +
                "-94.0167699222187,30.00133856232781,0 -94.01696474264762,30.0011658354582,0 " +
                "-94.01725844797869,30.00116220584756,0 -94.01783362463857,30.00065823806882,0 " +
                "-94.02051121997575,30.0029469331074,0 -94.02068007286539,30.00315282015036,0 " +
                "-94.02523329824645,30.00557016469119,0 -94.02583077874817,30.00618206349898,0 " +
                "-94.02715378532086,30.00734149684019,0 -94.02945633249999,30.00932316455952,0 " +
                "-94.03048698857359,30.01020755799982,0 -94.03065077607666,30.01037282830535,0 " +
                "-94.03164190371366,30.01121657342633,0 -94.03175386532674,30.01121250017685,0 " +
                "-94.03304745446847,30.00999044705652,0 -94.03354495468599,30.00941406261607,0 " +
                "-94.0394874572096,30.00379193922467,0 -94.0480810150969,30.00026072992457,0 " +
                "-94.04874308132871,29.99905421586491,0 -94.05131414044119,29.99768821366784,0";

            Route meow = new Route(data);

            Console.WriteLine("Total: " + meow.GetLength()); 
            Point pipe = new Point
            {
                Latitude = Geometry.ConvertDegreesToRadians(29.99278),
                Longitude = Geometry.ConvertDegreesToRadians(94.00139)
            };

            // TODO: Find closest line segment to pipe!
            // TODO: What if the point is on the line? Any weird 0s?
            Point routePoint1 = meow.RoutePoints[0];
            Point routePoint2 = meow.RoutePoints[1];

            Console.WriteLine("Hello, world!");
            Console.WriteLine(Geometry.CrossTrackDistance(routePoint1, routePoint2, pipe) * Geometry.RadiusOfEarthInMeters);
            Console.WriteLine(Geometry.AlongTrackDistance(routePoint1, routePoint2, pipe) * Geometry.RadiusOfEarthInMeters);
            
            Console.Read();
        }
    }
}
