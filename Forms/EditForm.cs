using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjektKordalski
{
    public partial class EditForm : Form
    {
        public Person EditedPerson { get; private set; }
        public EditForm()
        {
            InitializeComponent();
        }
        public EditForm(Person person)
        {
            InitializeComponent();

            // Ustaw początkowe wartości formularza na podstawie danych przekazanych z głównego formularza
            textBoxName.Text = person.Name;
            textBoxGender.Text = person.Gender;
            textBoxHeight.Text = person.Height;
            textBoxWeight.Text = person.Weight;
            textBoxBMI.Text = person.BMI.ToString();

            // Ustaw pole Id jako tylko do odczytu
            textBoxId.Text = person.Id.ToString();
            textBoxId.ReadOnly = true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Zapisz zmienione dane i zamknij formularz
            EditedPerson = new Person
            {
                Id = Convert.ToInt32(textBoxId.Text),
                Name = textBoxName.Text,
                Gender = textBoxGender.Text,
                Height = textBoxHeight.Text,
                Weight = textBoxWeight.Text,
                BMI = Convert.ToDouble(textBoxBMI.Text)
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Zamknij formularz bez zapisywania zmian
            DialogResult = DialogResult.Cancel;
            Close();
        }


    }
}
