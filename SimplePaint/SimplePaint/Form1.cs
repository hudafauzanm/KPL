using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
            lineToolStripMenuItem.BackColor = Color.Blue;
            typeShape = LINE;
            isPaint = true;
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && isPaint)
            {
                mulaiPoint = e.Location;
                canPaint = true;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if(canPaint)
            {
                selesaiPoint = e.Location;
                graphics.DrawLine(pen, mulaiPoint, selesaiPoint);
                canPaint = false;
            }
        }
    }
}
