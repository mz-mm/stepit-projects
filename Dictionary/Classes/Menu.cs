namespace Dictionary;

public class Menu
{
    private readonly string _currentDir;
    private readonly SortedDictionary<string, DictionaryService> _dictionaries;
    private string[] _files;
    private readonly List<string> _loadedDictionaries = new();

    public Menu(string currentDir, ref SortedDictionary<string, DictionaryService> dictionaries)
    {
        _currentDir = currentDir;
        _dictionaries = dictionaries;
        LoadDictionaries();
        MenuUi();
    }

    private void LoadDictionaries()
    {
        _files = Directory.GetFiles(_currentDir);

        foreach (var file in _files)
        {
            _dictionaries.Add(Path.GetFileNameWithoutExtension(file), new DictionaryService(file));
            _loadedDictionaries.Add(Path.GetFileNameWithoutExtension(file));
        }
    }

    private void MenuUi()
    {
        while (true)
        {
            Console.Clear();
            int choice;
            Console.WriteLine("Select Dictionary: ");

            for (var i = 0; i < _loadedDictionaries.Count; i++)
            {
                Console.WriteLine($"{i}. {_loadedDictionaries[i]}");
            }

            Console.WriteLine($"{_loadedDictionaries.Count}. Create new dictionary");
            Console.WriteLine($"{_loadedDictionaries.Count + 1}. Quit");


            try
            {
                choice = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            }
            catch (InvalidOperationException invalidOperationException)
            {
                Console.WriteLine(invalidOperationException);
                throw;
            }

            if (choice == _loadedDictionaries.Count)
            {
                UiAddDictionary();
            }

            else if (choice == _loadedDictionaries.Count + 1)
            {
                break;
            }

            else if (0 <= choice && choice < _loadedDictionaries.Count)
            {
                UiDictionary(choice);
            }
            else
            {
                Console.WriteLine("Enter a valid choice");
            }
        }
    }

    private void UiDictionary(int dictionaryChoice)
    {
        while (true)
        {
            Console.Clear();
            int choice;
            Console.WriteLine("Choose mode:");
            Console.WriteLine("1. Back");
            Console.WriteLine("2. Search");
            Console.WriteLine("3. Edit");

            try
            {
                choice = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            }
            catch (InvalidOperationException invalidOperationException)
            {
                Console.WriteLine(invalidOperationException);
                throw;
            }

            if (choice == 1) break;

            switch (choice)
            {
                case 2:
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"Search word in {_loadedDictionaries[dictionaryChoice]}");

                        var searchedWord = Console.ReadLine();
                        var definition = _dictionaries[_loadedDictionaries[dictionaryChoice]][searchedWord];
                        
                        Console.WriteLine(definition.TranslationWord);

                        for (var i = 0; i < definition.Definitions.Count; i++)
                        {
                            Console.WriteLine($"{definition.Definitions[i]} [{i}]");
                        }

                        Console.WriteLine();
                        Console.WriteLine("1. Back");
                        Console.WriteLine("2. Search word");
                        choice = int.Parse(Console.ReadLine());

                        if (choice == 1) break;
                    }

                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("1. Back");
                    Console.WriteLine("2. Add a word to dictionary");
                    Console.WriteLine("3. Edit word in dictionary");
                    Console.WriteLine("4. Delete word in dictionary");

                    try
                    {
                        choice = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                    }
                    catch (InvalidOperationException invalidOperationException)
                    {
                        Console.WriteLine(invalidOperationException);
                        throw;
                    }

                    if (choice == 1) break;

                    switch (choice)
                    {
                        case 2:
                            Console.Clear();
                            var word = PromptUserInput(
                                $"Enter word to add to {_loadedDictionaries[dictionaryChoice]}: ");
                            var translation = PromptUserInput("Enter translation: ");

                            Console.Write("How many definitions does the word have: ");
                            var definitionsCount = int.Parse(Console.ReadLine());

                            var definitions = new List<string>();

                            for (var i = 0; i < definitionsCount; i++)
                            {
                                var item = PromptUserInput($"Enter definition [{i}]: ");
                                definitions.Add(item);
                            }

                            _dictionaries[_loadedDictionaries[dictionaryChoice]]
                                .AddWordToDictionary(word, new Translation(translation, definitions));
                            break;

                        case 3:
                            Console.Clear();
                            var oldWord =
                                PromptUserInput($"Enter word to edit in {_loadedDictionaries[dictionaryChoice]}: ");
                            var newWord = PromptUserInput($"Enter new word to replace: ");

                            translation = PromptUserInput("Enter new translation: ");

                            Console.Write("How many definitions does the word have: ");
                            definitionsCount = int.Parse(Console.ReadLine());

                            definitions = new List<string>();

                            for (var i = 0; i < definitionsCount; i++)
                            {
                                var item = PromptUserInput($"Enter definition [{i}]: ");
                                definitions.Add(item);
                            }

                            _dictionaries[_loadedDictionaries[dictionaryChoice]].EditDictionary(oldWord, newWord,
                                new Translation(translation, definitions));
                            break;

                        case 4:
                            Console.Clear();
                            word = PromptUserInput(
                                $"Enter word to delete in {_loadedDictionaries[dictionaryChoice]}: ");
                            _dictionaries[_loadedDictionaries[dictionaryChoice]].DeleteWordFromDictionary(word);
                            break;


                        default:
                            Console.WriteLine("Enter a valid choice");
                            break;
                    }

                    break;

                default:
                    Console.WriteLine("Enter a valid choice");
                    break;
            }
        }
    }

    private void UiAddDictionary()
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
                _dictionaries.Add($"{fromTranslation}-{toTranslation}",
                    new DictionaryService(_currentDir, fromTranslation, toTranslation));
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

    private static string PromptUserInput(string message)
    {
        string userInput;
        do
        {
            Console.Clear();
            Console.Write(message);
            userInput = Console.ReadLine();

            if (userInput == null)
            {
                Console.WriteLine("Input cannot be null.");
            }
        } while (userInput == null);

        return userInput;
    }
}