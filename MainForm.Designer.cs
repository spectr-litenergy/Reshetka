using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reshetka
{
	partial class MainForm
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private Label labelTitle;
		private Label labelSize;
		private NumericUpDown numericSize;
		private Label labelMask;
		private TextBox textMask;
		private Label labelPlain;
		private TextBox textPlain;
		private Label labelCipher;
		private TextBox textCipher;
		private Button buttonEncrypt;
		private Button buttonDecrypt;
		private Button buttonSwap;
		private Button buttonClear;
		private Button buttonVisualize;

        private void InitializeComponent()
        {
            labelTitle = new Label();
            labelSize = new Label();
            numericSize = new NumericUpDown();
            labelMask = new Label();
            textMask = new TextBox();
            labelPlain = new Label();
            textPlain = new TextBox();
            labelCipher = new Label();
            textCipher = new TextBox();
            buttonEncrypt = new Button();
            buttonDecrypt = new Button();
            buttonSwap = new Button();
            buttonClear = new Button();
            buttonVisualize = new Button();
            ((System.ComponentModel.ISupportInitialize)numericSize).BeginInit();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTitle.Location = new Point(14, 12);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(369, 28);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Шифр решётки (Cardan grille cipher)";
            // 
            // labelSize
            // 
            labelSize.AutoSize = true;
            labelSize.Location = new Point(16, 59);
            labelSize.Name = "labelSize";
            labelSize.Size = new Size(172, 20);
            labelSize.TabIndex = 1;
            labelSize.Text = "Размер решётки (N×N):";
            // 
            // numericSize
            // 
            numericSize.Location = new Point(181, 56);
            numericSize.Margin = new Padding(3, 4, 3, 4);
            numericSize.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numericSize.Minimum = new decimal(new int[] { 4, 0, 0, 0 });
            numericSize.Name = "numericSize";
            numericSize.Size = new Size(73, 27);
            numericSize.TabIndex = 2;
            numericSize.Value = new decimal(new int[] { 4, 0, 0, 0 });
            numericSize.ValueChanged += numericSize_ValueChanged;
            numericSize.Validated += numericSize_Validated;
            // 
            // labelMask
            // 
            labelMask.AutoSize = true;
            labelMask.Location = new Point(16, 104);
            labelMask.Name = "labelMask";
            labelMask.Size = new Size(305, 20);
            labelMask.TabIndex = 3;
            labelMask.Text = "Маска решётки (N строк, символы X или .):";
            // 
            // textMask
            // 
            textMask.AcceptsReturn = true;
            textMask.AcceptsTab = true;
            textMask.Font = new Font("Consolas", 10F);
            textMask.Location = new Point(16, 128);
            textMask.Margin = new Padding(3, 4, 3, 4);
            textMask.Multiline = true;
            textMask.Name = "textMask";
            textMask.ReadOnly = true;
            textMask.ScrollBars = ScrollBars.Vertical;
            textMask.Size = new Size(342, 399);
            textMask.TabIndex = 4;
            // 
            // labelPlain
            // 
            labelPlain.AutoSize = true;
            labelPlain.Location = new Point(377, 59);
            labelPlain.Name = "labelPlain";
            labelPlain.Size = new Size(121, 20);
            labelPlain.TabIndex = 5;
            labelPlain.Text = "Исходный текст:";
            // 
            // textPlain
            // 
            textPlain.Location = new Point(377, 83);
            textPlain.Margin = new Padding(3, 4, 3, 4);
            textPlain.Multiline = true;
            textPlain.Name = "textPlain";
            textPlain.ScrollBars = ScrollBars.Vertical;
            textPlain.Size = new Size(479, 212);
            textPlain.TabIndex = 6;
            // 
            // labelCipher
            // 
            labelCipher.AutoSize = true;
            labelCipher.Location = new Point(377, 313);
            labelCipher.Name = "labelCipher";
            labelCipher.Size = new Size(169, 20);
            labelCipher.TabIndex = 7;
            labelCipher.Text = "Зашифрованный текст:";
            // 
            // textCipher
            // 
            textCipher.Location = new Point(377, 337);
            textCipher.Margin = new Padding(3, 4, 3, 4);
            textCipher.Multiline = true;
            textCipher.Name = "textCipher";
            textCipher.ScrollBars = ScrollBars.Vertical;
            textCipher.Size = new Size(479, 212);
            textCipher.TabIndex = 8;
            // 
            // buttonEncrypt
            // 
            buttonEncrypt.Location = new Point(377, 572);
            buttonEncrypt.Margin = new Padding(3, 4, 3, 4);
            buttonEncrypt.Name = "buttonEncrypt";
            buttonEncrypt.Size = new Size(137, 43);
            buttonEncrypt.TabIndex = 9;
            buttonEncrypt.Text = "Зашифровать →";
            buttonEncrypt.UseVisualStyleBackColor = true;
            buttonEncrypt.Click += buttonEncrypt_Click;
            // 
            // buttonDecrypt
            // 
            buttonDecrypt.Location = new Point(521, 572);
            buttonDecrypt.Margin = new Padding(3, 4, 3, 4);
            buttonDecrypt.Name = "buttonDecrypt";
            buttonDecrypt.Size = new Size(137, 43);
            buttonDecrypt.TabIndex = 10;
            buttonDecrypt.Text = "← Расшифровать";
            buttonDecrypt.UseVisualStyleBackColor = true;
            buttonDecrypt.Click += buttonDecrypt_Click;
            // 
            // buttonSwap
            // 
            buttonSwap.Location = new Point(665, 572);
            buttonSwap.Margin = new Padding(3, 4, 3, 4);
            buttonSwap.Name = "buttonSwap";
            buttonSwap.Size = new Size(96, 43);
            buttonSwap.TabIndex = 11;
            buttonSwap.Text = "Поменять";
            buttonSwap.UseVisualStyleBackColor = true;
            buttonSwap.Click += buttonSwap_Click;
            // 
            // buttonClear
            // 
            buttonClear.Location = new Point(768, 572);
            buttonClear.Margin = new Padding(3, 4, 3, 4);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(89, 43);
            buttonClear.TabIndex = 12;
            buttonClear.Text = "Очистить";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // buttonVisualize
            // 
            buttonVisualize.Location = new Point(16, 572);
            buttonVisualize.Margin = new Padding(3, 4, 3, 4);
            buttonVisualize.Name = "buttonVisualize";
            buttonVisualize.Size = new Size(343, 43);
            buttonVisualize.TabIndex = 13;
            buttonVisualize.Text = "Визуализация решётки...";
            buttonVisualize.UseVisualStyleBackColor = true;
            buttonVisualize.Click += buttonVisualize_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 640);
            Controls.Add(buttonVisualize);
            Controls.Add(buttonClear);
            Controls.Add(buttonSwap);
            Controls.Add(buttonDecrypt);
            Controls.Add(buttonEncrypt);
            Controls.Add(textCipher);
            Controls.Add(labelCipher);
            Controls.Add(textPlain);
            Controls.Add(labelPlain);
            Controls.Add(textMask);
            Controls.Add(labelMask);
            Controls.Add(numericSize);
            Controls.Add(labelSize);
            Controls.Add(labelTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimumSize = new Size(893, 676);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Шифр решётки";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)numericSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
