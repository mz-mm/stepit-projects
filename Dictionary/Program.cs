using Dictionary;

const string currentDir = "/Users/muhammedmammadhuseynov/Developer/stepit-projects/Dictionary/Data/";
var dictionaries = new SortedDictionary<string, DictionaryService>();

var menu = new Menu(currentDir, ref dictionaries);
menu.Run();