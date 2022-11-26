/* Orta Moreno Jair */
using System;
using System.Collections.Generic;
//Requerimiento 1. Construir un metodo para escribir en el archivo Lenguaje.cs identando el codigo
//                 "{" incrementa un tabulador, "}" decrementa un tabulador
//Requerimiento 2. Declarar un atributo "primeraProduccion" de tipo string y actualizarlo con la 
//                 primera produccion de la gramatica
//Requerimiento 3. La primera produccion es publica y el resto es privada
//Requerimiento 4. El constructor Lexico() parametrizado debe validar que 
//                 la extension del archivo a compilar sea .gen
//                 si no es .gen debe lanzar una excepcion
//Requerimiento 5. Resolver la ambiguedad de ST y SNT 
//                 Recorrer linea por linea el archivo .gram para extraer el nombre de cada produccion 
//Requerimiento 6. Agregar el parentesis izquierdo y derecho escapados en 
//                 la matriz de transiciones
//Requerimiento 7. Implementar el OR y la cerradura epsilon
//                 
namespace Generador
{
    public class Lenguaje : Sintaxis, IDisposable
    {
        //string cadenaImprimir = "";
        //int tabuladorPrograma;
        int tabuladorLenguaje;
        string primeraProduccion;
        int contProducciones = 0;
        List<string> listaSNT;

        public Lenguaje(string nombre) : base(nombre)
        {
            listaSNT = new List<string>();
            //tabuladorPrograma = 0;
            tabuladorLenguaje = 0;
            primeraProduccion = "";
        }
        public Lenguaje()
        {
            listaSNT = new List<string>();
            //tabuladorPrograma = 0;
            tabuladorLenguaje = 0;
            primeraProduccion = "";
        }

