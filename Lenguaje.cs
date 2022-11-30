/* Orta Moreno Jair */
using System;
using System.Collections.Generic;
//x Requerimiento 1. Construir un metodo para escribir en el archivo Lenguaje.cs identando el codigo
//                 "{" incrementa un tabulador, "}" decrementa un tabulador
//x Requerimiento 2. Declarar un atributo "primeraProduccion" de tipo string y actualizarlo con la 
//                 primera produccion de la gramatica
//x Requerimiento 3. La primera produccion es publica y el resto es privada
//x Requerimiento 4. El constructor Lexico() parametrizado debe validar que 
//                 la extension del archivo a compilar sea .gen
//                 si no es .gen debe lanzar una excepcion
//x Requerimiento 5. Resolver la ambiguedad de ST y SNT 
//                 Recorrer linea por linea el archivo .gram para extraer el nombre de cada produccion 
//Requerimiento 6. Agregar el parentesis izquierdo y derecho escapados en 
//                 la matriz de transiciones
// Requerimiento 7. Implementar la cerradura epsilon (parentesis izquierdo y derecho)
//                 
namespace Generador
{
    public class Lenguaje : Sintaxis, IDisposable
    {
        //guardamos la posicion y linea donde estamos 
        int posicionActual;
        int lineaActual;

        int tabuladorLenguaje;
        string primeraProduccion;
        int contProducciones = 0;
        List<string> listaSNT;

        public Lenguaje(string nombre) : base(nombre)
        {
            listaSNT = new List<string>();
            tabuladorLenguaje = 0;
            primeraProduccion = "";
        }
        public Lenguaje()
        {
            listaSNT = new List<string>();
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
            //Console.WriteLine("SNT: " + contenido);

        }
        private void Programa(string produccionPrincipal)
        {
            /*
            agregarSNT("Programa");
            agregarSNT("Librerias");
            agregarSNT("Variables");
            agregarSNT("ListaIdentificadores");
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
            //guardamos la posicion y linea donde estamos
            posicionActual = posicion;
            lineaActual = linea;
            int tam = getContenido().Length;
            //agregamos a la lista de SNT lo que se clasifica como SNT
            lenguaje.WriteLine("\t\t//Requerimiento 5.");
            clasificarSNT();
            //regresamos a la posicion y linea donde estabamos
            posicion = posicionActual - tam;
            linea = lineaActual;
            setPosicion(posicion);
            NextToken();
            listaProducciones(contProducciones);
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
        }
        private void listaProducciones(int contadorProducciones)
        {
            if (contadorProducciones == 0)
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
            simbolos(true);
            match(Tipos.FinProduccion);
            identarCodigo("}");
            if (!FinArchivo())
            {
                listaProducciones(contadorProducciones);
            }

        }
        private void simbolos(bool imprimir)
        {
            if (imprimir)
            {

                Console.WriteLine("getContenido: " + getContenido());
                if (getContenido() == "\\(")
                {
                    match("\\(");

                    if (!esSNT(getContenido()))
                    {
                        if (esTipo(getContenido()))
                        {
                            identarCodigo("if(getClasificacion()== Tipos." + getContenido() + ")");
                        }
                        else
                        {
                            identarCodigo("if(getContenido() ==\"" + getContenido() + "\")");
                        }
                    }
                    else
                    {
                        throw new Exception("Error en la produccion: " + getContenido());
                    }
                    identarCodigo("{");
                    simbolos(imprimir);
                    match("\\)");
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

                if (getClasificacion() != Tipos.FinProduccion && getContenido() != "\\)")
                {
                    simbolos(imprimir);
                }
            }
            else
            {
                Console.WriteLine("getContenido: " + getContenido());
                if (getContenido() == "\\(")
                {
                    match("\\(");
                    simbolos(imprimir);
                    match("\\)");
                }
                else if (esTipo(getContenido()))
                {
                    match(Tipos.ST);
                }
                else if (esSNT(getContenido()))
                {
                    match(Tipos.ST);
                }
                else if (getClasificacion() == Tipos.ST)
                {
                    match(Tipos.ST);
                }

                if (getClasificacion() != Tipos.FinProduccion && getContenido() != "\\)")
                {
                    simbolos(imprimir);
                } 
            }
        }
        public void clasificarSNT()
        {
            agregarSNT(getContenido());
            match(Tipos.ST);
            match(Tipos.Produce);
            simbolos(false);
            //simbolosClasificarSNT();
            match(Tipos.FinProduccion);
            if (!FinArchivo())
            {
                clasificarSNT();
            }
        }

        public void identarCodigo(string cadena)
        {
            if (cadena == ("}"))
            {
                tabuladorLenguaje--;
            }

            for (int i = 0; i < tabuladorLenguaje; i++)
            {
                lenguaje.Write("\t");
            }

            if (cadena == "{")
            {
                tabuladorLenguaje++;
            }
            lenguaje.WriteLine(cadena);

        }
        private void setPosicion(int pos)
        {
            archivo.DiscardBufferedData();
            archivo.BaseStream.Seek(pos, SeekOrigin.Begin);
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