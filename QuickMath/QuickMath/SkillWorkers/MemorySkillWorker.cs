using AquaDir;
using System;

namespace QuickMath.SkillWorkers
{
    class MemorySkillWorker
    {
        public int Right, Wrong;
        private int Level;
        public void CheckAnswer(string answer, string uanswer)
        {
            if (answer == uanswer) Right++;
            else Wrong++;
        }
        public string Expression
        {
            get {
                string range = "1";
                for (int i = 0; i < Lenght - 1; i++)
                {
                    range += "0";
                }
                return new Random().Next(range.ToInt32(),(range + "0").ToInt32()).ToString();
            }
        }
        public int Lenght
        {
            get {
                if (Level <= 4)
                    return 4;
                else if (Level <= 7)
                    return 5;
                else
                    return 6;
            }
        }
        public int SecondsToHide
        {
            get {
                if (Level.IsOneOf(1, 2, 5, 8))
                    return 3;
                else return Level.IsOneOf(3, 6, 9) ? 2 : 1;
            }
        }

        public MemorySkillWorker(int level)
        {
            Level = level;
        }
    }
}
