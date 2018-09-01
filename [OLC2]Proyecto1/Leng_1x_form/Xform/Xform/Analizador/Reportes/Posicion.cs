using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xform.Analizador.Reportes
{
    /**
     * @clase Posicion
     * @autor        : Jhonatan Lopez
     * @carnet       : 201325583
     * @universidad  : USAC
     * @facultad     : ingenieria
     * */

    abstract class Posicion
    {
        /**
         * Almacena la posicion del editor de los terminales
         * @linea
         * @columna
         * @level
         * */

        //Linea donde se encuentra el terminal
        public int Linea { get; set; }
        //Columna donde se encuentra el terminal
        public int Columna { get; set; }
        //Level donde se encuentra el terminal
        public string Level { get; set; }        
    }
}
