using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kviz
{
    public class Question
    {
        private string interrogator;
        private List<string> answers;
        private int rightAnswer;

        public string Interrogator { 
            get { return interrogator; } 
            set { interrogator = value; }
        }
        public List<string> Answers { 
            get { return answers; } 
            set { answers = value; }
        }
        public int RightAnswer { 
            get { return rightAnswer; } 
            set { if(value >= 0 && value < Answers.Count()) rightAnswer = value; }
        }

        public Question() 
        {
            Interrogator = "";
            Answers = new List<string>();
            RightAnswer = 0;
        }

        public Question(string interr)
        {
            Interrogator = interr;
            Answers = new List<string>();
            RightAnswer = 0;
        }

        public Question(string interr, List<string> answ, int ra) 
        { 
            Interrogator = interr;
            Answers = answ;
            RightAnswer = ra;
        }

        public void addAnswer(string answ)
        {
            Answers.Add(answ);
        }

        public override string ToString()
        {
            //return $"Interrogator: {Interrogator}, Answers: {Answers}, RightAnswer: {RightAnswer}";
            string output = Interrogator + "\n\n";
            for (int i = 0; i < Answers.Count(); i++)
            {
                int j = i + 1;
                output = output + j + ". " + Answers[i] + "\n";
            }
            output = output + "\n";
            return output;
        }

        public string AskQuestion()
        {
            string output = Interrogator+ "\n\n";
            for(int i = 0; i < Answers.Count();i++)
            {
                int j = i + 1;
                output = output+j+". " + Answers[i]+ "\n";
            }
            output = output + "\n";
            return output;
        }
    }
}
