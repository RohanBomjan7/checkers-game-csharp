
using System.Windows.Forms;

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
            boardPanel = new System.Windows.Forms.FlowLayoutPanel();
            uxMenuStrip = new System.Windows.Forms.MenuStrip();
            uxfileToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            uxNewGame = new System.Windows.Forms.ToolStripMenuItem();
            uxStatusStrip = new System.Windows.Forms.StatusStrip();
            uxToolStripStatusLabel_Turn = new System.Windows.Forms.ToolStripStatusLabel();
            uxMenuStrip.SuspendLayout();
            uxStatusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // boardPanel
            // 
            boardPanel.Location = new System.Drawing.Point(12, 50);
            boardPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            boardPanel.Name = "boardPanel";
            boardPanel.Size = new System.Drawing.Size(547, 226);
            boardPanel.TabIndex = 0;
            // 
            // uxMenuStrip
            // 
            uxMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            uxMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { uxfileToolStripMenu });
            uxMenuStrip.Location = new System.Drawing.Point(0, 0);
            uxMenuStrip.Name = "uxMenuStrip";
            uxMenuStrip.Padding = new System.Windows.Forms.Padding(10, 4, 0, 4);
            uxMenuStrip.Size = new System.Drawing.Size(702, 37);
            uxMenuStrip.TabIndex = 1;
            uxMenuStrip.Text = "menuStrip1";
            // 
            // uxfileToolStripMenu
            // 
            uxfileToolStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { uxNewGame });
            uxfileToolStripMenu.Name = "uxfileToolStripMenu";
            uxfileToolStripMenu.Size = new System.Drawing.Size(54, 29);
            uxfileToolStripMenu.Text = "File";
            // 
            // uxNewGame
            // 
            uxNewGame.Name = "uxNewGame";
            uxNewGame.Size = new System.Drawing.Size(200, 34);
            uxNewGame.Text = "New Game";
            uxNewGame.Click += NewGame_Click;
            // 
            // uxStatusStrip
            // 
            uxStatusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            uxStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { uxToolStripStatusLabel_Turn });
            uxStatusStrip.Location = new System.Drawing.Point(0, 301);
            uxStatusStrip.Name = "uxStatusStrip";
            uxStatusStrip.Padding = new System.Windows.Forms.Padding(23, 0, 2, 0);
            uxStatusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            uxStatusStrip.Size = new System.Drawing.Size(702, 32);
            uxStatusStrip.TabIndex = 2;
            uxStatusStrip.Text = "statusStrip1";
            // 
            // uxToolStripStatusLabel_Turn
            // 
            uxToolStripStatusLabel_Turn.Name = "uxToolStripStatusLabel_Turn";
            uxToolStripStatusLabel_Turn.Size = new System.Drawing.Size(47, 25);
            uxToolStripStatusLabel_Turn.Text = "Turn";
            // 
            // UserInterface
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(702, 333);
            Controls.Add(uxStatusStrip);
            Controls.Add(boardPanel);
            Controls.Add(uxMenuStrip);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            MainMenuStrip = uxMenuStrip;
            Margin = new System.Windows.Forms.Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserInterface";
            Text = "Checkers";
            uxMenuStrip.ResumeLayout(false);
            uxMenuStrip.PerformLayout();
            uxStatusStrip.ResumeLayout(false);
            uxStatusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel boardPanel;
        private System.Windows.Forms.MenuStrip uxMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem uxfileToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem uxNewGame;
        private System.Windows.Forms.StatusStrip uxStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel uxToolStripStatusLabel_Turn;
    }
}

