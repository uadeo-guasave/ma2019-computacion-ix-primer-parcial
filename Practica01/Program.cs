using System;
using System.Collections.Generic;
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
      // MetodoDePrueba();
      // var prueba = ClaveDeLocalizacion.Inicializar("1-1-0001-0001-00-00-00-01");
      // Console.WriteLine(prueba.Correcta);
      // using (var db = new SqliteContext())
      // {
      //   db.Database.EnsureCreated();
      //   var prueba = ClaveDeLocalizacion.Inicializar("1-1-0001-0001-00-00-00-01");
      //   db.Add(prueba);
      //   db.SaveChanges();
      // }
      // var c = new ClaveDeLocalizacion();
      // Console.WriteLine(c.ToString());

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
        nuevaClave.Nivel1 = int.Parse(claveSeparada[4]);
        nuevaClave.Nivel2 = int.Parse(claveSeparada[5]);
        nuevaClave.Fraccion = int.Parse(claveSeparada[6]);
        nuevaClave.Toma = int.Parse(claveSeparada[7]);
      }
      else if (claveSeparada.Length == 7)
      {
        nuevaClave.Nivel1 = 0;
        nuevaClave.Nivel2 = int.Parse(claveSeparada[4]);
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
      Console.WriteLine($"Nivel: {nuevaClave.Nivel1}-{nuevaClave.Nivel2}");
      Console.WriteLine($"Fraccion: {nuevaClave.Fraccion}");
      Console.WriteLine($"Toma: {nuevaClave.Toma}");
      Console.WriteLine($"Correcta: {nuevaClave.Correcta}");
    }

    private static void Archivos()
    {
      string ruta = @"/Users/bidkar/Documents/UDO/2019/Mayo-Agosto/Computacion9/PrimerParcial/Datos";

      var archivos = Directory.GetFiles(ruta);
      var claves = new List<ClaveDeLocalizacion>();
      foreach (var a in archivos)
      {
        Console.WriteLine($"Archivo: {Path.GetFileName(a)}");
        var contenido = File.ReadAllLines(a);
        var contador = 1;
        foreach (var linea in contenido)
        {
          if (contador > 1)
          {
            claves.Add(ClaveDeLocalizacion.Inicializar(linea.Replace("\"", "")));
          }
          contador++;
        }
      }
      using (var db = new SqliteContext())
      {
        db.Database.EnsureCreated();
        db.AddRange(claves);
        db.SaveChanges();
        Console.WriteLine("Claves guardadas!");
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

    private static void GuardarCSV()
    {
      using (var db = new SqliteContext())
      {
        // var claves = db.ClavesDeLocalizacion.
      }
    }
  }
}
