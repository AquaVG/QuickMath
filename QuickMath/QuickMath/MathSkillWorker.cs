using System;
using System.Collections.Generic;
using System.Data;

namespace QuickMath
{
    class MathSkillWorker
    {
        public int Right = 0, Wrong = 0;
        private List<char> operations;
        private Random m;
        public string Expression
        {
            get {
                char currentOp = operations[m.Next(0, operations.Count)];
                int first, second;
                if (currentOp == '+' || currentOp == '-')
                {
                    first = m.Next(1, 51);
                    second = m.Next(1, 51);
                }
                else if (currentOp == '*')
                {
                    first = m.Next(2, 12);
                    second = m.Next(2, 12);
                }
                else
                {
                    do
                    {
                        first = m.Next(4, 121);
                        second = m.Next(4, 121);
                    } while (first % second != 0 || first / second == 1);
                }
                if (first < second)
                {
                    int temp = first;
                    first = second;
                    second = temp;
                }
                return $"{first}{currentOp}{second}";
            }
        }
        public void CheckAnswer(string expression, int uAns)
        {
            int result = Convert.ToInt32(new DataTable().Compute(expression, null).ToString());
            if (result == uAns) Right++;
            else Wrong++;
        }
        public MathSkillWorker(List<char> ops)
        {
            m = new Random();
            operations = ops;
        }
    }
}
