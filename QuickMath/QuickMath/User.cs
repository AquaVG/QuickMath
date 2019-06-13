using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Name = name ?? throw new ArgumentNullException(nameof(name));
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
                while (score >= 200 && Level < 10)
                {
                    score -= 200;
                    Level++;
                }
            }
        }

        public PracticeTypeInfo(int level, int score)
        {
            this.score = 0;
            Level = level;
            Score = score;
        }
    }
    public enum PracType
    {
        Math,
        Memory
    }
}
