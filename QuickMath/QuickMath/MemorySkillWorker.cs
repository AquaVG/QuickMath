using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMath
{
    class MemorySkillWorker
    {
        public int Right, Wrong;
        private int Lenght;
        public void CheckAnswer(string answer, string uanswer)
        {
            if (answer == uanswer) Right++;
            else Wrong++;
        }
        public string Expression
        {
            get{
                string expression = "";
                for (int i = 0; i <= Lenght; i++)
                {
                    expression += new Random().Next(0, 10).ToString();
                }
                return expression;
            }
        }
        public MemorySkillWorker (int lenght)
        {
            Lenght = lenght;
        }
    }
}
