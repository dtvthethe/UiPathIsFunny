namespace UiPathIsFunny.View
{
    partial class FrmMain
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lsvConfig = new System.Windows.Forms.ListView();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConfigTag = new System.Windows.Forms.TextBox();
            this.btnBrowConfig = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnStart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtXAMpath = new System.Windows.Forms.TextBox();
            this.btnFolderXAML = new System.Windows.Forms.Button();
            this.lsvStatus = new System.Windows.Forms.ListView();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbExport = new System.Windows.Forms.ComboBox();
            this.chkDetail = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(833, 409);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.btnEdit);
            this.tabPage1.Controls.Add(this.lsvConfig);
            this.tabPage1.Controls.Add(this.btnRemove);
            this.tabPage1.Controls.Add(this.btnAdd);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtConfigTag);
            this.tabPage1.Controls.Add(this.btnBrowConfig);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(825, 383);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Config";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(520, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 23);
            this.btnSave.TabIndex = 26;
            this.btnSave.Text = "Save to Config";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(96, 54);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 25;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lsvConfig
            // 
            this.lsvConfig.FullRowSelect = true;
            this.lsvConfig.GridLines = true;
            this.lsvConfig.Location = new System.Drawing.Point(15, 83);
            this.lsvConfig.MultiSelect = false;
            this.lsvConfig.Name = "lsvConfig";
            this.lsvConfig.Size = new System.Drawing.Size(793, 284);
            this.lsvConfig.TabIndex = 24;
            this.lsvConfig.UseCompatibleStateImageBehavior = false;
            this.lsvConfig.View = System.Windows.Forms.View.Details;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(177, 54);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 23;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(15, 54);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 22;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Config tags:";
            // 
            // txtConfigTag
            // 
            this.txtConfigTag.Location = new System.Drawing.Point(87, 17);
            this.txtConfigTag.Name = "txtConfigTag";
            this.txtConfigTag.ReadOnly = true;
            this.txtConfigTag.Size = new System.Drawing.Size(300, 20);
            this.txtConfigTag.TabIndex = 20;
            // 
            // btnBrowConfig
            // 
            this.btnBrowConfig.Location = new System.Drawing.Point(425, 17);
            this.btnBrowConfig.Name = "btnBrowConfig";
            this.btnBrowConfig.Size = new System.Drawing.Size(75, 23);
            this.btnBrowConfig.TabIndex = 19;
            this.btnBrowConfig.Text = "Browser";
            this.btnBrowConfig.UseVisualStyleBackColor = true;
            this.btnBrowConfig.Click += new System.EventHandler(this.btnBrowConfig_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkDetail);
            this.tabPage2.Controls.Add(this.btnStart);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txtXAMpath);
            this.tabPage2.Controls.Add(this.btnFolderXAML);
            this.tabPage2.Controls.Add(this.lsvStatus);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.cmbExport);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(825, 383);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Make it Funny";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(426, 73);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 35;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "XAML folder:";
            // 
            // txtXAMpath
            // 
            this.txtXAMpath.Location = new System.Drawing.Point(109, 28);
            this.txtXAMpath.Name = "txtXAMpath";
            this.txtXAMpath.ReadOnly = true;
            this.txtXAMpath.Size = new System.Drawing.Size(311, 20);
            this.txtXAMpath.TabIndex = 33;
            // 
            // btnFolderXAML
            // 
            this.btnFolderXAML.Location = new System.Drawing.Point(426, 28);
            this.btnFolderXAML.Name = "btnFolderXAML";
            this.btnFolderXAML.Size = new System.Drawing.Size(75, 23);
            this.btnFolderXAML.TabIndex = 32;
            this.btnFolderXAML.Text = "Browser";
            this.btnFolderXAML.UseVisualStyleBackColor = true;
            this.btnFolderXAML.Click += new System.EventHandler(this.btnFolderXAML_Click);
            // 
            // lsvStatus
            // 
            this.lsvStatus.FullRowSelect = true;
            this.lsvStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvStatus.Location = new System.Drawing.Point(8, 165);
            this.lsvStatus.Name = "lsvStatus";
            this.lsvStatus.Size = new System.Drawing.Size(793, 191);
            this.lsvStatus.TabIndex = 31;
            this.lsvStatus.UseCompatibleStateImageBehavior = false;
            this.lsvStatus.View = System.Windows.Forms.View.Details;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Export to:";
            // 
            // cmbExport
            // 
            this.cmbExport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmbExport.Location = new System.Drawing.Point(96, 77);
            this.cmbExport.Name = "cmbExport";
            this.cmbExport.Size = new System.Drawing.Size(121, 21);
            this.cmbExport.TabIndex = 25;
            // 
            // chkDetail
            // 
            this.chkDetail.AutoSize = true;
            this.chkDetail.Location = new System.Drawing.Point(244, 79);
            this.chkDetail.Name = "chkDetail";
            this.chkDetail.Size = new System.Drawing.Size(84, 17);
            this.chkDetail.TabIndex = 36;
            this.chkDetail.Text = "Export detail";
            this.chkDetail.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(839, 410);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmMain";
            this.Text = "UiPath is Funny!";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConfigTag;
        private System.Windows.Forms.Button btnBrowConfig;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbExport;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ListView lsvConfig;
        private System.Windows.Forms.ListView lsvStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtXAMpath;
        private System.Windows.Forms.Button btnFolderXAML;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkDetail;
    }
}

