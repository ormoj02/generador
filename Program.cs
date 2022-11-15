/* Orta Moreno Jair */
using System;
using System.IO;
using System.Collections.Generic;

namespace Generador
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (Lenguaje a = new Lenguaje())
                {
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}