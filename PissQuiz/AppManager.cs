using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PissQuiz
{
    public class AppManager
    {
        private static readonly string QuizFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "PissQuizSaves", "Quiz"
        );

        private Quiz currentQuiz;
        private List<Question> questions;
        private int score;
        private int correctCount;

        public AppManager()
        {
            if (!Directory.Exists(QuizFolder))
                Directory.CreateDirectory(QuizFolder);
        }

        public void SaveQuiz(Quiz quiz)
        {
            string safeName = string.Join("_", quiz.Title.Split(Path.GetInvalidFileNameChars()));
            string path = Path.Combine(QuizFolder, safeName + ".json");
            string json = JsonSerializer.Serialize(quiz, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }


        public Quiz LoadQuiz(string fileName)
        {
            string path = Path.Combine(QuizFolder, fileName);
            if (!File.Exists(path))
                return null!;

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Quiz>(json)!;
        }

        public List<string> GetAllQuizFileNames()
        {
            List<string> files = new List<string>();
            if (Directory.Exists(QuizFolder))
            {
                foreach (var f in Directory.GetFiles(QuizFolder, "*.json"))
                    files.Add(Path.GetFileName(f));
            }
            return files;
        }

        public void StartQuiz(Quiz quiz)
        {
            currentQuiz = quiz;
            questions = new List<Question>(quiz.Questions);
            score = 0;
            correctCount = 0;

        }
         
        public Question CurrentQuestion
        {
            get
            {
                if (questions == null || score >= questions.Count)
                    return null;
                return questions[score];
            }
        }

        public bool SubmitAnswer(int selected)
        {
            if (CurrentQuestion == null)
                return false;

            bool correct = CurrentQuestion.correctAnswer == selected;
            if (correct) correctCount++;
            score++;
            return correct;
        }

        public bool IsQuizDone()
        {
            return questions != null && score >= questions.Count;
        }

        public int CorrectAnswers
        {
            get { return correctCount; }
        }

        public int TotalQuestions
        {
            get { return questions != null ? questions.Count : 0; }
        }
    }
}
