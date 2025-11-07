
namespace PissQuiz;

public class Quiz
{
    public string Title { get; set; }
    public List<Question> Questions { get; set; }

    public Quiz()
    {
        Questions = new List<Question>();
    }
}
