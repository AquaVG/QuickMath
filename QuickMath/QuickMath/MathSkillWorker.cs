using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuickMath
{
    class MathSkillWorker
    {
        public int Right = 0, Wrong = 0;
        private Random m;
        private int level;

        private Regex isNormalDouble = new Regex(@"^\d+\,\d5*$");
        private Regex getDigitPow2 = new Regex("(?<DigitValue>[^\\D]+)²", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
        private Regex getDigitPow3 = new Regex("(?<DigitValue>[^\\D]+)³", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
        private Regex getDigitSqrt = new Regex("√(?<DigitValue>[^\\D]+)", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
        public List<char> EnableActions
        {
            get {
                List<char> enableAct = new List<char>() { '+', '-' };
                if (level > 2)
                    enableAct.Add('*');
                if (level > 5)
                    enableAct.Add('/');
                if (level > 7)
                    enableAct.Add('²');
                if (level > 8)
                    enableAct.Add('√');
                if (level == 10)
                    enableAct.Add('³');
                return enableAct;
            }
        }

        public string Expression
        {
            get {
                double num1 = 0, num2 = 0;
                char operation = EnableActions[m.Next(0, EnableActions.Count)];
                if (operation == '+' || operation == '-')
                {
                    if (level >= 3)
                    {
                        if (m.Next(0, 3) == 2)
                        {
                            num1 = m.Next(1, 61);
                            num2 = m.Next(1, 61);
                        }
                        else
                        {
                            do
                            {
                                num1 = Math.Round(RandomNumberBetween(1, 51), 2);
                                num2 = Math.Round(RandomNumberBetween(1, 51), 2);
                            } while (!isNormalDouble.IsMatch(num1.ToString()) || !isNormalDouble.IsMatch(num2.ToString()));
                        }
                    }
                    else
                    {
                        num1 = m.Next(1, 51);
                        num2 = m.Next(1, 51);
                    }
                }
                else if (operation == '/')
                {
                    do
                    {
                        num1 = m.Next(2, (level < 9) ? 81 : 225);
                        num2 = m.Next(2, (level < 9) ? 81 : 225);
                    } while (num1 % num2 != 0 || num1 / num2 == 1);
                }
                else if (operation == '*')
                {
                    num1 = m.Next(2, (level < 7) ? 9 : 15);
                    num2 = m.Next(2, (level < 7) ? 9 : 15);
                }
                else if (operation == '√')
                {
                    if (level == 9)
                    {
                        do
                        {
                            num1 = m.Next(1, 225);
                        } while (Math.Sqrt(num1) % 1 != 0);
                        return $"√{num1}";
                    }
                    else
                    {
                        bool firsNumToSqrt = m.Next(0, 2) == 0;
                        bool forFirstLevelCond, forSecondLevelCond;

                        do
                        {
                            num1 = m.Next(1, firsNumToSqrt ? 225 : 21);
                            num2 = m.Next(1, firsNumToSqrt ? 225 : 21);

                            forFirstLevelCond = Math.Sqrt(num1) % 1 != 0 || Math.Sqrt(num1) < num2;
                            forSecondLevelCond = Math.Sqrt(num2) % 1 != 0;
                        } while (firsNumToSqrt ? forFirstLevelCond : forSecondLevelCond);

                        char localOp = ' ';
                        switch (m.Next(0, 3))
                        {
                            case 0:
                                localOp = '+';
                                break;
                            case 1:
                                localOp = '-';
                                break;
                            case 2:
                                localOp = '*';
                                break;
                        }
                        return firsNumToSqrt ? $"√{num1}{localOp}{num2}" : $"{num1}{localOp}√{num2}";
                    }
                }
                else if (operation == '²')
                {
                    if (level == 8 || level == 9)
                        return $"{m.Next(1, 16)}²";
                    else
                    {
                        bool firstNumToPow = m.Next(0, 2) == 0;
                        num1 = m.Next(1, 16);
                        char localOp = ' ';
                        switch (m.Next(0, 3))
                        {
                            case 0:
                                localOp = '+';
                                num2 = m.Next(1, 51);
                                break;
                            case 1:
                                localOp = '-';
                                num2 = m.Next(1, 51);
                                break;
                            case 2:
                                localOp = '*';
                                num2 = m.Next(1, 10);
                                break;
                        }
                        return Math.Pow(num1, 2) < num2 ? $"{num2}{localOp}{num1}²" : $"{num1}²{localOp}{num2}";
                    }
                }
                else
                    return $"{m.Next(1, 10)}³";
                if (num1 < num2)
                {
                    double temp = num1;
                    num1 = num2;
                    num2 = temp;
                }
                return $"{num1}{operation}{num2}";
            }

        }
        Random random = new Random();
        private double RandomNumberBetween(int max, int min)
        {
            return random.NextDouble() * (max - min) + min;
        }

        public void CheckAnswer(string expression, double uAns)
        {
            int digitValue;
            if (expression.Contains('²'))
            {
                digitValue = int.Parse(getDigitPow2.Match(expression).Groups["DigitValue"].Value);
                expression = expression.Replace($"{digitValue}²", Math.Pow(digitValue, 2).ToString());
            }
            if (expression.Contains('³'))
            {
                digitValue = int.Parse(getDigitPow3.Match(expression).Groups["DigitValue"].Value);
                expression = expression.Replace($"{digitValue}³", Math.Pow(digitValue, 3).ToString());
            }
            if (expression.Contains('√'))
            {
                digitValue = int.Parse(getDigitSqrt.Match(expression).Groups["DigitValue"].Value);
                expression = expression.Replace($"√{digitValue}", Math.Sqrt(digitValue).ToString());
            }
            expression = expression.Replace(',', '.');


            if (Convert.ToDouble(new DataTable().Compute(expression, null)) == uAns)
            {
                Right++;
                Debug.WriteLine("+");
            }
            else
            {
                Wrong++;
                Debug.WriteLine("-");
            }
        }
        public MathSkillWorker(int level)
        {
            this.level = level;
            m = new Random();
        }
    }
}
