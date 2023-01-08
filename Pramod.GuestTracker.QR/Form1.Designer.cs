
namespace Pramod.GuestTracker.QR
{
    partial class Form1
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
            this.btnGetGuestList = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnGenerateHubVerifyToken = new System.Windows.Forms.Button();
            this.btnGenerteQR = new System.Windows.Forms.Button();
            this.grdGuestList = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdGuestList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetGuestList
            // 
            this.btnGetGuestList.Location = new System.Drawing.Point(12, 15);
            this.btnGetGuestList.Name = "btnGetGuestList";
            this.btnGetGuestList.Size = new System.Drawing.Size(156, 23);
            this.btnGetGuestList.TabIndex = 0;
            this.btnGetGuestList.Text = "Get Latest Guest List";
            this.btnGetGuestList.UseVisualStyleBackColor = true;
            this.btnGetGuestList.Click += new System.EventHandler(this.btnGetGuestList_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnGenerateHubVerifyToken);
            this.splitContainer1.Panel1.Controls.Add(this.btnGenerteQR);
            this.splitContainer1.Panel1.Controls.Add(this.btnGetGuestList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grdGuestList);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 59;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnGenerateHubVerifyToken
            // 
            this.btnGenerateHubVerifyToken.Location = new System.Drawing.Point(597, 14);
            this.btnGenerateHubVerifyToken.Name = "btnGenerateHubVerifyToken";
            this.btnGenerateHubVerifyToken.Size = new System.Drawing.Size(191, 23);
            this.btnGenerateHubVerifyToken.TabIndex = 3;
            this.btnGenerateHubVerifyToken.Text = "Generate hub.verify_token";
            this.btnGenerateHubVerifyToken.UseVisualStyleBackColor = true;
            this.btnGenerateHubVerifyToken.Click += new System.EventHandler(this.btnGenerateHubVerifyToken_Click);
            // 
            // btnGenerteQR
            // 
            this.btnGenerteQR.Location = new System.Drawing.Point(229, 15);
            this.btnGenerteQR.Name = "btnGenerteQR";
            this.btnGenerteQR.Size = new System.Drawing.Size(161, 23);
            this.btnGenerteQR.TabIndex = 2;
            this.btnGenerteQR.Text = "Generate QR Codes";
            this.btnGenerteQR.UseVisualStyleBackColor = true;
            this.btnGenerteQR.Click += new System.EventHandler(this.btnGenerteQR_Click);
            // 
            // grdGuestList
            // 
            this.grdGuestList.AccessibleName = "Table";
            this.grdGuestList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdGuestList.Location = new System.Drawing.Point(0, 0);
            this.grdGuestList.Name = "grdGuestList";
            this.grdGuestList.Size = new System.Drawing.Size(800, 387);
            this.grdGuestList.TabIndex = 0;
            this.grdGuestList.Text = "sfDataGrid1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "QR Code Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdGuestList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetGuestList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grdGuestList;
        private System.Windows.Forms.Button btnGenerteQR;
        private System.Windows.Forms.Button btnGenerateHubVerifyToken;
    }
}

