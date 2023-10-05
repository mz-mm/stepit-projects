using Dictionary;

var config = new Config(
    currentDir: "../../../Data/", 
    dictionaries: new SortedDictionary<string, DictionaryService>()
    );

config.LoadDictionaries();

var menu = new Menu(config);

menu.MenuUi();