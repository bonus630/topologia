using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace br.corp.bonus630.topologia
{
    public class Point3
    {
        private double x = 0;
        private double y = 0;
        private double z = 0;
        public double Y { get { return this.y; } set{this.x = value;} }
        public double Z { get { return this.z; } set{this.y = value;} }
        public double X { get { return this.x; } set{this.z = value;} }
        public Point3(double x,double y,double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

    }
}
