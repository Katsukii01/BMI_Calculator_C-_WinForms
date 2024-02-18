using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout;
using iText.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Kernel.Exceptions;

namespace ProjektKordalski
{
    public class DietToPDF
    {
        private List<Diets> diets; //lista diet

        public void DietAdd(List<Diets> diets)
        {
            this.diets = diets;
        }

        public void GeneratePDF(int id)
        {
            try
            {
                Diets diet = diets.Find(d => d.Id == id);
                if (diet == null)
                {
                    MessageBox.Show($"Diet with ID {id} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                saveFileDialog.Title = "Save PDF File";
                saveFileDialog.FileName = $"Diet_{diet.BmiType}.pdf"; // nazwa pliku

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // towrzymy obiekt pdf writer
                    using (PdfWriter writer = new PdfWriter(filePath))
                    {
                        // tworzymy pdfdocument 
                        using (PdfDocument pdf = new PdfDocument(writer))
                        {
                            // tworzymy document
                            Document document = new Document(pdf);

                            // dodajemy tytul 
                            document.Add(new Paragraph($"Diet Information - {diet.BmiType}")
                                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                                .SetFontSize(20));

                            //odstep
                            document.Add(new Paragraph("\n"));

                            //opis diety
                            document.Add(new Paragraph($"Diet Description: {diet.Description}"));


                            // zamykamy dokument
                            document.Close();
                        }
                    }

                    MessageBox.Show($"PDF generated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected Exception: {ex.Message}\nStackTrace: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
