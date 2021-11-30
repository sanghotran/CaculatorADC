using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaculatorADC
{
    public partial class frm_ADC : Form
    {
        public frm_ADC()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            double result;
            result = Convert.ToDouble(txt_R.Text)/Math.Pow(2,Convert.ToDouble(txt_B.Text));
            txt_Q.Text = result.ToString();
        }

        private void frm_ADC_Load(object sender, EventArgs e)
        {
            cbo_Type.Items.Add("Natural");
            cbo_Type.Items.Add("Offset");
            cbo_Type.Items.Add("Two’s complement");

            cbo_Approx.Items.Add("Rounding");
            cbo_Approx.Items.Add("Truncation");

        }
        private void btn_Code_Click(object sender, EventArgs e)
        {                      
            txt_Code.Text = CaculatorCode();
        }
        private void btn_Value_Click(object sender, EventArgs e)
        {
            txt_Value.Text = CaculatorValue();
        }

        private string CaculatorCode()
        {
            string code = "";
            int mode = CheckTypeAndApprox();
            int[] b = new int[12];
            switch ( mode)
            {
                case 0:
                    MessageBox.Show("please select Type and Approx");
                    break;
                case 1:
                    for ( int i = 1; i <= Convert.ToInt32(txt_B.Text); i++ )
                    {
                        double value = 0;
                        b[i-1] = 1;
                        for (int j = 1; j <= Convert.ToInt32(txt_B.Text); j++)
                        {
                            value += b[j-1] * Math.Pow(2, -j);
                        }
                        value = value * Convert.ToDouble(txt_R.Text);
                        if (value > Convert.ToDouble(txt_Value.Text))
                            b[i-1] = 0;
                        code += b[i-1].ToString();
                    }
                    break;
                case 2:
                    for (int i = 1; i <= Convert.ToInt32(txt_B.Text); i++)
                    {
                        double value = 0;
                        b[i - 1] = 1;
                        for (int j = 1; j <= Convert.ToInt32(txt_B.Text); j++)
                        {
                            value += b[j - 1] * Math.Pow(2, -j);
                        }
                        value = value * Convert.ToDouble(txt_R.Text) - 0.5 * Convert.ToDouble(txt_R.Text);
                        if (value > Convert.ToDouble(txt_Value.Text))
                            b[i - 1] = 0;
                        code += b[i - 1].ToString();
                    }
                    break;
                case 3:
                    if (Convert.ToDouble(txt_Value.Text) < 0)
                    {
                        b[0] = 0;
                        code += 1;
                    }
                    else
                    {
                        b[0] = 1;
                        code += 0;
                    }
                    for (int i = 2; i <= Convert.ToInt32(txt_B.Text); i++)
                    {
                        double value = 0;
                        b[i - 1] = 1;
                        for (int j = 1; j <= Convert.ToInt32(txt_B.Text); j++)
                        {
                            value += b[j - 1] * Math.Pow(2, -j);
                        }
                        value = value * Convert.ToDouble(txt_R.Text) - 0.5 * Convert.ToDouble(txt_R.Text);
                        if (value > Convert.ToDouble(txt_Value.Text))
                            b[i - 1] = 0;
                        code += b[i - 1].ToString();
                    }
                    break;
                case 4:
                    for (int i = 1; i <= Convert.ToInt32(txt_B.Text); i++)
                    {
                        double value = 0;
                        b[i - 1] = 1;
                        for (int j = 1; j <= Convert.ToInt32(txt_B.Text); j++)
                        {
                            value += b[j - 1] * Math.Pow(2, -j);
                        }
                        value = value * Convert.ToDouble(txt_R.Text);
                        if (value > (Convert.ToDouble(txt_Value.Text) + Convert.ToDouble(txt_Q.Text) / 2))
                            b[i - 1] = 0;
                        code += b[i - 1].ToString();
                    }
                    break;
                case 5:
                    for (int i = 1; i <= Convert.ToInt32(txt_B.Text); i++)
                    {
                        double value = 0;
                        b[i - 1] = 1;
                        for (int j = 1; j <= Convert.ToInt32(txt_B.Text); j++)
                        {
                            value += b[j - 1] * Math.Pow(2, -j);
                        }
                        value = value * Convert.ToDouble(txt_R.Text) - 0.5 * Convert.ToDouble(txt_R.Text);
                        if (value > (Convert.ToDouble(txt_Value.Text) + Convert.ToDouble(txt_Q.Text) / 2))
                            b[i - 1] = 0;
                        code += b[i - 1].ToString();
                    }
                    break;
                case 6:
                    if (Convert.ToDouble(txt_Value.Text) < 0)
                    {
                        b[0] = 0;
                        code += 1;
                    }
                    else
                    {
                        b[0] = 1;
                        code += 0;
                    }
                    for (int i = 2; i <= Convert.ToInt32(txt_B.Text); i++)
                    {
                        double value = 0;
                        b[i - 1] = 1;
                        for (int j = 1; j <= Convert.ToInt32(txt_B.Text); j++)
                        {
                            value += b[j - 1] * Math.Pow(2, -j);
                        }
                        value = value * Convert.ToDouble(txt_R.Text) - 0.5 * Convert.ToDouble(txt_R.Text);
                        if (value > (Convert.ToDouble(txt_Value.Text) + Convert.ToDouble(txt_Q.Text) / 2))
                            b[i - 1] = 0;
                        code += b[i - 1].ToString();
                    }
                    break;
            }
            return code;
        }
        private string CaculatorValue()
        {
            double value = 0;
            int mode = CheckType();
            switch( mode)
            {
                case 0:
                    MessageBox.Show("pleas select Type");
                    break;
                case 1:
                    for( int i = 1; i <= txt_Code.TextLength; i++ )
                    {
                        value += Convert.ToDouble(txt_Code.Text[i - 1].ToString())* Math.Pow(2, -i);
                    }
                    value = value * Convert.ToDouble(txt_R.Text);
                    break;
                case 2:
                    for (int i = 1; i <= txt_Code.TextLength; i++)
                    {
                        value += Convert.ToDouble(txt_Code.Text[i - 1].ToString()) * Math.Pow(2, -i);
                    }
                    value = value * Convert.ToDouble(txt_R.Text) - 0.5 * Convert.ToDouble(txt_R.Text);
                    break;
                case 3:
                    for (int i = 1; i <= txt_Code.TextLength; i++)
                    {
                        if( i == 1)
                        {
                            if (Convert.ToInt16(txt_Code.Text[0].ToString()) == 0)
                                value += 1 * Math.Pow(2, -1);
                            else value += 0;
                        }
                        else
                        value += Convert.ToDouble(txt_Code.Text[i - 1].ToString()) * Math.Pow(2, -i);
                    }
                    value = value * Convert.ToDouble(txt_R.Text) - 0.5 * Convert.ToDouble(txt_R.Text);
                    break;
            }

            return value.ToString();
        }

        private int CheckTypeAndApprox()
        {
            int mode = 0;
            mode = CheckType();
            if(cbo_Approx.SelectedItem.ToString() == "Truncation")
            {
                switch( mode)
                {
                    case 1:
                        return 1;
                    case 2:
                        return 2;
                    case 3:
                        return 3;
                }
            }
            else if (cbo_Approx.SelectedItem.ToString() == "Rounding")
            {
                switch (mode)
                {
                    case 1:
                        return 4;
                    case 2:
                        return 5;
                    case 3:
                        return 6;
                }
            }
            return 0;
        }

        private int CheckType()
        {
            if (cbo_Type.SelectedItem.ToString() == "Natural")
            {
                return 1;
            }
            else if (cbo_Type.SelectedItem.ToString() == "Offset")
            {
                return 2;
            }
            else if (cbo_Type.SelectedItem.ToString() == "Two’s complement")
            {
                return 3;
            }
            return 0;
        }

        
    }
}
