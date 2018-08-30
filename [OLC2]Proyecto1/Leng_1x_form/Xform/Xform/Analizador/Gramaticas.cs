using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xform.Analizador
{
    class Gramaticas : Grammar
    {
        public static string[] palabras_reservadas = { "importar", "cadena", "booleano", "entero", "decimal", "fecha", "hora", "fechahora", "respuestas", "si",
                                           "sino", "vacio", "caso", "de", "valor", "default", "para", "repetir", "hasta", "hacer", 
                                           "mientras", "imprimir", "continuar", "romper", "retorno", "publico", "privado", "protegido", "nuevo", "nulo",
                                           "este", "super", "principal", "clase", "padre", "formulario", "pregunta", "grupo"};
        #region EXPRESIONES REGULARES
        CommentTerminal line = new CommentTerminal("LINE", "$$", "\n");
        CommentTerminal multiline = new CommentTerminal("MULTI_LINE", "$#", "#$");
        NumberLiteral entero = new NumberLiteral("ENTERO");
        RegexBasedTerminal edecimal = new RegexBasedTerminal("DECIMAL", @"[0-9]+[\.][0-9]+");
        RegexBasedTerminal cadena = new RegexBasedTerminal("CADENA", "[\"]([^\"\n]|[\\\"]|[\\\n])*[\"]");
        RegexBasedTerminal boolean = new RegexBasedTerminal("BOOLEAN", "verdadero|falso");
        RegexBasedTerminal id = new RegexBasedTerminal("ID", "[a-zA-Z]([a-zA-Z0-9]|[_])*");
        RegexBasedTerminal hora = new RegexBasedTerminal("HORA", "([\"][0-9][0-9][:][0-9][0-9][:][0-9][0-9][\"]|[\'][0-9][0-9][:][0-9][0-9][:][0-9][0-9][\'])");
        RegexBasedTerminal fecha = new RegexBasedTerminal("FECHA", "([\"][0-9][0-9][/][0-9][0-9][/][0-9][0-9][0-9][0-9][\"]|[\'][0-9][0-9][/][0-9][0-9][/][0-9][0-9][0-9][0-9][\'])");
        RegexBasedTerminal fechahora = new RegexBasedTerminal("FECHAHORA", "([\"][0-9][0-9][/][0-9][0-9][/][0-9][0-9][0-9][0-9][ ][0-9][0-9][:][0-9][0-9][:][0-9][0-9][\"]|[\'][0-9][0-9][/][0-9][0-9][/][0-9][0-9][0-9][0-9][ ][0-9][0-9][:][0-9][0-9][:][0-9][0-9][\'])");
        #endregion

        #region PRODUCCIONES
        NonTerminal EXPRESION = new NonTerminal("EXPRESION");
        NonTerminal LLAMADAS = new NonTerminal("LLAMADAS");
        NonTerminal LLAMADA = new NonTerminal("LLAMADA");
        NonTerminal VAL_PARAMETROS = new NonTerminal("VAL_PARAMETROS");
        NonTerminal DIM_ARREGLOS = new NonTerminal("DIM_ARREGLOS");
        NonTerminal DIM_ARREGLO = new NonTerminal("DIM_ARREGLO");
        NonTerminal ACC_ARREGLOS = new NonTerminal("ACC_ARREGLOS");
        NonTerminal ACC_ARREGLO = new NonTerminal("ACC_ARREGLO");
        NonTerminal ASIG_ARREGLOS = new NonTerminal("ASIG_ARREGLOS");
        NonTerminal ASIG_ARREGLO = new NonTerminal("ASIG_ARREGLO");
        NonTerminal ASIG_ARREGLO_U = new NonTerminal("ASIG_ARREGLO_U");
        NonTerminal ASIG_ARREGLO_M = new NonTerminal("ASIG_ARREGLO_M");
        NonTerminal DECLARACION = new NonTerminal("DECLARACION");
        NonTerminal ASIGNACION = new NonTerminal("ASIGNACION");
        NonTerminal TIPO_DATO = new NonTerminal("TIPO_DATO");
        NonTerminal VISIBILIDAD = new NonTerminal("VISIBILIDAD");
        NonTerminal ASIG_ESTE = new NonTerminal("ASIG_ESTE");
        NonTerminal SENT_SI = new NonTerminal("SENT_SI");
        NonTerminal SI = new NonTerminal("SI");
        NonTerminal VARIOS_SINO_SI = new NonTerminal("VARIOS_SINO_SI");
        NonTerminal SINO = new NonTerminal("SINO");
        NonTerminal SINO_SI = new NonTerminal("SINO_SI");
        NonTerminal CASOS_VALORES = new NonTerminal("CASOS_VALORES");
        NonTerminal CASO_VALOR = new NonTerminal("CASO_VALOR");
        NonTerminal CASO_DEFECTO = new NonTerminal("CASO_DEFECTO");
        NonTerminal SENT = new NonTerminal("SENT");
        NonTerminal VAR_CONTROL = new NonTerminal("VAR_CONTROL");
        NonTerminal SENTS = new NonTerminal("SENTS");
        NonTerminal METODOS = new NonTerminal("METODOS");
        NonTerminal METODO = new NonTerminal("METODO");
        NonTerminal TIPO_RETORNO = new NonTerminal("TIPO_RETORNO");
        NonTerminal PARAMETROS = new NonTerminal("PARAMETROS");
        NonTerminal PARAMETRO = new NonTerminal("PARAMETRO");
        NonTerminal LLAMADASUPER = new NonTerminal("LLAMADASUPER");
        NonTerminal FUNCION = new NonTerminal("FUNCION");
        NonTerminal FUNCIONES = new NonTerminal("FUNCIONES");
        NonTerminal CLASE = new NonTerminal("CLASE");
        NonTerminal HERENCIA = new NonTerminal("HERENCIA");
        NonTerminal CLASES = new NonTerminal("CLASES");
        NonTerminal IMPORTACIONES = new NonTerminal("IMPORTACIONES");
        NonTerminal IMPORTACION = new NonTerminal("IMPORTACION");
        NonTerminal INI = new NonTerminal("ARCHIVO_XFORM");
        NonTerminal PLANTILLA = new NonTerminal("PLANTILLA");
        #endregion
        public Gramaticas()
            : base(false)
        {
            #region TOKENS
            NonGrammarTerminals.Add(line);
            NonGrammarTerminals.Add(multiline);
            KeyTerm aumento = ToTerm("++");
            KeyTerm decremento = ToTerm("--");
            KeyTerm not = ToTerm("!");
            KeyTerm por = ToTerm("*");
            KeyTerm div = ToTerm("/");
            KeyTerm mod = ToTerm("%");
            KeyTerm mas = ToTerm("+");
            KeyTerm menos = ToTerm("-");
            KeyTerm menor = ToTerm("<");
            KeyTerm menorigual = ToTerm("<=");
            KeyTerm mayor = ToTerm(">");
            KeyTerm mayorigual = ToTerm(">=");
            KeyTerm diferente = ToTerm("!=");
            KeyTerm igual = ToTerm("==");
            KeyTerm and = ToTerm("&&");
            KeyTerm or = ToTerm("||");
            KeyTerm parizq = ToTerm("(");
            KeyTerm parder = ToTerm(")");
            KeyTerm punto = ToTerm(".");
            KeyTerm dospuntos = ToTerm(":");
            KeyTerm ptcoma = ToTerm(";");
            KeyTerm tigual = ToTerm("=");
            KeyTerm llaizq = ToTerm("{");
            KeyTerm llader = ToTerm("}");
            KeyTerm corizq = ToTerm("[");
            KeyTerm corder = ToTerm("]");
            KeyTerm coma = ToTerm(",");
            KeyTerm intder = ToTerm("?");
            #endregion
            
            #region PALABRAS RESERVADAS
            MarkReservedWords(palabras_reservadas);
            KeyTerm pr_importar = ToTerm("importar");
            KeyTerm pr_cadena = ToTerm("cadena");
            KeyTerm pr_booleano = ToTerm("booleano");
            KeyTerm pr_entero = ToTerm("entero");
            KeyTerm pr_decimal = ToTerm("decimal");
            KeyTerm pr_fecha = ToTerm("fecha");
            KeyTerm pr_hora = ToTerm("hora");
            KeyTerm pr_fechahora = ToTerm("fechahora");
            KeyTerm pr_respuestas = ToTerm("respuestas");
            KeyTerm pr_si = ToTerm("si");
            KeyTerm pr_sino = ToTerm("sino");
            KeyTerm pr_vacio = ToTerm("vacio");
            KeyTerm pr_caso = ToTerm("caso");
            KeyTerm pr_de = ToTerm("de");
            KeyTerm pr_valor = ToTerm("valor");
            KeyTerm pr_default = ToTerm("default");
            KeyTerm pr_para = ToTerm("para");
            KeyTerm pr_repetir = ToTerm("repetir");
            KeyTerm pr_hasta = ToTerm("hasta");
            KeyTerm pr_hacer = ToTerm("hacer");
            KeyTerm pr_mientras = ToTerm("mientras");
            KeyTerm pr_imprimir = ToTerm("imprimir");
            KeyTerm pr_continuar = ToTerm("continuar");
            KeyTerm pr_romper = ToTerm("romper");
            KeyTerm pr_retorno = ToTerm("retorno");
            KeyTerm pr_publico = ToTerm("publico");
            KeyTerm pr_privado = ToTerm("privado");
            KeyTerm pr_protegido = ToTerm("protegido");
            KeyTerm pr_nuevo = ToTerm("nuevo");
            KeyTerm pr_nulo = ToTerm("nulo");
            KeyTerm pr_este = ToTerm("este");
            KeyTerm pr_super = ToTerm("super");
            KeyTerm pr_principal = ToTerm("principal");
            KeyTerm pr_clase = ToTerm("clase");
            KeyTerm pr_padre = ToTerm("padre");
            KeyTerm pr_formulario = ToTerm("formulario");
            KeyTerm pr_pregunta = ToTerm("pregunta");
            KeyTerm pr_grupo = ToTerm("grupo");
            #endregion
            
            INI.Rule = IMPORTACIONES + CLASES;

            #region IMPORTACIONES
            /*#############################################################################*/
            /*##############################  IMPORTACIONES ###############################*/
            /*#############################################################################*/
            IMPORTACIONES.Rule = MakeStarRule(IMPORTACIONES, IMPORTACION);
            IMPORTACION.Rule = pr_importar + parizq + id + punto + id + parder + ptcoma;
            IMPORTACION.ErrorRule = SyntaxError + ptcoma;
            IMPORTACION.ErrorRule = SyntaxError + llader;
            #endregion

            #region CLASES METODOS Y FUNCIONES
            /*#############################################################################*/
            /*######################## CLASES METODOS Y FUNCIONES #########################*/
            /*#############################################################################*/
            CLASES.Rule = MakeStarRule(CLASES, CLASE);

            CLASE.Rule = pr_clase + id + VISIBILIDAD + HERENCIA + llaizq + FUNCIONES + llader;
            //CLASE.ErrorRule = SyntaxError + ptcoma;
            CLASE.ErrorRule = SyntaxError + llader;

            HERENCIA.Rule = pr_padre + id
                        | Empty;

            FUNCIONES.Rule = MakeStarRule(FUNCIONES, FUNCION);

            FUNCION.Rule = VISIBILIDAD + TIPO_RETORNO + id + parizq + PARAMETROS + parder + llaizq + SENTS + llader
                        | pr_principal + parizq + parder + llaizq + SENTS + llader
                        | id + parizq + PARAMETROS + parder + llaizq + LLAMADASUPER + SENTS + llader
                        | DECLARACION + ptcoma
                        | PLANTILLA
                        ;
            FUNCION.ErrorRule = SyntaxError + ptcoma;
            FUNCION.ErrorRule = SyntaxError + llader;

            PLANTILLA.Rule = pr_formulario + id + parizq + PARAMETROS + parder + llaizq + SENTS + llader
                           | pr_pregunta + id + parizq + PARAMETROS + parder + llaizq + SENTS + llader
                           | pr_grupo + id + parizq + PARAMETROS + parder + llaizq + SENTS + llader
                           ;

            LLAMADASUPER.Rule = pr_super + parizq + VAL_PARAMETROS + parder + ptcoma
                            | Empty;
            #endregion

            #region SENTENCIAS
            /*#############################################################################*/
            /*###############################   SENTENCIAS  ###############################*/
            /*#############################################################################*/
            SENTS.Rule = MakeStarRule(SENTS, SENT);

            SENT.Rule = pr_imprimir + parizq + EXPRESION + parder + ptcoma
                    | pr_repetir + llaizq + SENTS + llader + pr_hasta + parizq + EXPRESION + parder + ptcoma
                    | pr_hacer + llaizq + SENTS + llader + pr_mientras + parizq + EXPRESION + parder + ptcoma
                    | pr_para + parizq + VAR_CONTROL + ptcoma + EXPRESION + ptcoma + EXPRESION + parder + llaizq + SENTS + llader
                    | pr_mientras + parizq + EXPRESION + parder + llaizq + SENTS + llader
                    | pr_caso + parizq + EXPRESION + parder + pr_de + llaizq + CASOS_VALORES + CASO_DEFECTO + llader
                    | pr_retorno + ptcoma
                    | pr_retorno + EXPRESION + ptcoma
                    | pr_continuar + ptcoma
                    | pr_romper + ptcoma
                    | LLAMADAS + ptcoma
                    | SENT_SI
                    | ASIGNACION + ptcoma
                    | DECLARACION + ptcoma
                    ;
            SENT.ErrorRule = SyntaxError + ptcoma;
            SENT.ErrorRule = SyntaxError + llader;

            VAR_CONTROL.Rule = ASIGNACION
                    | DECLARACION
                    ;

            #region CASOS
            /*#############################################################################*/
            /*###############################     CASOS     ###############################*/
            /*#############################################################################*/
            CASOS_VALORES.Rule = MakePlusRule(CASOS_VALORES, CASO_VALOR);

            CASO_VALOR.Rule = pr_valor + EXPRESION + dospuntos + llaizq + SENTS + llader;

            CASO_DEFECTO.Rule = pr_default + dospuntos + llaizq + SENTS + llader
                            | Empty;
            #endregion

            #region SENT SI
            /*#############################################################################*/
            /*############################### SENTENCIA SI  ###############################*/
            /*#############################################################################*/
            SENT_SI.Rule = SI
                | SI + VARIOS_SINO_SI + SINO
                | SI + VARIOS_SINO_SI
                | SI + SINO
                ;

            SI.Rule = pr_si + parizq + EXPRESION + parder + llaizq + SENTS + llader;

            SINO_SI.Rule = pr_sino + SI;

            SINO.Rule = pr_sino + llaizq + SENTS + llader;

            VARIOS_SINO_SI.Rule = MakePlusRule(VARIOS_SINO_SI, SINO_SI);
            #endregion

            #region ASIGNACIONES
            /*#############################################################################*/
            /*############################### ASIGNACIONES  ###############################*/
            /*#############################################################################*/
            ASIGNACION.Rule = id + ACC_ARREGLOS + tigual + EXPRESION
                        | ASIG_ESTE + ACC_ARREGLOS + tigual + EXPRESION
                        | id + tigual + EXPRESION
                        | ASIG_ESTE + tigual + EXPRESION;

            ASIG_ESTE.Rule = pr_este + punto + id
                        | Empty;
            #endregion

            #region DECLARACIONES
            /*#############################################################################*/
            /*############################### DECLARACIONES ###############################*/
            /*#############################################################################*/
            DECLARACION.Rule = TIPO_DATO + VISIBILIDAD + id + DIM_ARREGLOS + tigual + pr_nuevo + TIPO_DATO + ACC_ARREGLOS
                        | TIPO_DATO + VISIBILIDAD + id + DIM_ARREGLOS + tigual + ASIG_ARREGLOS
                        | TIPO_DATO + VISIBILIDAD + id + DIM_ARREGLOS + tigual + EXPRESION
                        | TIPO_DATO + VISIBILIDAD + id + DIM_ARREGLOS + tigual + pr_nulo
                        | TIPO_DATO + VISIBILIDAD + id + DIM_ARREGLOS
                        | TIPO_DATO + VISIBILIDAD + id + tigual + pr_nuevo + TIPO_DATO + parizq + VAL_PARAMETROS + parder
                        | TIPO_DATO + VISIBILIDAD + id + tigual + EXPRESION
                        | TIPO_DATO + VISIBILIDAD + id + tigual + pr_nulo
                        | TIPO_DATO + VISIBILIDAD + id
                        ;
            #endregion
            #endregion

            #region AREA DE ARREGLOS
            /*#############################################################################*/
            /*EJEMPLO... DIM_ARREGLOS -> [ ]... ACCESO_ARREGLOS -> [ ID , 98 ]... ASIG_ARREGLOS -> { 32, 32 }  { { 23,23 },{ 23,21 } } */
            /*#############################################################################*/
            DIM_ARREGLOS.Rule = MakePlusRule(DIM_ARREGLOS, DIM_ARREGLO);

            DIM_ARREGLO.Rule = corizq + corder;

            ACC_ARREGLOS.Rule = MakePlusRule(ACC_ARREGLOS, ACC_ARREGLO);

            ACC_ARREGLO.Rule = corizq + EXPRESION + corder;

            ASIG_ARREGLOS.Rule = llaizq + ASIG_ARREGLO + llader;

            ASIG_ARREGLO.Rule = ASIG_ARREGLO_U
                                | ASIG_ARREGLO_M;

            ASIG_ARREGLO_U.Rule = MakeStarRule(ASIG_ARREGLO_U, coma, EXPRESION);

            ASIG_ARREGLO_M.Rule = MakeStarRule(ASIG_ARREGLO_M, coma, ASIG_ARREGLOS);
            #endregion

            #region EXPRESIONES
            /*#############################################################################*/
            /*################################ EXPRESIONES ################################*/
            /*#############################################################################*/
            EXPRESION.Rule = EXPRESION + or + EXPRESION
                        | EXPRESION + and + EXPRESION
                        | EXPRESION + igual + EXPRESION
                        | EXPRESION + diferente + EXPRESION
                        | EXPRESION + menor + EXPRESION
                        | EXPRESION + menorigual + EXPRESION
                        | EXPRESION + mayor + EXPRESION
                        | EXPRESION + mayorigual + EXPRESION
                        | EXPRESION + mas + EXPRESION
                        | EXPRESION + menos + EXPRESION
                        | EXPRESION + por + EXPRESION
                        | EXPRESION + div + EXPRESION
                        | EXPRESION + mod + EXPRESION
                        | aumento + EXPRESION
                        | decremento + EXPRESION
                        | EXPRESION + aumento
                        | EXPRESION + decremento
                        | not + EXPRESION
                        | mas + EXPRESION
                        | menos + EXPRESION
                        | parizq + EXPRESION + parder
                        | entero
                        | cadena
                        | hora
                        | fecha
                        | fechahora
                        | edecimal
                        | boolean
                        | LLAMADAS
                        ; 
            #endregion

            #region LLAMADAS
            /*#############################################################################*/
            /*############## LLAMADAS A FUNCIONES PROCEDIMIENTOS Y ARREGLOS ###############*/
            /*#############################################################################*/
            LLAMADAS.Rule = MakePlusRule(LLAMADAS, punto, LLAMADA);

            LLAMADA.Rule = id + parizq + VAL_PARAMETROS + parder
                        | id + ACC_ARREGLOS
                        | id
                        | pr_este
                        ;
            #endregion

            #region AREA DE PARAMETROS
            /*#############################################################################*/
            /*############## PARAMETROS DE METODOS Y VALORES DE PARAMETROS ################*/
            /*#############################################################################*/
            VAL_PARAMETROS.Rule = MakeStarRule(VAL_PARAMETROS, coma, EXPRESION);

            PARAMETROS.Rule = MakeStarRule(PARAMETROS, coma, PARAMETRO);

            PARAMETRO.Rule = TIPO_DATO + id + DIM_ARREGLOS
                        | TIPO_DATO + id;
            #endregion

            #region TIPO DE DATOS Y RETORNOS
            /*#############################################################################*/
            /*################## TIPO DE DATOS Y TIPO DE OBJETSO CON ID ###################*/
            /*#############################################################################*/
            TIPO_DATO.Rule = pr_cadena
                    | pr_booleano
                    | pr_entero
                    | pr_decimal
                    | pr_fecha
                    | pr_hora
                    | pr_fechahora
                    | id
                    ;

            TIPO_RETORNO.Rule = TIPO_DATO
                            | pr_vacio
                            ;
            #endregion

            #region VISIBILIDAD
            /*#############################################################################*/
            /*################################ VISIBILIDAD ################################*/
            /*#############################################################################*/
            VISIBILIDAD.Rule = pr_publico
                            | pr_privado
                            | pr_protegido
                            | Empty
                            ;
            #endregion

            #region PRECEDENCIA Y ASOCIATIVAD
            /*#############################################################################*/
            /*######################## PRECEDENCIA Y ASOCIATIVIDAD ########################*/
            /*#############################################################################*/
            RegisterOperators(8, Associativity.Right, "!", "^");
            RegisterOperators(7, Associativity.Left, "*", "/", "%");
            RegisterOperators(6, Associativity.Left, "+", "-");
            RegisterOperators(5, Associativity.Left, "<", "<=", ">", ">=");
            RegisterOperators(4, Associativity.Left, "==", "!=");
            RegisterOperators(3, Associativity.Left, "&&");
            RegisterOperators(2, Associativity.Left, "||");
            #endregion

            this.Root = INI;
        }
    }
}
