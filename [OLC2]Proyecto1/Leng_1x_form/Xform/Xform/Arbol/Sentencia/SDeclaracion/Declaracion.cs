using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xform.Arbol.OpExp;
using Xform.Arbol.Sentencia.SVisibilidad;
using Xform.Arbol.Sentencia.Tipo_Dato;

namespace Xform.Arbol.Sentencia.SDeclaracion
{
    /**
     * @clase Declaracion
     * @autor        : Jhonatan Lopez
     * @carnet       : 201325583
     * @universidad  : USAC
     * @facultad     : ingenieria
     * */

    class Declaracion
    {
        private TipoDato.Tipo tipo;
        private Visibilidad.Visibilidad visibilidad;
        private string id;

        private List<Expresion> dimensiones;
    }
}
