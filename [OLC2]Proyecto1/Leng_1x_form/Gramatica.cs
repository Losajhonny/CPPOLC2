/* IMPORTACIONES */
IMPORTACIONES.Rule = MakeStarRule(IMPORTACIONES, IMPORTACION);
IMPORTACION.Rule = pr_importar + parizq + id + punto + id + parder + ptcoma;





/* CLASE */
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
			| /* PLANTILLA */
			;









/* CONSTRUCTOR */
CONSTRUCTOR.Rule = id + parizq + LISTA_PARAMETROS + parder + llaizq + SUPER +  /* SENTENCIAS */ + llader;
/* FUNCION SUPER DEL METODO CONSTRUCTOR */
SUPER.Rule = pr_super + parizq + LISTA_VAL_PARAMETROS + parder
		| Empty
		;






/* METODO PRINCIPAL */
PRINCIPAL.Rule = pr_principal + parizq + LISTA_PARAMETROS + parder + llaizq + /* SENTENCIAS */ + llader;






/* METODO */
METODO.Rule = VISIBILIDAD + TIPO_RETORNO + id + parizq + LISTA_PARAMETROS + parder + llaizq + /* SENTENCIAS */ + llader







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
            | id
            ;







/* EXPRESIONES */
EXPRESION.Rule = ARITMETICA
			| RELACIONAL
			| LOGICA
			| OPERADOR
			| FACTOR
			| parizq + EXPRESION + parder
			| LLAMADAS
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
			| pr_verdadero
			| pr_falso
			| fecha
			| hora
			| fechahora
			| id
			;





/* LLAMADAS A FUNCIONES */
LLAMADAS.Rule = MLLAMADAS;
			|  NLLAMADAS; // llamdas de funciones nativas

MLLAMADA.Rule = MakePlusRule(MLLAMADA, tk_punto, LLAMADA);

LLAMADA.Rule = id + parizq + LISTA_VAL_PARAMETROS + parder //llamada a una clase
			| id + ACC_DIMENSIONES 				//llamada a un arreglo
			| id								//llamada a un atributo o declaracion
			| pr_este 							//llamada a su propia clase
			;

NLLAMADAS.Rule = NATIVA_CADENA
			| NATIVA_BOOLEANA
			| NATIVA_NUMERICA
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

ASIG_DIMENSION_U.Rule = MakeStarRule(ASIG_DIMENSION_U, coma, EXPRESION);

ASIG_DIMENSION_M.Rule = MakeStarRule(ASIG_DIMENSION_M, coma, ASIG_DIMENSIONES);







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
PARA.Rule = pr_para + parizq + VAR_CONTROL + ptcoma + EXPRESION + ptcoma + EXPRESION + parder + llaizq + SENTENCIAS + llader;

VAR_CONTROL.Rule = ASIGNACION
			| DECLARACION
            ;



/* MIENTRAS */
MIENTRAS.Rule = pr_mientras + parizq + EXPRESION + parder + llaizq + SENTENCIAS + llader;



/* SENTENCIA SI */
SENT_SI.Rule = SI
            | SI + VARIOS_SINO_SI + SINO
            | SI + VARIOS_SINO_SI
            | SI + SINO
            ;

SI.Rule = pr_si + parizq + EXPRESION + parder + llaizq + SENTS + llader;

SINO_SI.Rule = pr_sino + SI;

SINO.Rule = pr_sino + llaizq + SENTS + llader;

VARIOS_SINO_SI.Rule = MakePlusRule(VARIOS_SINO_SI, SINO_SI);




/* CASOS */
CASOS.Rule = pr_caso + parizq + EXPRESION + parder + pr_de + llaizq + CASOS_VALORES + CASO_DEFECTO + llader;

CASOS_VALORES.Rule = MakePlusRule(CASOS_VALORES, CASO_VALOR);

CASO_VALOR.Rule = pr_valor + EXPRESION + dospuntos + llaizq + SENTS + llader;

CASO_DEFECTO.Rule = pr_default + dospuntos + llaizq + SENTS + llader
				| Empty;



/* RETORNO */
RETORNO.Rule = pr_retorno
        | pr_retorno + EXPRESION
        ;