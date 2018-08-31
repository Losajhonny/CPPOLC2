using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xform.Analizador
{
    /**
     * @clase Reporte
     * @autor        : Jhonatan Lopez
     * @carnet       : 201325583
     * @universidad  : USAC
     * @facultad     : ingenieria
     * */

    class Reporte
    {
        /**
         * generacion de la tabla de errores
         * */
        public static void tablaErrores(List<Errors> lista, string path)
        {
            string cuerpo = "";
            cuerpo += "<table align=" + Convert.ToChar(34) + "center" + Convert.ToChar(34) + "border=" + Convert.ToChar(34) + "1" + Convert.ToChar(34) + "width=1000>";
            string fin = "</table>";
            int cambio = 1;
            string cuerpoTabla = "";

            cuerpoTabla += "<tr bgcolor=" + Convert.ToChar(34) + "#4682B4" + Convert.ToChar(34) + ">";
            cuerpoTabla += "<td><font color=" + Convert.ToChar(34) + "#FFFFFF" + Convert.ToChar(34) + "><Font size=4><center>Linea</center></td>";
            cuerpoTabla += "<td><font color=" + Convert.ToChar(34) + "#FFFFFF" + Convert.ToChar(34) + "><Font size=4><center>Columna</center></td>";
            cuerpoTabla += "<td><font color=" + Convert.ToChar(34) + "#FFFFFF" + Convert.ToChar(34) + "><Font size=4><center>Tipo de Error</center></td>";
            cuerpoTabla += "<td><font color=" + Convert.ToChar(34) + "#FFFFFF" + Convert.ToChar(34) + "><Font size=4><center>Descripcion</center></td>";
            cuerpoTabla += "</tr>";

            for (int i = 0; i < lista.Count; i++)
            {
                Errors temp = lista[i];

                if (cambio == 0)
                {
                    cuerpoTabla += "<tr bgcolor=" + Convert.ToChar(34) + "#4682B4" + Convert.ToChar(34) + ">";
                    cuerpoTabla += "<td><font color=" + Convert.ToChar(34) + "#FFFFFF" + Convert.ToChar(34) + "><Font size=4><center>" + temp.Linea + "</center></td>";
                    cuerpoTabla += "<td><font color=" + Convert.ToChar(34) + "#FFFFFF" + Convert.ToChar(34) + "><Font size=4><center>" + temp.Columna + "</center></td>";
                    cuerpoTabla += "<td><font color=" + Convert.ToChar(34) + "#FFFFFF" + Convert.ToChar(34) + "><Font size=4><center>" + temp.Tipo + "</center></td>";
                    cuerpoTabla += "<td><font color=" + Convert.ToChar(34) + "#FFFFFF" + Convert.ToChar(34) + "><Font size=4><center>" + temp.Descripcion + "</center></td>";
                    cuerpoTabla += "</tr>";
                    cambio = 1;
                }
                else
                {
                    cuerpoTabla += "<tr bgcolor=" + Convert.ToChar(34) + "#E6E6E6" + Convert.ToChar(34) + ">";
                    cuerpoTabla += "<td><font color=" + Convert.ToChar(34) + "#17202a" + Convert.ToChar(34) + "><Font size=4><center>" + temp.Linea + "</center></td>";
                    cuerpoTabla += "<td><font color=" + Convert.ToChar(34) + "#17202a" + Convert.ToChar(34) + "><Font size=4><center>" + temp.Columna + "</center></td>";
                    cuerpoTabla += "<td><font color=" + Convert.ToChar(34) + "#17202a" + Convert.ToChar(34) + "><Font size=4><center>" + temp.Tipo + "</center></td>";
                    cuerpoTabla += "<td><font color=" + Convert.ToChar(34) + "#17202a" + Convert.ToChar(34) + "><Font size=4><center>" + temp.Descripcion + "</center></td>";
                    cuerpoTabla += "</tr>";
                    cambio = 0;
                }
            }

            cuerpo += cuerpoTabla + fin;

            //definiendo la hora y fecha del reporte
            int año = DateTime.Now.Year;
            int mes = DateTime.Now.Month;
            int dia = DateTime.Now.Day;
            int hora = DateTime.Now.Hour;
            string format = "";

            if (hora >= 12)
            {
                format = "pm";
            }
            else
            {
                format = "am";
            }

            int minuto = DateTime.Now.Minute;
            int segundo = DateTime.Now.Second;

            string pag = "<html><head><title>TABLA DE ERRORES</title></head>" + "<meta charset=" + "\"" + "utf-8" + "\"" + ">" + "<body>" +
                "<body style=" + Convert.ToChar(34) + "text-align:justify;" + Convert.ToChar(34) + ">" +
                "<h1><center>TABLA DE ERRORES</center></h1>" +
                "<h2>Dia de ejecución: " + dia + " de " + getMes(mes) + " de " + año + "</h2>"
                + "<h2>Hora de ejecución: " + hora + ":" + minuto + ":" + segundo + " " + format + "</h2>"
                + " <br>" + cuerpo + "</body></html>";

            System.IO.StreamWriter w = new System.IO.StreamWriter(path);
            w.WriteLine(pag);
            w.Close();
            Process.Start(path);
        }

        /**
         * Retorna el nombre del mes en base a su numero
         * */
        public static string getMes(int mes)
        {
            string m = "";
            switch (mes)
            {
                case 1:
                    m = "Enero";
                    break;
                case 2:
                    m = "Febrero";
                    break;
                case 3:
                    m = "Marzo";
                    break;
                case 4:
                    m = "Abril";
                    break;
                case 5:
                    m = "Mayo";
                    break;
                case 6:
                    m = "Junio";
                    break;
                case 7:
                    m = "Julio";
                    break;
                case 8:
                    m = "Agosto";
                    break;
                case 9:
                    m = "Septiembre";
                    break;
                case 10:
                    m = "Octubre";
                    break;
                case 11:
                    m = "Noviembre";
                    break;
                default:
                    m = "Diciembre";
                    break;
            }
            return m;
        }
    }
}
