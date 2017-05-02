namespace Drone_Organizer
{
    partial class NewFrame
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
            this.ux_frame_number = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.ux_ok = new System.Windows.Forms.Button();
            this.ux_cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ux_frame_number)).BeginInit();
            this.SuspendLayout();
            // 
            // ux_frame_number
            // 
            this.ux_frame_number.Location = new System.Drawing.Point(12, 34);
            this.ux_frame_number.Name = "ux_frame_number";
            this.ux_frame_number.Size = new System.Drawing.Size(120, 20);
            this.ux_frame_number.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Frame Number";
            // 
            // ux_ok
            // 
            this.ux_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ux_ok.Location = new System.Drawing.Point(153, 8);
            this.ux_ok.Name = "ux_ok";
            this.ux_ok.Size = new System.Drawing.Size(75, 23);
            this.ux_ok.TabIndex = 2;
            this.ux_ok.Text = "Ok";
            this.ux_ok.UseVisualStyleBackColor = true;
            this.ux_ok.Click += new System.EventHandler(this.ux_ok_Click);
            // 
            // ux_cancel
            // 
            this.ux_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ux_cancel.Location = new System.Drawing.Point(153, 37);
            this.ux_cancel.Name = "ux_cancel";
            this.ux_cancel.Size = new System.Drawing.Size(75, 23);
            this.ux_cancel.TabIndex = 3;
            this.ux_cancel.Text = "Cancel";
            this.ux_cancel.UseVisualStyleBackColor = true;
            // 
            // NewFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 72);
            this.Controls.Add(this.ux_cancel);
            this.Controls.Add(this.ux_ok);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ux_frame_number);
            this.Name = "NewFrame";
            this.Text = "NewFrame";
            ((System.ComponentModel.ISupportInitialize)(this.ux_frame_number)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown ux_frame_number;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ux_ok;
        private System.Windows.Forms.Button ux_cancel;
    }
}