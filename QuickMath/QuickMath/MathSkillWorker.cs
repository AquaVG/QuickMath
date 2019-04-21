using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMath
{
    class MathSkillWorker
    {
        public static int Right = 0, Wrong = 0;
        private List<char> operations;
        private Random m;
        public string Expression {
            get {
                char currentOp = operations[m.Next(0, operations.Count)];
                int first, second;
                string result = "";
                if (currentOp == '+')
                {
                    first = m.Next(1, 51);
                    second = m.Next(1, 51);
                    result = $"{first}+{second}";
                }

                return result;
            }
        }
        public MathSkillWorker(List<char> ops)
        {
            m = new Random();
            operations = ops;
        }
    }
}
