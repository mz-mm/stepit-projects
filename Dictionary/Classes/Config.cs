namespace Dictionary;

public class Config
{
    public readonly string CurrentDir;
    public readonly List<string> LoadedDictionariesNames = new();
    private string[] _files;
    public readonly SortedDictionary<string, DictionaryService> Dictionaries;

    public Config(string currentDir, SortedDictionary<string, DictionaryService> dictionaries)
    {
        CurrentDir = currentDir;
        Dictionaries = dictionaries;
    }
    
    public void LoadDictionaries()
    {
        _files = Directory.GetFiles(CurrentDir);

        foreach (var file in _files)
        {
            Dictionaries.Add(Path.GetFileNameWithoutExtension(file), new DictionaryService(file));
            LoadedDictionariesNames.Add(Path.GetFileNameWithoutExtension(file));
        }
    }
}