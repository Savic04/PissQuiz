namespace PissQuiz;

public class Question
{
    public string Text { get; set; }
    public List<string> Options { get; set; }
    public int correctAnswer { get; set; }

    public Question()
    {
        Options = new List<string>();
    }
}