using System;
using System.Collections.Generic;
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
using NCalc;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CalculatorGui : Window
    {
        string currentNumber = string.Empty;
        string currentOperation = string.Empty;
        string overallExpression = string.Empty;
        bool mustClear = false;

        public CalculatorGui()
        {
            InitializeComponent();
        }

        // Button methods for calculator
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mustClear)
            {
                Button button = (Button)sender;
                switch (button.Name)
                {
                    case "buttonZero":
                        currentNumber += "0";
                        break;
                    case "buttonOne":
                        currentNumber += "1";
                        break;
                    case "buttonTwo":
                        currentNumber += "2";
                        break;
                    case "buttonThree":
                        currentNumber += "3";
                        break;
                    case "buttonFour":
                        currentNumber += "4";
                        break;
                    case "buttonFive":
                        currentNumber += "5";
                        break;
                    case "buttonSix":
                        currentNumber += "6";
                        break;
                    case "buttonSeven":
                        currentNumber += "7";
                        break;
                    case "buttonEight":
                        currentNumber += "8";
                        break;
                    case "buttonNine":
                        currentNumber += "9";
                        break;
                    case "buttonDot":
                        if (!currentNumber.Contains(".") && currentNumber != string.Empty) 
                        {
                            currentNumber += ".";
                            resultOutput.Text = currentNumber;
                        }
                        break;
                }
                if (currentNumber != string.Empty)
                {
                    resultOutput.Text = currentNumber;
                }
                else
                {
                    resultOutput.Text = "0";
                }
            }
        }

        private void ButtonOper_Click(object sender, RoutedEventArgs e)
        {
            if (currentNumber != string.Empty && !mustClear)
            {
                Button button = (Button)sender;
                switch (button.Name)
                {
                    case "buttonAdd":
                        currentOperation = "+";
                        break;
                    case "buttonSub":
                        currentOperation = "-";
                        break;
                    case "buttonDivide":
                        currentOperation = "/";
                        break;
                    case "buttonTimes":
                        currentOperation = "*";
                        break;
                }
                UpdateExpression(currentNumber, currentOperation);
                currentOperation = string.Empty;
                currentNumber = string.Empty;
            }
        }

        private void ButtonEquals_Click(object sender, RoutedEventArgs e)
        {
            if (overallExpression != string.Empty && !mustClear)
            {
                UpdateExpression(currentNumber);
                overallExpression = CheckOverallExpression(overallExpression);
                try
                {
                    resultOutput.Text = CalculateResult(overallExpression).ToString();
                    mustClear = true;
                }
                catch (Exception)
                {
                    resultOutput.Text = "Error";
                }
               mustClear = true;
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            resultOutput.Text = "0";
        }

        // Calculator logic 
        private void ClearAll()
        {
            currentNumber = string.Empty;
            overallExpression = string.Empty;
            currentOperation = string.Empty;
            mustClear = false;
        }

        private void UpdateExpression(string currentNumber, string currentOperation = "")
        {
            overallExpression += (currentNumber + currentOperation);
        }

        private double CalculateResult(string overallExpression)
        {
            NCalc.Expression expr = new NCalc.Expression(overallExpression);
            Func<double> f = expr.ToLambda<double>();
            return f();
        }

        private string CheckOverallExpression(string overallExpression)
        {
            string[] operators = { "+", "-", "*", "/" };
            bool result = operators.Any(x => overallExpression.EndsWith(x));
            if (result)
            {
                overallExpression = overallExpression.Remove(overallExpression.Length - 1);
            }
            return overallExpression;
        }
    }
}
