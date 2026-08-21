using System;

namespace TestClass
{
    public static class CommonUtil
    {
        public static float GetPercent(int inMaxHp, int inHp)
        {
            return (float)inHp / inMaxHp;
        }

        public static float GetPercent(float inMaxHp, float inHp)
        {
            return inHp / inMaxHp;
        }

        public static string GetPercentString(float inPercent)
        {
            string[] list = (inPercent * 100).ToString().Split('.');

            if (list.Length > 0)
            {
                return list[0];
            }
            else return "";
        }

        public static float GetPercentConvert(float inPercent)
        {
            return MathF.Round(inPercent, 2) * 100;
        }
    }
}
