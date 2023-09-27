using Dictionary;

// TODO: crud for words (add, read, remove, update)
// TODO: check how dictionaries do the translation and etc to fix english-russian.json
// TODO: the application must be more loosely coupled, meaning the ability to work with different types of WordDictionary classes that may have different implementation

var dictionaries = new Dictionary<string, WordDictionary>();
dictionaries.Add("EngRus", new WordDictionary("english-russian.json"));
dictionaries.Add("EngRus", new WordDictionary("russian", "english"));
