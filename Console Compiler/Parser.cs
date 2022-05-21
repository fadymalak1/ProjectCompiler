using System;
using System.Collections.Generic;


namespace ConsoleApp1
{   


    // ===============================================================================
    public class Parser
    {
        // input
        public string input = "";
        // "i*i$"
        private int indexOfInput = -1;
        // Stack
        Stack<string> strack = new Stack<string>();

        // Table of rules
        //39 terminals

        public string[,] table =
        {
             {"Program`", "Program`", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {"ClassDeclaration End Program`", "ClassDeclaration End Program`", "ε", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {"Category ID{ Class_Implementation}", "Category ID Derive { Class_Implementation}", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, "Func_Call Class_Implementation", null, null, "ε", null, null, "VarDeclaration Class_Implementation", "VarDeclaration Class_Implementation", "VarDeclaration Class_Implementation", "VarDeclaration Class_Implementation", "VarDeclaration Class_Implementation", "VarDeclaration Class_Implementation", "VarDeclaration Class_Implementation", "VarDeclaration Class_Implementation", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, null, null, null, null, null, null, "Func Decl ;| Func Decl { VarDeclaration Statements }", "Func Decl ;| Func Decl { VarDeclaration Statements }", "Func Decl ;| Func Decl { VarDeclaration Statements }", "Func Decl ;| Func Decl { VarDeclaration Statements }", "Func Decl ;| Func Decl { VarDeclaration Statements }", "Func Decl ;| Func Decl { VarDeclaration Statements }", "Func Decl ;| Func Decl { VarDeclaration Statements }", "Func Decl ;| Func Decl { VarDeclaration Statements }", null, null, null, null, null, null, null, null, null, "Comment Class_Implementation", null, null, null, "Comment Class_Implementation", null, null, null, null, "using_command Class_Implementation", null, null, null},
             {null, null, null, null, null, null, null, null, null, "Type ID (ParameterList)", "Type ID (ParameterList)", "Type ID (ParameterList)", "Type ID (ParameterList)", "Type ID (ParameterList)", "Type ID (ParameterList)", "Type ID (ParameterList)", "Type ID (ParameterList)", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, null, null, null, null, null, null, "Ilap", "Silap", "Clop","Series", "Ilapf", "Silapf","None","Logical", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, null, null, null, null, null, "ε", "Non-Empty List", "Non-Empty List", "Non-Empty List", "Non-Empty List","Non-Empty List", "Non-Empty List", "Non-Empty List", "Non-Empty List", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, null, null, null, null, null, null, "Type ID Non-Empty List`", "Type ID Non-Empty List`", "Type ID Non-Empty List`", "Type ID Non-Empty List`", "Type ID Non-Empty List`", "Type ID Non-Empty List`", "Type ID Non-Empty List`", "Type ID Non-Empty List`", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, null, null, null, null, null, "ε", null, null, null, null, null, null, null, null, null, null, null, null, ", Type ID Non-Empty List`", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, null, null, null, "ε", null, null, "Type ID_List ; VarDeclaration", "Type ID_List ; VarDeclaration", "Type ID_List ; VarDeclaration", "Type ID_List ; VarDeclaration", "Type ID_List ; VarDeclaration", "Type ID_List ; VarDeclaration", " Type ID_List ; VarDeclaration", " Type ID_List ; VarDeclaration", "ε", "ε", "ε", null, null, null, null, null, null, null, "ε", null, null, null, null, null, null, null, null, "ε", "ε", "ε"},
             {null, null, null, "ID ID_List`", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, "ε", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, ", ID ID_List`", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, null, null, null, "ε", null, null, "Statement Statements", "Statement Statements", "Statement Statements", "Statement Statements", "Statement Statements", "Statement Statements", "Statement Statements", "Statement Statements", "Statement Statements", "Statement Statements", "Statement Statements", null, null, null, null, null, null, null, "Statement Statements", null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, null, null, null, "ε", null, null, "Assignment", "Assignment", "Assignment", "Assignment", "Assignment", "Assignment", "Assignment", "Assignment", "If _Statement", "read (ID);", "write (Expression);", null, null, null, null, null, null, null, "terminatethis_Statement", null, null, null, null, null, null, null, null, "Rotatewhen_Statement", "Continuewhen_Statement", "Replywith _ Statement"},
             {null, null, null, null, null, null, "ε", null, null, "VarDeclaration = Expression;", "VarDeclaration = Expression;", "VarDeclaration = Expression;", "VarDeclaration = Expression;", "VarDeclaration = Expression;", "VarDeclaration = Expression;", "VarDeclaration = Expression;", "VarDeclaration = Expression;", "ε", "ε", "ε", null, null, null, null, null, null, null, "ε", null, null, null, null, null, null, null, null, "ε", "ε", "ε"},
             {null, null, null, "ID (Argument_List);", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, "NonEmpty_Argument_List", null, null, null, "ε", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "ε", null, null, null, null, null},
             {null, null, null, "Expression NonEmpty_Argument_List`", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "Expression NonEmpty_Argument_List`", null, null, null, null, null},
             {null, null, null, null, null, null, null, null, "ε", null, null, null, null, null, null, null, null, null, null, null, null," , Expression NonEmpty_Argument_List`" , null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, null, null, "{ statements }", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "if (Condition _Expression) Block Statements", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, "Condition |Condition Condition _Op Condition", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "Condition |Condition Condition _Op Condition", null, null, null, null},
             {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "and", "or", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, "Expression Comparison _Op Expression", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "Expression Comparison _Op Expression", null, null, null, null},
             {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "==", null, null, null, "!=", ">", "<=", null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "Rotate when(Condition _Expression) Block Statements", null, null},
             {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "Continuewhen ( expression ; expression ; expression ) Block Statements", null},
             {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "Replywith Expression ;"},
             {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "returnID ;", null, null, null, null, null, null, null, null, null, "Replywith Expression ;"},
             {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "terminatethis;", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null},
             {null, null, null, "Term Expression`", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "Term Expression`", null, null, null, null, null},
             {null, null, null, null, "ε", null, null, null, "ε", null, null, null, null, null, null, null, null, null, null, null, "ε", "ε", "ε", "ε", "ε", "ε", "ε", null, null, "Add_Op Term Expression`", "Add_Op Term Expression`", null, null, null, null, null, null, null, null},
             {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "+", "-", null, null, null, null, null, null, null, null},
             {null, null, null, "Factor Term`", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "Factor Term`", null, null, null, null, null},
             {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "*", "/", null, null, null, null, null, null},
             {null, null, null, "ID", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "Number", null, null, null, null, null},
             {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "<* STR *>", null, null, null, "-- STR", null, null, null, null, null, null, null, null},
             {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "using(F_name.txt);", null, null, null},
             {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,"STR", null, null, null, null},
        };

        public string[] nonTers =
        {"Program", "Program`", "ClassDeclaration", "Class_Implementation", "MethodDeclaration", "Func Decl","Type","ParameterList","Non-Empty List","Non-Empty List`","VarDeclaration","ID_List",
        "ID_List`","Statements","Statement","Assignment","Func _Call","Argument_List","NonEmpty_Argument_List","NonEmpty_Argument_List`","Block Statements","If _Statement","Condition _Expression","Condition _Op","Condition",
            "Comparison _Op","Rotate _Statement","Continuewhen _Statement","Replywith _Statement","terminatethis _Statement","Expression","Expression`","Add_Op","Term","Term`","Mul_Op","Factor","Comment","using_command","F_name"};
        
        public string[] terminals =
        {"Category ID" ,"Category ID Derive","&","ID",";","{", "}", "(",")","Ilap","Silap","Clop", "Series", "Ilapf", "Silapf","None","Logical","If","read","write","=",",","and","or","!",">","<","Terminatethis","returnID","+","-","*","/","Number","STR","using","Rotate","Continuewhen","Replywith"};
        public Parser(string inIt)
        {
            input = inIt;
        }
        private void pushRule(string rule)
        {
            for (int i = rule.Length - 1; i >= 0; i--)
            {
                var ch = rule[i];
                var str = ch.ToString();
                pushItem(str);
            }
        }
        // algorithm
        public void algorithm()
        {
            string[] Words = new string[100];
            for (int i = 0; !input[i].Equals("\0"); i++) {
                for (int j = 0;(int)input[j]!= 32; j++) {
                    Words[i] += input[j];
                    
                }
                pushItem(Words[i]);
            }
       //pushItem(input[0].ToString() + "");
            //
            pushItem("Program");
            // Read one token from input
            var token = read();
            string top = null;
            do
            {
                top = pop();
                // if top is non-terminal
                if (isNonTerminal(top))
                {
                    var rule = getRule(top, token);
                    pushRule(rule);
                }
                else
                if (isTerminal(top))
                {
                    if (!top.Equals(token))
                    {
                        error("this token is not corrent , By Grammer rule . Token : (" + token + ")");
                    }
                    else
                    {
                        Console.WriteLine("Matching: Terminal :( " + token + " )");
                        token = read();
                    }
                }
                else
                {
                    error("Never Happens , Because top : ( " + top + " )");
                }
                if (token.Equals("$"))
                {
                    break;
                }
            } while (true);
            if (token.Equals("$"))
            {
                Console.WriteLine("Input is Accepted by LL1");
            }
            else
            {
                Console.WriteLine("Input is not Accepted by LL1");
            }
        }
        private bool isTerminal(string s)
        {
            for (int i = 0; i < terminals.Length; i++)
            {
                if (s.Equals(terminals[i]))
                {
                    return true;
                }
            }
            return false;
        }
        private bool isNonTerminal(string s)
        {
            for (int i = 0; i < nonTers.Length; i++)
            {
                if (s.Equals(nonTers[i]))
                {
                    return true;
                }
            }
            return false;
        }
        private string read()
        {
            indexOfInput++;
            char ch = input[indexOfInput];
            var str = ch.ToString();
            return str;
        }
        private void pushItem(string s)
        {
            strack.Push(s);
        } 
        private string pop()
        {
            return strack.Pop();
        }
        private void error(string message)
        {
            Console.WriteLine(message);
        }
        public string getRule(String non, String term)
        {
            var row = getnonTermIndex(non);
            var column = getTermIndex(term);
            var rule = table[row, column];
            if (rule == null)
            {
                error("There is no Rule by this , Non-Terminal(" + non + ") ,Terminal(" + term + ") ");
            }
            return rule;
        }
        private int getnonTermIndex(string non)
        {
            for (int i = 0; i < nonTers.Length; i++)
            {
                if (non.Equals(nonTers[i]))
                {
                    return i;
                }
            }
            error(non + " is not NonTerminal");
            return -1;
        }
        private int getTermIndex(string term)
        {
            for (int i = 0; i < this.terminals.Length; i++)
            {
                if (term.Equals(this.terminals[i]))
                {
                    return i;
                }
            }
            error(term + " is not Terminal");
            return -1;
        }
    }
}
