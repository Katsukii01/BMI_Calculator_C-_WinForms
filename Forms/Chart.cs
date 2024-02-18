using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProjektKordalski
{
    public partial class Chart : Form
    {
        public Chart()
        {
            InitializeComponent();
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
                UpdateChart(people);
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

        private void UpdateChart(List<Person> people)
        {
            // Wyczyść poprzednie dane na wykresie
            chart1.Series.Clear();

            // Utwórz nowy szereg danych na wykresie
            Series series = new Series("BMI Series");
            series.ChartType = SeriesChartType.Line;

            // Dodaj punkty do szeregu danych
            foreach (Person person in people)
            {
                series.Points.AddXY(person.Id, person.BMI);
            }

            // Dodaj szereg danych do kontrolki Chart
            chart1.Series.Add(series);
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 60;
            // Ustaw etykiety na osiach
            chart1.ChartAreas[0].AxisX.Title = "Person ID";
            chart1.ChartAreas[0].AxisY.Title = "BMI";

            // Przypisz tytuł wykresu
            chart1.Titles.Clear();
            chart1.Titles.Add("BMI Chart");

            // Odśwież kontrolkę Chart
            chart1.Refresh();
        }
    }
}
