namespace ConsoleApp1;

public class Note
{
    // Timing (seconds or beats depending on source)
    public double Timing { get; set; }
    // Note type: 0 = Single, 1 = Hold
    public int NoteType { get; set; }
    // Note color: 0 = Pink, 1 = Blue, 2 = Yellow
    public int NoteColor { get; set; }
    // Visual size of the note
    public double Size { get; set; }
    // Duration/length for hold notes
    public double LengthOfNote { get; set; }
    // X and Y positions for the note
    public double XPos { get; set; }
    public double YPos { get; set; }

    // Constructor from CSV fields (all provided as strings).
    // This constructor converts textual note type and color values into the
    // integer encoding used by the rest of the application.
    public Note (string timing, string noteType, string noteColor, string size, 
        string lengthOfNote, string xPos, string yPos) {
        // Parse numeric fields with the default double/int parsers
        Timing = double.Parse(timing);

        // Convert note type names to integers.
        // Mapping: "Single" -> 0, "Hold" -> 1
        // If the value is already numeric, attempt to parse it as an int.
        string nt = noteType?.Trim() ?? string.Empty;
        if (nt.Equals("Single", System.StringComparison.OrdinalIgnoreCase))
        {
            NoteType = 0;
        }
        else if (nt.Equals("Hold", System.StringComparison.OrdinalIgnoreCase))
        {
            NoteType = 1;
        }
        else if (!int.TryParse(nt, out int parsedNt))
        {
            // Fallback default
            NoteType = 0;
        }
        else
        {
            NoteType = parsedNt;
        }

        // Convert note color names to integers.
        // Mapping: "Pink" -> 0, "Blue" -> 1, "Yellow" -> 2
        // If the value is already numeric, attempt to parse it as an int.
        string nc = noteColor?.Trim() ?? string.Empty;
        if (nc.Equals("Pink", System.StringComparison.OrdinalIgnoreCase))
        {
            NoteColor = 0;
        }
        else if (nc.Equals("Blue", System.StringComparison.OrdinalIgnoreCase))
        {
            NoteColor = 1;
        }
        else if (nc.Equals("Yellow", System.StringComparison.OrdinalIgnoreCase))
        {
            NoteColor = 2;
        }
        else if (!int.TryParse(nc, out int parsedNc))
        {
            // Fallback default
            NoteColor = 0;
        }
        else
        {
            NoteColor = parsedNc;
        }

        Size = double.Parse(size);
        LengthOfNote = double.Parse(lengthOfNote);
        XPos = double.Parse(xPos);
        YPos = double.Parse(yPos);
    }
    
    // Parameterless-ish constructor used for convenience; initializes defaults.
    public Note(string v)
    {
        Timing = 0;
        NoteType = 0;
        NoteColor = 0;
        Size = 1;
        LengthOfNote = 0;
        XPos = 0;
        YPos = 0;
    }

    // Provide a readable representation of the note for debugging/output.
    public override string ToString()
    {
        return $"Timing: {Timing}, Note Type: {NoteType}, Note Color: {NoteColor}, Size: {Size}, Length Of Note: {LengthOfNote}, X Pos: {XPos}, Y Pos: {YPos}";
    }
}
