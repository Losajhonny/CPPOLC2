using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xform.Analizador.Reportes;
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

    class Llamada : Posicion
    {
        /**
         * @call puntero a otra llamada. En una llamada puede ocurrir otra
         * @lista lista de valores de parametros o lista de valores de dimensiones
         * */
        private List<Expresion> expresiones;
        private List<Llamada> llamadas;
        private Tipo_Llamada tllamada;
        private Tipo_Nativa tnativa;

        private string id;

        /**
         * Constructor para las llamadas nativas
         * */
        public Llamada(Tipo_Nativa tnativa, List<Expresion> expresiones)
        {
            this.tnativa = tnativa;
            this.expresiones = expresiones;
            this.tllamada = Tipo_Llamada.NATIVA;
        }

        /**
         * Constructor de llamada de metodo, arreglo, id, este
         * */
        public Llamada(Tipo_Llamada tllamada, string id, List<Expresion> expresiones)
        {
            this.tllamada = tllamada;
            this.id = id;
            this.expresiones = expresiones;
            this.tnativa = Tipo_Nativa.NINGUNO;
        }

        /**
         * Constructor de llamada de id
         * */
        public Llamada(Tipo_Llamada tllamada, string id){
            this.tllamada = tllamada;
            this.id = id;
            this.tnativa = Tipo_Nativa.NINGUNO;
        }

        /**
         * Constructor de llamada de otra llamada
         * */
        public Llamada(Tipo_Llamada tllamada, List<Llamada> llamadas)
        {
            this.tllamada = tllamada;
            this.tnativa = Tipo_Nativa.NINGUNO;
            this.llamadas = llamadas;
        }

        /**
         * Clase de tipos de llamadas
         * */
        public enum Tipo_Llamada
        {
            NATIVA,
            ESTE,
            ID,
            ARREGLO,
            METODO,
            LISTA
        }

        /**
         * Clase de tipos de funciones nativas
         * */
        public enum Tipo_Nativa
        {
            CADENA,
            SUBCAD,
            POSCAD,
            BOOLEANO,
            ENTERO,
            TAM,
            RANDOM,
            MIN,
            MAX,
            POW,
            LOG,
            LOG10,
            ABS,
            SIN,
            COS,
            TAN,
            SQRT,
            PI,
            HOY,
            AHORA,
            FECHA,
            HORA,
            FECHAHORA,
            IMAGEN,
            VIDEO,
            AUDIO,

            NINGUNO
        }

        /**
         * Retorna el tipo_nativa
         * */
        public static Tipo_Nativa getNativa(string value)
        {
            if (value.ToLower().Equals("entero"))
            {
                return Tipo_Nativa.ENTERO;
            }
            else if (value.ToLower().Equals("tam"))
            {
                return Tipo_Nativa.TAM;
            }
            else if (value.ToLower().Equals("random"))
            {
                return Tipo_Nativa.RANDOM;
            }
            else if (value.ToLower().Equals("min"))
            {
                return Tipo_Nativa.MIN;
            }
            else if (value.ToLower().Equals("max"))
            {
                return Tipo_Nativa.MAX;
            }
            else if (value.ToLower().Equals("pow"))
            {
                return Tipo_Nativa.POW;
            }
            else if (value.ToLower().Equals("log"))
            {
                return Tipo_Nativa.LOG;
            }
            else if (value.ToLower().Equals("log10"))
            {
                return Tipo_Nativa.LOG10;
            }
            else if (value.ToLower().Equals("abs"))
            {
                return Tipo_Nativa.ABS;
            }
            else if (value.ToLower().Equals("sin"))
            {
                return Tipo_Nativa.SIN;
            }
            else if (value.ToLower().Equals("cos"))
            {
                return Tipo_Nativa.COS;
            }
            else if (value.ToLower().Equals("tan"))
            {
                return Tipo_Nativa.TAN;
            }
            else if (value.ToLower().Equals("sqrt"))
            {
                return Tipo_Nativa.SQRT;
            }
            else if (value.ToLower().Equals("pi"))
            {
                return Tipo_Nativa.PI;
            }
            else if (value.ToLower().Equals("hoy"))
            {
                return Tipo_Nativa.HOY;
            }
            else if (value.ToLower().Equals("ahora"))
            {
                return Tipo_Nativa.AHORA;
            }
            else if (value.ToLower().Equals("fecha"))
            {
                return Tipo_Nativa.FECHA;
            }
            else if (value.ToLower().Equals("hora"))
            {
                return Tipo_Nativa.HORA;
            }
            else
            {
                return Tipo_Nativa.FECHAHORA;
            }
        }
    }
}
