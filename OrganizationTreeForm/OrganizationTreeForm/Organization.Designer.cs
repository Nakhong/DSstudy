﻿namespace OrganizationTreeForm
{
    partial class Organization
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.OrgTV = new System.Windows.Forms.TreeView();
            this.searchTP = new System.Windows.Forms.TableLayoutPanel();
            this.searchControl1 = new OrganizationTreeForm.View.SearchControl();
            this.orgTC = new System.Windows.Forms.TabControl();
            this.ResultTP = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.playerControl = new OrganizationTreeForm.View.PlayerControl();
            this.AddTP = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.searchTP.SuspendLayout();
            this.orgTC.SuspendLayout();
            this.ResultTP.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.OrgTV);
            this.splitContainer1.Panel1.Controls.Add(this.searchTP);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.orgTC);
            this.splitContainer1.Size = new System.Drawing.Size(1525, 749);
            this.splitContainer1.SplitterDistance = 505;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // OrgTV
            // 
            this.OrgTV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OrgTV.Location = new System.Drawing.Point(0, 42);
            this.OrgTV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OrgTV.Name = "OrgTV";
            this.OrgTV.Size = new System.Drawing.Size(505, 707);
            this.OrgTV.TabIndex = 1;
            this.OrgTV.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OrgTV_NodeMouseClick);
            // 
            // searchTP
            // 
            this.searchTP.AutoSize = true;
            this.searchTP.ColumnCount = 1;
            this.searchTP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.06015F));
            this.searchTP.Controls.Add(this.searchControl1, 0, 0);
            this.searchTP.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchTP.Location = new System.Drawing.Point(0, 0);
            this.searchTP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.searchTP.Name = "searchTP";
            this.searchTP.RowCount = 1;
            this.searchTP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.searchTP.Size = new System.Drawing.Size(505, 42);
            this.searchTP.TabIndex = 0;
            // 
            // searchControl1
            // 
            this.searchControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchControl1.Location = new System.Drawing.Point(3, 5);
            this.searchControl1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Size = new System.Drawing.Size(499, 32);
            this.searchControl1.TabIndex = 2;
            this.searchControl1.TargetPlayerControl = null;
            this.searchControl1.TreeView = null;
            // 
            // orgTC
            // 
            this.orgTC.Controls.Add(this.ResultTP);
            this.orgTC.Controls.Add(this.AddTP);
            this.orgTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orgTC.Location = new System.Drawing.Point(0, 0);
            this.orgTC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.orgTC.Name = "orgTC";
            this.orgTC.SelectedIndex = 0;
            this.orgTC.Size = new System.Drawing.Size(1015, 749);
            this.orgTC.TabIndex = 0;
            // 
            // ResultTP
            // 
            this.ResultTP.Controls.Add(this.panel2);
            this.ResultTP.Location = new System.Drawing.Point(4, 25);
            this.ResultTP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ResultTP.Name = "ResultTP";
            this.ResultTP.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ResultTP.Size = new System.Drawing.Size(1007, 720);
            this.ResultTP.TabIndex = 0;
            this.ResultTP.Text = "결과";
            this.ResultTP.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.playerControl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1001, 716);
            this.panel2.TabIndex = 0;
            // 
            // playerControl
            // 
            this.playerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerControl.Location = new System.Drawing.Point(0, 0);
            this.playerControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.playerControl.Name = "playerControl";
            this.playerControl.Size = new System.Drawing.Size(1001, 716);
            this.playerControl.TabIndex = 0;
            // 
            // AddTP
            // 
            this.AddTP.Location = new System.Drawing.Point(4, 25);
            this.AddTP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddTP.Name = "AddTP";
            this.AddTP.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddTP.Size = new System.Drawing.Size(1007, 720);
            this.AddTP.TabIndex = 1;
            this.AddTP.Text = "추가";
            this.AddTP.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(-8, 194);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 2;
            // 
            // Organization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1525, 749);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Organization";
            this.Text = "축구 선수 정보";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.searchTP.ResumeLayout(false);
            this.orgTC.ResumeLayout(false);
            this.ResultTP.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView OrgTV;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl orgTC;
        private System.Windows.Forms.TabPage AddTP;
        private System.Windows.Forms.TabPage ResultTP;
        private System.Windows.Forms.Panel panel2;
        private View.PlayerControl playerControl;
        private System.Windows.Forms.TableLayoutPanel searchTP;
        private View.SearchControl searchControl1;
    }
}

