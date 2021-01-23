using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PaintProgram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            graphics = panel1.CreateGraphics();
        }

        bool CanPaint = false;
        Graphics graphics;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            CanPaint = true;
            if (drawSquare)
            {
                SolidBrush solidB = new SolidBrush(toolStripButton1.ForeColor);
                graphics.FillRectangle(solidB, e.X, e.Y, Convert.ToInt32(toolStripTextBox2.Text), Convert.ToInt32(toolStripTextBox2.Text));
                CanPaint = false;
                drawRectangle = false;
                drawCircle = false;
                drawSquare = true;
            }
            else if (drawRectangle)
            {
                SolidBrush solidB = new SolidBrush(toolStripButton1.ForeColor);
                graphics.FillRectangle(solidB, e.X, e.Y, Convert.ToInt32(toolStripTextBox2.Text) * 2, Convert.ToInt32(toolStripTextBox2.Text));
                CanPaint = false;
                drawSquare = false;
                drawCircle = false;
               drawRectangle = true;
            }
            else if (drawCircle)
            {
                SolidBrush solidB = new SolidBrush(toolStripButton1.ForeColor);
                graphics.FillEllipse(solidB, e.X, e.Y, Convert.ToInt32(toolStripTextBox2.Text), Convert.ToInt32(toolStripTextBox2.Text));
                CanPaint = false;
                drawRectangle = false;
                drawSquare = false;
                drawCircle = true;
            }

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            CanPaint = false;
            PreviousX = null;
            PreviousY = null;
        }
        int? PreviousX = null;
        int? PreviousY = null;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (CanPaint)
            {
                //    SolidBrush solid = new SolidBrush(Color.Black);
                //    graphics.FillEllipse(solid, e.X, e.Y, Convert.ToInt32(toolStripTextBox1.Text), Convert.ToInt32(toolStripTextBox1.Text));

                Pen pen = new Pen(toolStripButton1.ForeColor, float.Parse(toolStripTextBox1.Text));
                graphics.DrawLine(pen, new Point(PreviousX ?? e.X, PreviousY ?? e.Y), new Point(e.X, e.Y));
                PreviousX = e.X;
                PreviousY = e.Y;    

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ColorDialog colorD = new ColorDialog();
            if (colorD.ShowDialog() == DialogResult.OK)
            {
                toolStripButton1.ForeColor = colorD.Color;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            graphics.Clear(panel1.BackColor);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ColorDialog colorD = new ColorDialog();
            if (colorD.ShowDialog() == DialogResult.OK)
            {
                toolStripButton3.ForeColor = colorD.Color;
                panel1.BackColor = colorD.Color;
            }
        }

        bool drawSquare = false;
        private void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawSquare = true;
        }


        bool drawRectangle = false;
        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawRectangle = true; 
        }

        bool drawCircle = false;
        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawCircle = true;

        }


        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] imagePaths = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach(string path in imagePaths)
            {
                graphics.DrawImage(Image.FromFile(path), new Point(0, 0));
            }
        }

    }
}
