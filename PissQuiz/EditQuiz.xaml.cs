using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PissQuiz;

public partial class EditQuiz : Window
{
    private AppManager appManager;
    private Quiz currentQuiz;
    private List<string> quizFiles = new();

    public EditQuiz(AppManager manager)
    {
        InitializeComponent();
        appManager = manager;
        LoadQuizList();
    }

    private void LoadQuizList()
    {
        quizFiles = appManager.GetAllQuizFileNames();
        QuizListBox.ItemsSource = quizFiles;
    }

    private void QuizListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (QuizListBox.SelectedItem == null) return;

        string quizName = QuizListBox.SelectedItem.ToString()!;
        currentQuiz = appManager.LoadQuiz(quizName);

        if (currentQuiz == null)
        {
            MessageBox.Show("Kunde inte ladda quizet.");
            return;
        }

        TitleBox.Text = currentQuiz.Title;
        QuestionsList.ItemsSource = currentQuiz.Questions;
        QuestionsList.Items.Refresh();
    }

    private void AddQuestion_Click(object sender, RoutedEventArgs e)
    {
        if (currentQuiz == null)
        {
            MessageBox.Show("Välj ett quiz först!");
            return;
        }

        Question newQuestion = new Question
        {
            Text = "",
            Options = new List<string> { "", "", "", "" },
            correctAnswer = 0
        };
        currentQuiz.Questions.Add(newQuestion);
        QuestionsList.Items.Refresh();
    }

    private void RemoveQuestion_Click(object sender, RoutedEventArgs e)
    {
        if (QuestionsList.SelectedItem == null)
        {
            MessageBox.Show("Välj en fråga att ta bort!");
            return;
        }

        currentQuiz.Questions.Remove((Question)QuestionsList.SelectedItem);
        QuestionsList.Items.Refresh();
    }

    private void SaveQuiz_Click(object sender, RoutedEventArgs e)
    {
        if (currentQuiz == null)
        {
            MessageBox.Show("Inget quiz valt!");
            return;
        }

        currentQuiz.Title = TitleBox.Text;
        appManager.SaveQuiz(currentQuiz);

        MessageBox.Show($"Quiz \"{currentQuiz.Title}\" sparat!");
    }
}
