/* Orta Moreno Jair */
using System;
using System.Collections.Generic;

namespace Generador
{
    public class Lenguaje : Sintaxis, IDisposable
    {
        public void Dispose()
        {
            cerrar();
        }
    }
}