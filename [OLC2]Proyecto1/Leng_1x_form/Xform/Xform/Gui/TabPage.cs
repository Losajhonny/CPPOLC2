using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xform.Gui
{
    /**
     * @clase Principal
     * @autor        : Jhonatan Lopez
     * @carnet       : 201325583
     * @universidad  : USAC
     * @facultad     : ingenieria
     * */

    class TabPage : System.Windows.Forms.TabPage
    {
        /**
         * Atributo que identifica el tabpage
         * */
        private int id;

        /**
         * Atributo donde contiene el texto del tabpage
         * */
        private RichTextBox rtb_page;

        public TabPage(int id, string nombre, RichTextBox rtb_page)
            : base(nombre)
        {
            this.id = id;
            this.rtb_page = rtb_page;
            //se agrega el texto al controlador
            this.Controls.Add(rtb_page);
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public RichTextBox Rtb_page
        {
            get { return rtb_page; }
            set { rtb_page = value; }
        }
    }
}
