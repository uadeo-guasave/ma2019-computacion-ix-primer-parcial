using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
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
      // Preguntar si quieres guardar en formato CSV o TAB
      // Preguntar si a todos los valores le ponen comillas
      // Pedir el nombre del archivo (extension por default, csv o txt)
      GuardarCSV();

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
      // definir el buffer donde se guardaran las lineas procesadas
      var buffer = new StringBuilder();

      // obtener la primera linea
      var primeraLinea = ObtenerPrimeraLinea();
      // agregar la primera linea al buffer
      buffer.AppendLine(primeraLinea);

      // conectar a la db
      using (var db = new SqliteContext())
      {
        // obtener todos los registros de la tabla claves_de_localizacion
        var registros = db.ClavesDeLocalizacion.ToList();
        // obtener los nombres de columnas de tipo caracter
        var colsCadena = (from p in typeof(ClaveDeLocalizacion).GetProperties()
                          where p.PropertyType == typeof(string)
                          select p.Name).ToList();

        foreach (var r in registros)
        {
          // procesar las columnas de cada registro
          // agregar al buffer los registros procesados
          var linea = new List<string>();
          foreach (var p in r.GetType().GetProperties())
          {
            if (p.PropertyType == typeof(string))
            {
              linea.Add(ponerComillas(p.GetValue(r).ToString()));
            }
            else
            {
              linea.Add(p.GetValue(r).ToString());
            }
          }
          buffer.AppendLine(string.Join(",", linea));
        }
        
        // Console.WriteLine(buffer);
        // Escribir archivo CSV
        var ruta = @"/Users/bidkar/Documents/UDO/2019/Mayo-Agosto/Computacion9/PrimerParcial/Datos/claves_de_localizacion.csv";
        File.WriteAllText(ruta, buffer.ToString());
        Console.WriteLine("Archivo escrito correctamente");
      }
    }

    private static string ObtenerPrimeraLinea()
    {
      // obtener los nombres de las propiedades publicas de la clase a guardar en csv
      // var propiedades = typeof(ClaveDeLocalizacion).GetProperties().ToList();
      // var propiedades = (from p in typeof(ClaveDeLocalizacion).GetProperties() select p.Name).ToList();
      var propiedades = typeof(ClaveDeLocalizacion).GetProperties().Select(p => p.Name).ToList();
      var propsRevisadas = new List<string>();
      foreach (var p in propiedades)
      {
        // Console.Write(p + ",");
        // revisar si tiene espacios la propiedad
        // "texto con espacio".indexOf(" ") -> 5
        if (p.IndexOf(" ") >= 0)
        {
          propsRevisadas.Add(ponerComillas(p));
        }
        else
        {
          propsRevisadas.Add(p);
        }
      }
      // Console.WriteLine(string.Join(",", propiedades));
      // Console.WriteLine(propiedades.Join(","));
      return string.Join(",", propsRevisadas);
    }

    private static string ponerComillas(string p)
    {
      return "\"" + p + "\"";
    }
  }
}
