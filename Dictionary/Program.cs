using Dictionary;

var config = new Config(
    currentDir: "/Users/muhammedmammadhuseynov/Developer/stepit-projects/Dictionary/Data/", 
    dictionaries: new SortedDictionary<string, DictionaryService>()
    );

config.LoadDictionaries();

var menu = new Menu(config);

menu.MenuUi();