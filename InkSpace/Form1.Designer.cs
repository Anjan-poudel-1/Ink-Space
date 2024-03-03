namespace Component1Assignment
{
    partial class DrawShapes
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
            this.canvas = new System.Windows.Forms.PictureBox();
            this.codeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Logs = new System.Windows.Forms.Label();
            this.logsTextBox = new System.Windows.Forms.TextBox();
            this.executeBtn = new System.Windows.Forms.Button();
            this.chkSyntaxBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.commandLine = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pointerLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.SystemColors.Window;
            this.canvas.Location = new System.Drawing.Point(12, 64);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(867, 695);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // codeTextBox
            // 
            this.codeTextBox.AcceptsTab = true;
            this.codeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.codeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeTextBox.Location = new System.Drawing.Point(890, 61);
            this.codeTextBox.Multiline = true;
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.codeTextBox.Size = new System.Drawing.Size(507, 326);
            this.codeTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(578, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 2;
            // 
            // Logs
            // 
            this.Logs.AutoSize = true;
            this.Logs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Logs.Location = new System.Drawing.Point(885, 485);
            this.Logs.Name = "Logs";
            this.Logs.Size = new System.Drawing.Size(59, 25);
            this.Logs.TabIndex = 3;
            this.Logs.Text = "Logs";
            // 
            // logsTextBox
            // 
            this.logsTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.logsTextBox.Cursor = System.Windows.Forms.Cursors.No;
            this.logsTextBox.Location = new System.Drawing.Point(890, 513);
            this.logsTextBox.Multiline = true;
            this.logsTextBox.Name = "logsTextBox";
            this.logsTextBox.ReadOnly = true;
            this.logsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logsTextBox.Size = new System.Drawing.Size(507, 246);
            this.logsTextBox.TabIndex = 4;
            // 
            // executeBtn
            // 
            this.executeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.executeBtn.Location = new System.Drawing.Point(890, 430);
            this.executeBtn.Name = "executeBtn";
            this.executeBtn.Size = new System.Drawing.Size(247, 39);
            this.executeBtn.TabIndex = 5;
            this.executeBtn.Text = "Execute";
            this.executeBtn.UseVisualStyleBackColor = true;
            this.executeBtn.Click += new System.EventHandler(this.executeBtn_Click);
            // 
            // chkSyntaxBtn
            // 
            this.chkSyntaxBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSyntaxBtn.Location = new System.Drawing.Point(1143, 430);
            this.chkSyntaxBtn.Name = "chkSyntaxBtn";
            this.chkSyntaxBtn.Size = new System.Drawing.Size(254, 38);
            this.chkSyntaxBtn.TabIndex = 6;
            this.chkSyntaxBtn.Text = "Check Syntax";
            this.chkSyntaxBtn.UseVisualStyleBackColor = true;
            this.chkSyntaxBtn.Click += new System.EventHandler(this.chkSyntaxBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(887, 394);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Command Line: ";
            // 
            // commandLine
            // 
            this.commandLine.Location = new System.Drawing.Point(1037, 393);
            this.commandLine.Name = "commandLine";
            this.commandLine.Size = new System.Drawing.Size(360, 22);
            this.commandLine.TabIndex = 8;
            this.commandLine.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.commandLine_KeyPress);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1409, 28);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.helpToolStripMenuItem.Text = "Info";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.infoToolStripMenuItem.Text = "Help";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(887, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Program Code";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 18);
            this.label4.TabIndex = 11;
            this.label4.Text = "Canvas";
            // 
            // pointerLabel
            // 
            this.pointerLabel.AutoSize = true;
            this.pointerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pointerLabel.Location = new System.Drawing.Point(495, 42);
            this.pointerLabel.Name = "pointerLabel";
            this.pointerLabel.Size = new System.Drawing.Size(102, 18);
            this.pointerLabel.TabIndex = 12;
            this.pointerLabel.Text = "Pointer (0,0)";
            // 
            // DrawShapes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(1409, 771);
            this.Controls.Add(this.pointerLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.commandLine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkSyntaxBtn);
            this.Controls.Add(this.executeBtn);
            this.Controls.Add(this.logsTextBox);
            this.Controls.Add(this.Logs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.codeTextBox);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DrawShapes";
            this.Text = "DrawShapes";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.TextBox codeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Logs;
        private System.Windows.Forms.TextBox logsTextBox;
        private System.Windows.Forms.Button executeBtn;
        private System.Windows.Forms.Button chkSyntaxBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox commandLine;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label pointerLabel;
    }
}

