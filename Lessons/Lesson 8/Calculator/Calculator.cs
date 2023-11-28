namespace ConsoleCalculator
{
    public class Calculator
    {
        static void Main(string[] args)
        {

        }
        public int A { get; set; }
        public int B { get; set; }
        public Calculator(int a, int b)
        {
            A = a;
            B = b;
        }
        public int Sum() 
        {
            var sum = A + B;
            return sum; 
        }
        public int Div() 
        {
            var div = A / B;
            return div; 
        } 
        public int Sub() 
        { 
            var sub = A - B;
            return sub; 
        }
        public int Mul() 
        { 
            var mul = A * B;
            return mul; 
        } 
    }
}