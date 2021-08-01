using System;
using System.Linq;

namespace VijinerShifr
{
    internal class Program
    {
        public static void Main(string[] args)
        {   
           
            int itemMenu;
            do
            {
                Console.WriteLine("1)Зашифровать");
                Console.WriteLine("2)Расшифровать");
                Console.WriteLine("3)Выход");
                itemMenu = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Key:");
                string tempKey = new string(Console.ReadLine().ToUpper().Where(x => char.IsLetterOrDigit(x)).ToArray());
                Console.WriteLine("Text:");
                string text = new string(Console.ReadLine().ToUpper().Where(x => char.IsLetter(x)).ToArray());
                string key = "";
                Console.WriteLine("ROT:");
                int rot = Convert.ToInt32(Console.ReadLine());
                for (int i = 0, j = 0; i < text.Length; i++)
                {
                    if (char.IsLetter(text[i]))
                    {
                        Console.Write(text[i]);
                        key += tempKey[Mod(j++, tempKey.Length)];
                    }
                }

                Console.WriteLine();
                Console.WriteLine(key);
                switch (itemMenu)
                {
                    case 1:
                    {
                        Console.WriteLine("1)При помощи таблицы");
                        Console.WriteLine("2)Математическим способом");
                        itemMenu = Convert.ToInt32(Console.ReadLine());
                        switch (itemMenu)
                        {
                            case 1:
                            {
                                EncryptTextMatrix(text,key,rot);
                            }
                                break;
                            case 2:
                                EncryptText(text,key,rot);
                                break;
                        }
                    }
                        break;
                    case 2:
                    {
                        Console.WriteLine("1)При помощи таблицы");
                        Console.WriteLine("2)Математическим способом");
                        itemMenu = Convert.ToInt32(Console.ReadLine());
                        switch (itemMenu)
                        {
                            case 1:
                            {
                                DecryptTextMatrix(text,key,rot);
                            }
                                break;
                            case 2:
                                DecryptText(text,key,rot);
                                break;
                        }
                    }
                        break;
                }
                Console.WriteLine();
            } while (itemMenu != 3);

        }

        static void EncryptTextMatrix(string text,string key,int rot)
        {
            string alphabet = GetAlphabet(text);
            char[,] matrix = GetTable(rot,alphabet);
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(matrix[alphabet.IndexOf(key[i]),alphabet.IndexOf(text[i])]);
            }

            Console.WriteLine();
        }
        static void DecryptTextMatrix(string text,string key,int rot)
        {
            string alphabet = GetAlphabet(text);
            char[,] matrix = GetTable(rot,alphabet);

            for (int i = 0; i < text.Length; i++)
            {
                var indexOf = alphabet.IndexOf(key[i]);
                char element=' ';
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (matrix[j, indexOf] == text[i])
                    {
                        element = alphabet[j];
                        break;
                    }
                }
                Console.Write(element);
            }

            Console.WriteLine();
        }
        static void EncryptText(string text,string key,int rotIndex)
        {
            string alphabet =GetAlphabet(text);
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(alphabet[Mod(alphabet.IndexOf(key[i])+alphabet.IndexOf(text[i])+rotIndex,alphabet.Length)]);
            }
        }

        static void DecryptText(string text,string key,int rotIndex)
        {
            string alphabet = GetAlphabet(text);
            for (int i = 0; i < text.Length; i++)
            {
                var indexOf = alphabet.IndexOf(key[i]);
                var of = alphabet.IndexOf(text[i]);
                Console.Write(alphabet[Mod(of-indexOf-rotIndex,alphabet.Length)]);
            }
        }

        static char[,] GetTable(int ROT,string alphabet)
        {
            char[,] matrix = new char[alphabet.Length,alphabet.Length];
            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0,k = alphabet[0]+i+ROT; j < alphabet.Length; j++)
                {
                    k = k > alphabet[alphabet.Length-1] ? alphabet[0] : k;
                    matrix[i, j] =  (char)k;
                    k++;
                    Console.Write(matrix[i,j]);
                }

                Console.WriteLine();
            }

            return matrix;
        }

        static string GetAlphabet(string text)
        {
            bool isRusAlphabet = true;
            for (int i = 0; i < text.Length; i++)
            {
                if ("ABCDEFGHIKLMNOPQRSTVXYZ".Contains(text[i]))
                    isRusAlphabet = false;
            }
            return  isRusAlphabet?"АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ":"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }
        static int Mod(int first, int second) => first % second < 0 ? second + first % second : first % second;
    }
}