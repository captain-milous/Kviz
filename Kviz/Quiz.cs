using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kviz
{
    internal class Quiz
    {
        private string name;
        private List<Question> questions = new List<Question>();

        public string Name 
        { 
            get { return name; } 
            set { name = value; }
        }
        public List<Question> Questions
        {
            get { return questions; }
            set { questions = value; }
        }

        public Quiz() 
        {
            Name = "NoName";
            Questions = new List<Question>();
        }
        public Quiz(string name)
        {
            Name = name;
            Questions = new List<Question>();
        }
        public Quiz(string name, List<Question> questions)
        {
            Name = name;
            Questions = questions;
        }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }
        public void RemoveQuestion(Question question)
        {
            try
            {
                Questions.Remove(question);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }

        public override string ToString()
        {
            string output = "Kvíz " + Name + "\n\n----------------------------------------------------------------------------------\n";
            for(int i = 0; i < questions.Count;i++)
            {
                int j = i + 1;
                output = output + "\n" + j + ". Otázka\n" + questions[i].ToString() + "\n----------------------------------------------------------------------------------\n";
            }
            return output;
        }
    }
}
