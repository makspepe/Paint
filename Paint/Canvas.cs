using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Canvas : Form
    {
        private int oldX, oldY;
        public Bitmap bmp; //основ для переноса в picbox
        private Bitmap startBmp = null; 
        private Bitmap tmpBmp = null; 
        private string FileName = "";
        //private double zoom = 1;
        private bool SaveNeed; //для сохранений при закрытии окон/программы

        public Canvas()
        {
            InitializeComponent();
            bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            pictureBox1.Image = bmp;
            MouseWheel += Zoom;
        }

        public Canvas(String FileName)
        {
            InitializeComponent();
            bmp = new Bitmap(FileName); 
            this.FileName = FileName;
            Size = new Size(bmp.Width, bmp.Height);
            startBmp = new Bitmap(FileName);
            Graphics g = Graphics.FromImage(bmp);
            this.SaveNeed = false; //для каждого холста отдельная проверка на изменения

            pictureBox1.Image = bmp;
            pictureBox1.Width = bmp.Width;
            pictureBox1.Height = bmp.Height;
            MouseWheel += Zoom;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) //кисть/ластик, предпоказ для линии и круга
        {
            
            if (e.Button == MouseButtons.Left && MainForm.Type == "Обычная кисть") //сразу сохраняем на bmp кисть
            {
                Graphics g = Graphics.FromImage(bmp);
                SaveNeed = true;
                g.DrawLine(new Pen(MainForm.CurColor, MainForm.CurWidth), oldX, oldY, e.X, e.Y);
                
                oldX = e.X;
                oldY = e.Y;
                pictureBox1.Invalidate();
            }
            else if (e.Button == MouseButtons.Left && MainForm.Type == "Ластик") //сразу сохраняем на bmp ластик
            {
                Graphics g = Graphics.FromImage(bmp);
                Pen pen;
                if (startBmp == null)
                {
                    pen = new Pen(ColorTranslator.FromHtml("#ABABAB"), MainForm.CurWidth);
                }
                else
                {
                    pen = new Pen(startBmp.GetPixel(oldX, oldY), MainForm.CurWidth);
                }
                SaveNeed = true;
                g.DrawLine(pen, oldX, oldY, e.X, e.Y);

                oldX = e.X;
                oldY = e.Y;
                pictureBox1.Invalidate();
            }

            else if (e.Button == MouseButtons.Left && MainForm.Type == "Линия")  
            {
                tmpBmp = new Bitmap(bmp); //нужна только для отображения временных линий и кругов
                Graphics g = Graphics.FromImage(tmpBmp);
                g.DrawLine(new Pen(MainForm.CurColor, MainForm.CurWidth), oldX, oldY, e.X, e.Y);
                pictureBox1.Image = tmpBmp;
            }
            else if (e.Button == MouseButtons.Left && MainForm.Type == "Круг")
            {
                tmpBmp = new Bitmap(bmp); //нужна только для отображения временных линий и кругов
                Graphics g = Graphics.FromImage(tmpBmp);
                g.DrawEllipse(new Pen(MainForm.CurColor, MainForm.CurWidth), oldX, oldY, (e.X - oldX), (e.Y - oldY));
                pictureBox1.Image = tmpBmp;
            }
        }

        public void Zoom(object sender, MouseEventArgs e) // масштаб x2 x0.5
        {
            SaveNeed = true;
            Bitmap B = new Bitmap(bmp);
            int width, height;
            const double delta = 2; 
            if (e.Delta > 0) //+
            {
                width = (int)(B.Width / delta);
                height = (int)(B.Height / delta);
            }
            else //-
            {
                width = (int)(B.Width * delta);
                height = (int)(B.Height * delta);
            }
            Graphics g = Graphics.FromImage(B);
            Rectangle R1 = new Rectangle(0, 0, B.Width, B.Height);
            Rectangle R2 = new Rectangle(e.X - width / 2, e.Y - height / 2, width, height);
            g.Clear(BackColor);
            g.DrawImage(pictureBox1.Image, R1, R2, GraphicsUnit.Pixel);
            pictureBox1.Image = B;
            pictureBox1.Invalidate();
            bmp = B;
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) // строим звезду относительно координат мыши при нажатии лкм
        {
            SaveNeed = true;
            oldX = e.X;
            oldY = e.Y;
            if (MainForm.Type == "Звезда")
            {
                PointF[] pts = new PointF[5]; //набираем массив координат для рисования 

                double cx = e.X;
                double cy = e.Y;

                // Start at the top.
                double theta = -Math.PI / 2;
                double dtheta = 4 * Math.PI / 5;
                for (int i = 0; i < 5; i++)
                {
                    pts[i] = new PointF(
                        (float)(cx + 40 * Math.Cos(theta)),
                        (float)(cy + 40 * Math.Sin(theta)));
                    theta += dtheta;
                }
                SaveNeed = true;
                Graphics g = Graphics.FromImage(bmp);
                g.DrawPolygon(new Pen(MainForm.CurColor, MainForm.CurWidth), pts);
                pictureBox1.Invalidate();
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) //оставляем линию или круг на рисунке когда отпускаем лкм 
        {
            if (e.Button == MouseButtons.Left)
            {
                SaveNeed = true;
                //MainForm.Saved = false;
                Graphics g = Graphics.FromImage(bmp);
                switch (MainForm.Type)
                {
                    case "Линия":
                        g.DrawLine(new Pen(MainForm.CurColor, MainForm.CurWidth), oldX, oldY, e.X, e.Y); // e координаты мыши x y
                        tmpBmp.Dispose(); //fixed memoryleak
                        break;
                    case "Круг":
                        g.DrawEllipse(new Pen(MainForm.CurColor, MainForm.CurWidth), oldX, oldY, (e.X - oldX), (e.Y - oldY));
                        tmpBmp.Dispose(); //fixed memoryleak
                        break;
                }
                pictureBox1.Image = bmp;
                
                pictureBox1.Invalidate();
            }
        }

        public int CanvasWidth
        {
            get
            {
                return pictureBox1.Width;
            }
            set
            {
                
                Size = new Size(value+16, CanvasHeight); //16 пикселей по сторонам
                pictureBox1.Width = value;
                bmp = new Bitmap(value, pictureBox1.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(ColorTranslator.FromHtml("#ABABAB"));
                g.DrawImage(bmp, new Point(0, 0));
                pictureBox1.Image = bmp;
            }
        }
        public int CanvasHeight
        {
            get
            {
                return pictureBox1.Height;
            }
            set
            {
                
                Size = new Size(CanvasWidth+16, value+39); //пиксели снизу и сверху(под закрытие сворачивание)
                pictureBox1.Height = value;
                bmp = new Bitmap(pictureBox1.Width, value); 
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(ColorTranslator.FromHtml("#ABABAB"));
                g.DrawImage(bmp, new Point(0,0));
                pictureBox1.Image = bmp;
            }
        }

        public void SaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.Filter = "Windows Bitmap (*.bmp)|*.bmp| Файлы JPEG (*.jpg)|*.jpg| Файлы PNG (*.png)|*.png";
            ImageFormat[] ff = { ImageFormat.Bmp, ImageFormat.Jpeg, ImageFormat.Png }; //сохраняем в bmp, jpeg, png
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FileName = dlg.FileName;
                bmp.Save(dlg.FileName, ff[dlg.FilterIndex - 1]);
                SaveNeed = false;
            }
        }
        public void Save() //сохранение или сохранение с заданием имени и расширения
        {
            if (FileName == "")
                SaveAs();
            else
            {
                bmp.Save(FileName);
                SaveNeed = false;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Canvas_FormClosing(object sender, FormClosingEventArgs e) //закрытие с проверкой на изменения
        {
            MainForm.canvCount--;

            if (this.SaveNeed == true)
            {
                var res = MessageBox.Show("Хотите сохранить файл перед выходом?", "Сохранение", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    Save();
                    SaveNeed = false;
                }
                else if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}
