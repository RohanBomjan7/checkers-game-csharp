
namespace KSU.CIS300.Checkers
{
    partial class UserInterface
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
            this.uxFlowLayoutPanel_board = new System.Windows.Forms.FlowLayoutPanel();
            this.uxMenuStrip = new System.Windows.Forms.MenuStrip();
            this.uxfileToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.uxNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.uxStatusStrip = new System.Windows.Forms.StatusStrip();
            this.uxToolStripStatusLabel_Turn = new System.Windows.Forms.ToolStripStatusLabel();
            this.uxMenuStrip.SuspendLayout();
            this.uxStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxFlowLayoutPanel_board
            // 
            this.uxFlowLayoutPanel_board.Location = new System.Drawing.Point(7, 26);
            this.uxFlowLayoutPanel_board.Margin = new System.Windows.Forms.Padding(2);
            this.uxFlowLayoutPanel_board.Name = "uxFlowLayoutPanel_board";
            this.uxFlowLayoutPanel_board.Size = new System.Drawing.Size(75, 42);
            this.uxFlowLayoutPanel_board.TabIndex = 0;
            // 
            // uxMenuStrip
            // 
            this.uxMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxfileToolStripMenu});
            this.uxMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.uxMenuStrip.Name = "uxMenuStrip";
            this.uxMenuStrip.Size = new System.Drawing.Size(176, 24);
            this.uxMenuStrip.TabIndex = 1;
            this.uxMenuStrip.Text = "menuStrip1";
            // 
            // uxfileToolStripMenu
            // 
            this.uxfileToolStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxNewGame});
            this.uxfileToolStripMenu.Name = "uxfileToolStripMenu";
            this.uxfileToolStripMenu.Size = new System.Drawing.Size(37, 20);
            this.uxfileToolStripMenu.Text = "File";
            // 
            // uxNewGame
            // 
            this.uxNewGame.Name = "uxNewGame";
            this.uxNewGame.Size = new System.Drawing.Size(132, 22);
            this.uxNewGame.Text = "New Game";
            this.uxNewGame.Click += new System.EventHandler(this.uxNewGame_Click);
            // 
            // uxStatusStrip
            // 
            this.uxStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxToolStripStatusLabel_Turn});
            this.uxStatusStrip.Location = new System.Drawing.Point(0, 151);
            this.uxStatusStrip.Name = "uxStatusStrip";
            this.uxStatusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.uxStatusStrip.Size = new System.Drawing.Size(176, 22);
            this.uxStatusStrip.TabIndex = 2;
            this.uxStatusStrip.Text = "statusStrip1";
            // 
            // uxToolStripStatusLabel_Turn
            // 
            this.uxToolStripStatusLabel_Turn.Name = "uxToolStripStatusLabel_Turn";
            this.uxToolStripStatusLabel_Turn.Size = new System.Drawing.Size(31, 17);
            this.uxToolStripStatusLabel_Turn.Text = "Turn";
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(176, 173);
            this.Controls.Add(this.uxStatusStrip);
            this.Controls.Add(this.uxFlowLayoutPanel_board);
            this.Controls.Add(this.uxMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.uxMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserInterface";
            this.Text = "Checkers";
            this.uxMenuStrip.ResumeLayout(false);
            this.uxMenuStrip.PerformLayout();
            this.uxStatusStrip.ResumeLayout(false);
            this.uxStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel uxFlowLayoutPanel_board;
        private System.Windows.Forms.MenuStrip uxMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem uxfileToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem uxNewGame;
        private System.Windows.Forms.StatusStrip uxStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel uxToolStripStatusLabel_Turn;
    }
}

