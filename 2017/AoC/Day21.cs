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

            foreach (var row in this.Input)
            {
                var key = row.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries).First().Trim();

                if (key.IndexOf('/') == 2)
                {
                    GenerateCombinations2(key);
                }
                else
                {
                    GenerateCombinations3(key);
                }
            }

            this.Output1 = 1;
            this.Output2 = 1;
        }

        private void GenerateCombinations3(string key)
        {
            var originalKey = key;

            for (int j = 0; j < 4; j++)
            {
                AddKey(dic, originalKey, key);

                for (int i = 0; i < 7; i++)
                {
                    key = Rotate3(key);
                    AddKey(dic, originalKey, key);
                }

                // Flip 2º, 3º and 4º quadrant
                key = Flip(originalKey, j + 2);
            }
        }

        private string Flip(string key, int quadrant)
        {
            var aux = new StringBuilder();

            switch (quadrant)
            {
                case 2:

                    aux.Append(key[2]);
                    aux.Append(key[1]);
                    aux.Append(key[0]);
                    aux.Append("/");
                    aux.Append(key[6]);
                    aux.Append(key[5]);
                    aux.Append(key[4]);
                    aux.Append("/");
                    aux.Append(key[10]);
                    aux.Append(key[9]);
                    aux.Append(key[8]);

                    return aux.ToString();

                case 3:

                    aux.Append(key[8]);
                    aux.Append(key[9]);
                    aux.Append(key[10]);
                    aux.Append("/");
                    aux.Append(key[4]);
                    aux.Append(key[5]);
                    aux.Append(key[6]);
                    aux.Append("/");
                    aux.Append(key[0]);
                    aux.Append(key[1]);
                    aux.Append(key[2]);

                    return aux.ToString();

                case 4:

                    aux.Append(key[10]);
                    aux.Append(key[9]);
                    aux.Append(key[8]);
                    aux.Append("/");
                    aux.Append(key[6]);
                    aux.Append(key[5]);
                    aux.Append(key[4]);
                    aux.Append("/");
                    aux.Append(key[2]);
                    aux.Append(key[1]);
                    aux.Append(key[0]);

                    return aux.ToString();

                default:
                    return key;
            }
        }

        private void GenerateCombinations2(string key)
        {
            var originalKey = key;

            AddKey(dic, originalKey, key);

            for (int i = 0; i < 3; i++)
            {
                key = Rotate2(key);
                AddKey(dic, originalKey, key);
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

        private void AddKey(IDictionary<string, List<string>> dic, string key, string value)
        {
            if (!dic.ContainsKey(key))
            {
                dic.Add(key, new List<string>());
            }
            if (!dic[key].Contains(value))
            {
                dic[key].Add(value);
            }
        }
    }
}