using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class CanvasSize : Form
    {
        public int CanvasWidth = 0;
        public int CanvasHeight = 0;
        public CanvasSize()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            CanvasHeight = int.Parse(heightTextBox.Text);
            CanvasWidth = int.Parse(widthTextBox.Text);
            if (CanvasHeight < 30) CanvasHeight = 30; 
            if (CanvasWidth < 30) CanvasWidth = 30;
        }

        private void CanvasSize_Load(object sender, EventArgs e)
        {
            widthTextBox.Text = CanvasWidth.ToString();
            heightTextBox.Text = CanvasHeight.ToString();
        }
    }
}
