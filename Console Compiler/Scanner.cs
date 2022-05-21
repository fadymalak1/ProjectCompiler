using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public class Scanner
    {
        public int reject = -1;      

        public int asciiMin = 32;   

        public int asciiMax = 127;   

        private int numStates = 0;  

        private int[,] transition;   

        private int numFinal = 0;   

        private int[] finalStates;   

        public Dictionary<string, string> Words = new Dictionary<string, string>();
        public static Dictionary<string, string> other = new Dictionary<string, string>();

        public Scanner(int numState, int numFinal)
        {
            Words.Add("category", "Class");
            Words.Add("derive", "Inheritance");
            Words.Add("if", "Condition");
            Words.Add("else", "Condition");
            Words.Add("ilap", "Integer");
            Words.Add("silap", "SInteger");
            Words.Add("clop", "Character");
            Words.Add("series", "String");
            Words.Add("ilapf", "Float");
            Words.Add("silapf", "SFloat");
            Words.Add("none", "Void");
            Words.Add("logical", "Boolean");
            Words.Add("terminatethis", "break");
            Words.Add("rotatewhen", "Loop");
            Words.Add("continuewhen", "Loop");
            Words.Add("replywith", "Return");
            Words.Add("seop", "Struct");
            Words.Add("checksituationof", "Switch");
            Words.Add("program", "Start Statement");
            Words.Add("end", "End Statement");
            Words.Add("+", "Arithmetic Operation");
            Words.Add("-", "Arithmetic Operation");
            Words.Add("*", "Arithmetic Operation");
            Words.Add("/", "Arithmetic Operation");
            Words.Add("&&", "Logic Operators");
            Words.Add("||", "Logic Operators");
            Words.Add("~", "Logic Operators");
            Words.Add("==", "Relational Operators");
            Words.Add("<", "Relational Operators");
            Words.Add(">", "Relational Operators");
            Words.Add("!=", "Relational Operators");
            Words.Add("<=", "Relational Operators");
            Words.Add(">=", "Relational Operators");
            Words.Add("=", "Assignment operator");
            Words.Add(".", "Access Operator");
            Words.Add("{", "Braces");
            Words.Add("}", "Braces");
            Words.Add("[", "Braces");
            Words.Add("]", "Braces");
            Words.Add(@" ^[0 - 9] + (.[0 - 9] +) ? $", "Constant");
            Words.Add("\"", "Quotation Mark");
            Words.Add("'", "Quotation Mark");
            Words.Add("using", "Inclusion");
            Words.Add("<*", "Comment");
            Words.Add("*>", "Comment");
            Words.Add("--", "Comment");
            other.Add(@"^[a-zA-z_][a-zA-z0-9_]*$", "Identifier");
            this.numStates = numState;


            this.transition = new int[this.numStates, this.asciiMax + 1];

            this.finalStates = new int[numFinal];

            for (int s = 0; s < this.numStates; s++)
            {
                for (int j = 0; j < this.asciiMax; j++)
                {
                    this.transition[s, j] = this.reject;
                }
            }
        }
        public void addTrans(int currentState, char input, int nextState)
        {
            if (currentState >= 0 && currentState < this.numStates && nextState >= 0 && nextState <= this.numStates && (int)(input) >= this.asciiMin && (int)(input) <= this.asciiMax)
            {
                this.transition[currentState, input] = nextState;
            }
            else
            {
                Console.WriteLine("Problem with the transition: " + currentState.ToString() + " reading " + input.ToString() + " -> " + nextState.ToString());
            }
        }
        public void addFinal(int state)
        {
            if (this.numFinal >= this.finalStates.Length )
            {
                Console.WriteLine("No room for final state:" + state.ToString());
                return;
            }
            this.finalStates[this.numFinal] = state;
            this.numFinal++;
        }
        private bool isFinal(int state)
        {
            int j;
            for (j = 0; j < this.numFinal; j++)
            {
                if (state == this.finalStates[j])
                {
                    return true;
                }
            }
            return false;
        }
        bool isIdentifier(string lex)
        {
            bool isValid = false;
            var rx = new Regex(@"^[a-zA-z_][a-zA-z0-9_]*$", RegexOptions.Compiled);
            if (rx.IsMatch(lex))
            {
                isValid = true;
            }
            return isValid;
        }
        bool isNumber(string lex)
        {
            bool isValid = false;
            var rx = new Regex(@"^[0-9]+(.[0-9]+)?$", RegexOptions.Compiled);
            if (rx.IsMatch(lex))
            {
                isValid = true;
            }
            return isValid;
        }
        public bool recognize(String str)
        {
            var state = 1;
            var index = 0;
            char current;

            while (index < str.Length && state != this.reject)
            {
                current = str[index++];
                state = this.transition[state, current];
            }

            if (index == str.Length && this.isFinal(state)||isNumber(str)|| isIdentifier(str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    } 
}
