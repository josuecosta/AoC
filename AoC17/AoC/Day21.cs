namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Day21 : Master
    {
        private IDictionary<string, List<string>> dic;

        public void Run()
        {
            dic = new Dictionary<string, List<string>>();

            this.Output1 = 1;
            this.Output2 = 1;
        }

        private void GenerateCombinations3(string key)
        {
            if (!dic.ContainsKey(key))
            {
                dic.Add(key, new List<string> { key });
            }

            var originalKey = key;

            for (int i = 0; i < 7; i++)
            {
                key = Rotate3(key);
                dic[originalKey].Add(key);
            }
        }

        private string Rotate3(string s)
        {
            if (s.Length == 11)
            {
                var aux = new StringBuilder();

                aux.Append(s[4]);
                aux.Append(s[0]);
                aux.Append(s[1]);
                aux.Append("/");
                aux.Append(s[8]);
                aux.Append(s[5]);
                aux.Append(s[2]);
                aux.Append("/");
                aux.Append(s[9]);
                aux.Append(s[10]);
                aux.Append(s[6]);

                return aux.ToString();
            }
            return s;
        }

        private string Rotate2(string s)
        {
            if (s.Length == 5)
            {
                var aux = new StringBuilder();

                aux.Append(s[3]);
                aux.Append(s[0]);
                aux.Append("/");
                aux.Append(s[4]);
                aux.Append(s[1]);

                return aux.ToString();
            }
            return s;
        }
    }
}