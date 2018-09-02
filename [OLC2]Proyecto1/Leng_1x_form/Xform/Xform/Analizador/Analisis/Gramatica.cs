using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xform.Analizador.Analisis
{
    /**
     * @clase Gramatica
     * @autor        : Jhonatan Lopez
     * @carnet       : 201325583
     * @universidad  : USAC
     * @facultad     : ingenieria
     * */

    class Gramatica : Grammar
    {
        /**
         * Se define todas las palabras reservadas
         * Listado de palabras reservadas que tendra el lenguaje
         * */
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
        
        /**
         * Definicion de no terminales del lenguaje
         * se utilizan para hacer las producciones de
         * la gramatica
         * */
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
            /**
             * Expresiones regulares que definen los datos de entrada
             * de un tipo de dato ejemplo: id, cadena, entero, decimal, etc.
             * */
            #region EXPRESIONES REGULARES
            CommentTerminal line = new CommentTerminal("LINE", "$$", "\n", "\r\n");
            CommentTerminal multiline = new CommentTerminal("MULTI_LINE", "$#", "#$");
            NumberLiteral entero = new NumberLiteral("ENTERO");
            StringLiteral cadena = new StringLiteral("CADENA", "\"", StringOptions.AllowsAllEscapes);
            IdentifierTerminal id = new IdentifierTerminal("ID");

            RegexBasedTerminal ddecimal = new RegexBasedTerminal("DECIMAL", @"[0-9]+[\.][0-9]+");
            RegexBasedTerminal hora = new RegexBasedTerminal("HORA", "([\"][0-9][0-9][:][0-9][0-9][:][0-9][0-9][\"]|[\'][0-9][0-9][:][0-9][0-9][:][0-9][0-9][\'])");
            RegexBasedTerminal fecha = new RegexBasedTerminal("FECHA", "([\"][0-9][0-9][/][0-9][0-9][/][0-9][0-9][0-9][0-9][\"]|[\'][0-9][0-9][/][0-9][0-9][/][0-9][0-9][0-9][0-9][\'])");
            RegexBasedTerminal fechahora = new RegexBasedTerminal("FECHAHORA", "([\"][0-9][0-9][/][0-9][0-9][/][0-9][0-9][0-9][0-9][ ][0-9][0-9][:][0-9][0-9][:][0-9][0-9][\"]|[\'][0-9][0-9][/][0-9][0-9][/][0-9][0-9][0-9][0-9][ ][0-9][0-9][:][0-9][0-9][:][0-9][0-9][\'])");
	        #endregion

            /**
             * Agregando los comentarios
             * */
            NonGrammarTerminals.Add(line);
            NonGrammarTerminals.Add(multiline);

            /**
             * Palabras reservadas y Operadores y Simbolos
             * */
            #region TERMINALES
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
                pr_pi = ToTerm("pi"),
                pr_nulo = ToTerm("nulo"),
                pr_de = ToTerm("de");

            /**
             * LLamar al metodo MarkReservedWords con el vector de
             * palabras reservadas para darle mas prioridad que los
             * identificadores
             * */
            MarkReservedWords(palabras_reservadas);

            Terminal parizq = ToTerm("("),
                parder = ToTerm(")"),
                llaizq = ToTerm("{"),
                llader = ToTerm("}"),
                corizq = ToTerm("["),
                corder = ToTerm("]"),
                tk_punto = ToTerm("."),
                tk_coma = ToTerm(","),
                tk_mas = ToTerm("+"),
                tk_menos = ToTerm("-"),
                tk_por = ToTerm("*"),
                tk_div = ToTerm("/"),
                tk_pot = ToTerm("^"),
                tk_mod = ToTerm("%"),
                tk_ptcoma = ToTerm(";"),
                tk_mayor = ToTerm(">"),
                tk_menor = ToTerm("<"),
                tk_mayigual = ToTerm(">="),
                tk_menigual = ToTerm("<="),
                tk_diferent = ToTerm("!="),
                tk_igualr = ToTerm("=="),
                tk_and = ToTerm("&&"),
                tk_or = ToTerm("||"),
                tk_not = ToTerm("!"),
                tk_igual = ToTerm("="),
                tk_dospuntos = ToTerm(":"),
                tk_dmas = ToTerm("++"),
                tk_dmenos = ToTerm("--");
            #endregion


            /**
             * No Terminal INI : inicio de la gramatica
             * */
            INI.Rule = IMPORTACIONES + CLASES;


            /**
             * Importaciones
             * */
            IMPORTACIONES.Rule = MakeStarRule(IMPORTACIONES, IMPORTACION);
            IMPORTACION.Rule = pr_importar + parizq + id + tk_punto + id + parder + tk_ptcoma;


            /**
             * Clases
             * */
            CLASES.Rule = MakeStarRule(CLASES, CLASE);
            CLASE.Rule = pr_clase + id + VISIBILIDAD + HERENCIA + llaizq + FUNCIONES + llader;


            /**
             * Visibilidad
             * */
            VISIBILIDAD.Rule = pr_publico
				            | pr_protegido
				            | pr_privado
				            | Empty
				            ;


            /**
             * Herencia
             * */
            HERENCIA.Rule = pr_padre + id
			            | Empty
			            ;


            /**
             * Funciones
             * */
            FUNCIONES.Rule = MakeStarRule(FUNCIONES, FUNCION);
            FUNCION.Rule = CONSTRUCTOR
			            | PRINCIPAL
			            | METODO
			            | DECLARACIONES + tk_ptcoma
			            | PLANTILLA
			            ;


            /**
             * Plantilla
             * */
            //por el momoento no tomar en cuenta la plantiila
            PLANTILLA.Rule = FORMULARIO
			            ;
            FORMULARIO.Rule = pr_formulario + id + llaizq + FUNCIONES + llader;


            /**
             * Constructor
             * */
            CONSTRUCTOR.Rule = id + parizq + LISTA_PARAMETROS + parder + llaizq + SUPER +  SENTENCIAS + llader;
            SUPER.Rule = pr_super + parizq + LISTA_VAL_PARAMETROS + parder + tk_ptcoma
		            | Empty
		            ;


            /**
             * Metodo Principal
             * */
            PRINCIPAL.Rule = pr_principal + parizq + LISTA_PARAMETROS + parder + llaizq + SENT_PRINCIPAL + llader;
            SENT_PRINCIPAL.Rule = SENTENCIAS
					            | pr_nuevo + id + parizq + parder + tk_punto + id + tk_ptcoma
					            ;


            /**
             * Metodo
             * */
            METODO.Rule = VISIBILIDAD + TIPO_RETORNO + id + parizq + LISTA_PARAMETROS + parder + llaizq + SENTENCIAS + llader;


            /**
             * Tipo Retorno
             * */
            TIPO_RETORNO.Rule = TIPO_DATO
				            | pr_vacio
				            ;


            /**
             * Tipo dato
             * */
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

            
            /**
             * Expresiones
             * */
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


            /**
             * LLamadas a funciones, id, clases, etc
             * */
            LLAMADAS.Rule = MLLAMADAS
			            |  NLLAMADAS; // llamdas de funciones nativas

            MLLAMADAS.Rule = MakePlusRule(MLLAMADAS, tk_punto, LLAMADA);

            LLAMADA.Rule = id + parizq + LISTA_VAL_PARAMETROS + parder //llamada a un metodo
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
			            | pr_subcad + parizq + EXPRESION + tk_coma + EXPRESION + tk_coma + EXPRESION + parder
			            | pr_poscad + parizq + EXPRESION + tk_coma + EXPRESION + parder
			            ;

            NATIVA_BOOLEANA.Rule = pr_booleano + parizq + EXPRESION + parder;

            NATIVA_NUMERICA.Rule = pr_entero + parizq + LISTA_VAL_PARAMETROS + parder // ARGUMENTOS LISTA VALORES PARAMETROS
				            | pr_tam + parizq + EXPRESION + parder
				            | pr_random + parizq + LISTA_VAL_PARAMETROS + parder
                            | pr_random + parizq + parder
				            | pr_min + parizq + LISTA_VAL_PARAMETROS + parder
				            | pr_max + parizq + LISTA_VAL_PARAMETROS + parder
				            | pr_pow + parizq + EXPRESION + tk_coma + EXPRESION + parder
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
					            | pr_fecha + parizq + EXPRESION + parder
					            | pr_hora + parizq + EXPRESION + parder
					            | pr_fechahora + parizq + EXPRESION + parder
					            ;

            NATIVA_MULTIMEDIA.Rule = pr_imagen + parizq + EXPRESION + tk_coma + EXPRESION + parder
					            | pr_video + parizq + EXPRESION + tk_coma + EXPRESION + parder
					            | pr_audio + parizq + EXPRESION + tk_coma + EXPRESION + parder
					            ;

            VBOOLEANO.Rule = pr_verdadero
			            |	pr_falso
			            ;
            

            /**
             * Parametros
             * */
            LISTA_PARAMETROS.Rule = MakeStarRule(LISTA_PARAMETROS, tk_coma, PARAMETRO);
            LISTA_VAL_PARAMETROS.Rule = MakeStarRule(LISTA_VAL_PARAMETROS, tk_coma, EXPRESION);
            PARAMETRO.Rule = TIPO_DATO + DIMENSIONES + id;


            /**
             * Dimensiones
             * */
            DIMENSIONES.Rule = MakeStarRule(DIMENSIONES, DIMENSION);
            DIMENSION.Rule = corizq + corder;

            ACC_DIMENSIONES.Rule = MakePlusRule(ACC_DIMENSIONES, ACC_DIMENSION);

            ACC_DIMENSION.Rule = corizq + EXPRESION + corder;

            ASIG_DIMENSIONES.Rule = llaizq + ASIG_DIMENSION + llader;

            ASIG_DIMENSION.Rule = ASIG_DIMENSION_U
                           | ASIG_DIMENSION_M;

            ASIG_DIMENSION_U.Rule = MakeStarRule(ASIG_DIMENSION_U, tk_coma, EXPRESION);

            ASIG_DIMENSION_M.Rule = MakeStarRule(ASIG_DIMENSION_M, tk_coma, ASIG_DIMENSIONES);


            /**
             * Declaraciones
             * */
            DECLARACIONES.Rule = TIPO_DATO + VISIBILIDAD + id + tk_igual + pr_nuevo + TIPO_DATO + parizq + LISTA_PARAMETROS + parder
                            | TIPO_DATO + VISIBILIDAD + id + DIMENSIONES + tk_igual + pr_nuevo + TIPO_DATO + ACC_DIMENSIONES
                            | TIPO_DATO + VISIBILIDAD + id + DIMENSIONES + tk_igual + ASIG_DIMENSIONES
                            | TIPO_DATO + VISIBILIDAD + id + DIMENSIONES + tk_igual + EXPRESION
                            | TIPO_DATO + VISIBILIDAD + id + DIMENSIONES + tk_igual + pr_nulo
                            | TIPO_DATO + VISIBILIDAD + id + DIMENSIONES
                            | TIPO_DATO + VISIBILIDAD + id + tk_igual + pr_nulo
                            | TIPO_DATO + VISIBILIDAD + id + tk_igual + EXPRESION
                            | TIPO_DATO + VISIBILIDAD + id
				            ;


            /**
             * Asignaciones
             * */
            ASIGNACION.Rule = LLAMADAS + tk_igual + EXPRESION;


            /**
             * Sentencias
             * */
            SENTENCIAS.Rule = MakeStarRule(SENTENCIAS, SENTENCIA);

            SENTENCIA.Rule = IMPRIMIR + tk_ptcoma
                    | REPETIR + tk_ptcoma
                    | HACER + tk_ptcoma
                    | RETORNO + tk_ptcoma
                    | pr_continuar + tk_ptcoma
                    | pr_romper + tk_ptcoma
                    | LLAMADAS + tk_ptcoma
                    | ASIGNACION + tk_ptcoma
                    | DECLARACIONES + tk_ptcoma
                    | SENT_SI
                    | PARA
                    | MIENTRAS
                    | CASOS
                    ;


            /**
             * Imprimir
             * */
            IMPRIMIR.Rule = pr_imprimir + parizq + EXPRESION + parder;


            /**
             * Repetir
             * */
            REPETIR.Rule = pr_repetir + llaizq + SENTENCIAS + llader + pr_hasta + parizq + EXPRESION + parder;


            /**
             * Hacer
             * */
            HACER.Rule = pr_hacer + llaizq + SENTENCIAS + llader + pr_mientras + parizq + EXPRESION + parder;

            
            /**
             * Para
             * */
            PARA.Rule = pr_para + parizq + VAR_CONTROL + tk_ptcoma + EXPRESION + tk_ptcoma + EXPRESION + parder + llaizq + SENTENCIAS + llader;

            VAR_CONTROL.Rule = ASIGNACION
			            | DECLARACIONES
                        ;

            
            /**
             * Mientras
             * */
            MIENTRAS.Rule = pr_mientras + parizq + EXPRESION + parder + llaizq + SENTENCIAS + llader;
            

            /**
             * Sentencias si
             * */
            SENT_SI.Rule = SI
                        | SI + VARIOS_SINO_SI + SINO
                        | SI + VARIOS_SINO_SI
                        | SI + SINO
                        ;

            SI.Rule = pr_si + parizq + EXPRESION + parder + llaizq + SENTENCIAS + llader;

            SINO_SI.Rule = pr_sino + SI;

            SINO.Rule = pr_sino + llaizq + SENTENCIAS + llader;

            VARIOS_SINO_SI.Rule = MakePlusRule(VARIOS_SINO_SI, SINO_SI);

            
            /**
             * Casos
             * */
            CASOS.Rule = pr_caso + parizq + EXPRESION + parder + pr_de + llaizq + CASOS_VALORES + CASO_DEFECTO + llader;

            CASOS_VALORES.Rule = MakePlusRule(CASOS_VALORES, CASO_VALOR);

            CASO_VALOR.Rule = pr_valor + EXPRESION + tk_dospuntos + llaizq + SENTENCIAS + llader;

            CASO_DEFECTO.Rule = pr_default + tk_dospuntos + llaizq + SENTENCIAS + llader
				            | Empty;

            /**
             * Retornos
             * */
            RETORNO.Rule = pr_retorno
                    | pr_retorno + EXPRESION
                    ;


            /**
             * Precedencia y asociatividad
             * Se resuelve la ambiguedad que puede haber al 
             * generar el arbol sintactico
             * */
            #region PRECEDENCIA Y ASOCIATIVAD
            RegisterOperators(8, Associativity.Right, tk_not, tk_pot);
            RegisterOperators(7, Associativity.Left, tk_por, tk_div, tk_mod);
            RegisterOperators(6, Associativity.Left, tk_mas, tk_menos);
            RegisterOperators(5, Associativity.Left, tk_menor, tk_menigual, tk_mayor, tk_mayigual);
            RegisterOperators(4, Associativity.Left, tk_igualr, tk_diferent);
            RegisterOperators(3, Associativity.Left, tk_and);
            RegisterOperators(2, Associativity.Left, tk_or);
            #endregion

            /**
             * Indica la raiz del arbol que se generara y ademas es el 
             * simbolo inicial donde comenzara la gramttica
             * */
            this.Root = EXPRESION;
        }
    }
}
