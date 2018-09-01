using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xform.Arbol.OpExp;
using Xform.Arbol.Sentencia.Tipo_Dato;

namespace Xform.Arbol.Ast
{
    /**
     * @clase Arbols
     * @autor        : Jhonatan Lopez
     * @carnet       : 201325583
     * @universidad  : USAC
     * @facultad     : ingenieria
     * */

    class Arbols
    {
        /**
         * Clase que retorna el arbol para su ejecucion
         * funciones con letras minusculas son complementos de funciones
         * con letras mayusculas
         * */

        /**
         * Retorna el arbol de una expresion
         * */
        public Expresion EXPRESION(ParseTreeNode padre)
        {
            Expresion exp = null;
            switch (padre.ChildNodes.Count)
            {
                case 1:
                    if (padre.ChildNodes[0].ToString().Equals("ARITMETICA"))
                    {
                        exp = ARITMETICA(padre.ChildNodes[0]);
                    }
                    else if (padre.ChildNodes[0].ToString().Equals("RELACIONAL"))
                    {
                        exp = RELACIONAL(padre.ChildNodes[0]);
                    }
                    else if (padre.ChildNodes[0].ToString().Equals("LOGICA"))
                    {
                        exp = LOGICA(padre.ChildNodes[0]);
                    }
                    else if (padre.ChildNodes[0].ToString().Equals("OPERADOR"))
                    {
                        exp = OPERADOR(padre.ChildNodes[0]);
                    }
                    else if (padre.ChildNodes[0].ToString().Equals("FACTOR"))
                    {
                        exp = FACTOR(padre.ChildNodes[0]);
                    }
                    else if (padre.ChildNodes[0].ToString().Equals("LLAMADAS"))
                    {

                    }
                    break;
                case 3:// ( EXPRESION )
                    exp = EXPRESION(padre.ChildNodes[1]);
                    break;
                default:
                    break;
            }
            return exp;
        }

        /**
         * Retorna el arbol de una expresion aritmetica
         * */
        public Expresion ARITMETICA(ParseTreeNode padre)
        {
            Expresion exp = null;
            switch (padre.ChildNodes.Count)
            {
                case 2:// ( tmenos | tmas ) EXPRESION
                    Expresion r = EXPRESION(padre.ChildNodes[1]);
                    exp = new Expresion(Expresion.getOperador(padre.ChildNodes[0].Token.Text), r, Expresion.Tipo_Operacion.ARITMETICA);
                    break;
                case 3:// EXPRESION op EXPRESION
                    Expresion r1 = EXPRESION(padre.ChildNodes[0]);
                    Expresion r2 = EXPRESION(padre.ChildNodes[2]);
                    exp = new Expresion(r1, r2, Expresion.getOperador(padre.ChildNodes[1].Token.Text), Expresion.Tipo_Operacion.ARITMETICA);
                    break;
                default:
                    break;
            }
            return exp;
        }

        /**
         * Retorna el arbol de una expresion relacional
         * */
        public Expresion RELACIONAL(ParseTreeNode padre)
        {
            Expresion exp = null;
            //EXPRESION op EXPRESION
            Expresion r1 = EXPRESION(padre.ChildNodes[0]);
            Expresion r2 = EXPRESION(padre.ChildNodes[2]);
            exp = new Expresion(r1, r2, Expresion.getOperador(padre.ChildNodes[1].Token.Text), Expresion.Tipo_Operacion.RELACIONAL);

            return exp;
        }

        /**
         * Retorna el arbol de una expresion logica
         * */
        public Expresion LOGICA(ParseTreeNode padre)
        {
            Expresion exp = null;
            switch (padre.ChildNodes.Count)
            {
                case 2:// not EXPRESION
                    Expresion r = EXPRESION(padre.ChildNodes[1]);
                    exp = new Expresion(Expresion.getOperador(padre.ChildNodes[0].Token.Text), r, Expresion.Tipo_Operacion.LOGICO);
                    break;
                case 3:// EXPRESION op EXPRESION
                    Expresion r1 = EXPRESION(padre.ChildNodes[0]);
                    Expresion r2 = EXPRESION(padre.ChildNodes[2]);
                    exp = new Expresion(r1, r2, Expresion.getOperador(padre.ChildNodes[1].Token.Text), Expresion.Tipo_Operacion.LOGICO);
                    break;
                default:
                    break;
            }
            return exp;
        }

        /**
         * Retorna el arbol de una expresion
         * devuleve un incremento o decremento
         * */
        public Expresion OPERADOR(ParseTreeNode padre)
        {
            Expresion exp = null;
            if (padre.ChildNodes[0].ToString().Equals("EXPRESION"))
            {// EXPRESION ( dmas | dmenos )
                Expresion r = EXPRESION(padre.ChildNodes[0]);
                exp = new Expresion(r, Expresion.getOperador(padre.ChildNodes[1].Token.Text), Expresion.Tipo_Operacion.ARITMETICA);
            }
            else
            {// ( dmas | dmenos ) EXPRESION
                Expresion r = EXPRESION(padre.ChildNodes[1]);
                exp = new Expresion(Expresion.getOperador(padre.ChildNodes[0].Token.Text), r, Expresion.Tipo_Operacion.ARITMETICA);
            }
            return exp;
        }

        /**
         * Retorna el arbol de una expresion factor
         * */
        public Expresion FACTOR(ParseTreeNode padre)
        {
            Expresion exp = null;
            if (padre.ChildNodes[0].ToString().Equals("VBOOLEANO"))
            {
                exp = VBOOLEANO(padre.ChildNodes[0]);
            }
            else
            {
                exp = new Expresion(padre.ChildNodes[0].Token.Text, padre.ChildNodes[0].Token.Location.Line,
                    padre.ChildNodes[0].Token.Location.Column, TipoDato.getTipo(padre.ChildNodes[0].Token.Terminal.Name));
            }
            return exp;
        }

        /**
         * Retorna el arbol de una expresion booleana
         * */
        public Expresion VBOOLEANO(ParseTreeNode padre)
        {
            Expresion exp = new Expresion(padre.ChildNodes[0].Token.Text, padre.ChildNodes[0].Token.Location.Line,
                    padre.ChildNodes[0].Token.Location.Column, TipoDato.getTipo(padre.ChildNodes[0].Token.Terminal.Name));
            return exp;
        }
    }
}
