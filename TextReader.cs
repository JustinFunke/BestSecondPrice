//@Author = Justin Funke

namespace BestSecondPrice;

public class TextReader
{
    public static List<string> HoleSuchbegriffe()
    {
        var filePath = "Suchbegriffe.txt";

        var suchbegriffe = new List<string>(File.ReadAllLines(filePath));

        Console.WriteLine("Suchbegriffe geladen");
        Console.WriteLine($"Es wurden {suchbegriffe.Count} Suchbegriffe gefunden");
        Console.WriteLine($"================================================================================================================");

        return suchbegriffe;
    }
}