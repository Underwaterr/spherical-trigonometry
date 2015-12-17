using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace SphericalTrigonometry
{
    class SpreadRouteControlPointsBuilder
    {
        private string kmlFilePath;
        private static string CONTROL_POINT_TAG_NAME = "coordinates";
        private static XNamespace KML_NAMESPACE = "http://www.opengis.net/kml/2.2";

        public SpreadRouteControlPointsBuilder( string kmlFilePath)
        {
            this.kmlFilePath = kmlFilePath;
        }

        public SpreadRouteControlPointsBuilder()
        { 
        }

        public string routeControlPoints()
        {
            XDocument kmlFile = XDocument.Load(this.kmlFilePath);

            var controlPoints = from k in kmlFile.Descendants(KML_NAMESPACE + CONTROL_POINT_TAG_NAME)
                                select new LineSegmentControlPoints(k.Value);

            string coordinates = string.Empty;

            foreach (LineSegmentControlPoints lineSegmentControlPoints in controlPoints) 
            {
                if (lineSegmentControlPoints.isValid())
                {
                    string[] controlPointTokens = lineSegmentControlPoints.tokens();

                    foreach (string pt in controlPointTokens)
                    {
                        var ctrlPoint = new LineSegmentCtrlPoint(pt);

                        if (ctrlPoint.isValid())
                        {
                            coordinates = string.Concat(coordinates, ctrlPoint.value, " ");
                        } 
                    }
                }
            }

            return coordinates;
        }

        public List<LineSegmentCtrlPoint> routeControlPoints(string route)
        {
            List<LineSegmentCtrlPoint> ctrlPoints = new List<LineSegmentCtrlPoint>();

            LineSegmentControlPoints lineSegmentControlPoints = new LineSegmentControlPoints(route);

            if (lineSegmentControlPoints.isValid())
            {

                string[] controlPointTokens = lineSegmentControlPoints.tokens();
                foreach (string pt in controlPointTokens)
                {
                    var ctrlPoint = new LineSegmentCtrlPoint(pt);

                    if (ctrlPoint.isValid())
                    {
                        ctrlPoints.Add(ctrlPoint);
                    }
                }
            }

            return ctrlPoints;
        }

    }
}
