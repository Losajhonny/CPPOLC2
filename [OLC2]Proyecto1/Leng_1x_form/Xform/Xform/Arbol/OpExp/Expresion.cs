using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xform.Analizador.Reportes;

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
        protected Expresion r1;
        protected Expresion r2;
        protected Operador op;
        protected object value;
        
        /**
         * Constructor para representar una expresion de 2 operandos
         * */
        public Expresion(Expresion r1, Expresion r2, Operador op)
        {
            this.r1 = r1;
            this.r2 = r2;
            this.op = op;
        }

        /**
         * Constructor para representar una expresion de 1 operando
         * */
        public Expresion(Expresion r1, Operador op)
        {
            this.r1 = r1;
            this.op = op;
        }

        /**
         * Constructor para representar una expresion de 1 operando
         * */
        public Expresion(Operador op, Expresion r2)
        {
            this.r2 = r2;
            this.op = op;
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
        public Expresion(object value, int linea, int columna, string level)
        {
            this.Linea = linea;
            this.Columna = columna;
            this.Level = level;
            this.value = value;
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

        public object ejecutar()
        {
            throw new NotImplementedException();
        }
    }
}
