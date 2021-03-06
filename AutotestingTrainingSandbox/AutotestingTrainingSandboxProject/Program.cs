﻿using System;
using System.ComponentModel;
using System.Linq;

namespace AutotestingTrainingSandboxProject
{
    internal static class Program
    {
        private static readonly LocalizedText[][] _dictionary = 
        {
            new[]
            {
                new LocalizedText(Language.English, "Hello world!"),              
                new LocalizedText(Language.German, "Hallo Welt!"),
                new LocalizedText(Language.French, "Bonjour le monde!")
            },
            new[]
            {
                new LocalizedText(Language.English, "Good morning!"),
                new LocalizedText(Language.German, "Guten Morgen!"),
                new LocalizedText(Language.French, "Bon matin!")
            },
            new[]
            {
                new LocalizedText(Language.English, "Thank you very much!"),
                new LocalizedText(Language.German, "Vielen Dank!"),
                new LocalizedText(Language.French, "Merci beaucoup!")
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
            for (var i = 0; i < _dictionary.Length; i++)
            {
                var phrase = _dictionary[i].FirstOrDefault(x => x.Language == language);
                if (phrase != null)
                {
                    Console.WriteLine("{0}) {1}", i + 1, phrase);
                }              
            }
        }

        private static void PrintTranslation(Language originalLanguage, Language targetLanguage, int selectedPhrase)
        {
            var originalPhrase = _dictionary[selectedPhrase - 1].FirstOrDefault(x => x.Language == originalLanguage);
            var targetPhrase = _dictionary[selectedPhrase - 1].FirstOrDefault(x => x.Language == targetLanguage);
            Console.WriteLine("Original phrase:\n{0}", originalPhrase);
            Console.WriteLine("Translated phrase:\n{0}", targetPhrase);   
        }

        private static int CountPhrases(Language language)
        {
            return _dictionary.SelectMany(phrases => phrases).Count(phrase => phrase.Language == language);
        }

        private static int SelectPhrase(Language language)
        {
            Console.WriteLine("\nPlease select a number of the phrase for translation:");
            int selectedPhrase;
            while (!int.TryParse(Console.ReadLine(), out selectedPhrase) || !(selectedPhrase > 0 && selectedPhrase <= CountPhrases(language)))
            {
                Console.WriteLine("Incorrect number.\nPlease try again:");
            }

            return selectedPhrase;
        }

        private static Language SelectLanguage()
        {
            Console.WriteLine("\nPlease select a language:");
            Language selectedLanguage;
            while (!Enum.TryParse(Console.ReadLine(), true, out selectedLanguage) || !(selectedLanguage > 0 && (int)selectedLanguage <= Enum.GetValues(typeof(Language)).Length))
            {
                Console.WriteLine("Incorrect language.\nPlease try again:");
            }

            return selectedLanguage;
        }

        private static void PrintLanguages()
        {
            Console.WriteLine("There are the following languages available:");
            var enumType = typeof(Language);
            foreach (var language in Enum.GetNames(enumType))
            {
                var description = (DescriptionAttribute) Attribute.GetCustomAttribute(enumType.GetMember(language)[0], typeof(DescriptionAttribute));
                Console.Write(language);

                if (description?.Description != null)
                {
                    Console.Write(" [{0}]",description.Description);
                }

                Console.WriteLine();
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
