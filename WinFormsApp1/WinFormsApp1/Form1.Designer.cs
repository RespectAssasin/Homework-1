namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            groupBox1 = new GroupBox();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            dateTimePicker1 = new DateTimePicker();
            TBFam = new TextBox();
            TBName = new TextBox();
            TBOt4 = new TextBox();
            TBStr = new TextBox();
            TBCity = new TextBox();
            TBPhone = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 378);
            button1.Name = "button1";
            button1.Size = new Size(326, 23);
            button1.TabIndex = 0;
            button1.Text = "ПОСОМТРЕТЬ РЕЗУЛЬТАТЫ";
            button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(dateTimePicker1);
            groupBox1.Controls.Add(TBFam);
            groupBox1.Controls.Add(TBName);
            groupBox1.Controls.Add(TBOt4);
            groupBox1.Controls.Add(TBStr);
            groupBox1.Controls.Add(TBCity);
            groupBox1.Controls.Add(TBPhone);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(326, 360);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Анкета";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(218, 323);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(51, 19);
            radioButton2.TabIndex = 17;
            radioButton2.TabStop = true;
            radioButton2.Text = "Муж";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(118, 323);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(49, 19);
            radioButton1.TabIndex = 16;
            radioButton1.TabStop = true;
            radioButton1.Text = "Жен";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(118, 283);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(202, 23);
            dateTimePicker1.TabIndex = 15;
            // 
            // TBFam
            // 
            TBFam.Location = new Point(118, 43);
            TBFam.Name = "TBFam";
            TBFam.Size = new Size(202, 23);
            TBFam.TabIndex = 14;
            // 
            // TBName
            // 
            TBName.Location = new Point(118, 78);
            TBName.Name = "TBName";
            TBName.Size = new Size(202, 23);
            TBName.TabIndex = 13;
            // 
            // TBOt4
            // 
            TBOt4.Location = new Point(118, 115);
            TBOt4.Name = "TBOt4";
            TBOt4.Size = new Size(202, 23);
            TBOt4.TabIndex = 12;
            // 
            // TBStr
            // 
            TBStr.Location = new Point(118, 171);
            TBStr.Name = "TBStr";
            TBStr.Size = new Size(202, 23);
            TBStr.TabIndex = 11;
            // 
            // TBCity
            // 
            TBCity.Location = new Point(118, 201);
            TBCity.Name = "TBCity";
            TBCity.Size = new Size(202, 23);
            TBCity.TabIndex = 10;
            // 
            // TBPhone
            // 
            TBPhone.Location = new Point(118, 231);
            TBPhone.Name = "TBPhone";
            TBPhone.PasswordChar = '*';
            TBPhone.Size = new Size(202, 23);
            TBPhone.TabIndex = 9;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(22, 325);
            label8.Name = "label8";
            label8.Size = new Size(30, 15);
            label8.TabIndex = 8;
            label8.Text = "Пол";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(22, 289);
            label7.Name = "label7";
            label7.Size = new Size(90, 15);
            label7.TabIndex = 7;
            label7.Text = "Дата рождения";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(22, 234);
            label6.Name = "label6";
            label6.Size = new Size(55, 15);
            label6.TabIndex = 6;
            label6.Text = "Телефон";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(22, 204);
            label5.Name = "label5";
            label5.Size = new Size(40, 15);
            label5.TabIndex = 5;
            label5.Text = "Город";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 174);
            label4.Name = "label4";
            label4.Size = new Size(46, 15);
            label4.TabIndex = 4;
            label4.Text = "Страна";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 118);
            label3.Name = "label3";
            label3.Size = new Size(58, 15);
            label3.TabIndex = 3;
            label3.Text = "Отчество";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 81);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 2;
            label2.Text = "Имя";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 46);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 0;
            label1.Text = "Фамилия";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 405);
            Controls.Add(groupBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Анкета";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private TextBox TBStr;
        private TextBox TBCity;
        private TextBox TBPhone;
        private TextBox TBFam;
        private TextBox TBName;
        private TextBox TBOt4;
        private RadioButton radioButton1;
        private DateTimePicker dateTimePicker1;
        private RadioButton radioButton2;
    }
}