        public void Dispose()
        {
            cerrar();
        }
        private bool esSNT(string contenido)
        {
            return listaSNT.Contains(contenido);
        }
        private void agregarSNT(string contenido)
        {
            //Requerimiento 6.

            listaSNT.Add(contenido);
        }
        private void Programa(string produccionPrincipal)
        {
            agregarSNT("Programa");
            agregarSNT("Librerias");
            agregarSNT("Variables");
            agregarSNT("ListaIdentificadores");
            /* cadenaImprimir =
            "using System;\n" +
            "using System.IO;\n" +
            "\n" +
            "namespace Generico\n" +
            "{\n" +
            "public class Program\n" +
            "{\n" +
            "static void Main(string[] args)\n" +
            "{\n" +
            "try\n" +
            "{\n" +
            "using (Lenguaje a = new Lenguaje())\n" +
            "{\n" +
            "a." + produccionPrincipal + "();\n" +
            "}\n" +
            "}\n" +
            "catch (Exception e)\n" +
            "{\n" +
            "Console.WriteLine(e.Message);\n" +
            "}\n" +
            "}\n" +
            "}\n" +
            "}\n";


            imprimirCodigo("programa", cadenaImprimir); 
            */
            programa.WriteLine("using System;");
            programa.WriteLine("using System.IO;");
            programa.WriteLine("using System.Collections.Generic;");
            programa.WriteLine();
            programa.WriteLine("namespace Generico");
            programa.WriteLine("{");
            programa.WriteLine("\tpublic class Program");
            programa.WriteLine("\t{");
            programa.WriteLine("\t\tstatic void Main(string[] args)");
            programa.WriteLine("\t\t{");
            programa.WriteLine("\t\t\ttry");
            programa.WriteLine("\t\t\t{");
            programa.WriteLine("\t\t\t\tusing (Lenguaje a = new Lenguaje())");
            programa.WriteLine("\t\t\t\t{");
            programa.WriteLine("\t\t\t\t\ta." + produccionPrincipal + "();");
            programa.WriteLine("\t\t\t\t}");
            programa.WriteLine("\t\t\t}");
            programa.WriteLine("\t\t\tcatch (Exception e)");
            programa.WriteLine("\t\t\t{");
            programa.WriteLine("\t\t\t\tConsole.WriteLine(e.Message);");
            programa.WriteLine("\t\t\t}");
            programa.WriteLine("\t\t}");
            programa.WriteLine("\t}");
            programa.WriteLine("}");
        }
        public void gramatica()
        {
            cabecera();
            primeraProduccion = getContenido();
            Programa(primeraProduccion);
            cabeceraLenguaje();

            listaProducciones(contProducciones);
            
            /* cadenaImprimir =
            "}\n" +
            "}";
            imprimirCodigo("lenguaje", cadenaImprimir);
             */
            lenguaje.WriteLine("\t}");
            lenguaje.WriteLine("}");
        }
        private void cabecera()
        {
            match("Gramatica");
            match(":");
            match(Tipos.ST);
            match(Tipos.FinProduccion);
        }
        private void cabeceraLenguaje()
        {

            /* cadenaImprimir =
            "using System;\n" +
            "using System.Collections.Generic;\n" +
            "namespace Generico\n" +
            "{\n" +
            "public class Lenguaje : Sintaxis, IDisposable\n" +
            "{\n" +
            "public Lenguaje(string nombre) : base(nombre)\n" +
            "{\n" +
            "}\n" +
            "public Lenguaje()\n" +
            "{\n" +
            "}\n" +
            "public void Dispose()\n" +
            "{\n" +
            "cerrar();\n" +
            "}\n";
            imprimirCodigo("lenguaje", cadenaImprimir); */
            
            identarCodigo("using System;");
            identarCodigo("using System.Collections.Generic;");
            identarCodigo("namespace Generico");
            identarCodigo("{");
            identarCodigo("public class Lenguaje : Sintaxis, IDisposable");
            identarCodigo("{");
            identarCodigo("public Lenguaje(string nombre) : base(nombre)");
            identarCodigo("{");
            identarCodigo("}");
            identarCodigo("public Lenguaje()");
            identarCodigo("{");
            identarCodigo("}");
            identarCodigo("public void Dispose()");
            identarCodigo("{");
            identarCodigo("cerrar();");
            identarCodigo("}");
            /* lenguaje.WriteLine("using System;");
            lenguaje.WriteLine("using System.Collections.Generic;");
            lenguaje.WriteLine("namespace Generico");
            lenguaje.WriteLine("{");
            lenguaje.WriteLine("\tpublic class Lenguaje : Sintaxis, IDisposable");
            lenguaje.WriteLine("\t{");
            lenguaje.WriteLine("\t\tpublic Lenguaje(string nombre) : base(nombre)");
            lenguaje.WriteLine("\t\t{");
            lenguaje.WriteLine("\t\t}");
            lenguaje.WriteLine("\t\tpublic Lenguaje()");
            lenguaje.WriteLine("\t\t{");
            lenguaje.WriteLine("\t\t}");
            lenguaje.WriteLine("\t\tpublic void Dispose()");
            lenguaje.WriteLine("\t\t{");
            lenguaje.WriteLine("\t\t\tcerrar();");
            lenguaje.WriteLine("\t\t}"); */
        }
        private void listaProducciones(int contadorProducciones)
        {
            /* cadenaImprimir =
            "private void " + getContenido() + "()\n" +
            "{\n";
            imprimirCodigo("lenguaje", cadenaImprimir); */
            if(contadorProducciones == 0)
            {
                identarCodigo("public void " + getContenido() + "()");
                identarCodigo("{");
                contadorProducciones++;
            }
            else
            {
                identarCodigo("private void " + getContenido() + "()");
                identarCodigo("{");
            }
            match(Tipos.ST);
            match(Tipos.Produce);
            simbolos();
            match(Tipos.FinProduccion);
            /* cadenaImprimir = "}\n";
            imprimirCodigo("lenguaje", cadenaImprimir); */
            identarCodigo("}");
            if (!FinArchivo())
            {
                listaProducciones(contadorProducciones);
            }

        }
        private void simbolos()
        {
            if (getContenido() == "(")
            {
                match("(");
                identarCodigo("if ()");
                identarCodigo("{");
                simbolos();
                match(")");
                identarCodigo("}");
            }
            else if (esTipo(getContenido()))
            {
                identarCodigo("match(Tipos." + getContenido() + ");");
                match(Tipos.ST);
            }
            else if (esSNT(getContenido()))
            {
                identarCodigo(getContenido() + "();");
                match(Tipos.ST);
            }
            else if (getClasificacion() == Tipos.ST)
            {
                identarCodigo("match(\"" + getContenido() + "\");");
                match(Tipos.ST);
            }

            if (getClasificacion() != Tipos.FinProduccion && getContenido() != ")")
            {
                simbolos();
            }
        }

