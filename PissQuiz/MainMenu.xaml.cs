using System.Windows;
using System.Windows.Controls;

namespace PissQuiz
{
    public partial class MainMenu : UserControl
    {
        private AppManager manager;

        public MainMenu()
        {
            InitializeComponent();
            manager = new AppManager(); 
        }


        private void PlayButtonClick(object sender, RoutedEventArgs e)
        {

            var files = manager.GetAllQuizFileNames();
            if (files.Count == 0)
            {
                MessageBox.Show("Inga quiz hittades. Skapar standardquiz.");
                return;
            }
            string firstQuizFile = files[0]; 
            Quiz quiz = manager.LoadQuiz(firstQuizFile); 

            if (quiz != null)
            {
                manager.StartQuiz(quiz);

                PlayQuiz window = new PlayQuiz(manager, quiz);
                window.Show();
            }
            else
            {
                MessageBox.Show("Kunde inte starta quiz.");
            }
        }




        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            CreateQuiz createWindow = new CreateQuiz(manager);
            createWindow.ShowDialog();
        }


        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            EditQuiz editWindow = new EditQuiz(manager);
            editWindow.ShowDialog();
        }
    }
}
