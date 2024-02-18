using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektKordalski
{
    public partial class Diet : Form
    {
        public Diet()
        {
            InitializeComponent();
        }

        private List<Diets> diets = new List<Diets>();

        private void Diet_Load(object sender, EventArgs e)
        {
            // Ścieżka do pliku diets.csv
            string filePath = Path.Combine( "Files", "diets.csv");
            Console.WriteLine(filePath);

            // Sprawdź, czy plik istnieje
            if (File.Exists(filePath))
            {

                // Dodaj kolumny do DataGridView
                dataGridView1.Columns.Add("Id", "ID");
                dataGridView1.Columns.Add("BmiType", "BMI Type");
                dataGridView1.Columns.Add("Description", "Description");
                dataGridView1.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.AllowUserToAddRows = false;
                // Wczytaj dane z pliku CSV do listy diets
                diets = FileOper.ReadDietsFromCSV(filePath);

                // Odśwież DataGridView
                UpdateDietDataGridView();
            }
            else
            {
                MessageBox.Show("Plik diets.csv nie istnieje.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDietDataGridView()
        {
            // Wyczyść poprzednie dane w DataGridView
            dataGridView1.Rows.Clear();

            // Dodaj diety do DataGridView
            foreach (Diets diet in diets)
            {
                dataGridView1.Rows.Add(diet.Id, diet.BmiType, diet.Description);
            }
        }

        //pobieranie diety
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                int selectedDietId = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells[0].Value);
                DietToPDF pdf = new DietToPDF();
                pdf.DietAdd(diets);
                pdf.GeneratePDF(selectedDietId);
            }
            else
            {
                MessageBox.Show("Wybierz osobę do usunięcia.", "Brak wyboru", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
