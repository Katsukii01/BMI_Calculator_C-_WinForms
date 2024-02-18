using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektKordalski
{
    public partial class MainApp : Form
    {
        public MainApp()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem is ToolStripMenuItem clickedItem)
            {
                string itemName = clickedItem.Text;

                switch (itemName)
                {
                    case "BMI calculator":

                        OpenFormIfNotOpened(typeof(BMIcalc));
                        break;

                    case "Chart":
                        OpenFormIfNotOpened(typeof(Chart));
                        break;

                    case "Edit BMI data":
                        OpenFormIfNotOpened(typeof(DataEdit));
                        break;

                    case "Pick diet":
                        OpenFormIfNotOpened(typeof(Diet));
                        break;
                }
            }
        }
        private void OpenFormIfNotOpened(Type formType)
        {
            // Sprawdź, czy formularz jest już otwarty
            foreach (Form openForm in MdiChildren)
            {
                if (openForm.GetType() == formType)
                {
                    MessageBox.Show($"The {openForm.Text} form is already open. Close it before opening a new one.");
                    return;
                }
            }

            // Jeśli formularz nie jest otwarty, otwórz nowy jako MDI child
            Form newForm = (Form)Activator.CreateInstance(formType);
            newForm.MdiParent = this;
            newForm.Show();
        }
    }
}
