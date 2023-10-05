namespace Dictionary;

public class DictionaryUI
{
    private readonly Dictionary<string, Action> _menuEditOptions = new();
    private readonly Config _config;
    private int _dictionaryChoice;

    public DictionaryUI(Config config, int dictionaryChoice)
    {
        _config = config;
        _dictionaryChoice = dictionaryChoice;
        ConfigMenuOptions();
        Ui();
    }

    public DictionaryUI(Config config)
    {
        _config = config;
        CreateDictionary();
    }

    private void ConfigMenuOptions()
    {
        _menuEditOptions.Add("Add a word to dictionary", AddDictionaryWord);
        _menuEditOptions.Add("Edit word in dictionary", EditDictionaryWord);
        _menuEditOptions.Add("Remove word in dictionary", RemoveDictionaryWord);
    }

    private void Ui()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose mode:");
            Console.WriteLine("1. Back");
            Console.WriteLine("2. Search");
            Console.WriteLine("3. Edit");

            if (!int.TryParse(Console.ReadLine(), out var choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid choice.");
                continue;
            }

            if (choice == 1) break;

            switch (choice)
            {
                case 2:
                    while (true)
                    {
                        Console.Clear();
                        SearchWord();
                        Console.WriteLine();
                        Console.WriteLine("1. Back");
                        Console.WriteLine("2. Search word");
                        choice = int.Parse(Console.ReadLine());

                        if (choice == 1) break;
                    }

                    break;

                case 3:
                    Console.Clear();

                    Console.WriteLine("0. Back");
                    for (var i = 1; i < _menuEditOptions.Count + 1; i++)
                    {
                        Console.WriteLine($"{i}. {_menuEditOptions.Keys.ElementAt(i - 1)}");
                    }

                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid choice.");
                        continue;
                    }

                    if (choice == 0) break;

                    if (_menuEditOptions.TryGetValue(_menuEditOptions.Keys.ElementAt(choice - 1),
                            out var selectedOption))
                    {
                        selectedOption.Invoke();
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                    }

                    break;

                default:
                    Console.WriteLine("Enter a valid choice");
                    break;
            }
        }
    }

    private void CreateDictionary()
    {
        Console.Clear();
        Console.WriteLine("Enter dictionary language");
        var fromTranslation = Console.ReadLine();

        Console.WriteLine("Enter dictionary language to translate to");
        var toTranslation = Console.ReadLine();

        if (fromTranslation != null && toTranslation != null)
        {
            try
            {
                _config.Dictionaries.Add($"{fromTranslation}-{toTranslation}",
                    new DictionaryService(_config.CurrentDir, fromTranslation, toTranslation));
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(
                    $"Dictionary language can't be the same as the language translating to: {argumentException.Message}");
                throw;
            }
            catch (IOException ioException)
            {
                Console.WriteLine(ioException.Message);
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        else
        {
            Console.WriteLine("Dictionary language or the language translating to can't be empty");
        }
    }

    private void SearchWord()
    {
        Console.WriteLine($"Search word in {_config.LoadedDictionariesNames[_dictionaryChoice]}");

        var searchedWord = Console.ReadLine();
        var definition = _config.Dictionaries[_config.LoadedDictionariesNames[_dictionaryChoice]][searchedWord];

        Console.WriteLine(definition.TranslationWord);

        for (var i = 0; i < definition.Definitions.Count; i++)
        {
            Console.WriteLine($"{definition.Definitions[i]} [{i}]");
        }
    }

    private void AddDictionaryWord()
    {
        Console.Clear();
        var word = UserInput.Prompt(
            $"Enter word to add to {_config.LoadedDictionariesNames[_dictionaryChoice]}: ");
        var translation = UserInput.Prompt("Enter translation: ");

        Console.Write("How many definitions does the word have: ");
        var definitionsCount = int.Parse(Console.ReadLine());

        var definitions = new List<string>();

        for (var i = 0; i < definitionsCount; i++)
        {
            var item = UserInput.Prompt($"Enter definition [{i}]: ");
            definitions.Add(item);
        }

        _config.Dictionaries[_config.LoadedDictionariesNames[_dictionaryChoice]]
            .AddWordToDictionary(word, new Translation(translation, definitions));
    }

    private void EditDictionaryWord()
    {
        Console.Clear();
        var oldWord =
            UserInput.Prompt($"Enter word to edit in {_config.LoadedDictionariesNames[_dictionaryChoice]}: ");
        var newWord = UserInput.Prompt($"Enter new word to replace: ");

        var translation = UserInput.Prompt("Enter new translation: ");

        Console.Write("How many definitions does the word have: ");
        var definitionsCount = int.Parse(Console.ReadLine());

        var definitions = new List<string>();

        for (var i = 0; i < definitionsCount; i++)
        {
            var item = UserInput.Prompt($"Enter definition [{i}]: ");
            definitions.Add(item);
        }

        _config.Dictionaries[_config.LoadedDictionariesNames[_dictionaryChoice]].EditDictionary(oldWord, newWord,
            new Translation(translation, definitions));
    }

    private void RemoveDictionaryWord()
    {
        Console.Clear();
        var word = UserInput.Prompt(
            $"Enter word to delete in {_config.LoadedDictionariesNames[_dictionaryChoice]}: ");
        _config.Dictionaries[_config.LoadedDictionariesNames[_dictionaryChoice]].DeleteWordFromDictionary(word);
    }
}