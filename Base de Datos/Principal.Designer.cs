﻿namespace Base_de_Datos
{
    partial class Principal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.nBD = new System.Windows.Forms.Label();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.maximiza = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.creaTabla = new System.Windows.Forms.ToolStripButton();
            this.modificaTabla = new System.Windows.Forms.ToolStripButton();
            this.eliminaTabla = new System.Windows.Forms.ToolStripButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.abrir = new System.Windows.Forms.ToolStripButton();
            this.modificaBD = new System.Windows.Forms.ToolStripButton();
            this.eliminaBD = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximiza)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 81);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Menu Base de Datos";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator2,
            this.toolStripSeparator3,
            this.abrir,
            this.toolStripSeparator1,
            this.modificaBD,
            this.toolStripSeparator6,
            this.eliminaBD});
            this.toolStrip1.Location = new System.Drawing.Point(9, 21);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(180, 39);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Controls.Add(this.pictureBox2);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(15, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(165, 265);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tablas";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Location = new System.Drawing.Point(29, 36);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(130, 208);
            this.listBox1.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.toolStrip2);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(235, 58);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(145, 81);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Menu Tablas";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creaTabla,
            this.toolStripSeparator4,
            this.modificaTabla,
            this.toolStripSeparator5,
            this.eliminaTabla});
            this.toolStrip2.Location = new System.Drawing.Point(3, 21);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(132, 39);
            this.toolStrip2.TabIndex = 6;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // nBD
            // 
            this.nBD.AutoSize = true;
            this.nBD.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nBD.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.nBD.Location = new System.Drawing.Point(18, 151);
            this.nBD.Name = "nBD";
            this.nBD.Size = new System.Drawing.Size(37, 17);
            this.nBD.TabIndex = 6;
            this.nBD.Text = "BD :";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 39);
            // 
            // maximiza
            // 
            this.maximiza.Image = global::Base_de_Datos.Properties.Resources.maximizar;
            this.maximiza.Location = new System.Drawing.Point(936, 12);
            this.maximiza.Name = "maximiza";
            this.maximiza.Size = new System.Drawing.Size(29, 31);
            this.maximiza.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.maximiza.TabIndex = 10;
            this.maximiza.TabStop = false;
            this.maximiza.Click += new System.EventHandler(this.maximizar);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::Base_de_Datos.Properties.Resources.azul_medio1497605622;
            this.pictureBox6.Location = new System.Drawing.Point(-1, 49);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(10, 579);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 9;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Base_de_Datos.Properties.Resources.azul_medio1497605622;
            this.pictureBox5.Location = new System.Drawing.Point(-10, 593);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(1040, 10);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 8;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Base_de_Datos.Properties.Resources.azul_medio1497605622;
            this.pictureBox4.Location = new System.Drawing.Point(1011, 49);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(10, 579);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 7;
            this.pictureBox4.TabStop = false;
            // 
            // creaTabla
            // 
            this.creaTabla.AccessibleName = "creaTabla";
            this.creaTabla.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.creaTabla.Image = ((System.Drawing.Image)(resources.GetObject("creaTabla.Image")));
            this.creaTabla.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.creaTabla.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.creaTabla.Name = "creaTabla";
            this.creaTabla.Size = new System.Drawing.Size(36, 36);
            this.creaTabla.Text = "Crea Tabla";
            // 
            // modificaTabla
            // 
            this.modificaTabla.AccessibleName = "modificaTabla";
            this.modificaTabla.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.modificaTabla.Image = ((System.Drawing.Image)(resources.GetObject("modificaTabla.Image")));
            this.modificaTabla.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.modificaTabla.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.modificaTabla.Name = "modificaTabla";
            this.modificaTabla.Size = new System.Drawing.Size(36, 36);
            this.modificaTabla.Text = "Modifica Tabla";
            // 
            // eliminaTabla
            // 
            this.eliminaTabla.AccessibleName = "eliminaTabla";
            this.eliminaTabla.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.eliminaTabla.Image = ((System.Drawing.Image)(resources.GetObject("eliminaTabla.Image")));
            this.eliminaTabla.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.eliminaTabla.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eliminaTabla.Name = "eliminaTabla";
            this.eliminaTabla.Size = new System.Drawing.Size(36, 36);
            this.eliminaTabla.Text = "Elimina Tabla";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(983, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(29, 31);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.salir);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Base_de_Datos.Properties.Resources.azul_medio1497605622;
            this.pictureBox2.Location = new System.Drawing.Point(6, 36);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(139, 208);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AccessibleName = "nueva";
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton1.Text = "Nueva Base de Datos";
            // 
            // abrir
            // 
            this.abrir.AccessibleName = "abrir";
            this.abrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.abrir.Image = global::Base_de_Datos.Properties.Resources.abrir__1_;
            this.abrir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.abrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.abrir.Name = "abrir";
            this.abrir.Size = new System.Drawing.Size(36, 36);
            this.abrir.Text = "Abrir Base de Datos";
            // 
            // modificaBD
            // 
            this.modificaBD.AccessibleName = "modificaBD";
            this.modificaBD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.modificaBD.Image = ((System.Drawing.Image)(resources.GetObject("modificaBD.Image")));
            this.modificaBD.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.modificaBD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.modificaBD.Name = "modificaBD";
            this.modificaBD.Size = new System.Drawing.Size(36, 36);
            this.modificaBD.Text = "Renombrar Base de Datos";
            // 
            // eliminaBD
            // 
            this.eliminaBD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.eliminaBD.Image = ((System.Drawing.Image)(resources.GetObject("eliminaBD.Image")));
            this.eliminaBD.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.eliminaBD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eliminaBD.Name = "eliminaBD";
            this.eliminaBD.Size = new System.Drawing.Size(36, 36);
            this.eliminaBD.Text = "Eliminar Base de Datos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Rockwell Condensed", 30F);
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(449, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 47);
            this.label1.TabIndex = 1;
            this.label1.Text = "DaberanBD";
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label1_MouseMove);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-1, -6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1050, 58);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mueveVentana);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this.Controls.Add(this.maximiza);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.nBD);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Principal";
            this.Resize += new System.EventHandler(this.Principal_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximiza)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton abrir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton eliminaBD;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton creaTabla;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton modificaTabla;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton eliminaTabla;
        private System.Windows.Forms.Label nBD;
        private System.Windows.Forms.ToolStripButton modificaBD;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox maximiza;
    }
}

