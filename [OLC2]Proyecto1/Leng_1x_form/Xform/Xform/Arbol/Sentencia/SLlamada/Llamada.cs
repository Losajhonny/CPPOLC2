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
         * @expresiones         valores de acceso arreglo o de parametros
         * @llamadas            cuando vienen con '.'
         * @tllamada            especifica el tipo de llamada
         * @tnativa             especifica el tipo de funcion nativa que se llama
         * @id                  identificador de la llamada
         * */
        private List<Expresion> expresiones;
        private List<Llamada> llamadas;
        private Llamada ceste;

        private Tipo_Llamada tllamada;
        private Tipo_Nativa tnativa;

        private string id;

        /**
         * Constructor para las llamadas nativas
         * @tnativa         especifica que tipo de funcion nativa se ejecutara
         * @expresiones     lista de valores de parametros para las funciones nativas
         * */
        public Llamada(string tnativa, List<Expresion> expresiones)
        {
            this.tnativa = getNativa(tnativa);
            this.expresiones = expresiones;
            this.tllamada = Tipo_Llamada.NATIVA;
        }

        /**
         * Constructor de llamada de metodo, arreglo
         * @tllamada        tipo de llamada que se realiza
         * @id              identificador de un metodo o arreglo
         * @expresiones     son los valores de parametros para un metodo
         *                  o acceso de dimensiones para un arreglo
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
         * aqui se le especifica que es una llamada a un id
         * @id          identificador de la llamada de una variable o atributo
         * */
        public Llamada(string id){
            this.tllamada = Tipo_Llamada.ID;
            this.id = id;
            this.tnativa = Tipo_Nativa.NINGUNO;
        }

        /**
        * Constructor que se define para la palabra reservada Este
        * */
        public Llamada(){
            this.tllamada = Tipo_Llamada.ESTE;
            this.tnativa = Tipo_Nativa.NINGUNO;
        }

        /**
         * Constructor de llamada de otra llamada
         * @llamadas        lista de llamadas por '.'
         * */
        public Llamada(List<Llamada> llamadas)
        {
            this.tllamada = Tipo_Llamada.LISTA;
            this.tnativa = Tipo_Nativa.NINGUNO;
            this.llamadas = llamadas;
        }

        /**
        * Modifica el ceste de las llamadas
        * */
        public void setCLlamada(Llamada llamada){
            this.ceste = llamada;
        }

        /**
         * Clase de tipos de llamadas
         * */
        public enum Tipo_Llamada
        {
            ID,
            ARREGLO,
            METODO,
            ESTE,
            LISTA,
            NATIVA
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
            if (value.ToLower().Equals("cadena"))
            {
                return Tipo_Nativa.CADENA;
            }
            else if (value.ToLower().Equals("subcad"))
            {
                return Tipo_Nativa.SUBCAD;
            }
            else if (value.ToLower().Equals("poscad"))
            {
                return Tipo_Nativa.POSCAD;
            }
            else if (value.ToLower().Equals("booleano"))
            {
                return Tipo_Nativa.BOOLEANO;
            }
            else if (value.ToLower().Equals("entero"))
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
            else if (value.ToLower().Equals("fechahora"))
            {
                return Tipo_Nativa.FECHAHORA;
            }
            else if (value.ToLower().Equals("imagen"))
            {
                return Tipo_Nativa.IMAGEN;
            }
            else if (value.ToLower().Equals("video"))
            {
                return Tipo_Nativa.VIDEO;
            }
            else
            {
                return Tipo_Nativa.AUDIO;
            }
        }
    }
}
