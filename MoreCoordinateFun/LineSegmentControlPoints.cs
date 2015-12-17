using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphericalTrigonometry
{
    struct LineSegmentControlPoints
    {
        public readonly string values;

        public LineSegmentControlPoints(string value)
        {
            this.values = value;
        }

        public bool isValid() 
        {
            return !string.IsNullOrEmpty(this.values);
        }

        public string[] tokens() 
        {
            string[] tokenColletion = null;

            if (!string.IsNullOrEmpty(this.values))
            {
                tokenColletion = this.values.Split(' ');
            }

            return tokenColletion;
        }
    }
}
