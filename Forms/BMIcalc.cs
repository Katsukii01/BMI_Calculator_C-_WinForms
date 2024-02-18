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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.DataVisualization.Charting;
using System.Security.Cryptography;

namespace ProjektKordalski
{
    public partial class BMIcalc : Form
    {
        public BMIcalc()
        {
            InitializeComponent();
            DisplayBMIChart(0);
            button4.Visible = false;
            button4.Enabled = false;
        }

        private double GetBmi()
        {
            // Pobierz dane z formularza
            string name = textBox1.Text;
            string gender = GetSelectedGender();

            // Walidacja danych
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(gender))
            {
                MessageBox.Show("Please enter all required information.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            if (!double.TryParse(textBox2.Text, out double height) || !double.TryParse(textBox3.Text, out double weight))
            {
                MessageBox.Show("Please enter valid numerical values for height and weight.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            if (height <= 0 || weight <= 0)
            {
                MessageBox.Show("Height and weight must be positive values.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            // Wywołaj metodę obliczającą BMI z klasy BMIeq
            double bmi = BMIeq.CalculateBMI(height, weight);
            return bmi;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            double bmi = GetBmi();
            if (bmi > 0)
            {
                label6.Text = Math.Round(bmi, 3).ToString();
                if (bmi < 16)
                {
                    label6.Text += "\n starvation";
                    label6.Location = new System.Drawing.Point(276, 50);
                }
                else if (bmi >= 16 && bmi <= 16.99)
                {
                    label6.Text += "\n emaciation";
                    label6.Location = new System.Drawing.Point(276, 50);
                }
                else if (bmi >= 17 && bmi <= 18.49)
                {
                    label6.Text += "\n underweight";
                    label6.Location = new System.Drawing.Point(276, 50);
                }
                else if (bmi >= 18.5 && bmi <= 24.99)
                {
                    label6.Text += "\n correct value";
                    label6.Location = new System.Drawing.Point(276, 50);
                }
                else if (bmi >= 25 && bmi <= 29.99)
                {
                    label6.Text += "\n overweight";
                    label6.Location = new System.Drawing.Point(276, 50);
                }
                else if (bmi >= 30 && bmi <= 34.99)
                {
                    label6.Text += "\n I degree of obesity";
                    label6.Location = new System.Drawing.Point(236,50);
                }
                else if (bmi >= 35 && bmi <= 39.99)
                {
                    label6.Text += "\n II degree of obesity";
                    label6.Location = new System.Drawing.Point(236, 50);
                }
                else if (bmi >= 40)
                {
                    label6.Text += "\n  extreme obesity";
                    label6.Location = new System.Drawing.Point(236, 50);
                }
                // Wygeneruj i wyświetl wykres BMI w groupboxie 3
                DisplayBMIChart(bmi);
            }
            else
            {
                label6.Text = "";

                // Wygeneruj i wyświetl wykres BMI w groupboxie 3
                DisplayBMIChart(bmi);
            }
        }

        private string GetSelectedGender()
        {
            if (male.Checked)
            {
                return "Male";
            }
            else if (female.Checked)
            {
                return "Female";
            }
            else
            {
                return null;;
            }
        }
        private void DisplayDiet(double bmi)
        {
            button4.Visible = true;
            button4.Enabled = true;
        }

        private void DisplayBMIChart(double bmi)
        {
            if (bmi > 0)
            {
                DisplayDiet(bmi);
            }
            else
            {
                button4.Visible = false;
                button4.Enabled = false;
            }

            chart1.Series.Clear(); // Wyczyść istniejące serie, jeśli istnieją

            // Dodaj nową serię do wykresu
            chart1.Series.Add("BMI");

            // Dodaj punkt z wartością BMI
            chart1.Series["BMI"].Points.AddXY("Your BMI", bmi);

            // Dodaj obszary kolorów dla różnych stref BMI
            AddBMIChartZones();

            // Możesz dostosować wygląd wykresu, np. dodać etykiety, tytuły itp.
            chart1.ChartAreas[0].AxisX.Title = "Category";
            chart1.ChartAreas[0].AxisY.Title = "BMI Value";

            // Dodaj etykietę z wartością BMI nad punktem na wykresie
            chart1.Series["BMI"].Label = bmi.ToString("F2");

            // Włącz wyświetlanie wartości BMI dla każdego punktu
            chart1.Series["BMI"].IsValueShownAsLabel = true;

            // Aktualizuj wygląd wykresu
            chart1.Update();
        }
        private void AddBMIChartZones()
        {
            // Definiuj zakresy BMI i odpowiadające im kolory
            double[] bmiRanges = { 0, 16, 17, 18.5, 25, 30, 35, 40, double.MaxValue };
            Color[] zoneColors = { Color.Gray, Color.DarkOrange, Color.Yellow, Color.Green, Color.Yellow, Color.Orange, Color.Red, Color.DarkRed };

            // Ustaw stałą wysokość osi Y na 50
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 60;

            // Dodaj obszary kolorów dla różnych stref BMI
            for (int i = 0; i < bmiRanges.Length - 1; i++)
            {
                StripLine stripLine = new StripLine
                {
                    IntervalOffset = bmiRanges[i],
                    IntervalOffsetType = DateTimeIntervalType.Number,
                    Interval = bmiRanges[i + 1] - bmiRanges[i],
                    BackColor = zoneColors[i],
                    StripWidth = 50, // Szerokość obszaru koloru w kierunku osi Y (ustawiona na 50)
                    StripWidthType = DateTimeIntervalType.Number,
                    BorderWidth = 0 // Grubość obramowania ustawiona na 0, aby usunąć obramowanie
                };

                chart1.ChartAreas[0].AxisY.StripLines.Add(stripLine);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double bmi = GetBmi();
            if (bmi != -1)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Pliki CSV (*.csv)|*.csv|Wszystkie pliki (*.*)|*.*";
                saveFileDialog.Title = "Wybierz plik do zapisu";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // Wywołaj funkcję do zapisu danych z klasy FileOper
                    FileOper.WriteDataToCSV(filePath, textBox1.Text, GetSelectedGender(), textBox2.Text, textBox3.Text, bmi);

                    MessageBox.Show("Dane zostały zapisane do pliku.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        List<Diets> diets = new List<Diets>();
        private void button4_Click(object sender, EventArgs e)
        {
            // Ścieżka do pliku diets.csv
            string filePath = Path.Combine("Files", "diets.csv");
            Console.WriteLine(filePath);

            //id diety
            int dietId=1;

            // Sprawdź, czy plik istnieje
            if (File.Exists(filePath))
            {
                diets = FileOper.ReadDietsFromCSV(filePath);
            }

            //dopasuj diete do bmi
            double bmi = GetBmi();

            if (bmi < 16)
            {
                dietId = 1;
            }
            else if (bmi >= 16 && bmi <= 16.99)
            {
                dietId = 2;
            }
            else if (bmi >= 17 && bmi <= 18.49)
            {
                dietId = 3;
            }
            else if (bmi >= 18.5 && bmi <= 24.99)
            {
                dietId = 4;
            }
            else if (bmi >= 25 && bmi <= 29.99)
            {
                dietId = 5;
            }
            else if (bmi >= 30 && bmi <= 34.99)
            {
                dietId = 6;
            }
            else if (bmi >= 35 && bmi <= 39.99)
            {
                dietId = 7;
            }
            else if (bmi >= 40)
            {
                dietId = 8;
            }

            //generuj pdf z dieta do pobrania 
            DietToPDF pdf = new DietToPDF();
            pdf.DietAdd(diets);
            pdf.GeneratePDF(dietId);

        }
    }
}
