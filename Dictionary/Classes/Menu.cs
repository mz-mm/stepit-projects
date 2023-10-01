using System.Collections;

namespace Dictionary;

public class Menu
{
    private readonly string _currentDir;
    private SortedDictionary<string, DictionaryService> _dictionaries;
    private string[] _files;
    private List<string> _loadedDictionaries = new();
    
    public Menu(string currentDir, ref SortedDictionary<string, DictionaryService> dictionaries)
    {
        _currentDir = currentDir;
        _dictionaries = dictionaries;
        DictionaryLoader();
    }

    private void DictionaryLoader()
    {
        _files = Directory.GetFiles(_currentDir);

        foreach (var file in _files)
        {
            _dictionaries.Add(Path.GetFileNameWithoutExtension(file), new DictionaryService(file));
            _loadedDictionaries.Add(Path.GetFileNameWithoutExtension(file));
        }
    }
}