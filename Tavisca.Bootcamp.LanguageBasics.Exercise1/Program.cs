using System;

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

        public static int FindDigit(string equation)
        {
            /*
            I have solved the problem in the following manner :
            1) extract the operands
            2) find which one has the missing digit
            3) find the actual number that would make the equation work as expected
            4) replace the missing digit by comparing the string to actual result
            5) return the missing digit if the result matches the operand after replacing the missing digit
             */


            // extracting the numbers out of the equation.
            string[] num = new String[3];
            int[] number = new int[3];
            int questionMarkIndex= -1; // to store which operand has the missing digit.
            int index=0;
            int result=-1; // to store the character that is to be replaced by ?
            string temp; // to store the actual retrieved number that has the 'question mark'

            for(int i=0; i<equation.Length; i++){
                if(equation[i]=='*'||equation[i]=='='){
                    index++;
                }else{
                    num[index] += equation[i];
                }
            }
            
            // now let's find question mark index, to check which number has tha missing digit!
            // and store the information in the variable questionMarkIndex
            if(num[0].Contains('?')){
                questionMarkIndex = 0;
            }else if(num[1].Contains('?')){
                questionMarkIndex = 1;
            }else if(num[2].Contains('?')){
                questionMarkIndex = 2;
            }

            if(questionMarkIndex==0){
                //Console.WriteLine("missing number is First");
                // Now we retieve the number missing!
                number[1]= Int32.Parse(num[1]);
                number[2]= Int32.Parse(num[2]);

                number[0] = number[2]%number[1];
                if(number[0]!=0){
                    return -1;
                }else
                {
                    number[0] = number[2]/number[1];
                }
                temp = number[0].ToString();

                char[] temporary = temp.ToCharArray();
                char[] orignal = num[0].ToCharArray();
            
                // let's replace ? with the actual result.
                for(int i=0; i<orignal.Length; i++){
                    if(orignal[i]=='?'){
                        orignal[i]=temporary[i];
                        result= (int)Char.GetNumericValue(temporary[i]); // storing the missing digit in result
                    }
                }
                

                //now let's compare the numbers orignal and temporary
                num[0] = new String(orignal);
                temp = new String(temporary);
                if(String.Equals(num[0],temp)){
                    return result; // returning the missing digit
                }else {
                    return -1;
                }
                // Process finished for FIRST operand missing ?

            }else if(questionMarkIndex==1){
                //Console.WriteLine("missing number is Second");
                // Now we retieve the number missing!
                number[0]= Int32.Parse(num[0]);
                number[2]= Int32.Parse(num[2]);

                number[1] = number[2]%number[0];
                if(number[1]!=0){
                    return -1;
                }else
                {
                    number[1] = number[2]/number[0];
                }
                temp = number[1].ToString();

                char[] temporary = temp.ToCharArray();
                char[] orignal = num[1].ToCharArray();
            
                // let's replace ? with the actual result.
                for(int i=0; i<orignal.Length; i++){
                    if(orignal[i]=='?'){
                        orignal[i]=temporary[i];
                        result= (int)Char.GetNumericValue(temporary[i]); // storing the missing digit in result
                    }
                }
                

                //now let's compare the numbers orignal and temporary
                num[1] = new String(orignal);
                temp = new String(temporary);
                if(String.Equals(num[1],temp)){
                    return result; // returning the missing digit
                }else {
                    return -1;
                }
                // Process finished for SECOND operand missing ?

            }else if(questionMarkIndex==2){
                //Console.WriteLine("missing number is Third");
                // Now we retieve the number missing!
                number[1]= Int32.Parse(num[1]);
                number[0]= Int32.Parse(num[0]);

                number[2] = number[0]*number[1]; // multiplication to get actual result
                
                temp = number[2].ToString();

                char[] temporary = temp.ToCharArray();
                char[] orignal = num[2].ToCharArray();
            
                // let's replace ? with the actual result.
                for(int i=0; i<orignal.Length; i++){
                    if(orignal[i]=='?'){
                        orignal[i]=temporary[i];
                        result= (int)Char.GetNumericValue(temporary[i]); // storing the missing digit in result
                    }
                }
                

                //now let's compare the numbers orignal and temporary
                num[2] = new String(orignal);
                temp = new String(temporary);
                if(String.Equals(num[2],temp)){
                    return result; // returning the missing digit
                }else {
                    return -1;
                }
                // Process finished for first operand missing ?

            }

            return 1;
            throw new NotImplementedException();
        }
    }
}