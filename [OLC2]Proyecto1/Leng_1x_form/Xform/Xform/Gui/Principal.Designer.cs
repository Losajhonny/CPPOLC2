namespace Xform.Gui
{
    partial class Principal
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
            this.menubar = new System.Windows.Forms.MenuStrip();
            this.mi_archivo = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_abrir = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_guardar = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_guardarcomo = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_form = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_nuevo = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_cerrar = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_reporte = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_errores = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_manual = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_tecnio = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_usuario = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_gramatica = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_cargar = new System.Windows.Forms.Button();
            this.btn_generar = new System.Windows.Forms.Button();
            this.btn_verform = new System.Windows.Forms.Button();
            this.tab_control = new System.Windows.Forms.TabControl();
            this.rtb_consola = new System.Windows.Forms.RichTextBox();
            this.menubar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menubar
            // 
            this.menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_archivo,
            this.mi_form,
            this.mi_reporte,
            this.mi_manual});
            this.menubar.Location = new System.Drawing.Point(0, 0);
            this.menubar.Name = "menubar";
            this.menubar.Size = new System.Drawing.Size(781, 24);
            this.menubar.TabIndex = 2;
            this.menubar.Text = "menuStrip1";
            // 
            // mi_archivo
            // 
            this.mi_archivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_abrir,
            this.mi_guardar,
            this.mi_guardarcomo});
            this.mi_archivo.Name = "mi_archivo";
            this.mi_archivo.Size = new System.Drawing.Size(60, 20);
            this.mi_archivo.Text = "Archivo";
            // 
            // mi_abrir
            // 
            this.mi_abrir.Name = "mi_abrir";
            this.mi_abrir.Size = new System.Drawing.Size(152, 22);
            this.mi_abrir.Text = "Abrir";
            // 
            // mi_guardar
            // 
            this.mi_guardar.Name = "mi_guardar";
            this.mi_guardar.Size = new System.Drawing.Size(152, 22);
            this.mi_guardar.Text = "Guardar";
            // 
            // mi_guardarcomo
            // 
            this.mi_guardarcomo.Name = "mi_guardarcomo";
            this.mi_guardarcomo.Size = new System.Drawing.Size(152, 22);
            this.mi_guardarcomo.Text = "Guardar Como";
            // 
            // mi_form
            // 
            this.mi_form.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_nuevo,
            this.mi_cerrar});
            this.mi_form.Name = "mi_form";
            this.mi_form.Size = new System.Drawing.Size(77, 20);
            this.mi_form.Text = "Formulario";
            // 
            // mi_nuevo
            // 
            this.mi_nuevo.Name = "mi_nuevo";
            this.mi_nuevo.Size = new System.Drawing.Size(152, 22);
            this.mi_nuevo.Text = "Nueva Pestaña";
            this.mi_nuevo.Click += new System.EventHandler(this.mi_nuevo_Click);
            // 
            // mi_cerrar
            // 
            this.mi_cerrar.Name = "mi_cerrar";
            this.mi_cerrar.Size = new System.Drawing.Size(152, 22);
            this.mi_cerrar.Text = "Cerrar Pestaña";
            this.mi_cerrar.Click += new System.EventHandler(this.mi_cerrar_Click);
            // 
            // mi_reporte
            // 
            this.mi_reporte.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_errores});
            this.mi_reporte.Name = "mi_reporte";
            this.mi_reporte.Size = new System.Drawing.Size(60, 20);
            this.mi_reporte.Text = "Reporte";
            // 
            // mi_errores
            // 
            this.mi_errores.Name = "mi_errores";
            this.mi_errores.Size = new System.Drawing.Size(152, 22);
            this.mi_errores.Text = "Tabla Errores";
            this.mi_errores.Click += new System.EventHandler(this.mi_errores_Click);
            // 
            // mi_manual
            // 
            this.mi_manual.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_tecnio,
            this.mi_usuario,
            this.mi_gramatica});
            this.mi_manual.Name = "mi_manual";
            this.mi_manual.Size = new System.Drawing.Size(59, 20);
            this.mi_manual.Text = "Manual";
            // 
            // mi_tecnio
            // 
            this.mi_tecnio.Name = "mi_tecnio";
            this.mi_tecnio.Size = new System.Drawing.Size(159, 22);
            this.mi_tecnio.Text = "Manual Tecnico";
            // 
            // mi_usuario
            // 
            this.mi_usuario.Name = "mi_usuario";
            this.mi_usuario.Size = new System.Drawing.Size(159, 22);
            this.mi_usuario.Text = "Manual Usuario";
            // 
            // mi_gramatica
            // 
            this.mi_gramatica.Name = "mi_gramatica";
            this.mi_gramatica.Size = new System.Drawing.Size(159, 22);
            this.mi_gramatica.Text = "Gramatica";
            // 
            // btn_cargar
            // 
            this.btn_cargar.Location = new System.Drawing.Point(180, 38);
            this.btn_cargar.Name = "btn_cargar";
            this.btn_cargar.Size = new System.Drawing.Size(80, 70);
            this.btn_cargar.TabIndex = 3;
            this.btn_cargar.Text = "Cargar Archivo";
            this.btn_cargar.UseVisualStyleBackColor = true;
            // 
            // btn_generar
            // 
            this.btn_generar.Location = new System.Drawing.Point(340, 38);
            this.btn_generar.Name = "btn_generar";
            this.btn_generar.Size = new System.Drawing.Size(80, 70);
            this.btn_generar.TabIndex = 4;
            this.btn_generar.Text = "Generar Formulario";
            this.btn_generar.UseVisualStyleBackColor = true;
            this.btn_generar.Click += new System.EventHandler(this.btn_generar_Click);
            // 
            // btn_verform
            // 
            this.btn_verform.Location = new System.Drawing.Point(500, 38);
            this.btn_verform.Name = "btn_verform";
            this.btn_verform.Size = new System.Drawing.Size(80, 70);
            this.btn_verform.TabIndex = 5;
            this.btn_verform.Text = "Ver Formularios";
            this.btn_verform.UseVisualStyleBackColor = true;
            // 
            // tab_control
            // 
            this.tab_control.Location = new System.Drawing.Point(12, 114);
            this.tab_control.Name = "tab_control";
            this.tab_control.SelectedIndex = 0;
            this.tab_control.Size = new System.Drawing.Size(757, 453);
            this.tab_control.TabIndex = 6;
            // 
            // rtb_consola
            // 
            this.rtb_consola.Location = new System.Drawing.Point(16, 574);
            this.rtb_consola.Name = "rtb_consola";
            this.rtb_consola.Size = new System.Drawing.Size(749, 96);
            this.rtb_consola.TabIndex = 7;
            this.rtb_consola.Text = "";
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(781, 681);
            this.Controls.Add(this.rtb_consola);
            this.Controls.Add(this.tab_control);
            this.Controls.Add(this.btn_generar);
            this.Controls.Add(this.btn_cargar);
            this.Controls.Add(this.menubar);
            this.Controls.Add(this.btn_verform);
            this.MainMenuStrip = this.menubar;
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Principal";
            this.menubar.ResumeLayout(false);
            this.menubar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menubar;
        private System.Windows.Forms.ToolStripMenuItem mi_archivo;
        private System.Windows.Forms.ToolStripMenuItem mi_form;
        private System.Windows.Forms.ToolStripMenuItem mi_reporte;
        private System.Windows.Forms.ToolStripMenuItem mi_manual;
        private System.Windows.Forms.Button btn_cargar;
        private System.Windows.Forms.Button btn_generar;
        private System.Windows.Forms.Button btn_verform;
        private System.Windows.Forms.TabControl tab_control;
        private System.Windows.Forms.RichTextBox rtb_consola;
        private System.Windows.Forms.ToolStripMenuItem mi_abrir;
        private System.Windows.Forms.ToolStripMenuItem mi_guardar;
        private System.Windows.Forms.ToolStripMenuItem mi_guardarcomo;
        private System.Windows.Forms.ToolStripMenuItem mi_nuevo;
        private System.Windows.Forms.ToolStripMenuItem mi_cerrar;
        private System.Windows.Forms.ToolStripMenuItem mi_errores;
        private System.Windows.Forms.ToolStripMenuItem mi_tecnio;
        private System.Windows.Forms.ToolStripMenuItem mi_usuario;
        private System.Windows.Forms.ToolStripMenuItem mi_gramatica;
    }
}