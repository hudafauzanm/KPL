using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaint
{
    class Shape
    {
        private int type;
        private Point mulaiPoint;
        private Point selesaiPoint;

        public Shape (int type,Point mulaiPoint, Point selesaiPoint)
        {
            this.type = type;
            this.mulaiPoint = mulaiPoint;
            this.selesaiPoint = selesaiPoint;
        }

        public int getShapeType()
        {
            return this.type;
        }

        public Point getMulaiPoint()
        {
            return this.mulaiPoint;
        }

        public Point getSelesaiPoint()
        {
            return this.selesaiPoint;
        }

        public void setMulaiPoint(Point p)
        {
            mulaiPoint = p;
        }

        public void setSelesaiPoint (Point s)
        {
            selesaiPoint = s;
        }
    }
}
