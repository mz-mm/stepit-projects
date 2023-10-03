namespace Dictionary;

public class Menu
{
    private Config _config;
    private  DictionaryUI _dictionaryUi;

    public Menu(Config config)
    {
        _config = config;
    }

    public void MenuUi()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Select Dictionary: ");

            for (var i = 0; i < _config.LoadedDictionariesNames.Count; i++)
            {
                Console.WriteLine($"{i}. {_config.LoadedDictionariesNames[i]}");
            }

            Console.WriteLine($"{_config.LoadedDictionariesNames.Count}. Create new dictionary");
            Console.WriteLine($"{_config.LoadedDictionariesNames.Count + 1}. Quit");


            if (!int.TryParse(Console.ReadLine(), out var choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid choice.");
                continue;
            }

            if (choice == _config.LoadedDictionariesNames.Count)
            {
                _dictionaryUi = new DictionaryUI(_config);
            }

            else if (choice == _config.LoadedDictionariesNames.Count + 1)
            {
                break;
            }

            else if (0 <= choice && choice < _config.LoadedDictionariesNames.Count)
            {
                _dictionaryUi = new DictionaryUI(_config, choice);
            }
            else
            {
                Console.WriteLine("Enter a valid choice");
            }
        }
    }
}