using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiderX
{
    /*
     * Gerekli olan karakterler ve sayılar 
     */
    class Components
    {
        //OZEL KARAKTERLER
        public List<string> specialCharacters = new List<string>();
        //ALFABE KUCUK HARF
        public List<string> alphabeSmall = new List<string>();
        //AlphabeBig
        public List<string> alphabeBig = new List<string>();
        //Sayilar
        public List<int> sayilar = new List<int>();

        public async void Main()
        {
            string[] arrayChar = { "@", "!", "#", "$", "%", "&", "^", "+", "?", "*", ".", "£" };

            string[] arrayAlphabeSmall = { "a", "b", "c", "ç", "d", "e", "f", "g", "ğ", "h", "i", "ı",
                "j", "k", "l", "m", "n", "o", "ö", "p", "r", "s", "ş", "t", "u", "ü", "v", "y", "z" };

            string[] arrayAlphabeBig = { "A", "B", "C", "Ç", "D", "E", "F", "G", "Ğ", "H", "İ", "I",
                "J", "K", "L", "M", "N", "O", "Ö", "P", "R", "S", "Ş", "T", "U", "Ü", "V", "Y", "Z" };

            foreach (string item in arrayChar)
            {
                specialCharacters.Add(item);
            }
            foreach (string item in arrayAlphabeSmall)
            {
                alphabeSmall.Add(item);
            }
            foreach (string item in arrayAlphabeBig)
            {
                alphabeBig.Add(item);
            }
            for (int i = 0; i < 500; i++)
            {
                sayilar.Add(i);
            }
        }
    }
}
