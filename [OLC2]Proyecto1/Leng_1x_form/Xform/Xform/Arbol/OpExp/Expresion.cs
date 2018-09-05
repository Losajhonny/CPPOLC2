﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xform.Analizador.Reportes;
using Xform.Arbol.Sentencia.Tipo_Dato;
using Xform.Arbol.Sentencia.SLlamada;

namespace Xform.Arbol.OpExp
{
    /**
     * @clase Expresion
     * @autor        : Jhonatan Lopez
     * @carnet       : 201325583
     * @universidad  : USAC
     * @facultad     : ingenieria
     * */

    class Expresion : Posicion
    {
        /**
         * @r1      operando izquierda
         * @r2      operando derecha
         * @op      operador que utiliza
         * @top     tipo de operacion que se va a realizar
         * @tipo    tipo de dato que dal value
         * @value   valor de la expresion
         * @isLlamada para verificar si es una llamada o expresion
         * @llamada es una funcion o variable con arreglos
         * */
        private Expresion r1;
        private Expresion r2;
        private Operador operador;
        private Tipo_Operacion operacion;


        private TipoDato.Tipo tipo;
        private object value;


        private Llamada llamada;
        
        /**
         * Constructor para representar una expresion de 2 operandos
         * */
        public Expresion(Expresion r1, Expresion r2, Operador operador, Tipo_Operacion operacion)
        {
            this.r1 = r1;
            this.r2 = r2;
            this.operador = operador;
            this.operacion = operacion;
            this.tipo = TipoDato.Tipo.NINGUNO;
        }

        /**
         * Constructor para representar una expresion de 1 operando
         * */
        public Expresion(Expresion r1, Operador operador, Tipo_Operacion operacion)
        {
            this.r1 = r1;
            this.operador = operador;
            this.operacion = operacion;
            this.tipo = TipoDato.Tipo.NINGUNO;
        }

        /**
         * Constructor para representar una expresion de 1 operando
         * */
        public Expresion(Operador operador, Expresion r2, Tipo_Operacion operacion)
        {
            this.r2 = r2;
            this.operador = operador;
            this.operacion = operacion;
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
            this.operador = Operador.NINGUNO;
            this.operacion = Tipo_Operacion.NINGUNO;
        }

        /**
         * Constructor para representar una expresion con una llamada
         * */
        public Expresion(Llamada llamada, Tipo_Operacion operacion)
        {
            this.llamada = llamada;
            this.operador = Operador.NINGUNO;
            this.operacion = operacion;
            this.tipo = TipoDato.Tipo.NINGUNO;
        }

        /**
         * Clase enum donde almacena el tipo de operacion que se realiza
         * */
        public enum Tipo_Operacion
        {
            ARITMETICA,
            RELACIONAL,
            LOGICO,
            LLAMADA,
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
    }
}