        public void identarCodigo(string cadena)
        {
            if(cadena ==("}"))
            {
                tabuladorLenguaje--;
            }
            //escribimos los tabs que necesitamos
            //cadena = new string('\t', tabuladorLenguaje)+cadena;
            for(int i= 0; i < tabuladorLenguaje; i++)
            {
                lenguaje.Write("\t");
            }
            
            if(cadena =="{")
            {
                tabuladorLenguaje++;
            }
            
            
            lenguaje.WriteLine(cadena);
            
        }

        private void imprimirCodigo(string archivoEscritura, string cadenaImprimir)
        {
            string subCadena1, subCadena2;

            for (int i = 0; i < cadenaImprimir.Length; i++)
            {
                if (cadenaImprimir[i] == '{')
                {
                    if (archivoEscritura == "programa")
                    {
                        //tabuladorPrograma++;
                    }
                    else if (archivoEscritura == "lenguaje")
                    {
                        tabuladorLenguaje++;
                    }
                }
                else if (cadenaImprimir[i] == '}')
                {
                    if (archivoEscritura == "programa")
                    {
                        //tabuladorPrograma--;
                    }
                    else if (archivoEscritura == "lenguaje")
                    {
                        tabuladorLenguaje--;
                    }
                    
                    if (i > 0)
                    {
                        //editamos la cadena y le borramos un tabulador
                        subCadena1 = cadenaImprimir.Substring(0, i - 1);
                        subCadena2 = cadenaImprimir.Substring(i);
                        cadenaImprimir = subCadena1 + subCadena2;
                    }
                }

                if (cadenaImprimir[i] == '\n')
                {
                    subCadena1 = cadenaImprimir.Substring(0, i + 1);
                    subCadena2 = cadenaImprimir.Substring(i + 1);
                    if (archivoEscritura == "programa")
                    {
                        //cadenaImprimir = subCadena1 + new string('\t', //tabuladorPrograma) + subCadena2;
                    }
                    else if (archivoEscritura == "lenguaje")
                    {
                        cadenaImprimir = subCadena1 + new string('\t', tabuladorLenguaje) + subCadena2;
                    }
                }
                if (i == 0)
                {
                    if (archivoEscritura == "programa")
                    {
                        //cadenaImprimir = new string('\t', //tabuladorPrograma) + cadenaImprimir;
                    }
                    else if (archivoEscritura == "lenguaje")
                    {
                        cadenaImprimir = new string('\t', tabuladorLenguaje) + cadenaImprimir;
                    }
                }
            }
            //imprimimos la cadena
            if (archivoEscritura == "lenguaje")
            {
                lenguaje.WriteLine(cadenaImprimir);
            }
            else if (archivoEscritura == "programa")
            {
                programa.WriteLine(cadenaImprimir);
            }
        }

        private bool esTipo(string clasificacion)
        {
            switch (clasificacion)
            {
                case "Identificador":
                case "Numero":
                case "Caracter":
                case "Asignacion":
                case "Inicializacion":
                case "OperadorLogico":
                case "OperadorRelacional":
                case "OperadorTernario":
                case "OperadorTermino":
                case "OperadorFactor":
                case "IncrementoTermino":
                case "IncrementoFactor":
                case "FinSentencia":
                case "Cadena":
                case "TipoDato":
                case "Zona":
                case "Condicion":
                case "Ciclo":
                    return true;
            }
            return false;
        }
    }
}