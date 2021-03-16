using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColourSelect
{
    public partial class ColourText : TextBox 
    {
        private bool dec = true; //сейчас в 10чной?
        public bool ChangeBase = false; //нужна смена счисления?
        public bool Dec
        {
            set
            {
                dec = value;
            }
        }
        public ColourText()
        {
            InitializeComponent();
        }

        public ColourText(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void OnKeyPress(KeyPressEventArgs e) //ограничения на ввод
        {
            base.OnKeyPress(e); 
            char c = e.KeyChar;
            if (e.KeyChar == 22) //запрет на вставку чего-либо в текстбокс
            {
                e.Handled = true;
            }
            if (dec) //запрет на ввод чего-либо кроме цифр для 10
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
            }
            else //цифры и a-f для hex
            {
                if (!(char.IsDigit(c) || char.IsControl(c) || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f')))
                {
                    e.Handled = true;
                }
            }
        }


    public int Value => Convert.ToInt32(Text.ToLower(), dec ? 10 : 16); //перевод между сис счисл

        public void ChangeDecHex()
        {
            ChangeBase = true;
            if (Text != "" && !String.IsNullOrEmpty(Text)) //доп проверка если каким то образом получилось запарсить из буфера
            {
                Text = Value.ToString(!dec ? "" : "X"); 
            }
            else
            {
                Text = "0";
            }
        }

        protected override void OnTextChanged(EventArgs e) //обновление текстбоксов
        {

            if (ChangeBase)
            {
                return;
            }
            string val = dec ? "255" : "FF"; 
            int num = 0;
            try //ограничение на длину 
            {
                if (Text.Length < 3)
                {
                    num = Convert.ToInt32(Text.ToLower(), dec ? 10 : 16);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
            catch (Exception)
            {
                string s = "";
                for (int i = 0; i < Text.Length; ++i)
                {
                    if (dec)
                    {
                        if (Text[i] >= '0' && Text[i] <= '9') //еще один слой защиты, если каким-то образом не 0-9
                        {
                            s += Text[i];
                        }
                        else
                        {
                            s += 255;
                        }
                    }
                    else
                    {
                        if ((Text[i] >= '0' && Text[i] <= '9') || (Text[i] >= 'a' && Text[i] <= 'f') || (Text[i] >= 'A' && Text[i] <= 'F'))
                        {
                            s += Text[i];
                        }
                    }
                }
                if (dec && s.Length > 3) //лимит
                {
                    Text = s.Substring(0, 3);
                }
                if (!dec && s.Length > 2)
                {
                    Text = s.Substring(0, 2);
                }
                num = Convert.ToInt32(Text.ToLower(), dec ? 10 : 16);
            }
            if (num > 255)
            {
                //MessageBox.Show($"Числа должны быть от 0 до {val}");
                Text = val;
            }
            base.OnTextChanged(e);
        }
    }
}
