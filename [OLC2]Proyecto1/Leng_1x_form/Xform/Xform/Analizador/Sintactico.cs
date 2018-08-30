using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xform.Analizador
{
    class Sintactico
    {
        private static string grafo;
        private static List<Errors> error = new List<Errors>();

        public static ParseTreeNode analizar(string entrada)
        {
            Gramaticas gramatica = new Gramaticas();
            Parser parser = new Parser(gramatica);
            ParseTree arbol = parser.Parse(entrada);
            error.Clear();
            string tmp = arbol.SourceText;

            ParseTreeNode raiz = arbol.Root;
            if (raiz != null)
            {
                generar_grafoAST(raiz);
            }
            else
            {
                string cad = "";
                for (int i = 0; i < arbol.ParserMessages.Count; i++)
                {
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
                    error.Add(new Errors(arbol.ParserMessages[i].Location.Line+1, arbol.ParserMessages[i].Location.Column+1, "Sintactico",
                        arbol.ParserMessages[i].Level.ToString(), cad));
                    cad = "";
                    //arbol.ParserMessages[i].Message;
                }
            }
            return raiz;
        }

        public static List<Errors> getLista()
        {
            return error;
        }

        public static string ASTdot(ParseTreeNode raiz)
        {
            grafo = "digraph grafo{\n";
            grafo += raiz.GetHashCode() + "[label=\"" + escapar(raiz.ToString()) + "\"];\n";
            recorrerAst(raiz.GetHashCode(), raiz);
            grafo += "}";
            return grafo;
        }

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

        private static void recorrerAst(int padre, ParseTreeNode hijos)
        {
            foreach (ParseTreeNode hijo in hijos.ChildNodes)
            {
                int nombreHijo = hijo.GetHashCode();
                grafo += nombreHijo + "[label=\"" + escapar(hijo.ToString()) +
                    "\"];\n";
                grafo += padre + "->" + nombreHijo + ";\n";
                recorrerAst(nombreHijo, hijo);
            }
        }

        private static String escapar(String cadena)
        {
            cadena = cadena.Replace("\\", "\\\\");
            cadena = cadena.Replace("\"", "\\\"");

            return cadena;
        }
    }
}
