using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xform.Analizador.Reportes;
using Xform.Arbol.Sentencia.Tipo_Dato;

namespace Xform.Arbol.OpExp
{
    /**
     * @clase Expresion
     * @autor        : Jhonatan Lopez
     * @carnet       : 201325583
     * @universidad  : USAC
     * @facultad     : ingenieria
     * */

    class Expresion : Posicion, Sentencia.Sentencia
    {
        /**
         * @r1      operando izquierda
         * @r2      operando derecha
         * @op      operador que utiliza
         * @value   valor de la expresion
         * */
        private Expresion r1;
        private Expresion r2;
        private Operador op;
        private Tipo_Operacion top;


        private TipoDato.Tipo tipo;
        private object value;
        
        /**
         * Constructor para representar una expresion de 2 operandos
         * */
        public Expresion(Expresion r1, Expresion r2, Operador op, Tipo_Operacion t)
        {
            this.r1 = r1;
            this.r2 = r2;
            this.op = op;
            this.top = t;
            this.tipo = TipoDato.Tipo.NINGUNO;
        }

        /**
         * Constructor para representar una expresion de 1 operando
         * */
        public Expresion(Expresion r1, Operador op, Tipo_Operacion t)
        {
            this.r1 = r1;
            this.op = op;
            this.top = t;
            this.tipo = TipoDato.Tipo.NINGUNO;
        }

        /**
         * Constructor para representar una expresion de 1 operando
         * */
        public Expresion(Operador op, Expresion r2, Tipo_Operacion t)
        {
            this.r2 = r2;
            this.op = op;
            this.top = t;
            this.tipo = TipoDato.Tipo.NINGUNO;
        }

        /**
         * Constructor para representar el dato de una expresion
         * @value       valor de la expresion
         * @linea       linea donde se encuentra el valor
         * @columna     columna donde se encuentra el valor
         * @level       level donde se encuentra el valor
         * Los parametros sirven para indicar donde puede haber
         * un error semantico
         * */
        public Expresion(object value, int linea, int columna, TipoDato.Tipo tipo)
        {
            this.Linea = linea;
            this.Columna = columna;
            this.value = value;
            this.tipo = tipo;
            this.op = Operador.NINGUNO;
            this.top = Tipo_Operacion.NINGUNO;
        }

        /**
         * Clase enum donde almacena el tipo de operacion que se realiza
         * */
        public enum Tipo_Operacion
        {
            ARITMETICA,
            RELACIONAL,
            LOGICO,

            NINGUNO
        }
        
        /**
         * Clase enum donde almacena los operadores
         * Lista de operadores que funcionan para las expresiones
         * */
        public enum Operador
        {
            MAS,
            MENOS,
            POR,
            DIV,
            MOD,
            POT,

            DMAS,
            DMENOS,

            MENOR,
            MAYOR,
            IGUAL,
            DIFERENTE,
            MEN_IGUAL,
            MAY_IGUAL,

            NOT,
            AND,
            OR,

            NINGUNO
        }

        /**
         * Retorna un operador de cualquier expresion
         * */
        public static Operador getOperador(string op)
        {
            if (op.Equals("+"))
            {
                return Operador.MAS;
            }
            else if (op.Equals("-"))
            {
                return Operador.MENOS;
            }
            else if (op.Equals("*"))
            {
                return Operador.POR;
            }
            else if (op.Equals("/"))
            {
                return Operador.DIV;
            }
            else if (op.Equals("^"))
            {
                return Operador.POT;
            }
            else if (op.Equals("%"))
            {
                return Operador.MOD;
            }
            else if (op.Equals("<="))
            {
                return Operador.MEN_IGUAL;
            }
            else if (op.Equals(">="))
            {
                return Operador.MAY_IGUAL;
            }
            else if (op.Equals("<"))
            {
                return Operador.MENOR;
            }
            else if (op.Equals(">"))
            {
                return Operador.MAYOR;
            }
            else if (op.Equals("=="))
            {
                return Operador.IGUAL;
            }
            else if (op.Equals("!="))
            {
                return Operador.DIFERENTE;
            }
            else if (op.Equals("&&"))
            {
                return Operador.AND;
            }
            else if (op.Equals("||"))
            {
                return Operador.OR;
            }
            else if (op.Equals("!"))
            {
                return Operador.NOT;
            }
            else if (op.Equals("++"))
            {
                return Operador.DMAS;
            }
            else if (op.Equals("--"))
            {
                return Operador.DMENOS;
            }
            return Operador.NINGUNO;
        }

        public object ejecutar()
        {
            throw new NotImplementedException();
        }
    }
}
