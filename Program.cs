using System.IO;

namespace MorseCode
{
    public class MorseCodeTranslator
    {
        private static readonly Dictionary<char, string> charToMorse = new Dictionary<char, string>()
    {
      {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."},
      {'F', "..-."}, {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"},
      {'K', "-.-"}, {'L', ".-.."}, {'M', "--"}, {'N', "-."}, {'O', "---"},
      {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."}, {'S', "..."}, {'T', "-"},
      {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"}, {'Y', "-.--"},
      {'Z', "--.."}, {'0', "-----"}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"},
      {'4', "....-"}, {'5', "....."}, {'6', "-...."}, {'7', "--..."}, {'8', "---.."},
      {'9', "----."}

    };

        public static string TextToMorse(string text)
        {
            string morseCode = "";

            foreach (char character in text.ToUpper())
            {
                if (charToMorse.TryGetValue(character, out string morseChar))
                {
                    morseCode += morseChar + " "; //Append Morse Code for the cahracter
                }
                else if (character == ' ')
                {
                    morseCode += "  ";
                }
                else
                {
                    throw new CharacterNotInDictionaryException(character);
                }
            }

            return morseCode.Trim();
        }
    }

    public class CharacterNotInDictionaryException : Exception
    {
        public char Character { get; private set; }

        public CharacterNotInDictionaryException(char character) : base($"Character '{character}' is not in the Morse code dictionary.")
        {
            Character = character;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Morse Code Translator");
            Console.WriteLine("Enter a random English text.");
            string userInput = Console.ReadLine();

            try
            {
                string MorseResult = MorseCodeTranslator.TextToMorse(userInput);
                Console.WriteLine("Morse Code: " + MorseResult);

                string filepath = "morse_output.txt";

                File.WriteAllText(filepath, MorseResult);

                Console.WriteLine($"Morse Code exported to {filepath}");
            }
            catch (CharacterNotInDictionaryException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}