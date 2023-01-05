using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace Kviz
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region Úvod
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Vítejte v aplikaci Kvíz!");
            Console.WriteLine("Autor: Miloš Tesař C3b");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
            #endregion
            #region Původní List
            /*
            List<User> users = new List<User>
            {
                new User("Admin", "Admin1", true),
                new User("Milos", "Heslo1234"),
                new User("Solim", "Milos69",true )
            };
            string jsonString = JsonSerializer.Serialize(users);
            File.WriteAllText("users.json", jsonString);
            */
            #endregion            
            #region Braní uživatelů z user.json
            List<User> users = new List<User>();
            try
            {
                string jsonString = File.ReadAllText("users.json");
                users = JsonSerializer.Deserialize<List<User>>(jsonString);                
            }
            catch (System.NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion
            int input = 0;
            bool run = true;
            int strike = 0;
            int maxStrike = 3;
            #region Hlavní Menu
            while (run)
            {
                Console.WriteLine();
                Console.WriteLine("Hlavní MENU\n1 - Přihlašení\n2 - Exit");
                Console.Write("Vyberte možnost: ");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Zadaná Hodnota musí být integer!");
                    input = 0;
                }
                switch (input)
                {
                    case 1:
                        LogIn(users);
                        break;
                    case 2:
                        run = false;
                        break;
                    default:
                        Console.WriteLine();
                        strike++;
                        if (strike < maxStrike)
                        {
                            Console.WriteLine("Máte " + strike + " striků, jestli dosáhnete " + maxStrike + " striků aplikace se automaticky ukončí.");
                            Console.WriteLine("Vyberte celé číslo z nabídky..");
                        }
                        else
                        {
                            Console.WriteLine("Dosáhli jste " + strike + " striků. Aplikace se teď automaticky ukončí.");
                            run = false;
                        }
                        break;
                }
            
            }
            #endregion
            #region Konec
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Aplikace byla ukončena");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------------------");
            #endregion
        }

        static void LogIn(List<User> users)
        {
            bool run = true;
            int strike = 0;
            int maxStrike = 3;
            while (run)
            {
                Console.WriteLine();
                Console.Write("Zadejte své přihlašovací jméno: ");
                string username = Console.ReadLine();
                Console.Write("Zadejte své přihlašovací heslo: ");
                string password = Console.ReadLine();
                User user = users.FirstOrDefault(u => u.Name == username && u.Password == password);
                if (user != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Vitejte, " + user.Name);
                    Aplikace(user, users);
                    run = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Neplatné přihlašovací jméno nebo heslo.");
                    strike++;
                    if (strike < maxStrike)
                    {
                        Console.WriteLine("Máte " + strike + " striků, jestli dosáhnete " + maxStrike + " striků aplikace se automaticky ukončí.");
                        Console.WriteLine("Zkuste to prosím znovu.");
                    }
                    else
                    {
                        Console.WriteLine("Dosáhli jste " + strike + " striků. Aplikace se teď automaticky ukončí.");
                        run = false;
                    }
                }
            }
        }

        static void Aplikace(User user, List<User> users)
        {
            if (user.Admin)
            {
                AdminPanel(users);
            } 
            else if(!user.Admin) 
            {
                UserMenu();
            }
            Console.WriteLine();
            Console.WriteLine(user.Name+" byl odhlášen.");
        }

        static void AdminPanel(List<User> users)
        {
            List<string> kvizy = ImportNameQuizes();
            int input = 0;
            bool run = true;
            int strike = 0;
            int maxStrike = 3;
            while (run)
            {
                kvizy = ImportNameQuizes();
                Console.WriteLine();
                Console.WriteLine("Admin Panel\n1 - Hrát Kvíz\n2 - Správce Kvízů\n3 - Správce Uživatelů\n4 - Odhlásit se");
                Console.Write("Vyberte možnost: ");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Zadaná Hodnota musí být integer!");
                    input = 0;
                }
                switch (input)
                {
                    case 1:
                        Quiz kviz = ChooseQuiz(kvizy);
                        PlayQuiz(kviz);
                        break;
                    case 2:
                        QuizManager(kvizy);
                        break;
                    case 3:
                        UserManager(users);
                        break;
                    case 4:
                        run = false;
                        break;
                    default:
                        Console.WriteLine();
                        strike++;
                        if (strike < maxStrike)
                        {
                            Console.WriteLine("Máte " + strike + " striků, jestli dosáhnete " + maxStrike + " striků aplikace se automaticky ukončí.");
                            Console.WriteLine("Vyberte celé číslo z nabídky..");
                        }
                        else
                        {
                            Console.WriteLine("Dosáhli jste " + strike + " striků. Aplikace se teď automaticky ukončí.");
                            run = false;
                        }
                        break;
                }
            }

        }

        static void UserMenu() 
        {
            List<String> kvizy = ImportNameQuizes();
            int input = 0;
            bool run = true;
            int strike = 0;
            int maxStrike = 3;
            while (run)
            {
                kvizy = ImportNameQuizes();
                Console.WriteLine();
                Console.WriteLine("MENU\n1 - Hrát Kvíz\n2 - Odhlásit se");
                Console.Write("Vyberte možnost: ");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Zadaná Hodnota musí být integer!");
                    input = 0;
                }
                switch (input)
                {
                    case 1:
                        Quiz kviz = ChooseQuiz(kvizy);
                        PlayQuiz(kviz);
                        break;
                    case 2:
                        run = false;
                        break;
                    default:
                        Console.WriteLine();
                        strike++;
                        if (strike < maxStrike)
                        {
                            Console.WriteLine("Máte " + strike + " striků, jestli dosáhnete " + maxStrike + " striků aplikace se automaticky ukončí.");
                            Console.WriteLine("Vyberte celé číslo z nabídky..");
                        }
                        else
                        {
                            Console.WriteLine("Dosáhli jste " + strike + " striků. Aplikace se teď automaticky ukončí.");
                            run = false;
                        }
                        break;
                }
            }          
        }

        static void PlayQuiz(Quiz quiz)
        {
            int pocetSpravnych = 0;
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine();
            for (int i = 0;i < quiz.Questions.Count;i++) 
            { 
                int j = i + 1;
                int input = 0;
                Console.WriteLine();
                Console.Write(j+". otázka: " + quiz.Questions[i]);
                Console.Write("Vaše odpověď: ");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Zadaná Hodnota musí být integer!");
                    input = 0;
                }
                Console.WriteLine();
                if (input - 1 == quiz.Questions[i].RightAnswer)
                {
                    Console.WriteLine("Správně!");
                    pocetSpravnych++;
                }
                else
                {
                    Console.WriteLine("Špatně. Správná odpověď byla " + quiz.Questions[i].Answers[quiz.Questions[i].RightAnswer]);
                }
            }
            double procenta = 100 / quiz.Questions.Count() * pocetSpravnych;
            Console.WriteLine();
            Console.WriteLine("Dokončil/a jste Celý kvíz.");
            Console.WriteLine("Počet správných odpovědí: "+pocetSpravnych+"/"+ quiz.Questions.Count()+" ("+procenta+"%)");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------------------");
        }
        static Quiz ChooseQuiz(List<String> kvizy)
        {
            Console.WriteLine();
            Console.WriteLine("Aktivní kvízy: ");
            Console.WriteLine();
            int i = 1;
            foreach (var kviz in kvizy)
            {
                Console.WriteLine(i + ". " + kviz.ToString());
                i++;
            }            
            int input = 0;
            Quiz output = new Quiz();
            bool run = true;
            while (run)
            {
                Console.WriteLine();
                Console.Write("Vyberte z nabídky: ");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Zadaná Hodnota musí být integer!");
                    input = 0;
                }
                if(input > 0 && input <= kvizy.Count)
                {
                    input = input - 1;
                    string chosenQuiz = kvizy[input]+"";
                    output = ImportQuiz(kvizy[input]);
                    run = false;
                }
                else if(input == 0)
                {
                    Console.WriteLine();
                    run = false;
                }
            }          
            return output;
        }

        static List<String> ImportNameQuizes() 
        {
            string filePath = "C:\\Users\\milda\\source\\repos\\PV\\Kviz\\Kviz\\bin\\Debug\\net6.0\\NazvyKvizu.txt";
            List<string> nazvyKvizu = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        nazvyKvizu.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
            return nazvyKvizu; 
        }   
        
        static Quiz ImportQuiz(string nazev)
        {
            Quiz quiz = new Quiz();
            nazev = "C:\\Users\\milda\\source\\repos\\PV\\Kviz\\Kviz\\bin\\Debug\\net6.0\\kvízy\\" + nazev + ".json";
            try
            {
                string jsonString = File.ReadAllText(nazev);
                quiz = JsonSerializer.Deserialize<Quiz>(jsonString);
                Console.WriteLine();
                Console.WriteLine("Kvíz byl úspěšně načtený.");
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }            
            return quiz;
        }

        static void QuizManager(List<String> kvizy)
        {
            int input = 0;
            bool run = true;
            int strike = 0;
            int maxStrike = 3;
            while (run)
            {
                Console.WriteLine();
                Console.WriteLine("Správce Kvízů\n1 - Seznam Kvízů\n2 - Vytvořit kvíz\n3 - Editovat kvíz\n4 - Smazat kvíz\n5 - Zpět do Admin Panelu");
                Console.Write("Vyberte možnost: ");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Zadaná Hodnota musí být integer!");
                    input = 0;
                }
                switch (input)
                {
                    case 1:
                        int i = 1;
                        Console.WriteLine();
                        Console.WriteLine("Seznam aktivních kvízů.");
                        Console.WriteLine();
                        foreach (var kviz in kvizy)
                        {
                            Console.WriteLine(i + ". " + kviz.ToString());
                            i++;
                        }
                        Console.WriteLine();
                        break;
                    case 2:
                        CreateQuiz();
                        break;
                    case 3:                        
                        //EditQuiz();
                        break;
                    case 4:
                        //DeleteQuiz();
                        break;
                    case 5:
                        run = false;
                        break;
                    default:
                        Console.WriteLine();
                        if (strike < maxStrike)
                        {
                            strike++;
                            Console.WriteLine("Máte " + strike + " striků, jestli dosáhnete " + maxStrike + " striků aplikace se automaticky ukončí.");
                            Console.WriteLine("Vyberte celé číslo z nabídky..");
                        }
                        else
                        {
                            Console.WriteLine("Dosáhli jste " + strike + " striků. Aplikace se teď automaticky ukončí.");
                            run = false;
                        }
                        break;
                }
            }
        }

        static void CreateQuiz()
        {
            List<string> kvizy = ImportNameQuizes();
            string quizName = "invalidName";
            int input = 0;
            bool run = true;
            while (run)
            {                
                Console.WriteLine();
                Console.Write("Zadejte název kvízu: ");
                try
                {
                    quizName = Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    quizName = "invalidName";
                }
                Console.WriteLine();
                Console.WriteLine(quizName + " - Je tento název v pořádku? ");
                Console.WriteLine("1 - ANO\n2 - NE");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Zadaná Hodnota musí být integer!");
                    input = 0;
                }
                if(input == 1)
                {
                    bool duplicita = false;
                    foreach(string key in kvizy)
                    {
                        if (quizName == key)
                        {
                            duplicita = true;
                        }
                    }
                    if (!duplicita)
                    {
                        kvizy.Add(quizName);
                        SaveQuizNames(kvizy);
                        run = false;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Tento název byl již použit. Zadejte prosím jiný.");
                    }
                }
            }
            run = true;
            Quiz newQuiz = new Quiz(quizName);
            while (run)
            {                
                newQuiz.AddQuestion(NewQuestion());
                Console.WriteLine();
                Console.WriteLine("Chcete přidat dalsší otázku do vašeho kvízu? ");
                Console.WriteLine("1 - ANO\n2 - NE");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Zadaná Hodnota musí být integer!");
                    input = 0;
                }
                if(input != 1)
                {
                    run = false;
                }
            }
            SaveQuiz(newQuiz);
        }

        static void SaveQuiz(Quiz quiz)
        {
            try
            {
                string jsonQuiz = JsonSerializer.Serialize(quiz);
                string path = "C:\\Users\\milda\\source\\repos\\PV\\Kviz\\Kviz\\bin\\Debug\\net6.0\\kvízy\\" + quiz.Name + ".json";
                File.WriteAllText(path, jsonQuiz);
                Console.WriteLine();
                Console.WriteLine("Kvíz byl úspěšně uložen.");
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }           
        }
        static Question NewQuestion()
        {
            Question question = new Question();
            bool run = true;
            int input = 0;
            string interrogator = "Invalid";
            int numberOfAnswers = 0; 
            List<string> answers = new List<string>();
            while (run)
            {
                #region Interrogator
                run = true;
                while (run)
                {
                    Console.WriteLine();
                    Console.Write("Zadejte otázku: ");
                    try
                    {
                        interrogator = Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        interrogator = "invalidName";
                    }
                    Console.WriteLine();
                    Console.WriteLine(interrogator + " - Je to v pořádku? ");
                    Console.WriteLine("1 - ANO\n2 - NE");
                    try
                    {
                        input = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Zadaná Hodnota musí být integer!");
                        input = 0;
                    }
                    if (input == 1)
                    {
                        question.Interrogator = interrogator;
                        run = false;
                    }
                }
                #endregion
                #region Answers
                run = true;
                while (run)
                {
                    Console.WriteLine();
                    Console.Write("Zadejte počet odpovědí pro tuto otázku: ");
                    try
                    {
                        input = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        input = 0;
                    }
                    if (input < 2 || input > 6)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Počet možnýcho odpovědí může být v rozmezí 2 - 6.");
                    }
                    else
                    {
                        run = false;
                        numberOfAnswers = input;
                    }
                }
                for (int i = 0; i < numberOfAnswers; i++)
                {
                    int j = i + 1;
                    run = true;
                    while (run)
                    {
                        Console.WriteLine();
                        Console.WriteLine(interrogator);
                        Console.Write("Zadejte " + j + ". odpověď na otázku: ");
                        string answer = "invalid";
                        try
                        {
                            answer = Console.ReadLine();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            answer = "invalid";
                        }
                        Console.WriteLine();
                        Console.WriteLine(answer + " - Je to v pořádku? ");
                        Console.WriteLine("1 - ANO\n2 - NE");
                        try
                        {
                            input = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Zadaná Hodnota musí být integer!");
                            input = 0;
                        }
                        if (input == 1)
                        {
                            answers.Add(answer);
                            run = false;
                        }
                    }
                }
                question.Answers = answers;
                #endregion
                #region Right Answer
                run = true;
                while (run)
                {
                    Console.WriteLine();
                    Console.WriteLine(interrogator);
                    Console.Write("Zadejte číslo správné odpovědi: ");
                    int rightAnswer = 0;
                    try
                    {
                        rightAnswer = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        rightAnswer = 0;
                    }
                    if(rightAnswer > 0 && rightAnswer <= numberOfAnswers)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Je to správně? ");
                        Console.WriteLine("1 - ANO\n2 - NE");
                        try
                        {
                            input = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Zadaná Hodnota musí být integer!");
                            input = 0;
                        }
                        if (input == 1)
                        {
                            question.RightAnswer = rightAnswer - 1;
                            run = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Číslo je mimo rozmezí 1 - "+ numberOfAnswers);
                    }                   
                }
                #endregion
                run = true;
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine(question);               
                Console.WriteLine();
                Console.WriteLine("Je celá otázka správně? ");
                Console.WriteLine("1 - ANO\n2 - NE");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Zadaná Hodnota musí být integer!");
                    input = 0;
                }
                if (input == 1)
                {
                    run = false;
                }
            }
            return question;
        }
        //Still in progress..
        static void EditQuiz(Quiz quiz)
        {
            Console.WriteLine("Still in progress..");
        }
        //Still in progress..
        static void DeleteQuiz(Quiz quiz)
        {
            Console.WriteLine("Still in progress..");
        }

        static void SaveQuizNames(List<String> kvizy)
        {
            string filePath = "C:\\Users\\milda\\source\\repos\\PV\\Kviz\\Kviz\\bin\\Debug\\net6.0\\NazvyKvizu.txt";
            File.WriteAllLines(filePath, kvizy);
        }

        static void UserManager(List<User> users)
        {
            int input = 0;
            bool run = true;
            int strike = 0;
            int maxStrike = 3;
            while (run)
            {
                Console.WriteLine();
                Console.WriteLine("Správce Uživatelů\n1 - Seznam Uživatelů\n2 - Přidat uživatele\n3 - Editovat uživatele\n4 - Odebrat uživatele\n5 - Zpět do Admin Panelu");
                Console.Write("Vyberte možnost: ");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Zadaná Hodnota musí být integer!");
                    input = 0;
                }
                switch (input)
                {
                    case 1:
                        int i = 1;
                        Console.WriteLine();
                        Console.WriteLine("Seznam aktivních uživatelů.");
                        Console.WriteLine();
                        foreach (var user in users)
                        {
                            Console.WriteLine(i+". "+user.ToString());
                            i++;
                        }
                        Console.WriteLine();
                        break;
                    case 2:
                        Console.WriteLine("Still in progress..");
                        break;
                    case 3:
                        Console.WriteLine("Still in progress..");
                        break;
                    case 4:
                        Console.WriteLine("Still in progress..");
                        break;
                    case 5:
                        run = false;
                        break;
                    default:
                        Console.WriteLine();
                        if (strike < maxStrike)
                        {
                            strike++;
                            Console.WriteLine("Máte " + strike + " striků, jestli dosáhnete " + maxStrike + " striků aplikace se automaticky ukončí.");
                            Console.WriteLine("Vyberte celé číslo z nabídky..");
                        }
                        else
                        {
                            Console.WriteLine("Dosáhli jste " + strike + " striků. Aplikace se teď automaticky ukončí.");
                            run = false;
                        }
                        break;
                }
            }
        }


    }
}