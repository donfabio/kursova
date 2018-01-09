using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
   

    public partial class Form1 : Form
    {

        public int size = 100;
        public int posX, posY;
        public List<Shape> shapes = new List<Shape>();
        public int activeShape = -1;


        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            textBox1.BackColor = colorDialog1.Color;
            textBoxSize.Text = size.ToString();
        }

        

        private void buttonCircle_Click(object sender, EventArgs e)
        {

            shapes.Add(new Shape(pictureBox1.Width / 2 - size / 2, pictureBox1.Height / 2 - size / 2,size,colorDialog1.Color,'c'));
            activeShape = shapes.Count - 1;
            drawShape();
        }

        private void buttonRectangle_Click(object sender, EventArgs e)
        {
            shapes.Add(new Shape(pictureBox1.Width / 2 - size / 2, pictureBox1.Height / 2 - size / 2, size, colorDialog1.Color, 'r'));
            activeShape = shapes.Count - 1;
            drawShape();
        }

        private void buttonEllipse_Click(object sender, EventArgs e)
        {
            shapes.Add(new Shape(pictureBox1.Width / 2 - size / 2, pictureBox1.Height / 2 - size / 2, size, colorDialog1.Color, 'e'));
            activeShape = shapes.Count - 1;
            drawShape();
            
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            size += 25;
            textBoxSize.Text = size.ToString();
            if (activeShape != -1)
            {
                shapes[activeShape].SetSize(size);
            }
            drawShape();
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if (size != 25)
            {
                size -= 25;
                textBoxSize.Text = size.ToString();
                if (activeShape != -1)
                {
                    shapes[activeShape].SetSize(size);
                }
                drawShape();
            }
        }

        private void drawShape()
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBox1.Width, pictureBox1.Height);
            foreach(Shape s in shapes)
            {
                if (s.GetType() == 'c')
                {
                    g.FillEllipse(s.GetBrush(), s.GetX(), s.GetY(), s.GetSize(), s.GetSize());
                }else if(s.GetType() == 'r')
                {
                    g.FillRectangle(s.GetBrush(), s.GetX(), s.GetY(), s.GetSize(), s.GetSize());
                }
                else
                {
                    g.FillEllipse(s.GetBrush(), s.GetX(), s.GetY(), s.GetSize()*2, s.GetSize());
                }
            }
            
            pictureBox1.Refresh();
        }

        

        private void buttonColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            textBox1.BackColor = colorDialog1.Color;
            shapes[activeShape].SetBrush(colorDialog1.Color);
            drawShape();
        }

       
        private void textBoxSize_TextChanged(object sender, EventArgs e)
        {
            size = Int32.Parse(textBoxSize.Text);
            if (activeShape != -1)
            {
                shapes[activeShape].SetSize(size);
            }
            drawShape();
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int i = 0;
            bool change = false;
            foreach(Shape s in shapes)
            {
                if(e.X>(s.GetX()) && e.X < (s.GetX() + s.GetSize() ) 
                    && e.Y > (s.GetY()) && e.Y < (s.GetY()+ s.GetSize()))
                {
                    if(activeShape != i)
                    {
                        activeShape = i;
                        change = true;
                        break;
                    }
                    
                }
                i++;
            }
            if (!change)
            {
                shapes[activeShape].SetPos(e.X, e.Y);
                drawShape();
            }
        }
        
    }

    public class Shape
    {
        int posX;
        int posY;
        int size;
        SolidBrush b;
        char type;

        public Shape(int X, int Y, int _size, Color c,char _type)
        {
            posX = X;
            posY = Y;
            size = _size;
            b = new SolidBrush(c);
            type = _type;
        }

        public int GetX()
        {
            return posX;
        }

        public int GetY()
        {
            return posY;
        }

        public int GetSize()
        {
            return size;
        }

        public SolidBrush GetBrush()
        {
            return b;
        }

        public char GetType()
        {
            return type;
        }

        public void SetPos(int _x, int _y)
        {
            posX = _x - size / 2;
            posY = _y - size / 2;
        }

        public void SetSize(int _s)
        {
            size = _s ;
        }

        public void SetBrush(Color _c)
        {
            b = new SolidBrush(_c);
        }
    }
}
