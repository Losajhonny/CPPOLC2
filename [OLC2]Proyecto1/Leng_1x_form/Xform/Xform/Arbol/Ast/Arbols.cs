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
            switch (padre.ChildNodes.Count)
            {
                case 1:
                    if (padre.ChildNodes[0].Term.Name.Equals("ARITMETICA"))
                    {
                        return ARITMETICA(padre.ChildNodes[0]);
                    }
                    else if (padre.ChildNodes[0].Term.Name.Equals("RELACIONAL"))
                    {
                        return RELACIONAL(padre.ChildNodes[0]);
                    }
                    else if (padre.ChildNodes[0].Term.Name.Equals("LOGICA"))
                    {
                        return LOGICA(padre.ChildNodes[0]);
                    }
                    else if (padre.ChildNodes[0].Term.Name.Equals("OPERADOR"))
                    {
                        return OPERADOR(padre.ChildNodes[0]);
                    }
                    else if (padre.ChildNodes[0].Term.Name.Equals("FACTOR"))
                    {
                        return FACTOR(padre.ChildNodes[0]);
                    }
                    else if (padre.ChildNodes[0].Term.Name.Equals("LLAMADAS"))
                    {
                        return new Expresion(LLAMADAS(padre.ChildNodes[0]), Expresion.Tipo_Operacion.LLAMADA);
                    }
                    return null;
                case 3:// ( EXPRESION )
                    return EXPRESION(padre.ChildNodes[1]);
                default:
                    return null;
            }
        }

        /**
         * Retorna el arbol de una expresion aritmetica
         * */
        public Expresion ARITMETICA(ParseTreeNode padre)
        {
            switch (padre.ChildNodes.Count)
            {
                case 2:// ( tmenos | tmas ) EXPRESION   [Unarios]
                    Expresion r = EXPRESION(padre.ChildNodes[1]);
                    return new Expresion(Expresion.getOperador(padre.ChildNodes[0].Token.Text), 
                        r, Expresion.Tipo_Operacion.ARITMETICA);
                case 3:// EXPRESION op EXPRESION
                    Expresion r1 = EXPRESION(padre.ChildNodes[0]);
                    Expresion r2 = EXPRESION(padre.ChildNodes[2]);
                    return new Expresion(r1, r2, 
                        Expresion.getOperador(padre.ChildNodes[1].Token.Text), 
                        Expresion.Tipo_Operacion.ARITMETICA);
                default:
                    return null;
            }
        }

        /**
         * Retorna el arbol de una expresion relacional
         * */
        public Expresion RELACIONAL(ParseTreeNode padre)
        {
            //EXPRESION op EXPRESION
            Expresion r1 = EXPRESION(padre.ChildNodes[0]);
            Expresion r2 = EXPRESION(padre.ChildNodes[2]);
            return new Expresion(r1, r2, 
                Expresion.getOperador(padre.ChildNodes[1].Token.Text), 
                Expresion.Tipo_Operacion.RELACIONAL);
        }

        /**
         * Retorna el arbol de una expresion logica
         * */
        public Expresion LOGICA(ParseTreeNode padre)
        {
            switch (padre.ChildNodes.Count)
            {
                case 2:// not EXPRESION
                    Expresion r = EXPRESION(padre.ChildNodes[1]);
                    return new Expresion(Expresion.getOperador(padre.ChildNodes[0].Token.Text), 
                        r, Expresion.Tipo_Operacion.LOGICO);
                case 3:// EXPRESION op EXPRESION
                    Expresion r1 = EXPRESION(padre.ChildNodes[0]);
                    Expresion r2 = EXPRESION(padre.ChildNodes[2]);
                    return new Expresion(r1, r2, 
                        Expresion.getOperador(padre.ChildNodes[1].Token.Text), 
                        Expresion.Tipo_Operacion.LOGICO);
                default:
                    return null;
            }
        }

        /**
         * Retorna el arbol de una expresion
         * devuleve un incremento o decremento
         * */
        public Expresion OPERADOR(ParseTreeNode padre)
        {//operadores de incremento, decremento
            if (padre.ChildNodes[0].Term.Name.Equals("EXPRESION"))
            {// EXPRESION ( dmas | dmenos )
                Expresion r = EXPRESION(padre.ChildNodes[0]);
                return new Expresion(r, 
                    Expresion.getOperador(padre.ChildNodes[1].Token.Text), 
                    Expresion.Tipo_Operacion.ARITMETICA);
            }
            else
            {// ( dmas | dmenos ) EXPRESION
                Expresion r = EXPRESION(padre.ChildNodes[1]);
                return new Expresion(Expresion.getOperador(padre.ChildNodes[0].Token.Text), 
                    r, Expresion.Tipo_Operacion.ARITMETICA);
            }
        }

        /**
         * Retorna el arbol de una expresion factor
         * */
        public Expresion FACTOR(ParseTreeNode padre)
        {//cadena | entero | ddecimal | VBOOLEANO | fecha | hora | fechahora | id
            if (padre.ChildNodes[0].Term.Name.Equals("VBOOLEANO"))
            {
                return VBOOLEANO(padre.ChildNodes[0]);
            }
            else
            {
                return new Expresion(padre.ChildNodes[0].Token.Text, 
                    padre.ChildNodes[0].Token.Location.Line,
                    padre.ChildNodes[0].Token.Location.Column, 
                    TipoDato.getTipo(padre.ChildNodes[0].Token.Terminal.Name));
            }
        }

        /**
         * Retorna el arbol de una expresion booleana
         * */
        public Expresion VBOOLEANO(ParseTreeNode padre)
        {//( verdadero | falso )
            Expresion exp = new Expresion(padre.ChildNodes[0].Token.Text, 
                padre.ChildNodes[0].Token.Location.Line,
                padre.ChildNodes[0].Token.Location.Column, 
                TipoDato.getTipo(padre.ChildNodes[0].Token.Terminal.Name));
            return exp;
        }
    
        /**
         * Retorna el arbol de una llamada
         * */
        public Llamada LLAMADAS(ParseTreeNode padre)
        {
            if (padre.ChildNodes[0].Term.Name.Equals("MLLAMADAS"))
            {//MLlamadas
                return new Llamada(MLLAMADAS(padre.ChildNodes[0]));
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
                    llamada = new Llamada(padre.ChildNodes[0].Term.Name, lista);
                    llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
                    llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
                    break;
                case 6://pr_poscad + parizq + EXPRESION + tk_coma + EXPRESION + parder
                    lista = new List<Expresion>();
                    lista.Add(EXPRESION(padre.ChildNodes[2]));
                    lista.Add(EXPRESION(padre.ChildNodes[4]));
                    llamada = new Llamada(padre.ChildNodes[0].Term.Name, lista);
                    llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
                    llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
                    break;
                default://pr_subcad + parizq + EXPRESION + tk_coma + EXPRESION + tk_coma + EXPRESION + parder
                    lista = new List<Expresion>();
                    lista.Add(EXPRESION(padre.ChildNodes[2]));
                    lista.Add(EXPRESION(padre.ChildNodes[4]));
                    lista.Add(EXPRESION(padre.ChildNodes[6]));
                    llamada = new Llamada(padre.ChildNodes[0].Term.Name, lista);
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
            /**
              pr_entero + parizq + LISTA_VAL_PARAMETROS + parder
            | pr_min + parizq + LISTA_VAL_PARAMETROS + parder
            | pr_max + parizq + LISTA_VAL_PARAMETROS + parder
            | pr_random + parizq + LISTA_VAL_PARAMETROS + parder

            | pr_pow + parizq + EXPRESION + tk_coma + EXPRESION + parder

            | pr_tam + parizq + EXPRESION + parder
            | pr_log + parizq + EXPRESION + parder
            | pr_log10 + parizq + EXPRESION + parder
            | pr_abs + parizq + EXPRESION + parder
            | pr_sin + parizq + EXPRESION + parder
            | pr_cos + parizq + EXPRESION + parder
            | pr_tan + parizq + EXPRESION + parder
            | pr_sqrt + parizq + EXPRESION + parder

            | pr_pi + parizq + parder
            | pr_random + parizq + parder
            * */
            Llamada llamada = null;
            switch (padre.ChildNodes.Count)
            {
                case 3://pi, random
                    llamada = new Llamada(padre.ChildNodes[0].Term.Name, new List<Expresion>());
                    break;
                case 4:
                    if(padre.ChildNodes[0].Token.Text.ToLower().Equals("entero") ||
                        padre.ChildNodes[0].Token.Text.ToLower().Equals("random") ||
                        padre.ChildNodes[0].Token.Text.ToLower().Equals("max") ||
                        padre.ChildNodes[0].Token.Text.ToLower().Equals("min"))
                    {
                        llamada = new Llamada(padre.ChildNodes[0].Term.Name, 
                            LISTA_VAL_PARAMETROS(padre.ChildNodes[2]));
                    }
                    else
                    {
                        List<Expresion> lista = new List<Expresion>();
                        lista.Add(EXPRESION(padre.ChildNodes[2]));
                        llamada = new Llamada(padre.ChildNodes[0].Term.Name, lista);
                    }
                    break;
                default:
                    List<Expresion> listas = new List<Expresion>();
                    listas.Add(EXPRESION(padre.ChildNodes[2]));
                    listas.Add(EXPRESION(padre.ChildNodes[4]));
                    llamada = new Llamada(padre.ChildNodes[0].Term.Name, listas);
                    break;
            }
            return llamada;
        }

        /**
         * Retorna la llamada nativa booleana
         * */
        public Llamada NATIVA_BOOLEANA(ParseTreeNode padre)
        {//pr_booleano + parizq + EXPRESION + parder
            Llamada llamada;
            List<Expresion> lista = new List<Expresion>();
            lista.Add(EXPRESION(padre.ChildNodes[2]));
            llamada = new Llamada(padre.ChildNodes[0].Term.Name, lista);
            llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
            llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
            return llamada;
        }

        /**
         * Retorna la llamada nativa fechahora
         * */
        public Llamada NATIVA_FECHAHORA(ParseTreeNode padre)
        {
            /**
            pr_hoy + parizq + parder
            | pr_ahora + parizq + parder
            | pr_fecha + parizq + EXPRESION + parder
            | pr_hora + parizq + EXPRESION + parder
            | pr_fechahora + parizq + EXPRESION + parder
            * */
            Llamada llamada = null;
            List<Expresion> lista = new List<Expresion>();
            switch (padre.ChildNodes.Count)
            {
                case 3:
                    llamada = new Llamada(padre.ChildNodes[0].Term.Name, new List<Expresion>());
                    llamada = new Llamada(padre.ChildNodes[0].Term.Name, lista);
                    llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
                    llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
                    break;
                default:
                    lista.Add(EXPRESION(padre.ChildNodes[2]));
                    llamada = new Llamada(padre.ChildNodes[0].Term.Name, lista);
                    llamada = new Llamada(padre.ChildNodes[0].Term.Name, lista);
                    llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
                    llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
                    break;
            }
            return llamada;
        }

        /**
         * Retorna la llamada nativa multimedia
         * */
        public Llamada NATIVA_MULTIMEDIA(ParseTreeNode padre)
        {
            /**
            pr_imagen + parizq + EXPRESION + tk_coma + EXPRESION + parder
            | pr_video + parizq + EXPRESION + tk_coma + EXPRESION + parder
            | pr_audio + parizq + EXPRESION + tk_coma + EXPRESION + parder
            * */
            Llamada llamada = null;
            List<Expresion> expresiones = new List<Expresion>();
            expresiones.Add(EXPRESION(padre.ChildNodes[2]));
            expresiones.Add(EXPRESION(padre.ChildNodes[4]));
            llamada = new Llamada(padre.ChildNodes[0].Term.Name, expresiones);
            llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
            llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
            return llamada;
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
                case 1://id
                    llamada = new Llamada(padre.ChildNodes[0].Token.Text);
                    llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
                    llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
                    break;
                case 2://id + ACC_DIMENSIONES
                    llamada = new Llamada(Llamada.Tipo_Llamada.ARREGLO, padre.ChildNodes[0].Token.Text, ACC_DIMENSIONES(padre.ChildNodes[1]));
                    llamada.Linea = padre.ChildNodes[0].Token.Location.Line;
                    llamada.Columna = padre.ChildNodes[0].Token.Location.Column;
                    break;
                case 3://este . llamada
                    llamada = new Llamada();
                    llamada.setCLlamada(LLAMADA(padre.ChildNodes[2]));
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
