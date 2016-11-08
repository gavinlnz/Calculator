using System;

using Xamarin.Forms;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// The current state of the equation construction.
        /// Initial state is 1.
        /// This changes to -1 when the user enters the first digit.
        /// It changes to 1 when the user enters more digits for the first operand.
        /// It changes to -2 when the user selects an operator (+-/*)
        /// It changes to 2 when the user selects more digits for the second operand.
        /// When = (Calculate) is clicked the state changes to -1, so the answer can be used as operand 1
        /// for a new calculation.
        /// </summary>
        private int _currentState = 1;
        private double _firstNumber;
        private double _secondNumber;
        private string _mathOperator;

        public MainPage()
        {
            InitializeComponent();
            OnClear(this, null);
        }

        private void OnSelectNumber(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var pressed = button.Text;
            if (resultText.Text == "0" || _currentState < 0)
            {
                resultText.Text = "";
                if (_currentState < 0)
                    _currentState *= -1;
            }
            resultText.Text += pressed;
            double number;
            if (double.TryParse(resultText.Text, out number))
            {
                resultText.Text = number.ToString("N0");
                if (_currentState == 1)
                {
                    _firstNumber = number;
                }
                else
                {
                    _secondNumber = number;
                }
            }
        }

        private void OnSelectOperator(object sender, EventArgs e)
        {
            _currentState = -2;
            var button = (Button)sender;
            var pressed = button.Text;
            _mathOperator = pressed;
        }

        private void OnClear(object sender, EventArgs e)
        {
            _currentState = 1;
            _firstNumber = 0.0;
            _secondNumber = 0.0;
            _mathOperator = string.Empty;
            resultText.Text = "0";

        }

        private void OnCalculate(object sender, EventArgs e)
        {
            if (_currentState == 2)
            {
                double result = 0;

                switch (_mathOperator)
                {
                    case "+":
                        result = _firstNumber + _secondNumber;
                        break;
                    case "-":
                        result = _firstNumber - _secondNumber;
                        break;
                    case "÷":
                        result = _firstNumber / _secondNumber;
                        break;
                    case "×":
                        result = _firstNumber * _secondNumber;
                        break;
                }

                _firstNumber = result;
                resultText.Text = result.ToString("N");
                _currentState = -1;
            }
        }
    }
}
