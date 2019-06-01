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
                string range = "1";
                for (int i = 0; i < Lenght-1; i++)
                {
                    range += "0";
                }
                return new Random().Next(Convert.ToInt32(range), Convert.ToInt32(range + "0")).ToString();
            }
        }
        public MemorySkillWorker (int lenght)
        {
            Lenght = lenght;
        }
    }
}
