namespace TcpSendFiles
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param message="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStartServer = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxSend = new System.Windows.Forms.TextBox();
            this.buttonSendData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAddFile = new System.Windows.Forms.Button();
            this.labelFileName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxIPserver = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStartServer
            // 
            this.buttonStartServer.Location = new System.Drawing.Point(485, 34);
            this.buttonStartServer.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStartServer.Name = "buttonStartServer";
            this.buttonStartServer.Size = new System.Drawing.Size(129, 57);
            this.buttonStartServer.TabIndex = 0;
            this.buttonStartServer.Text = "Запуск сервера";
            this.buttonStartServer.UseVisualStyleBackColor = true;
            this.buttonStartServer.Click += new System.EventHandler(this.buttonStartServer_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(33, 191);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(323, 260);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(622, 34);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(4);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(131, 57);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Подключение клиента";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxSend
            // 
            this.textBoxSend.Location = new System.Drawing.Point(407, 191);
            this.textBoxSend.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSend.Multiline = true;
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.Size = new System.Drawing.Size(335, 260);
            this.textBoxSend.TabIndex = 3;
            // 
            // buttonSendData
            // 
            this.buttonSendData.Location = new System.Drawing.Point(559, 564);
            this.buttonSendData.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSendData.Name = "buttonSendData";
            this.buttonSendData.Size = new System.Drawing.Size(183, 28);
            this.buttonSendData.TabIndex = 4;
            this.buttonSendData.Text = "Отправить сообщение";
            this.buttonSendData.UseVisualStyleBackColor = true;
            this.buttonSendData.Click += new System.EventHandler(this.buttonSendData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 158);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Получение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(404, 158);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Отправка";
            // 
            // buttonAddFile
            // 
            this.buttonAddFile.Location = new System.Drawing.Point(407, 564);
            this.buttonAddFile.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddFile.Name = "buttonAddFile";
            this.buttonAddFile.Size = new System.Drawing.Size(133, 28);
            this.buttonAddFile.TabIndex = 4;
            this.buttonAddFile.Text = "Добавить файл";
            this.buttonAddFile.UseVisualStyleBackColor = true;
            this.buttonAddFile.Click += new System.EventHandler(this.buttonAddFile_Click);
            // 
            // labelFileName
            // 
            this.labelFileName.Location = new System.Drawing.Point(8, 17);
            this.labelFileName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(307, 20);
            this.labelFileName.TabIndex = 10;
            this.labelFileName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelFileName);
            this.groupBox1.Location = new System.Drawing.Point(407, 503);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(335, 39);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Файл";
            // 
            // textBoxIPserver
            // 
            this.textBoxIPserver.Location = new System.Drawing.Point(175, 116);
            this.textBoxIPserver.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxIPserver.Name = "textBoxIPserver";
            this.textBoxIPserver.Size = new System.Drawing.Size(132, 22);
            this.textBoxIPserver.TabIndex = 8;
            this.textBoxIPserver.Text = "127.0.0.1";
            this.textBoxIPserver.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 119);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "IP адрес сервера";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 605);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxIPserver);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAddFile);
            this.Controls.Add(this.buttonSendData);
            this.Controls.Add(this.textBoxSend);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonStartServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "TCP - отправка файлов по сети";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStartServer;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonSendData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAddFile;
        public System.Windows.Forms.TextBox textBoxSend;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.TextBox textBoxIPserver;
        private System.Windows.Forms.Label label3;
    }
}

