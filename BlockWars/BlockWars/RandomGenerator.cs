using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame2
{
    static class RandomGenerator
    {
        private static Random mRandom;

        static RandomGenerator()
        {
            mRandom = new Random();
        }

        public static bool CheckEvent(float probability)
        {
            double number = mRandom.NextDouble();
            return number <= probability;
        }

        public static float RandomDamage(float maxDamage)
        {
            return (float)(maxDamage * mRandom.NextDouble());
        }
    }
}
