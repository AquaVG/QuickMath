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
                if (score < 0 && Level > 1)
                {
                    score = 200 + score;
                    Level--;
                }
                else
                {
                    while (score >= 200 && Level < 10)
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
