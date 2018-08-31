using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xform.Analizador;

namespace Xform.Gui
{
    /**
     * @clase Principal
     * @autor        : Jhonatan Lopez
     * @carnet       : 201325583
     * @universidad  : USAC
     * @facultad     : ingenieria
     * */

    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            crearTabPage();
        }

        /**
         * Eventos que realizan los botones segun su funciones
         * */
        private void mi_nuevo_Click(object sender, EventArgs e)
        {
            crearTabPage();
        }

        private void mi_cerrar_Click(object sender, EventArgs e)
        {
            cerrarTabPage();
        }

        private void btn_generar_Click(object sender, EventArgs e)
        {
            generar();
        }

        private void mi_errores_Click(object sender, EventArgs e)
        {
            Reporte.tablaErrores(Sintactico.error, "Tabla_Errores.html");
        }

        /**
         * Crea una pestaña nueva
         * */
        private void crearTabPage()
        {
            int noTab = 0;
            bool ban = false;
            //buscar el noTab que esta disponible
            while (true)
            {
                for (int i = 0; i < tab_control.TabCount; i++)
                {
                    TabPage tmp = (TabPage)tab_control.TabPages[i];
                    if (tmp.Id == noTab)
                    {
                        //en caso de que el id existe utilizar una bandera
                        ban = true;
                        break;
                    }
                }

                if (!ban)
                {
                    //cuando no existe crear el TabPage
                    String nombre = "Archivo " + (noTab + 1).ToString();
                    RichTextBox rt = new RichTextBox();
                    rt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left | AnchorStyles.Right))));
                    rt.BorderStyle = BorderStyle.None;
                    rt.ScrollBars = RichTextBoxScrollBars.Both;
                    rt.SetBounds(0, 0, 200, 100);
                    rt.BackColor = Color.WhiteSmoke;

                    TabPage tp = new TabPage(noTab, nombre, rt);
                    tab_control.TabPages.Add(tp);
                    break;
                }
                ban = false;
                noTab++;
            }

        }

        /**
         * Cierra una pestaña seleccionada
         * */
        private void cerrarTabPage()
        {
            TabPage tp = (TabPage)tab_control.SelectedTab;
            tab_control.TabPages.Remove(tp);
        }

        /**
         * Genera y analiza el archivo (formulario)
         * */
        private void generar()
        {
            TabPage tp = (TabPage)tab_control.SelectedTab;
            ParseTreeNode raiz = Sintactico.analizar(tp.Rtb_page.Text);
            if (raiz == null)
            {
                rtb_consola.Text = "error";
            }
            else
            {
                rtb_consola.Text = "";
            }
        }
    }
}
