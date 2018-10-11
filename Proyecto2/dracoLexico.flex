package Analizadores.DracoScript;

import java_cup.runtime.Symbol; 

%% 
%class DsLexico
%public 
%line 
%char 
%cup 
%unicode
%ignorecase

%init{ 
    yyline = 1; 
    yychar = 1; 
%init} 

ENTERO          		=   	[0-9]+
DECIMAL         		=   	[0-9]+ ( "." [0-9]+ )?
CADENA          		=   	[\"] ([^\"\n]|(\\\"))* [\"]
CARACTER        		=   	[\'] ([^\'\n]|(\\\')) [\']
ARCHIVO					=		[\'] ([^\'\n]|(\\\'))* "." ([^\'\n]|(\\\'))* [\']
ID              		=   	["_"A-Za-z] ["_"A-Za-z0-9]*

BLANCOS					=		[ \r\t]+
COMENNTSIMPLE 			=		"$$".*\n
COMMENTMULTIPLE			=		"$*".*"*$"

%%

{COMENNTSIMPLE}			{}
{COMMENTMULTIPLE}		{}
{CADENA}				{ return new Symbol(sym.tk_cadena, yyline, yychar, (yytext()).substring(1, yytext().length()-1)); }
{CARACTER}				{ return new Symbol(sym.tk_caracter, yyline, yychar, (yytext()).substring(1, yytext().length()-1)); }
{ARCHIVO}				{ return new Symbol(sym.tk_archivo, yyline, yychar, (yytext()).substring(1, yytext().length()-1)); }

"var"					{ return new Symbol(sym.pr_var, yyline, yychar, yytext()); }
"if"					{ return new Symbol(sym.pr_if, yyline, yychar, yytext()); }
"not"					{ return new Symbol(sym.pr_not, yyline, yychar, yytext()); }
"elif"					{ return new Symbol(sym.pr_elif, yyline, yychar, yytext()); }
"smash"					{ return new Symbol(sym.pr_smash, yyline, yychar, yytext()); }
"while"					{ return new Symbol(sym.pr_while, yyline, yychar, yytext()); }
"for"					{ return new Symbol(sym.pr_for, yyline, yychar, yytext()); }
"print"					{ return new Symbol(sym.pr_print, yyline, yychar, yytext()); }
"runmultdasm"			{ return new Symbol(sym.pr_runmultdasm, yyline, yychar, yytext()); }
"point"					{ return new Symbol(sym.pr_point, yyline, yychar, yytext()); }
"quadrate"				{ return new Symbol(sym.pr_quadrate, yyline, yychar, yytext()); }
"oval"					{ return new Symbol(sym.pr_oval, yyline, yychar, yytext()); }
"string"				{ return new Symbol(sym.pr_string, yyline, yychar, yytext()); }
"line" 					{ return new Symbol(sym.pr_line, yyline, yychar, yytext()); }
"true" 					{ return new Symbol(sym.pr_true, yyline, yychar, yytext()); }
"false"					{ return new Symbol(sym.pr_false, yyline, yychar, yytext()); }

"+"						{ return new Symbol(sym.tk_mas, yyline, yychar, yytext()); }
"-" 					{ return new Symbol(sym.tk_menos, yyline, yychar, yytext()); }
"/"						{ return new Symbol(sym.tk_division, yyline, yychar, yytext()); }
"*" 					{ return new Symbol(sym.tk_por, yyline, yychar, yytext()); }
"^" 					{ return new Symbol(sym.tk_potencia, yyline, yychar, yytext()); }
"%"						{ return new Symbol(sym.tk_modulo, yyline, yychar, yytext()); }

"++"					{ return new Symbol(sym.tk_adicion, yyline, yychar, yytext()); }
"--"					{ return new Symbol(sym.tk_sustraccion, yyline, yychar, yytext()); }

"=="					{ return new Symbol(sym.tk_igualop, yyline, yychar, yytext()); }
"!="					{ return new Symbol(sym.tk_diferente, yyline, yychar, yytext()); }
"<"						{ return new Symbol(sym.tk_menor, yyline, yychar, yytext()); }
">"						{ return new Symbol(sym.tk_mayor, yyline, yychar, yytext()); }
"<="					{ return new Symbol(sym.tk_menorigual, yyline, yychar, yytext()); }
">="					{ return new Symbol(sym.tk_mayorigual, yyline, yychar, yytext()); }

"&&"					{ return new Symbol(sym.tk_and, yyline, yychar, yytext()); }
"||"					{ return new Symbol(sym.tk_or, yyline, yychar, yytext()); }
"!"						{ return new Symbol(sym.tk_not, yyline, yychar, yytext()); }

":=:"					{ return new Symbol(sym.tk_igual, yyline, yychar, yytext()); }
";"						{ return new Symbol(sym.tk_ptcoma, yyline, yychar, yytext()); }
","						{ return new Symbol(sym.tk_coma, yyline, yychar, yytext()); }
"("						{ return new Symbol(sym.tk_parizq, yyline, yychar, yytext()); }
")"						{ return new Symbol(sym.tk_parder, yyline, yychar, yytext()); }
"{"						{ return new Symbol(sym.tk_llaizq, yyline, yychar, yytext()); }
"}"						{ return new Symbol(sym.tk_llader, yyline, yychar, yytext()); }


\n 						{yychar=1;}

{BLANCOS}				{}
{ENTERO}				{ return new Symbol(sym.tk_entero, yyline, yychar, (yytext()).substring(1, yytext().length()-1)); }
{DECIMAL}				{ return new Symbol(sym.tk_decimal, yyline, yychar, (yytext()).substring(1, yytext().length()-1)); }
{ID}					{ return new Symbol(sym.tk_id, yyline, yychar, (yytext()).substring(1, yytext().length()-1)); }

. {
    System.err.println("Este es un error lexico: "+yytext()+", en la linea: "+yyline+", en la columna: "+yychar);
}