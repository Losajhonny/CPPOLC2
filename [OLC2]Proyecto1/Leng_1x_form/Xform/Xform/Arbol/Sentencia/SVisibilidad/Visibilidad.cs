using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xform.Arbol.Sentencia.SVisibilidad
{
    /**
     * @clase Visibilidad
     * @autor        : Jhonatan Lopez
     * @carnet       : 201325583
     * @universidad  : USAC
     * @facultad     : ingenieria
     * */

    class Visibilidad
    {
        /**
         * Clase enum donde almacena la visibilidad
         * */
        public enum Visibilidad{
            PUBLICO,
            PRIVADO,
            PROTEGIDO
        }

        /**
         * Retorna el tipo de visibilidad
         * */
        public static Visibilidad getVisibilidad(string value)
        {
            if (value.ToLower().Equals("publico"))
            {
                return Visibilidad.PUBLICO;
            }
            else if (value.ToLower().Equals("privado"))
            {
                return Visibilidad.PRIVADO;
            }
            else
            {
                return Visibilidad.PROTEGIDO;
            }
        }
    }
}
