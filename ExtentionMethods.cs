using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaziMarket1
{
    public static class ExtentionMethods
    {
        public static string DevideMoneyNumber(this int x)
        {
            char[] b = x.ToString().ToCharArray();
            string s = null;
            for (int i = 0; i < b.Length; i++)
            {
                s += b[i];
                if (i == b.Length - 4 || i == b.Length - 7 || i == b.Length - 10 || i == b.Length - 13 || i == b.Length - 16)
                {
                    s += ",";
                }
            }
            return s;
        }


        public static bool StarRate(this double rate)
        {
            if (rate*10%10 >= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}