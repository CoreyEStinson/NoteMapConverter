using System.Globalization;
using System.Text;
using System.Text.Json;

namespace ConsoleApp1;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter the file's path: ");
        string path = Console.ReadLine()!;
        if (!File.Exists(path))
        {
            Console.WriteLine($"File note not found at {path}");
            return;
        }

        List<Note> notes = new List<Note>();
        StreamReader sr = new StreamReader(path);
        string header = sr.ReadLine()!;

        int lineNumber = 1;
        string? line; 
        while ((line = sr.ReadLine()) != null)
        {
            lineNumber++;
            List<string> fields = SplitCsvLine(line);
            if (fields.Count < 7)
            {
                Console.WriteLine($"Skipping line {lineNumber}: not enough fields ({fields.Count}).");
                continue;
            }

            if (TryParseNote(fields, out Note? note, out string? err))
            {
                if (note != null) notes.Add(note);
            }
            else
            {
                Console.WriteLine($"Skipping line {lineNumber}: {err}");
            }
        }

        foreach (Note n in notes)
        {
            Console.WriteLine(n);
        }

        WriteToJson(notes);
    }

    private static List<string> SplitCsvLine(string line)
    {
        List<string> result = new List<string>();
        StringBuilder cur = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    cur.Append('"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(cur.ToString());
                cur.Clear();
            }
            else
            {
                cur.Append(c);
            }
        }

        result.Add(cur.ToString());

        return result;
    }

    public static void WriteToJson(List<Note> notes)
    {
        try
        {
            // Use a relative filename but show the absolute location so you can find it
            string fileName = "notes.json";
            string fullPath = Path.GetFullPath(fileName);

            Console.WriteLine($"Writing JSON to: {fullPath}");
            Console.WriteLine($"Notes to write: {notes?.Count ?? 0}");

            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(notes, options);

            File.WriteAllText(fullPath, json);

            var info = new FileInfo(fullPath);
            Console.WriteLine($"JSON file written. Exists={info.Exists}, Size={info.Length} bytes");
        }
        catch (Exception e)
        {
            // Print the full exception so you can see permission or path issues
            Console.WriteLine("Failed to write JSON: " + e);
        }
    }

    private static bool TryParseNote(List<string> f, out Note? note, out string? error)
    {
        note = null;
        error = null;

        var ci = CultureInfo.InvariantCulture;

        // Check if the timing is a valid double
        if (!double.TryParse(f[0], NumberStyles.Float | NumberStyles.AllowThousands, ci, out double timing))
        {
            error = "invalid timing";
            return false;
        }
        if (timing < 0)
        {
            error = "timing must be >= 0";
            return false;
        }

        if (!(f[1].Equals("Single", StringComparison.OrdinalIgnoreCase) || 
            f[1].Equals("Hold", StringComparison.OrdinalIgnoreCase)))
        {
            error = "invalid note type - must be \"Single\" or \"Hold\"";
            return false;
        }

        if (!(f[2].Equals("Pink", StringComparison.OrdinalIgnoreCase) ||
            f[2].Equals("Blue", StringComparison.OrdinalIgnoreCase) ||
            f[2].Equals("Yellow", StringComparison.OrdinalIgnoreCase)))
        {
            error = "invalid note color - must be \"Pink\", \"Blue\", or \"Yellow\"";
            return false;
        }

        if (!double.TryParse(f[3], NumberStyles.Float | NumberStyles.AllowThousands, ci, out double size))
        {
            error = "invalid size";
            return false;
        }
        if (size <= 0)
        {
            error = "size must be > 0";
            return false;
        }

        if (!double.TryParse(f[4], NumberStyles.Float | NumberStyles.AllowThousands, ci, out double lengthOfNote))
        {
            error = "invalid length of note";
            return false;
        }
        if (lengthOfNote < 0)
        {
            error = "length of note must be >= 0";
            return false;
        }

        if (!double.TryParse(f[5], NumberStyles.Float | NumberStyles.AllowThousands, ci, out double xPos))
        {
            error = "invalid X position";
            return false;
        }
        if (double.Parse(f[5]) > 1 || double.Parse(f[5]) < 0)
        {
            error = "invalid X position - must be between 0 and 1";
            return false;
        }

        if (!double.TryParse(f[6], NumberStyles.Float | NumberStyles.AllowThousands, ci, out double yPos))
        {
            error = "invalid Y position";
            return false;
        }
        if (double.Parse(f[6]) > 1 || double.Parse(f[6]) < 0)
        {
            error = "invalid Y position - must be between 0 and 1";
            return false;
        }

        note = new Note(f[0], f[1], f[2], f[3], f[4], f[5], f[6]);


        return true;
    }
}