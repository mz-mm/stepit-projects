namespace Dictionary;

public static class UserInput
{
    private static string? _userInput;
    public static string Prompt(string message)
    {
        do
        {
            Console.Clear();
            Console.Write(message);
            _userInput = Console.ReadLine();

            if (_userInput == null)
            {
                Console.WriteLine("Input cannot be null.");
            }
        } while (_userInput == null);

        return _userInput;
    } 
}