using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MyCourceApp.Scrips
{
    class Parse
    {
        public string pares(string pattern, string patternTwo)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            String responce = wc.DownloadString("https://stbank.by/private_client/currency_exchange_operations/");
            string input = pattern;
            MatchCollection match = Regex.Matches(responce, input);
            string all = "";
            foreach (Match m in match)
            {
                all = m.Value;
            }

            string patt = patternTwo;
            string gotstr = "";
            MatchCollection mainMatch = Regex.Matches(all, patt);
            foreach (Match m in mainMatch)
            {
                gotstr = m.Value;
            }

            return gotstr;
        }
    }
}
