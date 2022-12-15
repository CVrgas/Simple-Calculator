// See https://aka.ms/new-console-template for more information

caculator();
void caculator()
{
    while(true)
    {
        // get input string
        var input = outputText();

        // Empty to Stop
        if (string.IsNullOrEmpty(input))
        {
            break;            
        }

        // help console
        if (input == "help")
        {
            helpText();
        }

        //validate string
        else if(IsValid(input) == false)
        {
            Console.Clear();
            Console.WriteLine("No valid Opeation\n");
        }

        else
        {

            //caculate string
            input = doTheMath(input).ToString();
            Console.Clear() ;
            Console.WriteLine($"Result: {input}\n");

        }
        
    }

}

double doTheMath(string input)
{
    // List of operations requested
    List<char> operations = new List<char>();

    //list of available operators 
    List<char> available= new List<char>() { '*', '/', '+', '-'};

    // list of numbers
    List<double> numbers = new(Array.ConvertAll(input.Split(available.ToArray()), double.Parse));


    // Add to operator to operations list
    foreach (char c in input)
    {
        if (!Char.IsDigit(c))
        {
            operations.Add(c);
        }
    }


    // result
    double result = 0;

    // while there is operator in operations
    while (operations.Count > 0) 
    {
        // Do the math on Order * > / > + > -
        foreach (char c in available)
        {
            // while operations contains operator,  Example: 1+2*2*2, will do 2 Multiplication first.
            while (operations.Contains(c))
            {
                // Index needed to find number after and before operator and remove thos numbers after processed
                var i = operations.IndexOf(c);

                // actually do the math
                result = selecOperation(c, numbers[i], numbers[i + 1]);

                //remove number and insert result for next operation
                operations.RemoveAt(i);
                numbers.RemoveRange(i, 2);
                numbers.Insert(i, result);
            }
        }
    }
    return result;
}

bool IsValid(string input)
{
    List<char> available = new List<char>() {'+', '-', '*', '/'};
    char[] inputArray= input.ToCharArray();

    // if there is a letter is not valid
    if (inputArray.Any(char.IsLetter)) return false;

    // if the first char is simbol is not valid
    if (!int.TryParse(input.First().ToString(), out int result)) return false;

    
    foreach (char c in inputArray)
    {
        if (!Char.IsDigit(c))
        {
            var i = Array.IndexOf(inputArray, c);

            // if next to operator there is a operator is not valid
            if (!char.IsDigit(inputArray[i+1])) return false;

            // if "operation" is no supported is not valid
            if (!available.Contains(c)) return false;

        }        
    }

    return true;
}

string outputText()
{
    // greating text
    Console.WriteLine("Enter Operation");
    Console.WriteLine("enter 'help' for more information");

    // receive Input
    var input = Console.ReadLine();
    return input;
}
static void helpText()
{
    Console.Clear();
    Console.WriteLine("Console Calculator made for any lenght operation\n");
    Console.WriteLine("Available Operations are \n Addition --> + \n Multiplication --> * \n Subtraction --> - \n Division --> / \n");
    Console.WriteLine("Example: input -> 5+5*2\n");
    Console.WriteLine("Does not allow negative number (e.g: -10+5)\n");
    Console.WriteLine("pres any key to continue");
    Console.ReadLine();
    Console.Clear();
}


// operations
double selecOperation(char c, double first, double second)
{
    // select with operation to do
    switch (c)
    {
        case '+':
            return addition(first, second);


        case '-':
            return subtraction(first, second);


        case '*':
            return multiplication(first, second);


        case '/':
            return division(first, second);
    }
    return 0;
}
double addition(double x, double y)
{
    return x + y;
}
double subtraction(double x, double y)
{
    return x - y;
}
double multiplication(double x, double y)
{
    return x * y;
}
double division(double x, double y)
{
    return x / y;
}

