using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektKordalski
{
    public partial class DataEdit : Form
    {
        public DataEdit()
        {
            InitializeComponent();
        }
        private void DataEdit_Load(object sender, EventArgs e)
        {

        }

        private List<Person> people = new List<Person>();
        private string currentFilePath; // Ścieżka do aktualnie otwartego pliku

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki CSV (*.csv)|*.csv|Wszystkie pliki (*.*)|*.*";
            openFileDialog.Title = "Wybierz plik do załadowania";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = openFileDialog.FileName;

                LoadDataFromCSV(currentFilePath);
                UpdateDataGrid();
            }
        }

        private void LoadDataFromCSV(string filePath)
        {
            try
            {
                people = FileOper.ReadDataFromCSV(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wczytywania danych z pliku: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDataGrid()
        {
            // Wyczyść istniejące kolumny
            dataGridView1.Columns.Clear();

            // Dodaj kolumny dla klasy Person
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Id", HeaderText = "ID" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Name", HeaderText = "Name" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Gender", HeaderText = "Gender" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Height", HeaderText = "Height" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Weight", HeaderText = "Weight" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "BMI", HeaderText = "BMI" });

            // Ustaw źródło danych dla DataGridView
            dataGridView1.DataSource = people;
        }

        //usuwanie zaznaczonej osoby
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                int selectedPersonId = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells[0].Value);

                FileOper.DeletePersonFromCSV(currentFilePath, selectedPersonId);
                LoadDataFromCSV(currentFilePath);
                UpdateDataGrid();
            }
            else
            {
                MessageBox.Show("Wybierz osobę do usunięcia.", "Brak wyboru", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //edycja osoby
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                int selectedPersonId = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells[0].Value);

                // Znajdź indeks osoby w liście
                int indexOfSelectedPerson = people.FindIndex(person => person.Id == selectedPersonId);

                // Sprawdź, czy osoba została znaleziona
                if (indexOfSelectedPerson != -1)
                {
                    // Otwórz formularz do edycji
                    EditForm editForm = new EditForm(people[indexOfSelectedPerson]);

                    // Sprawdź, czy użytkownik zatwierdził zmiany
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        // Zaktualizuj dane w oryginalnej liście people
                        people[indexOfSelectedPerson] = editForm.EditedPerson;

                        // Zapisz zaktualizowane dane z powrotem do pliku
                        FileOper.UpdateDataInCSV(currentFilePath, people);

                        // Ponownie wczytaj dane i zaktualizuj interfejs
                        LoadDataFromCSV(currentFilePath);
                        UpdateDataGrid();
                    }
                }
                else
                {
                    MessageBox.Show("Nie można odnaleźć osoby do edycji.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Wybierz osobę do edycji.", "Brak wyboru", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }
}
