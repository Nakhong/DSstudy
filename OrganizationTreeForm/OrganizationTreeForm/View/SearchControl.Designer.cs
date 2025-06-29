namespace OrganizationTreeForm.View
{
    partial class SearchControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.searchTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // searchTB
            // 
            this.searchTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchTB.Location = new System.Drawing.Point(0, 0);
            this.searchTB.Margin = new System.Windows.Forms.Padding(0);
            this.searchTB.Name = "searchTB";
            this.searchTB.Size = new System.Drawing.Size(336, 21);
            this.searchTB.TabIndex = 1;
            // 
            // SearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.searchTB);
            this.Name = "SearchControl";
            this.Size = new System.Drawing.Size(336, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchTB;
    }
}
