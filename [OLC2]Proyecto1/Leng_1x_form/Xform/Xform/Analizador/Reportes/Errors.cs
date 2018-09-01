using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xform.Analizador.Reportes
{
    /**
     * @clase Errors
     * @autor        : Jhonatan Lopez
     * @carnet       : 201325583
     * @universidad  : USAC
     * @facultad     : ingenieria
     * */

    class Errors : Posicion
    {
        /**
         * Atributos que definen la clase
         * */
        private string tipo;
        private string descripcion;

        public Errors(int linea, int columna, string tipo, string nivel, string descripcion)
        {
            this.Linea = linea;
            this.Columna = columna;
            this.Level = nivel;
            this.tipo = tipo;
            this.descripcion = descripcion;
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
    }
}
