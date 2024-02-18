using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektKordalski
{
    internal class FileOper
    {

        //operacja na osobie
        public static int GetNextId(string filePath)
        {
            try
            {
                List<Person> people = ReadDataFromCSV(filePath);

                // Jeśli lista jest pusta, zwróć 1 jako pierwszy identyfikator
                if (people.Count == 0)
                {
                    return 1;
                }

                // Znajdź najwyższy identyfikator w liście
                int maxId = people.Max(person => person.Id);

                // Zwróć następny identyfikator
                return maxId + 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas pobierania następnego identyfikatora: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; // Zwróć -1 w przypadku błędu
            }
        }


        public static void WriteDataToCSV(string filePath, string name, string gender, string height, string weight, double bmi)
        {
            try
            {
                // Sprawdź, czy plik istnieje
                bool fileExists = File.Exists(filePath);
                int nextId = 1;
                // Zapisz dane
                if (fileExists){
                    nextId = GetNextId(filePath);
                }

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    // Jeżeli plik nie istnieje, dodaj nagłówki
                    if (!fileExists)
                    {
                        writer.WriteLine("Id,Name,Gender,Height,Weight,BMI");
                    }

                
                    writer.WriteLine($"{nextId},{name},{gender},{height},{weight},{bmi}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas zapisywania danych do pliku: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static List<Person> ReadDataFromCSV(string filePath)
        {
            List<Person> people = new List<Person>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Pomijaj pierwszą linię (nagłówki)
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        // Dodaj osobę do listy
                        people.Add(new Person
                        {
                            Id = Convert.ToInt32(values[0]),
                            Name = values[1],
                            Gender = values[2],
                            Height = values[3],
                            Weight = values[4],
                            BMI = Convert.ToDouble(values[5])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wczytywania danych z pliku: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return people;
        }

        public static void UpdateDataInCSV(string filePath, List<Person> updatedPeople)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false)) // 'false' oznacza nadpisanie pliku
                {
                    // Dodaj nagłówki
                    writer.WriteLine("Id,Name,Gender,Height,Weight,BMI");

                    // Zapisz dane
                    foreach (var person in updatedPeople)
                    {
                        writer.WriteLine($"{person.Id},{person.Name},{person.Gender},{person.Height},{person.Weight},{person.BMI}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas aktualizacji danych w pliku: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void DeletePersonFromCSV(string filePath, int personId)
        {
            try
            {
                List<Person> people = ReadDataFromCSV(filePath);

                // Znajdź indeks osoby do usunięcia
                int indexToRemove = people.FindIndex(person => person.Id == personId);

                if (indexToRemove != -1)
                {
                    // Usuń osobę z listy
                    people.RemoveAt(indexToRemove);

                    // Zapisz zaktualizowane dane z powrotem do pliku
                    UpdateDataInCSV(filePath, people);
                }
                else
                {
                    MessageBox.Show($"Nie znaleziono osoby o Id: {personId}", "Brak osoby", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas usuwania osoby z pliku: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Operacje na dietach 
        public static List<Diets> ReadDietsFromCSV(string filePath)
        {
            List<Diets> diets = new List<Diets>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Pomijaj pierwszą linię (nagłówki)
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        // Dodaj dietę do listy
                        diets.Add(new Diets
                        {
                            Id = Convert.ToInt32(values[0]),
                            BmiType = values[1],
                            Description = values[2]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wczytywania danych z pliku: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return diets;
        }
    }
}
