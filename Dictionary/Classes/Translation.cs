namespace Dictionary;

public class Translation
{
    public readonly string TranslationWord;
    public  List<string> Definitions { get; set; }

    public Translation(string translationWord, List<string> definitions)
    {
        TranslationWord = translationWord;
        Definitions = definitions;
    }
}