using System;
using System.IO;
using Practica01.Models;

namespace Practica01
{
  class Program
  {
    static void Main(string[] args)
    {
      // Directorios();
      // Archivos();
      MetodoDePrueba();

      Console.ReadLine();
    }

    private static void MetodoDePrueba()
    {
      string clave = "1-1-0001-0001-00-00-00-01";
      var claveSeparada = clave.Split("-", StringSplitOptions.RemoveEmptyEntries);
      var nuevaClave = new ClaveDeLocalizacion();
      nuevaClave.Original = clave;
      nuevaClave.Subsistema = int.Parse(claveSeparada[0]);
      nuevaClave.Sector = int.Parse(claveSeparada[1]);
      nuevaClave.Manzana = int.Parse(claveSeparada[2]);
      nuevaClave.Lote = int.Parse(claveSeparada[3]);
      if (claveSeparada.Length == 8)
      {
        nuevaClave.Nivel[0] = int.Parse(claveSeparada[4]);
        nuevaClave.Nivel[1] = int.Parse(claveSeparada[5]);
        nuevaClave.Fraccion = int.Parse(claveSeparada[6]);
        nuevaClave.Toma = int.Parse(claveSeparada[7]);
      }
      else if (claveSeparada.Length == 7)
      {
        nuevaClave.Nivel[0] = 0;
        nuevaClave.Nivel[1] = int.Parse(claveSeparada[4]);
        nuevaClave.Fraccion = int.Parse(claveSeparada[5]);
        nuevaClave.Toma = int.Parse(claveSeparada[6]);
      }
      else
      {
        Console.WriteLine("lo que sea");
      }
      Console.WriteLine($"Clave original: {nuevaClave.Original}");
      Console.WriteLine($"Subsistema: {nuevaClave.Subsistema}");
      Console.WriteLine($"Sector: {nuevaClave.Sector}");
      Console.WriteLine($"Manzana: {nuevaClave.Manzana}");
      Console.WriteLine($"Lote: {nuevaClave.Lote}");
      Console.WriteLine($"Nivel: {nuevaClave.Nivel[0]}-{nuevaClave.Nivel[1]}");
      Console.WriteLine($"Fraccion: {nuevaClave.Fraccion}");
      Console.WriteLine($"Toma: {nuevaClave.Toma}");
      Console.WriteLine($"Correcta: {nuevaClave.Correcta}");
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
