using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xform.Analizador
{
    /**
     * @clase Errors
     * @autor        : Jhonatan Lopez
     * @carnet       : 201325583
     * @universidad  : USAC
     * @facultad     : ingenieria
     * */

    class Errors
    {
        /**
         * Atributos que definen la clase
         * */
        private int linea;
        private int columna;
        private string tipo;
        private string level;
        private string descripcion;

        public Errors(int line, int col, string _tipo, string nivel, string des)
        {
            this.linea = line;
            this.columna = col;
            this.tipo = _tipo;
            this.level = nivel;
            this.descripcion = des;
        }

        public int Linea
        {
            get { return linea; }
            set { linea = value; }
        }

        public int Columna
        {
            get { return columna; }
            set { columna = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string Level
        {
            get { return level; }
            set { level = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
    }
}
