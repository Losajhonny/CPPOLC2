using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xform.Arbol.OpExp;

namespace Xform.Arbol.Sentencia.SLlamada
{
    /**
     * @clase Llamada
     * @autor        : Jhonatan Lopez
     * @carnet       : 201325583
     * @universidad  : USAC
     * @facultad     : ingenieria
     * */

    class Llamada
    {
        /**
         * @call puntero a otra llamada. En una llamada puede ocurrir otra
         * @lista lista de valores de parametros o lista de valores de dimensiones
         * */
        private Llamada call;
        private List<Expresion> lista;

        /**
         * Clase de tipos de llamadas
         * */
        public enum Tipo_Llamada
        {
            NATIVA,
            ESTE,
            ID,
            ARREGLO,
            CLASE,

            NINGUNO
        }
    }
}
