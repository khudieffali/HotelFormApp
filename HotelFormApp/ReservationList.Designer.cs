
namespace HotelFormApp
{
    partial class ReservationList
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
            this.dtgReservation = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgReservation)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgReservation
            // 
            this.dtgReservation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgReservation.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgReservation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgReservation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgReservation.Location = new System.Drawing.Point(0, 214);
            this.dtgReservation.Name = "dtgReservation";
            this.dtgReservation.RowHeadersWidth = 51;
            this.dtgReservation.RowTemplate.Height = 29;
            this.dtgReservation.Size = new System.Drawing.Size(931, 381);
            this.dtgReservation.TabIndex = 0;
            // 
            // ReservationList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.ClientSize = new System.Drawing.Size(931, 595);
            this.Controls.Add(this.dtgReservation);
            this.Name = "ReservationList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ReservationList";
            this.Load += new System.EventHandler(this.ReservationList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgReservation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgReservation;
    }
}