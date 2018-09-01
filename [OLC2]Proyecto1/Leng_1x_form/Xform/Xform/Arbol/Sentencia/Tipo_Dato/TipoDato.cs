using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xform.Arbol.Sentencia.Tipo_Dato
{
    /**
     * @clase TipoDato
     * @autor        : Jhonatan Lopez
     * @carnet       : 201325583
     * @universidad  : USAC
     * @facultad     : ingenieria
     * */

    class TipoDato
    {
        /**
         * Clase enum donde almacena los tipos de datos
         * */
        public enum Tipo
        {
            ENTERO,
            DECIMAL,
            CADENA,
            BOOLEANO,
            FECHA,
            HORA,
            FECHAHORA,
            ID,
            RESPUESTAS,

            NINGUNO
        }

        /**
         * Retorna un tipo de dato de cualquier valor
         * */
        public static Tipo getTipo(string value)
        {
            if (value.ToLower().Equals("entero"))
            {
                return Tipo.ENTERO;
            }
            else if (value.ToLower().Equals("decimal"))
            {
                return Tipo.DECIMAL;
            }
            else if (value.ToLower().Equals("cadena"))
            {
                return Tipo.CADENA;
            }
            else if (value.ToLower().Equals("fecha"))
            {
                return Tipo.FECHA;
            }
            else if (value.ToLower().Equals("hora"))
            {
                return Tipo.HORA;
            }
            else if (value.ToLower().Equals("fechahora"))
            {
                return Tipo.FECHAHORA;
            }
            else if (value.ToLower().Equals("respuestas"))
            {
                return Tipo.RESPUESTAS;
            }
            return Tipo.NINGUNO;
        }
    }
}
