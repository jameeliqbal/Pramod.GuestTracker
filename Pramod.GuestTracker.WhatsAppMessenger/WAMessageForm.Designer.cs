
namespace Pramod.GuestTracker.WhatsAppMessenger
{
    partial class WAMessageForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtGuestList = new System.Windows.Forms.TextBox();
            this.btnSendInvites = new System.Windows.Forms.Button();
            this.btnBrowseGuestList = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Guest List";
            // 
            // txtGuestList
            // 
            this.txtGuestList.Location = new System.Drawing.Point(13, 29);
            this.txtGuestList.Multiline = true;
            this.txtGuestList.Name = "txtGuestList";
            this.txtGuestList.ReadOnly = true;
            this.txtGuestList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGuestList.Size = new System.Drawing.Size(510, 44);
            this.txtGuestList.TabIndex = 1;
            // 
            // btnSendInvites
            // 
            this.btnSendInvites.Location = new System.Drawing.Point(13, 79);
            this.btnSendInvites.Name = "btnSendInvites";
            this.btnSendInvites.Size = new System.Drawing.Size(591, 61);
            this.btnSendInvites.TabIndex = 2;
            this.btnSendInvites.Text = "Send Invites";
            this.btnSendInvites.UseVisualStyleBackColor = true;
            this.btnSendInvites.Click += new System.EventHandler(this.btnSendInvites_Click);
            // 
            // btnBrowseGuestList
            // 
            this.btnBrowseGuestList.Location = new System.Drawing.Point(529, 29);
            this.btnBrowseGuestList.Name = "btnBrowseGuestList";
            this.btnBrowseGuestList.Size = new System.Drawing.Size(75, 44);
            this.btnBrowseGuestList.TabIndex = 2;
            this.btnBrowseGuestList.Text = "Browse...";
            this.btnBrowseGuestList.UseVisualStyleBackColor = true;
            this.btnBrowseGuestList.Click += new System.EventHandler(this.btnBrowseGuestList_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.btnBrowseGuestList);
            this.splitContainer1.Panel1.Controls.Add(this.btnSendInvites);
            this.splitContainer1.Panel1.Controls.Add(this.txtGuestList);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtLog);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 145;
            this.splitContainer1.TabIndex = 3;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(800, 301);
            this.txtLog.TabIndex = 0;
            // 
            // WAMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "WAMessageForm";
            this.Text = "WhatsApp Message Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGuestList;
        private System.Windows.Forms.Button btnSendInvites;
        private System.Windows.Forms.Button btnBrowseGuestList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtLog;
    }
}

