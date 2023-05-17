using System.Windows.Forms;

namespace Files_Net
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBack = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.filterBtn = new System.Windows.Forms.Button();
            this.filterByDateBtn = new System.Windows.Forms.Button();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.btnViewDiskProperties = new System.Windows.Forms.Button();
            this.btnViewDirectoryProperties = new System.Windows.Forms.Button();
            this.btnViewFileProperties = new System.Windows.Forms.Button();
            this.btnViewImage = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.btnViewSecurity = new System.Windows.Forms.Button();
            this.btnViewTextFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 8);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "<<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(93, 8);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(75, 23);
            this.btnForward.TabIndex = 1;
            this.btnForward.Text = ">>";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(174, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Path: ";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(747, 8);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(153, 27);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(221, 9);
            this.txtInput.Name = "txtInput";
            this.txtInput.ReadOnly = true;
            this.txtInput.Size = new System.Drawing.Size(507, 22);
            this.txtInput.TabIndex = 4;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(12, 41);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(716, 397);
            this.webBrowser1.TabIndex = 5;
            // 
            // filterBtn
            // 
            this.filterBtn.Location = new System.Drawing.Point(747, 41);
            this.filterBtn.Name = "filterBtn";
            this.filterBtn.Size = new System.Drawing.Size(153, 27);
            this.filterBtn.TabIndex = 6;
            this.filterBtn.Text = "Filter files";
            this.filterBtn.UseVisualStyleBackColor = true;
            this.filterBtn.Click += new System.EventHandler(this.filterBtn_Click);
            // 
            // filterByDateBtn
            // 
            this.filterByDateBtn.Location = new System.Drawing.Point(747, 74);
            this.filterByDateBtn.Name = "filterByDateBtn";
            this.filterByDateBtn.Size = new System.Drawing.Size(153, 26);
            this.filterByDateBtn.TabIndex = 7;
            this.filterByDateBtn.Text = "Filter directory";
            this.filterByDateBtn.UseVisualStyleBackColor = true;
            this.filterByDateBtn.Click += new System.EventHandler(this.filterByDateBtn_Click);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(747, 106);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(153, 22);
            this.dateTimePicker.TabIndex = 8;
            // 
            // btnViewDiskProperties
            // 
            this.btnViewDiskProperties.Location = new System.Drawing.Point(747, 134);
            this.btnViewDiskProperties.Name = "btnViewDiskProperties";
            this.btnViewDiskProperties.Size = new System.Drawing.Size(153, 27);
            this.btnViewDiskProperties.TabIndex = 9;
            this.btnViewDiskProperties.Text = "Disk Properties";
            this.btnViewDiskProperties.UseVisualStyleBackColor = true;
            this.btnViewDiskProperties.Click += new System.EventHandler(this.btnViewDiskProperties_Click);
            // 
            // btnViewDirectoryProperties
            // 
            this.btnViewDirectoryProperties.Location = new System.Drawing.Point(747, 168);
            this.btnViewDirectoryProperties.Name = "btnViewDirectoryProperties";
            this.btnViewDirectoryProperties.Size = new System.Drawing.Size(153, 26);
            this.btnViewDirectoryProperties.TabIndex = 10;
            this.btnViewDirectoryProperties.Text = "Directory Properties";
            this.btnViewDirectoryProperties.UseVisualStyleBackColor = true;
            this.btnViewDirectoryProperties.Click += new System.EventHandler(this.btnViewDirectoryProperties_Click);
            // 
            // btnViewFileProperties
            // 
            this.btnViewFileProperties.Location = new System.Drawing.Point(747, 200);
            this.btnViewFileProperties.Name = "btnViewFileProperties";
            this.btnViewFileProperties.Size = new System.Drawing.Size(152, 25);
            this.btnViewFileProperties.TabIndex = 11;
            this.btnViewFileProperties.Text = "File Properties";
            this.btnViewFileProperties.UseVisualStyleBackColor = true;
            this.btnViewFileProperties.Click += new System.EventHandler(this.btnViewFileProperties_Click);
            // 
            // btnViewImage
            // 
            this.btnViewImage.Location = new System.Drawing.Point(747, 231);
            this.btnViewImage.Name = "btnViewImage";
            this.btnViewImage.Size = new System.Drawing.Size(152, 27);
            this.btnViewImage.TabIndex = 12;
            this.btnViewImage.Text = "View Image";
            this.btnViewImage.UseVisualStyleBackColor = true;
            this.btnViewImage.Click += new System.EventHandler(this.btnViewImage_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox.Location = new System.Drawing.Point(906, 8);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(306, 430);
            this.pictureBox.TabIndex = 13;
            this.pictureBox.TabStop = false;
            // 
            // btnViewSecurity
            // 
            this.btnViewSecurity.Location = new System.Drawing.Point(747, 265);
            this.btnViewSecurity.Name = "btnViewSecurity";
            this.btnViewSecurity.Size = new System.Drawing.Size(153, 27);
            this.btnViewSecurity.TabIndex = 14;
            this.btnViewSecurity.Text = "View Security";
            this.btnViewSecurity.UseVisualStyleBackColor = true;
            this.btnViewSecurity.Click += new System.EventHandler(this.btnViewSecurity_Click);
            // 
            // btnViewTextFile
            // 
            this.btnViewTextFile.Location = new System.Drawing.Point(747, 299);
            this.btnViewTextFile.Name = "btnViewTextFile";
            this.btnViewTextFile.Size = new System.Drawing.Size(153, 27);
            this.btnViewTextFile.TabIndex = 15;
            this.btnViewTextFile.Text = "View Text Files";
            this.btnViewTextFile.UseVisualStyleBackColor = true;
            this.btnViewTextFile.Click += new System.EventHandler(this.btnViewTextFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 450);
            this.Controls.Add(this.btnViewTextFile);
            this.Controls.Add(this.btnViewSecurity);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.btnViewImage);
            this.Controls.Add(this.btnViewFileProperties);
            this.Controls.Add(this.btnViewDirectoryProperties);
            this.Controls.Add(this.btnViewDiskProperties);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.filterByDateBtn);
            this.Controls.Add(this.filterBtn);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnBack);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnBack;
        private Button btnForward;
        private Label label1;
        private Button btnOpen;
        private TextBox txtInput;
        private WebBrowser webBrowser1;
        private Button filterBtn;
        private Button filterByDateBtn;
        private DateTimePicker dateTimePicker;
        private Button btnViewDiskProperties;
        private Button btnViewDirectoryProperties;
        private Button btnViewFileProperties;
        private Button btnViewImage;
        private PictureBox pictureBox;
        private Button btnViewSecurity;
        private Button btnViewTextFile;
    }
}

