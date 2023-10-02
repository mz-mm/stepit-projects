using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;

namespace Dictionary;

public class DictionaryService
{
    private readonly CsvConfiguration _cvsConfig = new(CultureInfo.CurrentCulture) { HasHeaderRecord = false };
    private readonly SortedDictionary<string, Translation> _wordDictionary = new();
    private readonly string _path;


    // Create a word dictionary
    public DictionaryService(string currentDir, string fromTranslation, string toTranslation)
    {
        if (fromTranslation == toTranslation)
            throw new ArgumentException("fromTranslation and toTranslation should not be the same");

        _path = currentDir + $"{fromTranslation.ToLower()}-{toTranslation.ToLower()}.csv";
        using var fileStream = new FileStream(_path, FileMode.CreateNew);
    }


    // Load a ready word dictionary
    public DictionaryService(string path)
    {
        _path = path;

        using var streamReader = File.OpenText(_path);
        using var csvReader = new CsvReader(streamReader, _cvsConfig);

        // Turns each row in the csv file into a WordDictionary objects and converts that into a list
        foreach (var dict in csvReader.GetRecords<WordDictionary>().ToList())
        {
            _wordDictionary.Add(dict.Word, new Translation(dict.TranslatedWord, dict.Definition.Split(";").ToList()));
        }
    }


    public void AddWordToDictionary(string word, Translation translation)
    {
        _wordDictionary.Add(word, translation);
        RewriteDictionary(_wordDictionary);
    }


    public void EditDictionary(string oldWord, string newWord, Translation translation)
    {
        _wordDictionary.Remove(oldWord);
        _wordDictionary.Add(newWord, translation);
        RewriteDictionary(_wordDictionary);
    }


    public void DeleteWordFromDictionary(string word)
    {
        _wordDictionary.Remove(word);
        RewriteDictionary(_wordDictionary);
    }


    // Rewrite the dictionary .csv file
    private void RewriteDictionary(SortedDictionary<string, Translation> dictionary)
    {
        using var fileStream = new FileStream(_path, FileMode.Create);
        using var streamWriter = new StreamWriter(fileStream);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.CurrentCulture);

        foreach (var dict in dictionary)
        {
            csvWriter.WriteField(dict.Key);
            csvWriter.WriteField(dict.Value.TranslationWord);
            csvWriter.WriteField(string.Join(";", dict.Value.Definitions));
            csvWriter.NextRecord();
        }
    }


    // Indexer to access words by word index
    public Translation this[string word] => _wordDictionary[word];
}