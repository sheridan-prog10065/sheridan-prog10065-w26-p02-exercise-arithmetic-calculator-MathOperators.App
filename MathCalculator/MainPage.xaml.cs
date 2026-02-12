using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MathCalculator;

public partial class MainPage : ContentPage
{
    /// <summary>
    /// Define a field variable to remember the history of expressions calculated
    /// </summary>
    private ObservableCollection<string> _expList;

    public MainPage()
    {
        InitializeComponent();

        //Initialize field variables of the page
        _expList = new ObservableCollection<string>();

        //Bind the collection view for the expression histor with the expression list
        _lstExpHistory.ItemsSource = _expList;

	}

	private void OnCalculate(object sender, EventArgs e)
	{
        //Obtain the input from the user: left op, right op and operator
        string leftOperandInput = _txtLeftOp.Text;
        double leftOperand = double.Parse(leftOperandInput);

        double rightOperand = double.Parse(_txtRightOp.Text);

        string opInput = (string)_pckOperand.SelectedItem;
        char op = opInput[0];

		//Check the chosen operator and perform the corresponding operation
		double result = PerformArithmeticOperation(op, leftOperand, rightOperand);

        //Display the arithmetic expression in the output control
        string expression = $"{leftOperand} {op} {rightOperand} = {result}";
        _txtMathExp.Text = expression;

        //Remember the calculated expression in the history of the calculator
        _expList.Add(expression);
	}

	private double PerformArithmeticOperation(char op, double leftOperand, double rightOperand)
	{
        //Check the type of operand (op) and perform the corresponding operation
        double result;
        switch (op)
        {
            case '+':
                result = PerformAddition(leftOperand, rightOperand);
                break;

            case '-':
                result = PerformSubtraction(leftOperand, rightOperand);
                break;

            case '*':
                result = PerformMultiplication(leftOperand, rightOperand);
                break;

            case '/':
                result = PerformDivision(leftOperand, rightOperand);
                break;

            case '%':
                result = PerformDivRemainder(leftOperand, rightOperand);
                break;

            default:
                Debug.Assert(false, "Unknown arithmetic operation. Default result returned");
                result = 0;
                break;
        }

        return result;
	}

	private double PerformAddition(double leftOperand, double rightOperand)
	{
        double result;
        result = leftOperand + rightOperand;
        return result;
	}

	private double PerformSubtraction(double leftOperand, double rightOperand)
	{
		double result = leftOperand - rightOperand;
        return result;
	}

	private double PerformMultiplication(double leftOperand, double rightOperand)
	{
        return leftOperand * rightOperand;
	}

	private double PerformDivision(double leftOperand, double rightOperand)
	{
        //Check whether the division operation is integer or real division
        string divOp = (string) _pckOperand.SelectedItem;
        if (divOp.Contains("int", StringComparison.OrdinalIgnoreCase))
        {
            //Perform an integer division which is done when both operands are integers
            int leftIntOp = (int)leftOperand;
            int rightIntOp = (int)rightOperand;
            
            int result = leftIntOp / rightIntOp;
            return result;
        }
        else
        {
            double result = leftOperand / rightOperand;
            return result;
        }
	}

	private double PerformDivRemainder(double leftOperand, double rightOperand)
	{
        return leftOperand % rightOperand;
	}

}