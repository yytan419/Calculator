using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string equation = "";

            do
            {
                Console.WriteLine("Please type your equation and then press Enter (enter 'exit' to quit)");
                equation = Console.ReadLine();

                if (equation != "exit")
                {
                    double res = Calculate(equation);
                    Console.WriteLine("result : " + res + "\n");
                }

            } while (equation != "exit");
           
        }

        public static double Calculate(string num)
        {
            double result = 0;
            string str = num.Replace(" ", "");

            //declare operator + - * /
            char[] oprArray = { '+', '-', '*', '/' };

            //check frequency of occurances of operator
            int plusOprCount = str.Split("+").Length - 1;
            int minusOprCount = str.Split("-").Length - 1;
            int timesOprCount = str.Split("*").Length - 1;
            int divideOprCount = str.Split("/").Length - 1;

            int numOperator = str[0] == '-' ? (plusOprCount + minusOprCount + timesOprCount + divideOprCount - 1) : (plusOprCount + minusOprCount + timesOprCount + divideOprCount);

            //get bracket occurances
            int bracketCount = str.Split("(").Length - 1;

            //if there is bracket in the string
            if (bracketCount >= 1)
            {
                //last open bracket position
                int lastOpenBracketIndex = str.LastIndexOf("(");

                //substring to get string after last open bracket 
                string stringAfterOpenBracket = str.Substring(lastOpenBracketIndex + 1);

                //get first close bracket position from stringAfterOpenBracket
                int closeBracketIndex = stringAfterOpenBracket.IndexOf(")");

                //get string inside bracket
                string stringInsideBracket = str.Substring(lastOpenBracketIndex + 1, closeBracketIndex);

                result = Calculate(stringInsideBracket);

                //replace value get
                str = str.Replace("(" + stringInsideBracket + ")", result.ToString());
            }
            else
            {
                //if more than one operator found & either * or / found
                if (numOperator > 1 && (timesOprCount + divideOprCount) > 0)
                {
                    char[] oprArr = { '*', '/' };

                    //get position of first * or / operator 
                    int oprIndex = str.IndexOfAny(oprArr);

                    //get string before * or / operator
                    string frontString = str.Substring(0, oprIndex);

                    //get string after * or / operator
                    string lastString = str.Substring(oprIndex + 1);

                    //get position of last operator value of frontString
                    int oprLastIndex = frontString.LastIndexOfAny(oprArray);

                    //get string after oprLastIndex from frontString
                    string num1 = frontString.Substring(oprLastIndex + 1);

                    //get operator value
                    string oprValue = str[oprIndex].ToString();

                    //get first operator position from lastString
                    int lastStringOprIndex = lastString.IndexOfAny(oprArray);
                    string num2 = lastStringOprIndex == -1 ? lastString : lastString.Substring(0, lastStringOprIndex);

                    result = Calculate(num1 + oprValue + num2);

                    str = str.Replace(num1 + oprValue + num2, result.ToString());
                }
                else
                {
                    string tmp = "";
                    int oprIndex = 0;
                    string firstValue = "";
                    string opr = "";
                    string secondValue = "";

                    //handle when first value is negative
                    if (str[0] == '-')
                    {
                        tmp = "" + str.Substring(1);
                        oprIndex = tmp.IndexOfAny(oprArray);
                        firstValue = "-" + tmp.Substring(0, oprIndex);
                        opr = Convert.ToString(tmp[oprIndex]);
                        secondValue = tmp.Substring(oprIndex + 1);
                    }
                    else
                    {
                        oprIndex = str.IndexOfAny(oprArray);
                        firstValue = str.Substring(0, oprIndex);
                        opr = Convert.ToString(str[oprIndex]);
                        secondValue = str.Substring(oprIndex + 1);
                    }

                    //no more operator
                    if (secondValue.IndexOfAny(oprArray) == -1)
                    {
                        switch (opr)
                        {
                            case "+":
                                result = Convert.ToDouble(firstValue) + Convert.ToDouble(secondValue);
                                break;
                            case "-":
                                result = Convert.ToDouble(firstValue) - Convert.ToDouble(secondValue);
                                break;
                            case "*":
                                result = Convert.ToDouble(firstValue) * Convert.ToDouble(secondValue);
                                break;
                            case "/":
                                result = Convert.ToDouble(firstValue) / Convert.ToDouble(secondValue);
                                break;
                        }
                        str = str.Replace(firstValue + opr + secondValue, result.ToString());
                    }
                    else
                    {
                        int oprIndex2 = secondValue.IndexOfAny(oprArray);
                        string num2 = secondValue.Substring(0, oprIndex2);

                        switch (opr)
                        {
                            case "+":
                                result = Convert.ToDouble(firstValue) + Convert.ToDouble(num2);
                                break;
                            case "-":
                                result = Convert.ToDouble(firstValue) - Convert.ToDouble(num2);
                                break;
                            case "*":
                                result = Convert.ToDouble(firstValue) * Convert.ToDouble(num2);
                                break;
                            case "/":
                                result = Convert.ToDouble(firstValue) / Convert.ToDouble(num2);
                                break;
                        }

                        str = str.Replace(firstValue + opr + num2, result.ToString());
                    }
                }
            }

            plusOprCount = str.Split("+").Length - 1;
            minusOprCount = str.Split("-").Length - 1;
            timesOprCount = str.Split("*").Length - 1;
            divideOprCount = str.Split("/").Length - 1;

            numOperator = str[0] == '-' ? (plusOprCount + minusOprCount + timesOprCount + divideOprCount - 1) : (plusOprCount + minusOprCount + timesOprCount + divideOprCount);

            //if there is one or more than 1 oprerator found
            if (numOperator >= 1)
            {
                return Calculate(str);
            }
            else
            {
                return result;
            }
        }
    }
}
