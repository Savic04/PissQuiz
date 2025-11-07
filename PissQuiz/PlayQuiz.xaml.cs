using System.Windows;
using System.Windows.Controls;

namespace PissQuiz
{
    public partial class PlayQuiz : Window
    {
        public AppManager appManager;
        public Quiz currentQuiz;
        public int Questions = 0;
        public int correctAnswers = 0;
        public int totalanswers = -1;

        public PlayQuiz(AppManager manager, Quiz quiz)
        {
            InitializeComponent();
            appManager = manager;
            currentQuiz = quiz;
            ShowQuestion();
        }

        private void ShowQuestion()
        {
            if (Questions >= currentQuiz.Questions.Count)
            {
                MessageBox.Show($" Du fick: {correctAnswers}/{currentQuiz.Questions.Count} rätt.");
                Close();
                return;
            }

            var q = currentQuiz.Questions[Questions];
            QuizTitle.Text = currentQuiz.Title;
            QuestionText.Text = q.Text;

            AnswerOptions.Children.Clear();
            totalanswers = -1;

            for (int i = 0; i < q.Options.Count; i++)
            {
                var rb = new RadioButton
                {
                    Content = q.Options[i],
                    GroupName = "Answers",
                    Margin = new Thickness(0, 5, 0, 5),
                    Tag = i
                };
                rb.Checked += (s, e) => totalanswers = (int)((RadioButton)s).Tag;
                AnswerOptions.Children.Add(rb);
            }

            SubmitButton.Visibility = Visibility.Visible;
            NextButton.Visibility = Visibility.Collapsed;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (totalanswers == -1)
            {
                MessageBox.Show("Välj ett alternativ först!");
                return;
            }

            var q = currentQuiz.Questions[Questions];
            if (totalanswers == q.correctAnswer)
            {
                MessageBox.Show("Rätt svar");
                correctAnswers++;
            }
            else
            {
                MessageBox.Show($"Fel svar");
            }

            SubmitButton.Visibility = Visibility.Collapsed;
            NextButton.Visibility = Visibility.Visible;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            Questions++;
            ShowQuestion();
        }
    }
}


