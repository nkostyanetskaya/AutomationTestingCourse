using System;

namespace AutotestingTrainingSandboxProject
{
    internal static class Program
    {
        private static readonly LocalizedText[][] _dictionary = new[]
        {
            new[]
            {
                new LocalizedText
                {
                    Language = Language.English,
                    Text = "Hello world!"
                },
                new LocalizedText
                {
                    Language = Language.German,
                    Text = "Hallo Welt!"
                },
                new LocalizedText
                {
                    Language = Language.French,
                    Text = "Bonjour le monde!"
                }
            },
            new[]
            {
                new LocalizedText
                {
                    Language = Language.English,
                    Text = "Good morning!"
                },
                new LocalizedText
                {
                    Language = Language.German,
                    Text = "Guten Morgen!"
                },
                new LocalizedText
                {
                    Language = Language.French,
                    Text = "Bon matin!"
                }
            },
            new[]
            {
                new LocalizedText
                {
                    Language = Language.English,
                    Text = "Thank you very much!"
                },
                new LocalizedText
                {
                    Language = Language.German,
                    Text = "Vielen Dank!"
                },
                new LocalizedText
                {
                    Language = Language.French,
                    Text = "Merci beaucoup!"
                }
            }
        };


        private static void WaitKeyPressForExit()
        {
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadLine();
        }

        private static void PrintPhrases(Language language)
        {
            Console.WriteLine("\nThe following phrases available:");
            for (int i = 0; i < _dictionary.Length; i++)
            {
                foreach (var phrase in _dictionary[i])
                {
                    if (phrase.Language == language)
                    {
                        Console.WriteLine($"{i+1}) {phrase}");
                    }
                }
            }
        }

        private static void PrintTranslation(Language originalLanguage, Language targetLanguage, int selectedPhrase)
        {
            LocalizedText originalPhrase = null, targetPhrase = null;
            foreach (var phrase in _dictionary[selectedPhrase - 1])
            {
                if (phrase.Language == originalLanguage)
                {
                    originalPhrase = phrase;
                }

                if (phrase.Language == targetLanguage)
                {
                    targetPhrase = phrase;
                }
            }

            Console.WriteLine($"Original phrase:\n{originalPhrase}");
            Console.WriteLine($"Translated phrase:\n{targetPhrase}");
        }

        private static int CountPhrases(Language language)
        {
            var counter = 0;
            foreach (var phrases in _dictionary)
            {
                foreach (var phrase in phrases)
                {
                    if (phrase.Language == language)
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }

        private static int SelectPhrase(Language language)
        {
            Console.WriteLine("\nPlease select a number of the phrase for translation:");
            int selectedPhrase;
            while (!int.TryParse(Console.ReadLine(), out selectedPhrase) && selectedPhrase >0 && selectedPhrase <= CountPhrases(language))
            {
                Console.WriteLine("Incorrect number.\nPlease try again:");
            }

            return selectedPhrase;
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

        private static void PrintLanguages()
        {
            Console.WriteLine("There are the following languages available:");
            foreach (var language in Enum.GetNames(typeof(Language)))
            {
                Console.WriteLine(language);
            }
        }

        private static void Main()
        {
            PrintLanguages();
            var originalLanguage = SelectLanguage();
            PrintPhrases(originalLanguage);
            Console.Write("\nEnter a data for the translation below.");
            var selectedPhrase = SelectPhrase(originalLanguage);
            var targetLanguage = SelectLanguage();
            PrintTranslation(originalLanguage, targetLanguage, selectedPhrase);
            WaitKeyPressForExit();
        }
    }
}
