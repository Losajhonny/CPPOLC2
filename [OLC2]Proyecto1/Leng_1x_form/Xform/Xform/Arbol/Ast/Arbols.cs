using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xform.Arbol.OpExp;
using Xform.Arbol.Sentencia.SLlamada;
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
                    if (padre.ChildNodes[0].Term.Name.Equals("ARITMETICA"))
                    {
                        exp = ARITMETICA(padre.ChildNodes[0]);
                    }
                    else if (padre.ChildNodes[0].Term.Name.Equals("RELACIONAL"))
                    {
                        exp = RELACIONAL(padre.ChildNodes[0]);
                    }
                    else if (padre.ChildNodes[0].Term.Name.Equals("LOGICA"))
                    {
                        exp = LOGICA(padre.ChildNodes[0]);
                    }
                    else if (padre.ChildNodes[0].Term.Name.Equals("OPERADOR"))
                    {
                        exp = OPERADOR(padre.ChildNodes[0]);
                    }
                    else if (padre.ChildNodes[0].Term.Name.Equals("FACTOR"))
                    {
                        exp = FACTOR(padre.ChildNodes[0]);
                    }
                    else if (padre.ChildNodes[0].Term.Name.Equals("LLAMADAS"))
                    {
                        exp = new Expresion(LLAMADAS(padre.ChildNodes[0]), Expresion.Tipo_Operacion.LLAMADA);
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
            if (padre.ChildNodes[0].Term.Name.Equals("EXPRESION"))
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
        {//cadena | entero | ddecimal | VBOOLEANO | fecha | hora | fechahora | id
            Expresion exp = null;
            if (padre.ChildNodes[0].Term.Name.Equals("VBOOLEANO"))
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
        {//( verdadero | falso )
            Expresion exp = new Expresion(padre.ChildNodes[0].Token.Text, padre.ChildNodes[0].Token.Location.Line,
                    padre.ChildNodes[0].Token.Location.Column, TipoDato.getTipo(padre.ChildNodes[0].Token.Terminal.Name));
            return exp;
        }
    
        /**
         * Retorna el arbol de una llamada
         * */
        public Llamada LLAMADAS(ParseTreeNode padre)
        {
            if (padre.ChildNodes[0].Term.Name.Equals("MLLAMADAS"))
            {//MLlamadas
                return new Llamada(Llamada.Tipo_Llamada.LISTA, MLLAMADAS(padre.ChildNodes[0]));
            }
            else
            {//NLlamadas
                return NLLAMADA(padre.ChildNodes[0]);
            }
        }

        /**
         * Retorna la una llamada nativa
         * */
        public Llamada NLLAMADA(ParseTreeNode padre)
        {
            if (padre.ChildNodes[0].Term.Name.Equals("NATIVA_CADENA"))
            {
                return NATIVA_CADENA(padre.ChildNodes[0]);
            }
            else if (padre.ChildNodes[0].Term.Name.Equals("NATIVA_BOOLEANA"))
            {
                return NATIVA_BOOLEANA(padre.ChildNodes[0]);
            }
            else if (padre.ChildNodes[0].Term.Name.Equals("NATIVA_NUMERICA"))
            {
                return NATIVA_NUMERICA(padre.ChildNodes[0]);
            }
            else if (padre.ChildNodes[0].Term.Name.Equals("NATIVA_FECHAHORA"))
            {
                return NATIVA_FECHAHORA(padre.ChildNodes[0]);
            }
            else
            {
                return NATIVA_MULTIMEDIA(padre.ChildNodes[0]);
            }
        }

        /**
         * Retorna la llamada nativa cadena
         * */
        public Llamada NATIVA_CADENA(ParseTreeNode padre)
        {
            Llamada llamada = null;
            List<Expresion> lista;
            switch (padre.ChildNodes.Count)
            {
                case 4://pr_cadena + parizq + EXPRESION + parder
                    lista = new List<Expresion>();
                    lista.Add(EXPRESION(padre.ChildNodes[2]));
                    llamada = new Llamada(Llamada.Tipo_Nativa.CADENA, lista);
                    llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
                    llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
                    break;
                case 6://pr_poscad + parizq + EXPRESION + tk_coma + EXPRESION + parder
                    lista = new List<Expresion>();
                    lista.Add(EXPRESION(padre.ChildNodes[2]));
                    lista.Add(EXPRESION(padre.ChildNodes[4]));
                    llamada = new Llamada(Llamada.Tipo_Nativa.POSCAD, lista);
                    llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
                    llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
                    break;
                default://pr_subcad + parizq + EXPRESION + tk_coma + EXPRESION + tk_coma + EXPRESION + parder
                    lista = new List<Expresion>();
                    lista.Add(EXPRESION(padre.ChildNodes[2]));
                    lista.Add(EXPRESION(padre.ChildNodes[4]));
                    lista.Add(EXPRESION(padre.ChildNodes[6]));
                    llamada = new Llamada(Llamada.Tipo_Nativa.SUBCAD, lista);
                    llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
                    llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
                    break;
            }
            return llamada;
        }

        /**
         * Retorna la llamada nativa numerica
         * */
        public Llamada NATIVA_NUMERICA(ParseTreeNode padre)
        {

        }

        public Llamada NATIVA_BOOLEANA(ParseTreeNode padre)
        {//pr_booleano + parizq + EXPRESION + parder
            Llamada llamada;
            List<Expresion> lista = new List<Expresion>();
            lista.Add(EXPRESION(padre.ChildNodes[2]));
            llamada = new Llamada(Llamada.Tipo_Nativa.BOOLEANO, lista);
            llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
            llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
            return llamada;
        }

        public Llamada NATIVA_FECHAHORA(ParseTreeNode padre)
        {

        }

        public Llamada NATIVA_MULTIMEDIA(ParseTreeNode padre)
        {

        }
        /**
         * Retorna lista de llamadas
         * */
        public List<Llamada> MLLAMADAS(ParseTreeNode padre)
        {//MakePlusRule(MLLAMADAS, tk_punto, LLAMADA)
            List<Llamada> lista = new List<Llamada>();
            for (int i = 0; i < padre.ChildNodes.Count; i++)
            {
                lista.Add(LLAMADA(padre.ChildNodes[i]));
            }
            return lista;
        }

        /**
         * Retorna una llamada
         * */
        public Llamada LLAMADA(ParseTreeNode padre)
        {
            Llamada llamada = null;
            switch (padre.ChildNodes.Count)
            {
                case 1://( id | pr_este )
                    if (padre.ChildNodes[0].Term.Name.Equals("ID"))
                    {
                        llamada = new Llamada(Llamada.Tipo_Llamada.ID, padre.ChildNodes[0].Token.Text);
                        llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
                        llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
                    }
                    else
                    {
                        llamada = new Llamada(Llamada.Tipo_Llamada.ESTE, padre.ChildNodes[0].Token.Text);
                        llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
                        llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
                    }
                    break;
                case 2://id + ACC_DIMENSIONES
                    llamada = new Llamada(Llamada.Tipo_Llamada.ARREGLO, padre.ChildNodes[0].Token.Text, ACC_DIMENSIONES(padre.ChildNodes[1]));
                    llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
                    llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
                    break;
                case 4://id + parizq + LISTA_VAL_PARAMETROS + parder
                    llamada = new Llamada(Llamada.Tipo_Llamada.METODO, padre.ChildNodes[0].Token.Text, LISTA_VAL_PARAMETROS(padre.ChildNodes[2]));
                    llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
                    llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
                    break;
                default:
                    break;
            }
            return llamada;
        }
    
        /**
         * Retorna una lista de valores de parametros
         * */
        public List<Expresion> LISTA_VAL_PARAMETROS(ParseTreeNode padre)
        {//MakeStarRule(LISTA_VAL_PARAMETROS, tk_coma, EXPRESION)
            List<Expresion> lista = new List<Expresion>();
            for (int i = 0; i < padre.ChildNodes.Count; i++)
            {
                lista.Add(EXPRESION(padre.ChildNodes[i]));
            }
            return lista;
        }

        /**
         * Retorna una lista de expresiones de acceso de dimensiones
         * */
        public List<Expresion> ACC_DIMENSIONES(ParseTreeNode padre)
        {//MakePlusRule(ACC_DIMENSIONES, ACC_DIMENSION)
            List<Expresion> lista = new List<Expresion>();
            for (int i = 0; i < padre.ChildNodes.Count; i++)
            {
                lista.Add(ACC_DIMENSION(padre.ChildNodes[i]));
            }
            return lista;
        }
    
        /**
         * Retorna la expresion de un acceso dimension
         * */
        public Expresion ACC_DIMENSION(ParseTreeNode padre)
        {//[ EXPRESION ]
            return EXPRESION(padre.ChildNodes[1]);
        }
    }
}
