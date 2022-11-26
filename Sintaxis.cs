/* Orta Moreno Jair */
using System;

namespace Generador
{
    public class Sintaxis : Lexico
    {
        public Sintaxis()
        {
            NextToken();
        }
        public Sintaxis(string nombre) : base(nombre)
        {
            NextToken();
        }

        public void match(string espera)
        {
            //Console.WriteLine(getContenido()+" "+espera);
            if (espera == getContenido())
            {
                NextToken();
            }
            else
            {
                throw new Error("Error de sintaxis, se espera un " + espera + " en linea: " + linea, log);
            }
            
        }
        public void match(Tipos espera)
        {
            //Console.WriteLine(getClasificacion()+" "+espera);
            if (espera == getClasificacion())
            {
                NextToken();
            }
            else
            {
                throw new Error("Error de sintaxis, se espera un " + espera + " en linea: " + linea, log);
            }
        }
    }
}