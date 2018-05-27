using System;
using System.Collections.Generic;

namespace AutotestingTrainingSandboxProject
{
    internal class Program
    {
        private static void Main()
        {
            var phrases = new Dictionary<Language, List<string>>
            {
                {
                    Language.English,
                    new List<string>
                    {
                        "Hello world!",
                        "Good morning!",
                        "Thank you very much!"
                    }
                },
                {
                    Language.German,
                    new List<string>
                    {
                        "Hallo Welt!",
                        "Guten Morgen!",
                        "Vielen Dank!"
                    }
                },
                {
                    Language.French,
                    new List<string>
                    {
                        "Bonjour le monde!",
                        "Bon matin!",
                        "Merci beaucoup!"
                    }
                },
                {
                    Language.Russian,
                    new List<string>()
                },
                {
                    Language.Ukrainian,
                    null
                }
            };

            Console.WriteLine("The following languages contain at least one phrase:");
            foreach (var item in phrases)
            {
                if (item.Value != null && item.Value.Count > 0)
                {
                    Console.WriteLine(item.Key);
                }
            }

            Console.WriteLine("\nPlease select a language:");
            Language selectedLanguage;
            while (!Enum.TryParse(Console.ReadLine(), out selectedLanguage))
            {
                Console.WriteLine("Incorrect language.\nPlease try again:");
            }
            
            Console.WriteLine("\nThe following phrases available:");
            foreach (var phrase in phrases[selectedLanguage])
            {
                Console.WriteLine(phrase);
            }

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadLine();
        }
    }
}
