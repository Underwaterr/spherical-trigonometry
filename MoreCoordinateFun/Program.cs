using System;
using System.Collections.Generic;
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

            //Route meow = new Route(data);

            //Console.WriteLine("Total: " + meow.GetLength()); 
            //Point pipe = new Point
            //{
            //    Latitude = Geometry.ConvertDegreesToRadians(29.99278),
            //    Longitude = Geometry.ConvertDegreesToRadians(94.00139)
            //};

            //// TODO: Find closest line segment to pipe!
            //// TODO: What if the point is on the line? Any weird 0s?
            //Point routePoint1 = meow.RoutePoints[0];
            //Point routePoint2 = meow.RoutePoints[1];

            //Console.WriteLine("Hello, world!");
            //Console.WriteLine("Cross talk distance is : "+Geometry.CrossTrackDistance(routePoint1, routePoint2, pipe) * Geometry.RadiusOfEarthInMeters);
            //Console.WriteLine("Along the track distance is : " + Geometry.AlongTrackDistance(routePoint1, routePoint2, pipe) * Geometry.RadiusOfEarthInMeters);
            //"c:\\temp\\test-to-use-for-parsing.kml"

            //"C:\\temp\\TX-1-Loop-SMoPac-Expy.kml"
            //"c:\\temp\\test-to-use-for-parsing.kml"

            //"C:\\temp\\Oil_Facilities\\doc-oil-facility.kml"
            //"C:\\temp\\route-progress-tracking-2.kml"

            string controlPoints = new SpreadRouteControlPointsBuilder("C:\\temp\\simple.kml").routeControlPoints();
            
            //string progressPin1 = "-97.77008338648402,30.27566457545169,0";
            //string progressPin2 = "-97.77027163183571,30.27561301992712,0";
            //string progressPin3 = "-97.7702012075341,30.27550678279508,144.1867842035163";
            //string progressPin4 = "-97.7687691967713,30.27745856255383,1.999999998159262";
            string progressPin007 = "-97.7714690,30.2644195,0";
            string progressPinBond007II = "-97.7703899999,30.275599,0";
            string progressPinBond007III = "-97.76897734494912,30.27704468875487,0";
            string updatedProgressPinBond007III = "-97.76911111111112,30.27704468875487,0";
            string progressPinBond007IV = "-97.76747332890632,30.2790466884187,0";
            string progressPinBond007V = "-97.76579465479254,30.28117304550298,0";
            string progressPinBond007VI = "-97.76494127786233,30.28257595864542,0";

            string crestewoodPin1 = "-104.1676145094278,31.716349524599,0";
                                     
            string FirstSimplePin = "-97.73219928919221,30.39619339186326,0";

            LineSegmentCtrlPoint ctrlPoint = new LineSegmentCtrlPoint(FirstSimplePin);



            Point pointOfInterest = point(ctrlPoint.tokens());
            List<LineSegmentCtrlPoint> route = new SpreadRouteControlPointsBuilder().routeControlPoints(controlPoints);

            List<string> crossTrackDistances = new List<string>();
            string msg = string.Empty;
            double minimumCrossTrack = 0;

            for (int i = 0; i < route.Count - 1; i++)
            {
                var firstPointTokens = route[i].tokens();
                var firstPt = point(firstPointTokens);

                var secondPointTokens = route[i + 1].tokens();
                var secondPt = point(secondPointTokens);

                double crossTrackDistance = Math.Abs(Geometry.CrossTrackDistance(firstPt, secondPt, pointOfInterest)) * Geometry.RadiusOfEarthInFeet;
                double alongTrackDistance = (Geometry.AlongTrackDistance(firstPt, secondPt, pointOfInterest) * Geometry.RadiusOfEarthInFeet);
                double trackDistance = (Geometry.HaversineDistance(firstPt, secondPt) * Geometry.RadiusOfEarthInFeet);

                //double distanceToLineSegment = EuclideanGeometry.LineToPointDistance2D(new double[2] { firstPt.Longitude, firstPt.Latitude }, 
                //                               new double[2] { secondPt.Longitude, secondPt.Latitude }, 
                //                               new double[2] { pointOfInterest.Longitude, pointOfInterest.Latitude}, true); 

                //msg = "Current mininum is : " + minimumCrossTrack;
                if (i == 0 || (crossTrackDistance < minimumCrossTrack)) //  && alongTrackDistance <= trackDistance
                {
                    minimumCrossTrack = crossTrackDistance;

                    msg = " First Point Longitude " + Geometry.RadianToDegree(firstPt.Longitude) + " Latitude : " + Geometry.RadianToDegree(firstPt.Latitude) +
                          " Cross Track distance is " + crossTrackDistance + " Second Point is Longitude : " + Geometry.RadianToDegree(secondPt.Longitude) + " Second Point Latitude is : " +
                          Geometry.RadianToDegree(secondPt.Latitude) + " Along track distance is : " + alongTrackDistance + " Track Distance : " + trackDistance;
                    

                }
            }

            crossTrackDistances.Add(msg);
            

            string formmattedDatetime = DateTime.Now.ToString("MM-dd-yy-H-mm-ss");

            System.IO.File.WriteAllLines(@"C:\temp\cross-track-results\results-" + formmattedDatetime + ".log", crossTrackDistances);
            Point losAngelosAirport = new Point() { Latitude = 0.592539, Longitude = 2.066470 };
            Point jfkAirport = new Point() { Latitude = 0.709186, Longitude = 1.287762 };
            Point offCourseLocation = new Point() { Latitude = 0.6021386, Longitude = 2.033309};
            double crossTrackRadian = Geometry.CrossTrackDistance(losAngelosAirport, jfkAirport, offCourseLocation);

            double myCalc = Geometry.crosstrack_error(losAngelosAirport, jfkAirport, offCourseLocation);

            double atd = Geometry.AlongTrackDistance(losAngelosAirport, jfkAirport, offCourseLocation);

            Console.WriteLine("Cross Track radian is : " + crossTrackRadian + " My calculation of the distance is : " + myCalc+ " Along track distance is : "+atd);


            Point firstSegmentLinePt1 = point(new string[3] { "-97.77686", "30.265412", "0" });
            Point firstSegmentLinePt2 = point(new string[3] { "-97.777018", "30.265636", "0" });

            double xtd = Math.Abs(Geometry.CrossTrackDistance(firstSegmentLinePt1, firstSegmentLinePt2, pointOfInterest));

            double xtdInFeet = Geometry.RadianToDegree(xtd);

            Point secondLineSegmentPt1 = point(new string[3] { "-97.7720246", "30.2730715", "0" });
            Point secondLineSegmentPt2 = point(new string[3] { "-97.771618", "30.273601", "0" });
            double xtd2 = Math.Abs(Geometry.CrossTrackDistance(secondLineSegmentPt1, secondLineSegmentPt2, pointOfInterest));

            double xtd2InFeet = Geometry.RadianToDegree(xtd2) ;
            
            Console.Read();
        }

        static Point point(string[] ctrlPointTokens) 
        {
            return new Point() { 
                                    Latitude = Geometry.ConvertDegreesToRadians(Convert.ToDouble(ctrlPointTokens[1])), 
                                    Longitude = Geometry.ConvertDegreesToRadians(Convert.ToDouble(ctrlPointTokens[0])) 
                                };
        }
    }
}
