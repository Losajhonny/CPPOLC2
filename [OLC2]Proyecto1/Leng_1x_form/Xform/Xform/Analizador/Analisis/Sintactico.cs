using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xform.Analizador.Reportes;

namespace Xform.Analizador.Analisis
{
    /**
     * @clase Sintactico
     * @autor        : Jhonatan Lopez
     * @carnet       : 201325583
     * @universidad  : USAC
     * @facultad     : ingenieria
     * */

    class Sintactico
    {
        /**
         * variable que guarda el archivo dot del grafo del arbol
         * */
        private static string grafo;

        /**
         * Lista de errores del archivo de analisis
         * */
        public static List<Errors> error = new List<Errors>();

        /**
         * Funcion analizar se le envia la cadena en la que se analizara y
         * retorna el arbol de analisis sintactico. Cuando el analisis no 
         * se encuentra error entonces genera una imagen del arbol si no
         * genera una lista de posibles errores que ocurrio durante el analisis
         * retorn la raiz del arbol
         * @param entrada
         * @return
         * */
        public static ParseTreeNode analizar(string entrada)
        {
            Gramatica gramatica = new Gramatica();
            Parser parser = new Parser(gramatica);
            ParseTree arbol = parser.Parse(entrada);

            //Limpiar lista de errores cada vez que se haga un analisis
            error.Clear();

            //raiz que retornara la funcion
            ParseTreeNode raiz = arbol.Root;
            
            if (raiz != null)
            {
                //generacion del arbol ast como imagen
                generar_grafoAST(raiz);
            }
            else
            {
                //cadena que se guardar como mensaje de error
                string cad = "";
                //recorriendo los errores que tiene el arbol
                for (int i = 0; i < arbol.ParserMessages.Count; i++)
                {
                    //recorriendo los terminales con errores para poder informar
                    for (int j = 0; j < arbol.ParserMessages[i].ParserState.ExpectedTerminals.Count; j++)
                    {
                        if (j == 0)
                        {
                            cad += arbol.ParserMessages[i].ParserState.ExpectedTerminals.ElementAt(j).Name;
                        }
                        else
                        {
                            cad += ", " + arbol.ParserMessages[i].ParserState.ExpectedTerminals.ElementAt(j).ToString();
                        }
                    }
                    cad = "Se esperaba: " + cad;
                    //agregando el error que se encontro
                    error.Add(new Errors(arbol.ParserMessages[i].Location.Line+1, arbol.ParserMessages[i].Location.Column+1, "Sintactico",
                        arbol.ParserMessages[i].Level.ToString(), cad));
                    cad = "";
                }
            }
            return raiz;
        }

        /**
         * Genera una cadena donde contiene la estructura .dot
         * para la generacion de la imagen del arbol
         * */
        private static string ASTdot(ParseTreeNode raiz)
        {
            grafo = "digraph grafo{\n";
            grafo += "node [shape = egg];\n";
            grafo += raiz.GetHashCode() + "[label=\"" + escapar(raiz.ToString()) + "\", style = filled, color = lightblue];\n";
            recorrerAst(raiz.GetHashCode(), raiz);
            grafo += "}";
            return grafo;
        }

        /**
         * Realiza el recorrido del arbol agregando la cadena
         * correspondiente al grafo
         * */
        private static void recorrerAst(int padre, ParseTreeNode hijos)
        {
            foreach (ParseTreeNode hijo in hijos.ChildNodes)
            {
                int nombreHijo = hijo.GetHashCode();
                grafo += nombreHijo + "[label=\"" + escapar(hijo.ToString()) +
                    "\", style = filled, color = lightblue];\n";
                grafo += padre + "->" + nombreHijo + ";\n";
                recorrerAst(nombreHijo, hijo);
            }
        }

        /**
         * Elimina el caracter de escape que puede generar problemas con
         * el lenguaje dot y agrega el caracter que lo reconoce
         * */
        private static String escapar(String cadena)
        {
            cadena = cadena.Replace("\\", "\\\\");
            cadena = cadena.Replace("\"", "\\\"");

            return cadena;
        }

        /**
         * Genera la imagen del grafo y lo almacena en la carpeta del proyecto
         * */
        public static void generar_grafoAST(ParseTreeNode raiz)
        {
            String grafo = ASTdot(raiz);
            StreamWriter w = new StreamWriter("arbolAst.dot");
            w.WriteLine(grafo);
            w.Close();

            ProcessStartInfo p = new ProcessStartInfo("C:\\release\\bin\\dot.exe");
            p.WindowStyle = ProcessWindowStyle.Normal;
            p.RedirectStandardOutput = true;
            p.UseShellExecute = false;
            p.CreateNoWindow = true;
            p.WindowStyle = ProcessWindowStyle.Hidden;
            p.Arguments = "-Tpng arbolAst.dot -o arbolAst.png";
            Process.Start(p);
        }
    }
}
