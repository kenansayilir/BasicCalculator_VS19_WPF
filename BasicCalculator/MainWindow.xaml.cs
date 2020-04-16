using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        decimal currentvalue = 0;
        decimal calculatedvalue = 0;
        decimal memoryvalue = 0;
        Boolean isNewValue = true;
        NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
        CultureInfo provider = new CultureInfo("en-US");
        string process = "";

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;


            if(btn == btn0 || btn == btn1 || btn == btn2 || btn == btn3 || btn == btn4 || btn == btn5 || btn == btn6 || btn == btn7 || btn == btn8 || btn == btn9 || btn == btnpoint)
            {
                if(txtDigits.Text == "0" || isNewValue)
                {
                    if (btn == btnpoint)
                    {
                        txtDigits.Text = "0";
                    }
                    else
                    {
                        txtDigits.Text = "";
                    }
                    isNewValue = false;
                }
                txtDigits.Text = txtDigits.Text + btn.Tag.ToString();
            }
            else
            {
                currentvalue = decimal.Parse(txtDigits.Text, style, provider);

                switch (btn.Tag.ToString())
                {
                    case "clear":
                        currentvalue = 0;
                        calculatedvalue = 0;
                        memoryvalue = 0;
                        setDigits();
                        break;
                    case "back":
                        if(txtDigits.Text.Length <= 1)
                        {
                            txtDigits.Text = "0";
                        }
                        else
                        {
                            txtDigits.Text = txtDigits.Text.Substring(0, txtDigits.Text.Length - 1);
                        }
                        break;
                    case "mc":
                        memoryvalue = 0;
                        break;
                    case "mr":
                        currentvalue = memoryvalue;
                        setDigits();
                        break;
                    case "m+":
                        memoryvalue = (memoryvalue + decimal.Parse(txtDigits.Text, style, provider));
                        break;
                    case "m-":
                        memoryvalue = (memoryvalue - decimal.Parse(txtDigits.Text, style, provider));
                        break;
                    case "-1":
                        if (txtDigits.Text != "0")
                        {
                            if (txtDigits.Text.Contains("-"))
                            {
                                txtDigits.Text = txtDigits.Text.Replace("-", "");
                            }
                            else
                            {
                                txtDigits.Text = "-" + txtDigits.Text;
                            }
                        }
                        break;
                    case "=":
                        calculate();
                        break;
                    case "square":
                        setprocess("square");
                        calculate();
                        break;
                    case "squareroot":
                        setprocess("squareroot");
                        calculate();
                        break;
                    case "oneover":
                        setprocess("oneover");
                        calculate();
                        break;
                    case "%":
                        setprocess("%");
                        break;
                    case "+":
                        setprocess("+");
                        break;
                    case "-":
                        setprocess("-");
                        break;
                    case "*":
                        setprocess("*");
                        break;
                    case "/":
                        setprocess("/");
                        break;
                    default:
                        break;
                }
            }
        }

        private void setprocess(string prc)
        {
            if(process.Length > 0)
            {
                calculate();
            }
            process = prc;
            isNewValue = true;
            calculatedvalue = decimal.Parse(txtDigits.Text, style, provider);
        }

        private void calculate()
        {

            decimal value1 = calculatedvalue;
            decimal value2 = decimal.Parse(txtDigits.Text, style, provider);
            switch (process)
            {
                case "square":
                    currentvalue = value1 * value1;
                    break;
                case "squareroot":
                    if(value1 > 0)
                    {
                        currentvalue = (decimal)Math.Sqrt((double)value1);
                    }
                    break;
                case "oneover":
                    currentvalue = 1 / value1;
                    break;
                case "+":
                    currentvalue = value1 + value2;
                    break;
                case "-":
                    currentvalue = value1 - value2;
                    break;
                case "*":
                    currentvalue = value1 * value2;
                    break;
                case "/":
                    currentvalue = value1 / value2;
                    break;
                case "%":
                    currentvalue = value1 * value2 / 100;
                    break;
                default:
                    break;
            }
            process = "";
            isNewValue  = true;
            calculatedvalue = 0;
            setDigits();
        }
        private void setDigits()
        {
            if (((currentvalue - (int)currentvalue) != 0))
            {
                txtDigits.Text = currentvalue.ToString().Replace(",", ".");
            }
            else
            {
                txtDigits.Text = ((int)currentvalue).ToString().Replace(",", ".");
            }
            currentvalue = 0;
        }
    }
}
