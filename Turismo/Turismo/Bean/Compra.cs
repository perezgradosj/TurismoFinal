using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turismo.Bean
{
    public class Compra
    {
        public int idpaquete { get; set; }
        public int idusuario { get; set; }
        public string correo { get; set; }
        public string celular { get; set; }
        public int total { get; set; }
        public int npasajHabDobTriple { get; set; }
        public int npasajHabSimple { get; set; }
        public string fechaInicio { get; set; }
    }
}