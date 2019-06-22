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
            //Console.ReadKey(true);
        }

        private static void Test(string args, int expected)
        {
            var result = FindDigit(args).Equals(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"{args} : {result}");
        }

        public static int FindDigit(string equation)
        {
            // extracting the numbers out of the equation.
            string[] num = new String[3];
            int[] number = new int[3];
            int questionMarkIndex= -1;
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
            /* checking the extration:
            Console.WriteLine("one:"+num[0]+" two:"+num[1]+" Three:"+num[2]);
            */
            
            // now let's find question mark index, to check which number has tha missing digit!
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
                // end of this if statement

            }else if(questionMarkIndex==1){
                //Console.WriteLine("missing number is Second");
            }else if(questionMarkIndex==2){
                //Console.WriteLine("missing number is Third");
            }

            return 1;
            throw new NotImplementedException();
        }
    }
}