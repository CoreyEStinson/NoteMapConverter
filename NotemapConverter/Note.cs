namespace ConsoleApp1;

public class Note
{
    public double Timing { get; set; }
    public int NoteType { get; set; }
    public int NoteColor { get; set; }
    public double Size { get; set; }
    public double LengthOfNote { get; set; }
    public double XPos { get; set; }
    public double YPos { get; set; }

    public Note (string timing, string noteType, string noteColor, string size, 
        string lengthOfNote, string xPos, string yPos) {
        Timing = double.Parse(timing);
        NoteType = int.Parse(noteType);
        NoteColor = int.Parse(noteColor);
        Size = double.Parse(size);
        LengthOfNote = double.Parse(lengthOfNote);
        XPos = double.Parse(xPos);
        YPos = double.Parse(yPos);
    }
    
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
}
