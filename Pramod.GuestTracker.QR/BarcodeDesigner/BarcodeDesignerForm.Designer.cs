namespace BarcodeClient
{
    partial class BarcodeDesignerForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.barcodeStructureTreeView = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.barcodeRendererPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.barcodeRendererComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.scaleTrackBar = new System.Windows.Forms.TrackBar();
            this.previewPanel = new System.Windows.Forms.Panel();
            this.barcodeStructureControl1 = new Vintasoft.Barcode.BarcodeStructureViewer();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scaleTrackBar)).BeginInit();
            this.previewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(2, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(779, 519);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.barcodeStructureTreeView);
            this.groupBox2.Location = new System.Drawing.Point(3, 424);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(252, 92);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Barcode Structure";
            // 
            // barcodeStructureTreeView
            // 
            this.barcodeStructureTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barcodeStructureTreeView.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.barcodeStructureTreeView.Location = new System.Drawing.Point(3, 16);
            this.barcodeStructureTreeView.Name = "barcodeStructureTreeView";
            this.barcodeStructureTreeView.Size = new System.Drawing.Size(246, 73);
            this.barcodeStructureTreeView.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.barcodeRendererPropertyGrid);
            this.groupBox1.Controls.Add(this.barcodeRendererComboBox);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 415);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Render";
            // 
            // renderPropertyGrid
            // 
            this.barcodeRendererPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.barcodeRendererPropertyGrid.Location = new System.Drawing.Point(6, 46);
            this.barcodeRendererPropertyGrid.Name = "renderPropertyGrid";
            this.barcodeRendererPropertyGrid.Size = new System.Drawing.Size(243, 363);
            this.barcodeRendererPropertyGrid.TabIndex = 1;
            this.barcodeRendererPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.renderPropertyGrid_PropertyValueChanged);
            // 
            // renderComboBox
            // 
            this.barcodeRendererComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.barcodeRendererComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.barcodeRendererComboBox.FormattingEnabled = true;
            this.barcodeRendererComboBox.Location = new System.Drawing.Point(6, 19);
            this.barcodeRendererComboBox.Name = "renderComboBox";
            this.barcodeRendererComboBox.Size = new System.Drawing.Size(243, 21);
            this.barcodeRendererComboBox.TabIndex = 0;
            this.barcodeRendererComboBox.SelectedIndexChanged += new System.EventHandler(this.rendererComboBox_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.scaleTrackBar);
            this.groupBox3.Controls.Add(this.previewPanel);
            this.groupBox3.Location = new System.Drawing.Point(0, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(517, 516);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Preview";
            // 
            // scaleTrackBar
            // 
            this.scaleTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scaleTrackBar.LargeChange = 10;
            this.scaleTrackBar.Location = new System.Drawing.Point(6, 11);
            this.scaleTrackBar.Maximum = 400;
            this.scaleTrackBar.Minimum = 10;
            this.scaleTrackBar.Name = "scaleTrackBar";
            this.scaleTrackBar.Size = new System.Drawing.Size(508, 45);
            this.scaleTrackBar.TabIndex = 1;
            this.scaleTrackBar.TickFrequency = 10;
            this.scaleTrackBar.Value = 50;
            this.scaleTrackBar.ValueChanged += new System.EventHandler(this.scaleTrackBar_ValueChanged);
            // 
            // previewPanel
            // 
            this.previewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.previewPanel.AutoScroll = true;
            this.previewPanel.BackColor = System.Drawing.Color.Silver;
            this.previewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewPanel.Controls.Add(this.barcodeStructureControl1);
            this.previewPanel.Location = new System.Drawing.Point(3, 62);
            this.previewPanel.Name = "previewPanel";
            this.previewPanel.Size = new System.Drawing.Size(511, 454);
            this.previewPanel.TabIndex = 4;
            this.previewPanel.SizeChanged += new System.EventHandler(this.previewPanel_SizeChanged);
            // 
            // barcodeStructureControl1
            // 
            this.barcodeStructureControl1.BarcodeRenderer = null;
            this.barcodeStructureControl1.Location = new System.Drawing.Point(43, 28);
            this.barcodeStructureControl1.Name = "barcodeStructureControl1";
            this.barcodeStructureControl1.RenderingScale = 5F;
            this.barcodeStructureControl1.Size = new System.Drawing.Size(150, 150);
            this.barcodeStructureControl1.TabIndex = 0;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(622, 527);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(703, 527);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // BarcodeDesignerForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.splitContainer1);
            this.Name = "BarcodeDesignerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Barcode Designer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scaleTrackBar)).EndInit();
            this.previewPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView barcodeStructureTreeView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel previewPanel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TrackBar scaleTrackBar;
        private Vintasoft.Barcode.BarcodeStructureViewer barcodeStructureControl1;
        private System.Windows.Forms.PropertyGrid barcodeRendererPropertyGrid;
        private System.Windows.Forms.ComboBox barcodeRendererComboBox;
    }
}