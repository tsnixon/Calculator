using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        double da = 0d;
        double db = 0d;
        double dResult = 0d;
        string operand = "";
        bool firstValueSet = false;

        public Form1()
        {
            InitializeComponent();
        }


        bool ValidateValue()
        {
            long l = 0l;

            if (tbValues.Text.Length > 0)
                if (!long.TryParse(tbValues.Text.Replace("-", "").Replace(".", ""), out l))
                    return false;

            return true;
        }


        private void btnEquals_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateValue())
                {
                    double.TryParse(tbValues.Text, out db);

                    switch (operand)
                    {
                        case "+":
                            dResult = da + db;
                            break;

                        case "-":
                            dResult = da - db;
                            break;

                        case "X":
                            dResult = da * db;
                            break;

                        case "/":
                            if (db == 0)
                                throw new Exception("Error");

                            dResult = da / db;
                            break;

                        default:
                            break;
                    }

                    string s = dResult.ToString();
                    int pos = s.IndexOf(".");
                    string s2 = s.Substring(pos + 1);
                    int i;

                    int.TryParse(s2, out i);

                    if (i == 0)
                    {
                        i = (int)dResult;
                        tbValues.Text = i.ToString();
                    }
                    else
                        tbValues.Text = dResult.ToString();

                    firstValueSet = false;
                }
            }
            catch (Exception ex)
            {
                tbValues.Text = "Error";
            }
        }


        private void OperandClick(object sender, EventArgs e)
        {
            if (ValidateValue())
            {
                operand = ((Button)sender).Text;

                double.TryParse(tbValues.Text, out da);
            }
            else
                operand = "";
        }


        private void NumberClick(object sender, EventArgs e)
        {
            if (operand.Length > 0 && !firstValueSet)
            {
                tbValues.Text = "";
                firstValueSet = true;
            }

            if (tbValues.Text == "Error")
                Reset();

            if (tbValues.Text.Replace("-", "").Replace(".", "").Length < 9)
            {
                if (tbValues.Text == "0")
                    tbValues.Text = "";

                tbValues.Text += ((Button)sender).Text;
            }
        }


        private void btnAC_Click(object sender, EventArgs e)
        {
            Reset();
        }


        private void Reset()
        {
            tbValues.Text = "0";

            da = 0d;
            db = 0d;
            dResult = 0d;
            operand = "";

            firstValueSet = false;
        }


        private void btnDecimal_Click(object sender, EventArgs e)
        {
            if (operand.Length > 0 && !firstValueSet)
            {
                tbValues.Text = "";
                firstValueSet = true;
            }

            if (!tbValues.Text.Contains("."))
                tbValues.Text += ".";
        }


        private void btnNegate_Click(object sender, EventArgs e)
        {
            if (tbValues.Text.Contains("-"))
                tbValues.Text = tbValues.Text.Replace("-", "");
            else
                tbValues.Text = "-" + tbValues.Text;
        }


        private void btnPercent_Click(object sender, EventArgs e)
        {
            int i = 0;
            double d = 0.0d;

            if (tbValues.Text.Contains("."))
            {
                double.TryParse(tbValues.Text, out d);
                d /= 100d;
            }
            else
            {
                int.TryParse(tbValues.Text, out i);
                d = i / 100d;
            }

            tbValues.Text = d.ToString();
        }
    }
}