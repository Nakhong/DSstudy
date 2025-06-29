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
            this.searchTP = new System.Windows.Forms.TableLayoutPanel();
            this.searchBtn = new System.Windows.Forms.Button();
            this.searchTP.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchTB
            // 
            this.searchTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchTB.Location = new System.Drawing.Point(0, 3);
            this.searchTB.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.searchTB.Name = "searchTB";
            this.searchTB.Size = new System.Drawing.Size(274, 21);
            this.searchTB.TabIndex = 1;
            // 
            // searchTP
            // 
            this.searchTP.AutoSize = true;
            this.searchTP.ColumnCount = 2;
            this.searchTP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.06015F));
            this.searchTP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.93985F));
            this.searchTP.Controls.Add(this.searchBtn, 1, 0);
            this.searchTP.Controls.Add(this.searchTB, 0, 0);
            this.searchTP.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchTP.Location = new System.Drawing.Point(0, 0);
            this.searchTP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.searchTP.Name = "searchTP";
            this.searchTP.RowCount = 1;
            this.searchTP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.searchTP.Size = new System.Drawing.Size(370, 29);
            this.searchTP.TabIndex = 2;
            // 
            // searchBtn
            // 
            this.searchBtn.AutoSize = true;
            this.searchBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchBtn.Location = new System.Drawing.Point(277, 2);
            this.searchBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(90, 25);
            this.searchBtn.TabIndex = 1;
            this.searchBtn.Text = "검색";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // SearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.searchTP);
            this.Name = "SearchControl";
            this.Size = new System.Drawing.Size(370, 28);
            this.searchTP.ResumeLayout(false);
            this.searchTP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchTB;
        private System.Windows.Forms.TableLayoutPanel searchTP;
        private System.Windows.Forms.Button searchBtn;
    }
}
