using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SimplePaint
{
    public partial class Form1 : Form
    {
        private readonly int LINE = 1;
        private readonly int RECT = 2;
        private readonly int CIRC = 3;
        private readonly int OPACITY = 2;
        private int typeShape;
        private ArrayList listShape = new ArrayList();
        private Point mulaiPoint;
        private Point selesaiPoint;
        private Pen pen;
        private Graphics graphics;
        private bool isPaint = false;
        private bool canPaint = false;
        private bool isMove = false;


        public Form1()
        {
            InitializeComponent();
            pen = new Pen(Color.Blue)
            {
                Width = OPACITY
            };
            graphics = panel1.CreateGraphics();
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetType();
            lineToolStripMenuItem.BackColor = Color.Blue;
            typeShape = LINE;
            isPaint = true;
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetType();
            rectangleToolStripMenuItem.BackColor = Color.Blue;
            typeShape = RECT;
            isPaint = true;
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetType();
            circleToolStripMenuItem.BackColor = Color.Blue;
            typeShape = CIRC;
            isPaint = true;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mulaiPoint = e.Location;
            if (e.Button == MouseButtons.Left && isPaint)
            {
                canPaint = true;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            selesaiPoint = e.Location;
            if (canPaint)
            {
                listShape.Add(new Shape(typeShape, mulaiPoint, selesaiPoint));
                //listShape.Add(new Shape(10, , selesaiPoint))
                canPaint = false;
                DrawAllShape();
            }
            else if(isMove)
            {
                DragShape();
            }
        }

        private void DragShape()
        {
            foreach(Shape shape in listShape)
            {
                if(shape.getShapeType() == LINE)
                {
                    if(CekTitikLine(shape))
                    {
                        Point jarak = new Point(selesaiPoint.X - mulaiPoint.X, selesaiPoint.Y - mulaiPoint.Y);
                        shape.setMulaiPoint(new Point(shape.getMulaiPoint().X + jarak.X, shape.getMulaiPoint().Y + jarak.Y));
                        shape.setSelesaiPoint(new Point(shape.getSelesaiPoint().X + jarak.X, shape.getSelesaiPoint().Y + jarak.Y));
                        DrawAllShape();
                        break;
                    }

                }
                else if (shape.getShapeType() == RECT)
                {
                    if (CekTitikRect(shape))
                    {
                        Point jarak = new Point(selesaiPoint.X - mulaiPoint.X, selesaiPoint.Y - mulaiPoint.Y);
                        shape.setMulaiPoint(new Point(shape.getMulaiPoint().X + jarak.X, shape.getMulaiPoint().Y + jarak.Y));
                        shape.setSelesaiPoint(new Point(shape.getSelesaiPoint().X + jarak.X, shape.getSelesaiPoint().Y + jarak.Y));
                        DrawAllShape();
                        break;
                    }
                }
                else if (shape.getShapeType() == CIRC)
                {
                    if (CekTitikCirc(shape))
                    {
                        Point jarak = new Point(selesaiPoint.X - mulaiPoint.X, selesaiPoint.Y - mulaiPoint.Y);
                        shape.setMulaiPoint(new Point(shape.getMulaiPoint().X + jarak.X, shape.getMulaiPoint().Y + jarak.Y));
                        shape.setSelesaiPoint(new Point(shape.getSelesaiPoint().X + jarak.X, shape.getSelesaiPoint().Y + jarak.Y));
                        DrawAllShape();
                        break;
                    }
                }
            }
        }

        private bool CekTitikCirc(Shape shape)
        {
            Rectangle circ = new Rectangle(Math.Min(shape.getMulaiPoint().X, shape.getSelesaiPoint().X),
                        Math.Min(shape.getMulaiPoint().Y, shape.getSelesaiPoint().Y),
                        Math.Abs(shape.getMulaiPoint().X - shape.getSelesaiPoint().X),
                        Math.Abs(shape.getMulaiPoint().Y - shape.getSelesaiPoint().Y));
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddEllipse(circ);

            return myPath.IsVisible(mulaiPoint);
        }

        private bool CekTitikRect(Shape shape)
        {
            Rectangle rect = new Rectangle(Math.Min(shape.getMulaiPoint().X, shape.getSelesaiPoint().X),
                        Math.Min(shape.getMulaiPoint().Y, shape.getSelesaiPoint().Y),
                        Math.Abs(shape.getMulaiPoint().X - shape.getSelesaiPoint().X),
                        Math.Abs(shape.getMulaiPoint().Y - shape.getSelesaiPoint().Y));
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddRectangle(rect);

            return myPath.IsVisible(mulaiPoint);
        }

        private bool CekTitikLine(Shape shape)
        {
            var path = new GraphicsPath();
            path.AddLine(shape.getMulaiPoint(), shape.getSelesaiPoint());
            return path.IsOutlineVisible(mulaiPoint, pen);
        }

        private void ResetType()
        {
            typeShape = 0;
            isPaint = false;
            isMove = false;
            lineToolStripMenuItem.BackColor = Color.White;
            rectangleToolStripMenuItem.BackColor = Color.White;
            circleToolStripMenuItem.BackColor = Color.White;
            dragToolStripMenuItem.BackColor = Color.White;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DrawAllShape()
        {
            this.Refresh();
            foreach(Shape shape in listShape)
            {
                if (shape.getShapeType() == LINE)
                {
                    DrawLine(shape);
                }
                else if (shape.getShapeType() == RECT)
                {
                    DrawRect(shape);
                }
                else if (shape.getShapeType() == CIRC)
                {
                    DrawCirc(shape);
                }
            }
        }

        private void DrawCirc(Shape shape)
        {
            Rectangle rectangle = new Rectangle(Math.Min(shape.getMulaiPoint().X, shape.getSelesaiPoint().X),
                        Math.Min(shape.getMulaiPoint().Y, shape.getSelesaiPoint().Y),
                        Math.Abs(shape.getMulaiPoint().X - shape.getSelesaiPoint().X),
                        Math.Abs(shape.getMulaiPoint().Y - shape.getSelesaiPoint().Y));
            graphics.DrawEllipse(pen, rectangle);
        }

        private void DrawRect(Shape shape)
        {
            Rectangle rectangle = new Rectangle(Math.Min(shape.getMulaiPoint().X, shape.getSelesaiPoint().X),
                        Math.Min(shape.getMulaiPoint().Y, shape.getSelesaiPoint().Y),
                        Math.Abs(shape.getMulaiPoint().X - shape.getSelesaiPoint().X),
                        Math.Abs(shape.getMulaiPoint().Y - shape.getSelesaiPoint().Y));
            graphics.DrawRectangle(pen, rectangle);
        }

        private void DrawLine(Shape shape)
        {
            graphics.DrawLine(pen, shape.getMulaiPoint(), shape.getSelesaiPoint());
        }

        private void dragToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetType();
            dragToolStripMenuItem.BackColor = Color.Blue;
            isMove = true;

        }
    }
}
