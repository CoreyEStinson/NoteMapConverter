using System.Text;

namespace ConsoleApp1;

public class Program
{
    public static void Main(string[] args)
    {
        string path = "data/notes.csv";
        if (!File.Exists(path))
        {
            Console.WriteLine($"File note found at {path}");
            return;
        }

        List<Note> notes = new List<Note>();
        StreamReader sr = new StreamReader(path);
        string header = sr.ReadLine()!;

        string? line; 
        while ((line = sr.ReadLine()) != null)
        {
            List<string> fields = SplitCsvLine(line);
            if (fields.Count < 7) continue;

            notes.Add(new Note(fields[0], fields[1], fields[2], fields[3], fields[4],
                fields[5], fields[6]));
        }

        foreach (Note n in notes)
        {
            Console.WriteLine($"");
        }


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
}