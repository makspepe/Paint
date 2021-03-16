using PluginInterface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Paint
{
    public partial class MainForm : Form
    {
        public static Color CurColor = Color.Red; //цвет пера
        public static int CurWidth = 3; //толщина
        public static int canvCount = 1; //нельзя сохранять если нет открытых холстов
        public static string Type = ""; //тип пера - будет выбран 0 элемент из списка (кисть)
        public MouseEventHandler mouseHandler = null;
        private Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) //main load
        {
            toolStripComboBox1.SelectedIndex = 0;
            toolStripTextBox1.Text = CurWidth.ToString();
            FindPlugins(); //читает плагины median и rev из /dll/
            CreatePluginsMenu(); //показ загруженных плагинов

            Canvas frmChild = new Canvas(); //создаем новый холст при загрузке
            frmChild.MdiParent = this;
            frmChild.Show();
            сохранитьToolStripMenuItem.Enabled = true;
            сохранитьКакToolStripMenuItem.Enabled = true; 
        }


        #region Файл
        private void файлToolStripMenuItem_MouseHover(object sender, EventArgs e) //Просто не даем сохранять когда нет открытых холстов
        {
            if (canvCount == 0)
            {
                сохранитьToolStripMenuItem.Enabled = false;
                сохранитьКакToolStripMenuItem.Enabled = false;
            }
            else
            {
                сохранитьToolStripMenuItem.Enabled = true;
                сохранитьКакToolStripMenuItem.Enabled = true;
            }
        }
        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canvCount++;
            сохранитьToolStripMenuItem.Enabled = true;
            сохранитьКакToolStripMenuItem.Enabled = true;
            Canvas frmChild = new Canvas();
            frmChild.MdiParent = this;
            frmChild.Show();
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e) //открыть файл
        {
            OpenFileDialog dlg = new OpenFileDialog();
            //dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; //начинаем просмотр с .exe
            dlg.Filter = "Windows Bitmap(*.bmp)|*.bmp|Файлы JPEG(*.jpg)|*.jpg |Файлы PNG(*.png)|*.png|Все файлы ()*.*|*.*";
            var result = dlg.ShowDialog();
            if (result == DialogResult.OK) //замена холста
            {
                Canvas frmChild = new Canvas(dlg.FileName);
                frmChild.MdiParent = this;
                frmChild.Show();
                canvCount++;
            }
            else if (result == DialogResult.No)
            {
                ((Canvas)ActiveMdiChild).SaveAs();
            }
        }
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((Canvas)ActiveMdiChild).SaveAs();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((Canvas)ActiveMdiChild).Save();
        }
      
        private void выходToolStripMenuItem_Click(object sender, EventArgs e) //выход и сохранение
        {
            if (canvCount > 0)
            {
                var result = MessageBox.Show("Вы точно хотите выйти?\nВесь несохраненный прогресс будет отменен", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите выйти?\nВесь несохраненный прогресс будет отменен", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

            }
            else
            {
                e.Cancel = true;
                this.Activate();
            }
        }
        #endregion

        #region Рисунок
        private void рисунокToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            размерХолстаToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }

        private void размерХолстаToolStripMenuItem_Click(object sender, EventArgs e) //изменение холста
        {
            CanvasSize cs = new CanvasSize();
            cs.CanvasWidth = ((Canvas)ActiveMdiChild).CanvasWidth;
            cs.CanvasHeight = ((Canvas)ActiveMdiChild).CanvasHeight;
            if (cs.ShowDialog() == DialogResult.OK)
            {
                
                ((Canvas)ActiveMdiChild).CanvasWidth = cs.CanvasWidth;
                ((Canvas)ActiveMdiChild).CanvasHeight = cs.CanvasHeight;
                
            }
        }
        #endregion

        #region Окно

        private void каскадомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void сверхуВнизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void упорядочитьЗначкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void слеваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }


        #endregion

        #region Цвета
        private void красныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurColor = Color.Red;
        }

        private void синийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurColor = Color.Blue;
        }

        private void зеленыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurColor = Color.Green;
        }

        private void другойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                CurColor = cd.Color;*/
            DifferentColour dc = new DifferentColour(CurColor);
            if (dc.ShowDialog() == DialogResult.OK)
            {
                CurColor = dc.ColorForm;
            }
        }
        #endregion

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e) //кисти
        {
            Type = toolStripComboBox1.SelectedItem.ToString();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e) //изменение толщины
        {
            try
            {
                CurWidth = int.Parse(toolStripTextBox1.Text);
            }
            catch
            {
                //MessageBox.Show("Значение должн быть целым числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                toolStripTextBox1.Text = "1";
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e) //about
        {
            AboutBox frmAbout = new AboutBox();
            frmAbout.ShowDialog();
        }
        
        #region Plugins
        private void FindPlugins() // Ищем плагины в /dll/ и загружаем их
        {
            List<string> files = new List<string>();
            if (ConfigurationManager.AppSettings.Count == 0)
            {
                // папка с плагинами
                string folder = Application.StartupPath.Replace("\\bin\\Debug", "\\dll\\"); // paint/dll dll-файлы в этой папке
                //MessageBox.Show(folder);
                
                files =new List<string>(Directory.GetFiles(folder, "*.dll"));
            }
            else
            {
                foreach(string path in ConfigurationManager.AppSettings.Keys)
                {
                    files.Add(ConfigurationManager.AppSettings.GetValues(path)[0]);
                }
            }
            string dialog = ("Название\t\tАвтор\t\tВерсия\n\n");
            foreach (string file in files)
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);
                    foreach (Type type in assembly.GetTypes())
                    {
                        var attr = type.GetCustomAttribute<VersionAttribute>();
                        Type iface = type.GetInterface("PluginInterface.IPlugin");
                        if (iface != null)
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin.Name, plugin);
                            dialog += ($"{plugin.Name}\t{plugin.Author}\t\t{attr.Major}.{attr.Minor}\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки плагина\n" + ex.Message);
                }
            MessageBox.Show(dialog.PadLeft(50));
        }
        private void CreatePluginsMenu() //тулстрип с загруженными плагинами
        {
            foreach (var plugin in plugins)
            {
                ToolStripMenuItem toolStrip = new ToolStripMenuItem(plugin.Key);
                toolStrip.Click += OnPluginClick;
                расширенияToolStripMenuItem.DropDownItems.Add(toolStrip);
            }
        }

        private void OnPluginClick(object sender, EventArgs args) // активация плагина 
        {
            if (canvCount == 0) // проверка на существование холста
            {
                MessageBox.Show("Создайте холст", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var form = ((Canvas)ActiveMdiChild);
                IPlugin plugin = plugins[((ToolStripMenuItem)sender).Text];
                //MessageBox.Show(form.bmp.Size.ToString());
                form.bmp = plugin.Transform(form.pictureBox1.Image as Bitmap);
                form.pictureBox1.Image = form.bmp;
                form.pictureBox1.Invalidate();
                //MessageBox.Show(form.bmp.Size.ToString());
            }
        }




        #endregion

        
    }
}
