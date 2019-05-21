using System;
using System.IO;

namespace Practica01
{
  class Program
  {
    static void Main(string[] args)
    {
      // Directorios();
      // Archivos();

      Console.ReadLine();
    }

    private static void Archivos()
    {
      string ruta = @"/Users/bidkar/Documents/UDO/2019/Mayo-Agosto/Computacion9/PrimerParcial/Datos";

      var archivos = Directory.GetFiles(ruta);
      foreach (var a in archivos)
      {
        Console.WriteLine($"Archivo: {Path.GetFileName(a)}");
        var contenido = File.ReadAllLines(a);
        foreach (var linea in contenido)
        {
          Console.WriteLine(linea);
        }
      }
    }

    private static void Directorios()
    {
      string rutaActual = Directory.GetCurrentDirectory();
      Console.WriteLine($"Directorio actual: {rutaActual}");

      string[] directorios = Directory.GetDirectories(rutaActual, "*", SearchOption.AllDirectories);
      foreach (var d in directorios)
      {
        Console.WriteLine($"Directorio: {d}");
      }
    }
  }
}
