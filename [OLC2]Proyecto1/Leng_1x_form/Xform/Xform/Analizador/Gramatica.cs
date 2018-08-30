﻿using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xform.Analizador
{
    /// <summary>
    /// universidad:    @ USAC
    /// autor :         @ Jhonatan Lopez
    /// carnet:         @ 201325583
    /// </summary>
    class Gramatica : Grammar
    {
        public static string[] palabras_reservadas = { "importar", "clase", "publico", "privado", "protegido",
                                                    "padre", "formulario", "super", "principal", "nuevo",
                                                    "vacio", "cadena", "booleano", "entero", "decimal",
                                                    "fecha", "hora", "fechahora", "respuestas", "este",
                                                    "subcad", "poscad", "tam", "random", "min",
                                                    "max", "pow", "log", "log10", "abs",
                                                    "sin", "cos", "tan", "sqrt", "pi",
                                                    "hoy", "ahora", "imagen", "video", "audio",
                                                    "verdadero", "falso", "nulo", "imprimir", "repetir",
                                                    "hasta", "hacer", "mientras", "para", "si",
                                                    "sino", "caso", "valor", "default", "retorno",
                                                    "continuar", "romper", "de" };
        
        #region NO TERMINALES
        NonTerminal INI = new NonTerminal("INI"),
            IMPORTACIONES = new NonTerminal("IMPORTACIONES"),
            IMPORTACION = new NonTerminal("IMPORTACION"),
            CLASES = new NonTerminal("CLASES"),
            CLASE = new NonTerminal("CLASE"),
            VISIBILIDAD = new NonTerminal("VISIBILIDAD"),
            HERENCIA = new NonTerminal("HERENCIA"),
            FUNCIONES = new NonTerminal("FUNCIONES"),
            FUNCION = new NonTerminal("FUNCION"),
            CONSTRUCTOR = new NonTerminal("CONSTRUCTOR"),
            PRINCIPAL = new NonTerminal("PRINCIPAL"),
            METODO = new NonTerminal("METODO"),
            PLANTILLA = new NonTerminal("PLANTILLA"),
            FORMULARIO = new NonTerminal("FORMULARIO"),
            LISTA_PARAMETROS = new NonTerminal("LISTA_PARAMETROS"),
            SUPER = new NonTerminal("SUPER"),
            LISTA_VAL_PARAMETROS = new NonTerminal("LISTA_VAL_PARAMETROS"),
            SENTENCIAS = new NonTerminal("SENTENCIAS"),
            SENT_PRINCIPAL = new NonTerminal("SENT_PRINCIPAL"),
            TIPO_RETORNO = new NonTerminal("TIPO_RETORNO"),
            TIPO_DATO = new NonTerminal("TIPO_DATO"),
            EXPRESION = new NonTerminal("EXPRESION"),
            ARITMETICA = new NonTerminal("ARITMETICA"),
            RELACIONAL = new NonTerminal("RELACIONAL"),
            LOGICA = new NonTerminal("LOGICA"),
            OPERADOR = new NonTerminal("OPERADOR"),
            FACTOR = new NonTerminal("FACTOR"),
            LLAMADAS = new NonTerminal("LLAMADAS"),
            MLLAMADAS = new NonTerminal("MLLAMADAS"),
            NLLAMADAS = new NonTerminal("NLLAMADAS"),
            LLAMADA = new NonTerminal("LLAMADA"),
            ACC_DIMENSIONES = new NonTerminal("ACC_DIMENSIONES"),
            NATIVA_CADENA = new NonTerminal("NATIVA_CADENA"),
            NATIVA_BOOLEANA = new NonTerminal("NATIVA_BOOLEANA"),
            NATIVA_NUMERICA = new NonTerminal("NATIVA_NUMERICA"),
            NATIVA_FECHAHORA = new NonTerminal("NATIVA_FECHAHORA"),
            NATIVA_MULTIMEDIA = new NonTerminal("NATIVA_MULTIMEDIA"),
            VBOOLEANO = new NonTerminal("VBOOLEANO"),
            PARAMETRO = new NonTerminal("PARAMETRO"),
            DIMENSIONES = new NonTerminal("DIMENSIONES"),
            ACC_DIMENSION = new NonTerminal("ACC_DIMENSION"),
            ASIG_DIMENSIONES = new NonTerminal("ASIG_DIMENSIONES"),
            ASIG_DIMENSION = new NonTerminal("ASIG_DIMENSION"),
            ASIG_DIMENSION_U = new NonTerminal("ASIG_DIMENSION_U"),
            ASIG_DIMENSION_M = new NonTerminal("ASIG_DIMENSION_M"),
            DECLARACIONES = new NonTerminal("DECLARACIONES"),
            ASIGNACION = new NonTerminal("ASIGNACION"),
            SENTENCIA = new NonTerminal("SENTENCIA"),
            IMPRIMIR = new NonTerminal("IMPRIMIR"),
            REPETIR = new NonTerminal("REPETIR"),
            RETORNO = new NonTerminal("RETORNO"),
            SENT_SI = new NonTerminal("SENT_SI"),
            PARA = new NonTerminal("PARA"),
            MIENTRAS = new NonTerminal("MIENTRAS"),
            CASOS = new NonTerminal("CASOS"),
            VAR_CONTROL = new NonTerminal("VAR_CONTROL"),
            SI = new NonTerminal("SI"),
            VARIOS_SINO_SI = new NonTerminal("VARIOS_SINO_SI"),
            SINO = new NonTerminal("SINO"),
            SINO_SI = new NonTerminal("SINO_SI"),
            CASOS_VALORES = new NonTerminal("CASOS_VALORES"),
            CASO_DEFECTO = new NonTerminal("CASO_DEFECTO"),
            CASO_VALOR = new NonTerminal("CASO_VALOR"),
            HACER = new NonTerminal("HACER"),
            DIMENSION = new NonTerminal("DIMENSION");

            
        #endregion
        
        public Gramatica()
            : base(false)
        {
            #region EXPRESIONES REGULARES
	        #endregion

            #region TERMINALES
            /* PALABRAS RESERVADAS */
            KeyTerm pr_importar = ToTerm("importar"),
                pr_clase = ToTerm("clase"),
                pr_publico = ToTerm("publico"),
                pr_privado = ToTerm("privado"),
                pr_protegido = ToTerm("protegido"),
                pr_padre = ToTerm("padre"),
                pr_formulario = ToTerm("formulario"),
                pr_super = ToTerm("super"),
                pr_principal = ToTerm("principal"),
                pr_nuevo = ToTerm("nuevo"),
                pr_vacio = ToTerm("vacio"),
                pr_cadena = ToTerm("cadena"),
                pr_entero = ToTerm("entero"),
                pr_decimal = ToTerm("decimal"),
                pr_fecha = ToTerm("fecha"),
                pr_hora = ToTerm("hora"),
                pr_fechahora = ToTerm("fechahora"),
                pr_booleano = ToTerm("booleano"),
                pr_respuestas = ToTerm("respuestas"),
                pr_este = ToTerm("este"),
                pr_subcad = ToTerm("subcad"),
                pr_poscad = ToTerm("poscad"),
                pr_retorno = ToTerm("retorno"),
                pr_default = ToTerm("default"),
                pr_valor = ToTerm("valor"),
                pr_caso = ToTerm("caso"),
                pr_sino = ToTerm("sino"),
                pr_si = ToTerm("si"),
                pr_mientras = ToTerm("mientras"),
                pr_para = ToTerm("para"),
                pr_hacer = ToTerm("hacer"),
                pr_repetir = ToTerm("repetir"),
                pr_hasta = ToTerm("hasta"),
                pr_imprimir = ToTerm("imprimir"),
                pr_romper = ToTerm("romper"),
                pr_continuar = ToTerm("continuar"),
                pr_verdadero = ToTerm("verdadero"),
                pr_falso = ToTerm("falso"),
                pr_audio = ToTerm("audio"),
                pr_video = ToTerm("video"),
                pr_imagen = ToTerm("imagen"),
                pr_ahora = ToTerm("ahora"),
                pr_hoy = ToTerm("hoy"),
                pr_tam = ToTerm("tam"),
                pr_random = ToTerm("random"),
                pr_min = ToTerm("min"),
                pr_max = ToTerm("max"),
                pr_pow = ToTerm("pow"),
                pr_log = ToTerm("log"),
                pr_log10 = ToTerm("log10"),
                pr_abs = ToTerm("abs"),
                pr_sin = ToTerm("sin"),
                pr_cos = ToTerm("cos"),
                pr_tan = ToTerm("tan"),
                pr_sqrt = ToTerm("sqrt"),
                pr_pi = ToTerm("pi")
                ;



            /* TOKENS */
            KeyTerm parizq = ToTerm("("),
                parder = ToTerm(")"),
                tk_ptcoma = ToTerm(";"),
                llaizq = ToTerm("{"),
                llader = ToTerm("}"),
                tk_punto = ToTerm("."),
                tk_mas = ToTerm("+"),
                tk_menos = ToTerm("-"),
                tk_por = ToTerm("*"),
                tk_div = ToTerm("/");
            #endregion

            INI.Rule = IMPORTACIONES + CLASES;

            /* IMPORTACIONES */
            IMPORTACIONES.Rule = MakeStarRule(IMPORTACIONES, IMPORTACION);
            IMPORTACION.Rule = pr_importar + parizq + id + tk_punto + id + parder + tk_ptcoma;





            /* CLASE */
            CLASES.Rule = MakeStarRule(CLASES, CLASE);
            CLASE.Rule = pr_clase + id + VISIBILIDAD + HERENCIA + llaizq + FUNCIONES + llader;






            /* VISIBILIDAD */
            VISIBILIDAD.Rule = pr_publico
				            | pr_protegido
				            | pr_privado
				            | Empty
				            ;






            /* HERENCIA */
            HERENCIA.Rule = pr_padre + id
			            | Empty
			            ;









            /* FUNCIONES */
            FUNCIONES.Rule = MakeStarRule(FUNCIONES, FUNCION);
            FUNCION.Rule = CONSTRUCTOR
			            | PRINCIPAL
			            | METODO
			            | DECLARACIONES + tk_ptcoma
			            | PLANTILLA
			            ;




            //por el momoento no tomar en cuenta la plantiila
            PLANTILLA.Rule = FORMULARIO
			            ;

            FORMULARIO.Rule = pr_formulario + id + llaizq + FUNCIONES + llader;






            /* CONSTRUCTOR */
            CONSTRUCTOR.Rule = id + parizq + LISTA_PARAMETROS + parder + llaizq + SUPER +  SENTENCIAS + llader;
            /* FUNCION SUPER DEL METODO CONSTRUCTOR */
            SUPER.Rule = pr_super + parizq + LISTA_VAL_PARAMETROS + parder
		            | Empty
		            ;






            /* METODO PRINCIPAL */
            PRINCIPAL.Rule = pr_principal + parizq + LISTA_PARAMETROS + parder + llaizq + SENT_PRINCIPAL + llader;


            SENT_PRINCIPAL.Rule = SENTENCIAS
					            | pr_nuevo + id + parizq + parder + tk_punto + id + tk_ptcoma
					            ;



            /* METODO */
            METODO.Rule = VISIBILIDAD + TIPO_RETORNO + id + parizq + LISTA_PARAMETROS + parder + llaizq + SENTENCIAS + llader;







            /* TIPO_RETORNO */
            TIPO_RETORNO.Rule = TIPO_DATO
				            | pr_vacio
				            ;

            /* TIPO_DATO */
            TIPO_DATO.Rule = pr_cadena
                        | pr_booleano
                        | pr_entero
                        | pr_decimal
                        | pr_fecha
                        | pr_hora
                        | pr_fechahora
                        | pr_respuestas
                        | id
                        ;







            /* EXPRESIONES */
            EXPRESION.Rule = ARITMETICA
			            | RELACIONAL
			            | LOGICA
			            | OPERADOR
			            | FACTOR
			            | LLAMADAS
			            | parizq + EXPRESION + parder
			            ;

            ARITMETICA.Rule = EXPRESION + tk_mas + EXPRESION
			            |	EXPRESION + tk_menos + EXPRESION
			            |	EXPRESION + tk_por + EXPRESION
			            |	EXPRESION + tk_div + EXPRESION
			            |	EXPRESION + tk_pot + EXPRESION
			            |	EXPRESION + tk_mod + EXPRESION
			            |	tk_menos + EXPRESION
			            |	tk_mas + EXPRESION
			            ;

            RELACIONAL.Rule = EXPRESION + tk_mayor + EXPRESION
			            |	EXPRESION + tk_menor + EXPRESION
			            |	EXPRESION + tk_mayigual + EXPRESION
			            |	EXPRESION + tk_menigual + EXPRESION
			            |	EXPRESION + tk_diferent + EXPRESION
			            |	EXPRESION + tk_igualr + EXPRESION
			            ;

            LOGICA.Rule = EXPRESION + tk_and + EXPRESION
			            | EXPRESION + tk_or + EXPRESION
			            | tk_not + EXPRESION
			            ;

            OPERADOR.Rule = tk_dmas + EXPRESION
			            |	tk_dmenos + EXPRESION
			            |	EXPRESION + tk_dmas
			            |	EXPRESION + tk_dmenos
			            ;

            FACTOR.Rule = cadena
			            | entero
			            | ddecimal
			            | VBOOLEANO
			            | fecha
			            | hora
			            | fechahora
			            | id
			            ;





            /* LLAMADAS A FUNCIONES */
            LLAMADAS.Rule = MLLAMADAS
			            |  NLLAMADAS; // llamdas de funciones nativas

            MLLAMADAS.Rule = MakePlusRule(MLLAMADAS, tk_punto, LLAMADA);

            LLAMADA.Rule = id + parizq + LISTA_VAL_PARAMETROS + parder //llamada a una clase
			            | id + ACC_DIMENSIONES 				//llamada a un arreglo
			            | id								//llamada a un atributo o declaracion
			            | pr_este 							//llamada a su propia clase
			            ;

            NLLAMADAS.Rule = NATIVA_CADENA
			            | NATIVA_BOOLEANA
			            | NATIVA_NUMERICA
			            | NATIVA_FECHAHORA
			            | NATIVA_MULTIMEDIA
			            ;

            NATIVA_CADENA.Rule = pr_cadena + parizq + EXPRESION + parder 	// fun nat cadena
			            | pr_subcad + parizq + cadena + tk_coma + EXPRESION + tk_coma + EXPRESION + parder
			            | pr_poscad + parizq + cadena + tk_coma + EXPRESION + parder
			            ;

            NATIVA_BOOLEANA.Rule = pr_booleano + parizq + EXPRESION + parder;

            NATIVA_NUMERICA.Rule = pr_entero + parizq + LISTA_VAL_PARAMETROS + parder // ARGUMENTOS LISTA VALORES PARAMETROS
				            | pr_tam + parizq + EXPRESION + parder
				            | pr_random + parizq + LISTA_VAL_PARAMETROS + parder
				            | pr_min + parizq + LISTA_VAL_PARAMETROS + parder
				            | pr_max + parizq + LISTA_VAL_PARAMETROS + parder
				            | pr_pow + parizq + EXPRESION + tk_coma + entero + parder
				            | pr_log + parizq + EXPRESION + parder
				            | pr_log10 + parizq + EXPRESION + parder
				            | pr_abs + parizq + EXPRESION + parder
				            | pr_sin + parizq + EXPRESION + parder
				            | pr_cos + parizq + EXPRESION + parder
				            | pr_tan + parizq + EXPRESION + parder
				            | pr_sqrt + parizq + EXPRESION + parder
				            | pr_pi + parizq + parder
				            ;

            NATIVA_FECHAHORA.Rule = pr_hoy + parizq + parder
					            | pr_ahora + parizq + parder
					            | pr_fecha + parizq + cadena + parder
					            | pr_hora + parizq + cadena + parder
					            | pr_fechahora + parizq + cadena + parder
					            ;

            NATIVA_MULTIMEDIA.Rule = pr_imagen + parizq + EXPRESION + tk_coma + VBOOLEANO
					            | pr_video + parizq + EXPRESION + tk_coma + VBOOLEANO
					            | pr_audio + parizq + EXPRESION + tk_coma + VBOOLEANO
					            ;

            VBOOLEANO.Rule = pr_verdadero
			            |	pr_falso
			            ;






            /* LISTA DE PARAMETROS ( PARAMETRO )*/
            LISTA_PARAMETROS.Rule = MakeStarRule(LISTA_PARAMETROS, tk_coma, PARAMETRO);
            LISTA_VAL_PARAMETROS.Rule = MakeStarRule(LISTA_VAL_PARAMETROS, tk_coma, EXPRESION);
            PARAMETRO.Rule = TIPO + DIMENSIONES + id;







            /* DIMENSIONES */
            DIMENSIONES.Rule = MakeStarRule(DIMENSIONES, DIMENSION);
            DIMENSION.Rule = corizq + corder;

            ACC_DIMENSIONES.Rule = MakePlusRule(ACC_DIMENSIONES, ACC_DIMENSION);

            ACC_DIMENSION.Rule = corizq + EXPRESION + corder;

            ASIG_DIMENSIONES.Rule = llaizq + ASIG_DIMENSION + llader;

            ASIG_DIMENSION.Rule = ASIG_DIMENSION_U
                           | ASIG_DIMENSION_M;

            ASIG_DIMENSION_U.Rule = MakeStarRule(ASIG_DIMENSION_U, tk_coma, EXPRESION);

            ASIG_DIMENSION_M.Rule = MakeStarRule(ASIG_DIMENSION_M, tk_coma, ASIG_DIMENSIONES);







            /* DECLARACIONES */
            DECLARACIONES.Rule = TIPO_DATO + VISIBILIDAD + id + tk_igual + pr_nuevo + TIPO_DATO + parizq + LISTA_PARAMETROS + parder
                            | TIPO_DATO + VISIBILIDAD + id + DIMENSIONES + tigual + pr_nuevo + TIPO_DATO + ACC_DIMENSIONES
                            | TIPO_DATO + VISIBILIDAD + id + DIMENSIONES + tigual + ASIG_DIMENSIONES
                            | TIPO_DATO + VISIBILIDAD + id + DIMENSIONES + tigual + EXPRESION
                            | TIPO_DATO + VISIBILIDAD + id + DIMENSIONES + tigual + pr_nulo
                            | TIPO_DATO + VISIBILIDAD + id + DIMENSIONES
                            | TIPO_DATO + VISIBILIDAD + id + tigual + pr_nulo
                            | TIPO_DATO + VISIBILIDAD + id + tigual + EXPRESION
                            | TIPO_DATO + VISIBILIDAD + id
				            ;





            /* ASIGNACIONES */
            ASIGNACION.Rule = LLAMADAS + tk_igual + EXPRESION;








            /* SENTENCIAS */
            SENTENCIAS.Rule = MakeStarRule(SENTENCIAS, SENTENCIA);

            SENTENCIA.Rule = IMPRIMIR + ptcoma
                    | REPETIR + ptcoma
                    | HACER + ptcoma
                    | RETORNO + ptcoma
                    | pr_continuar + ptcoma
                    | pr_romper + ptcoma
                    | LLAMADAS + ptcoma
                    | ASIGNACION + ptcoma
                    | DECLARACIONES + ptcoma
                    | SENT_SI
                    | PARA
                    | MIENTRAS
                    | CASOS
                    ;





            /* IMPRIMIR */
            IMPRIMIR.Rule = pr_imprimir + parizq + EXPRESION + parder;



            /* REPETIR */
            REPETIR.Rule = pr_repetir + llaizq + SENTENCIAS + llader + pr_hasta + parizq + EXPRESION + parder;



            /* HACER */
            HACER.Rule = pr_hacer + llaizq + SENTENCIAS + llader + pr_mientras + parizq + EXPRESION + parder;



            /* PARA */
            PARA.Rule = pr_para + parizq + VAR_CONTROL + tk_ptcoma + EXPRESION + tk_ptcoma + EXPRESION + parder + llaizq + SENTENCIAS + llader;

            VAR_CONTROL.Rule = ASIGNACION
			            | DECLARACIONES
                        ;



            /* MIENTRAS */
            MIENTRAS.Rule = pr_mientras + parizq + EXPRESION + parder + llaizq + SENTENCIAS + llader;



            /* SENTENCIA SI */
            SENT_SI.Rule = SI
                        | SI + VARIOS_SINO_SI + SINO
                        | SI + VARIOS_SINO_SI
                        | SI + SINO
                        ;

            SI.Rule = pr_si + parizq + EXPRESION + parder + llaizq + SENTENCIAS + llader;

            SINO_SI.Rule = pr_sino + SI;

            SINO.Rule = pr_sino + llaizq + SENTENCIAS + llader;

            VARIOS_SINO_SI.Rule = MakePlusRule(VARIOS_SINO_SI, SINO_SI);




            /* CASOS */
            CASOS.Rule = pr_caso + parizq + EXPRESION + parder + pr_de + llaizq + CASOS_VALORES + CASO_DEFECTO + llader;

            CASOS_VALORES.Rule = MakePlusRule(CASOS_VALORES, CASO_VALOR);

            CASO_VALOR.Rule = pr_valor + EXPRESION + dospuntos + llaizq + SENTENCIAS + llader;

            CASO_DEFECTO.Rule = pr_default + dospuntos + llaizq + SENTENCIAS + llader
				            | Empty;



            /* RETORNO */
            RETORNO.Rule = pr_retorno
                    | pr_retorno + EXPRESION
                    ;
        }
    }
}
