using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphericalTrigonometry
{
    struct LineSegmentCtrlPoint
    {

        public readonly string value;

        public LineSegmentCtrlPoint(string value)
        {
            this.value = value;
        }

        public bool isValid()
        {
            //I trust but I verify!!
            bool valid = false;
            if (!string.IsNullOrEmpty(this.value))
            {
                string[] constituents = this.value.Split(',');

                if (constituents.Length == 3)
                {
                    valid = true;
                }
            }
            return valid;
        }

        public string[] tokens()
        {
            return this.value.Split(',');
        }
    }
}
