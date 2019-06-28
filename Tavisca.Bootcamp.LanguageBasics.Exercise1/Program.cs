using System;
using System.Text.RegularExpressions;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            Test("42*47=1?74", 9);
            Test("4?*47=1974", 2);
            Test("42*?7=1974", 4);
            Test("42*?47=1974", -1);
            Test("2*12?=247", -1);
            Console.ReadKey(true);
        }

        private static void Test(string args, int expected)
        {
            var result = FindDigit(args).Equals(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"{args} : {result}");
        }

        public class Operand
        {
            String[] num = new String[3]; // stores the operand strings
            int[] number = new int[3]; // stores operand integers
            int questionMarkIndex= -1; // to store which operand has the missing digit.
            int validNumber; // This will be our number that makes the equation valid.
            String missingOperand;
            String validOperand;
            int result = 0;
            int notDivisible =0;

            public void ExtractNum(string equation){
                
                Regex pattern = new Regex(@"(?<num0>.*)\*(?<num1>.*)=(?<num2>.*)");
                Match match = pattern.Match(equation);
                this.num[0] = match.Groups["num0"].Value;
                this.num[1] = match.Groups["num1"].Value;
                this.num[2] = match.Groups["num2"].Value;
                
                this.notDivisible =0; // initialization for each testcase.                
            }

            public void FindQuestionMark()
            {
                if(this.num[0].Contains('?'))
                {
                    this.questionMarkIndex = 0;
                }else if(this.num[1].Contains('?'))
                {
                    this.questionMarkIndex = 1;
                }else if(this.num[2].Contains('?'))
                {
                    this.questionMarkIndex = 2;
                }
                
            }

            public void InitializeNumbers()
            {
                this.missingOperand = this.num[this.questionMarkIndex];
                if(this.questionMarkIndex==0)
                {
                    if(Int32.TryParse(this.num[1],out this.number[1]))
                        this.result = -1;
                    if(Int32.TryParse(this.num[2],out this.number[2]))
                        this.result = -1;
                    if(this.number[2] % this.number[1] != 0)
                    {
                        this.notDivisible = 1;
                    }
                    this.number[0] = this.number[2] / this.number[1];
                }else if(this.questionMarkIndex==1)
                {
                    if(Int32.TryParse(this.num[0],out this.number[0]))
                        this.result = -1;
                    if(Int32.TryParse(this.num[2],out this.number[2]))
                        this.result = -1;
                    if(this.number[2] % this.number[0] != 0)
                    {
                        this.notDivisible = 1;
                    }
                    this.number[1] = this.number[2] / this.number[0];
                }else if(this.questionMarkIndex==2)
                {
                    if(Int32.TryParse(this.num[0],out this.number[0]))
                        this.result = -1;
                    if(Int32.TryParse(this.num[1],out this.number[1]))
                        this.result = -1;
                    this.number[2] = this.number[0] * this.number[1]; 
                }

                this.validNumber = this.number[this.questionMarkIndex];     // Valid number in integer
                this.validOperand = this.number[this.questionMarkIndex].ToString();  // Valid operand string

            }


            public int FindMissingDigit()
            {
                char[] newone = this.validOperand.ToCharArray();

                // let's replace ? with the actual result.
                int index = this.missingOperand.IndexOf('?');
                
                    char missingDigit = newone[index];
                    missingOperand = this.missingOperand.Replace('?',missingDigit);
                    // Compare the strings for equality, id true then the missingdigit is the result to be returned
                    if(String.Equals(this.missingOperand,this.validOperand)==true && this.notDivisible==0)
                    {
                        this.result= (int)Char.GetNumericValue(missingDigit);
                    }else
                    {
                        this.result = -1;
                    }
                return this.result;
            }

        }

        /*
            I have solved the problem in the following manner :
            1) extract the operands
            2) find which one has the missing digit
            3) find the actual number that would make the equation work as expected
            4) replace the missing digit by comparing the string to actual result
            5) return the missing digit if the result matches the operand after replacing the missing digit
        */

        public static int FindDigit(string equation)
        {
            var operand = new Operand();

            // extracting the numbers out of the equation. and finding the operand with missing digit
            operand.ExtractNum(equation);
            operand.FindQuestionMark();
            operand.InitializeNumbers();
            int result = operand.FindMissingDigit();
            return result;

        }
    }
}