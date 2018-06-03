using System;
using System.Collections.Generic;

namespace AutotestingTrainingSandboxProject
{
    internal static class Program
    {
        private static readonly Dictionary<Language, string[]> _phrases = new Dictionary<Language, string[]> {
            {
                Language.English,
                new[] {
                    "Hello world!",
                    "Good morning!",
                    "Thank you very much!"
                }
            }, {
                Language.German,
                new[] {
                    "Hallo Welt!",
                    "Guten Morgen!",
                    "Vielen Dank!"
                }
            }, {
                Language.French,
                new[] {
                    "Bonjour le monde!",
                    "Bon matin!",
                    "Merci beaucoup!"
                }
            }, {
                Language.Russian,
                new string[0]
            }, {
                Language.Ukrainian,
                null
            }
        };

        private static void WaitKeyPressForExit()
        {
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadLine();
        }

        private static void PrintSelectedLanguagePhrases(Language selectedLanguage)
        {
            Console.WriteLine("\nThe following phrases available:");
            foreach (var phrase in _phrases[selectedLanguage])
            {
                Console.WriteLine(phrase);
            }
        }

        private static Language SelectLanguage()
        {
            Console.WriteLine("\nPlease select a language:");
            Language selectedLanguage;
            while (!Enum.TryParse(Console.ReadLine(), out selectedLanguage))
            {
                Console.WriteLine("Incorrect language.\nPlease try again:");
            }

            return selectedLanguage;
        }

        private static void PrintNonEmptyLanguages()
        {
            Console.WriteLine("The following languages contain at least one phrase:");
            foreach (var item in _phrases)
            {
                if (item.Value != null && item.Value.Length > 0)
                {
                    Console.WriteLine(item.Key);
                }
            }
        }

        private static void Main()
        {
            PrintNonEmptyLanguages();
            Language selectedLanguage = SelectLanguage();
            PrintSelectedLanguagePhrases(selectedLanguage);
            WaitKeyPressForExit();
        }
    }
}
