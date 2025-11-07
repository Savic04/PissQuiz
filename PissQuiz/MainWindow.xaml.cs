using System.Windows;


namespace PissQuiz;


public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainContentArea.Content = new PissQuiz.MainMenu();
    }
}
