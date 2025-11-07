using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PissQuiz
{
    public class QuestionModel
    {
        public string Header { get; set; }
        public string QuestionText { get; set; }
        public List<ChoiceModel> Options { get; set; }
    }

    public class ChoiceModel
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
    public partial class CreateQuiz : Window
    {
        AppManager appManager;
        List<QuestionModel> questions = new List<QuestionModel>();

        public CreateQuiz(AppManager manager)
        {
            InitializeComponent();
            appManager = manager;

            QuestionPanels.ItemsSource = questions;
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {

            QuestionModel q = new QuestionModel();
            q.Header = "Fråga " + (questions.Count + 1);
            q.Options = new List<ChoiceModel>();

            for (int i = 0; i < 4; i++)
            {
                q.Options.Add(new ChoiceModel());
            }

            questions.Add(q);
            UpdateHeaders();
            QuestionPanels.Items.Refresh();
        }

        private void RemoveQuestion_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            QuestionModel q = btn.DataContext as QuestionModel;

            if (q != null)
            {
                questions.Remove(q);
                UpdateHeaders();
                QuestionPanels.Items.Refresh();
            }
        }

        private void UpdateHeaders()
        {
            for (int i = 0; i < questions.Count; i++)
            {
                questions[i].Header = "Fråga " + (i + 1);
            }
        }

        private void SaveQuiz_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;

            if (title == "")
            {
                MessageBox.Show("Skriv in en titel!");
                return;
            }

            if (questions.Count == 0)
            {
                MessageBox.Show("Lägg till minst en fråga!");
                return;
            }

            Quiz quiz = new Quiz();
            quiz.Title = title;

            foreach (QuestionModel q in questions)
            {
                if (string.IsNullOrWhiteSpace(q.QuestionText))
                {
                    MessageBox.Show(q.Header + ": Du måste skriva en fråga!");
                    return;
                }

                Question question = new Question();
                question.Text = q.QuestionText;

                bool hasCorrect = false;

                for (int i = 0; i < q.Options.Count; i++)
                {
                    string text = q.Options[i].Text;
                    if (text == null) text = "";
                    question.Options.Add(text);

                    if (q.Options[i].IsCorrect)
                    {
                        question.correctAnswer = i;
                        hasCorrect = true;
                    }
                }

                if (!hasCorrect)
                {
                    MessageBox.Show(q.Header + ": Du måste välja vilket svar som är rätt!");
                    return;
                }

                quiz.Questions.Add(question);
            }

            appManager.SaveQuiz(quiz);
            MessageBox.Show("Quiz \"" + quiz.Title + "\" sparat!");
            Close();
        }
    }
 
}
