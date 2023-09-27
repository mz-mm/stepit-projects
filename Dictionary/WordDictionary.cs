namespace Dictionary;

using System.Text.Json;

public class WordDictionary
{
    public string from;
    public string to;
    public Transilation[] Transilations { get; set; }

    // Create a word dictionary
    // TODO: Check if the file with this name already exists
    // TODO: Check if the language that we are translating from and to are not the same, should not be able to do a english to english dictionary
    public WordDictionary(string fromTranslation, string toTranslation)
    {
        try
        {
            var fileStream = new FileStream($"{from.ToLower()}-{to.ToLower()}.json", FileMode.Create);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    // Load a ready word dictionary
    public WordDictionary(string path)
    {
        if (File.Exists(path))
        {
            try
            {
                using (var fileStream = new FileStream(path, FileMode.Open))
                {
                    // TODO: load dictionary from files through constructor
                    var WordDictFile = JsonSerializer.Deserialize<WordDictionary>(fileStream);
                    Console.WriteLine(WordDictFile);
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Error deserializing JSON: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("File not found: " + path);
        }
    }

}