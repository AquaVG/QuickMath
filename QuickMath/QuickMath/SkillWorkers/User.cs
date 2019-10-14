using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AquaDir;
using System.Threading.Tasks;

namespace QuickMath
{
    public class User
    {
        public string Name;
        public PracticeTypeInfo MathInfo;
        public PracticeTypeInfo MemoryInfo;

        public User(string name, PracticeTypeInfo mathInfo, PracticeTypeInfo memoryInfo)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            MathInfo = mathInfo;
            MemoryInfo = memoryInfo;
        }
    }
    public struct PracticeTypeInfo
    {
        private int score;
         
        public int Level { get; set; }
        public int Score
        {
            get => score;
            set {
                score = value;
                if (score < 0 && Level > 1) //In case substraction our new score will be less than 0, we "level-down" and new score now equals 200 - value
                {
                    score = 200 + score;
                    Level--;
                }
                else
                {
                    while (score >= 200 && Level < 10)//otherwise we just substract 200 from score till score more than 200. Btw doing "level-up"
                    {
                        score -= 200;
                        Level++;
                    }
                }
            }
        }
        public PracticeTypeInfo(int level)
        {
            score = 0;
            Level = level;
        }
    }
    public enum PracType
    {
        Math,
        Memory
    }
}
