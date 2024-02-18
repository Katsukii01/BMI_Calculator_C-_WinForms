namespace ProjektKordalski
{
    partial class MainApp
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bMICalculatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editBMIDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pickDietToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bMICalculatorToolStripMenuItem,
            this.chartToolStripMenuItem,
            this.editBMIDataToolStripMenuItem,
            this.pickDietToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1677, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // bMICalculatorToolStripMenuItem
            // 
            this.bMICalculatorToolStripMenuItem.Name = "bMICalculatorToolStripMenuItem";
            this.bMICalculatorToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.bMICalculatorToolStripMenuItem.Text = "BMI calculator";
            // 
            // chartToolStripMenuItem
            // 
            this.chartToolStripMenuItem.Name = "chartToolStripMenuItem";
            this.chartToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.chartToolStripMenuItem.Text = "Chart";
            // 
            // editBMIDataToolStripMenuItem
            // 
            this.editBMIDataToolStripMenuItem.Name = "editBMIDataToolStripMenuItem";
            this.editBMIDataToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.editBMIDataToolStripMenuItem.Text = "Edit BMI data";
            // 
            // pickDietToolStripMenuItem
            // 
            this.pickDietToolStripMenuItem.Name = "pickDietToolStripMenuItem";
            this.pickDietToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.pickDietToolStripMenuItem.Text = "Pick diet";
            // 
            // MainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1677, 981);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BMI APP";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem bMICalculatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editBMIDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pickDietToolStripMenuItem;
    }
}

