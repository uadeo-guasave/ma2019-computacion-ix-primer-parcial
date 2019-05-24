using System;

namespace Practica01.Models
{
  public class ClaveDeLocalizacion
  {
    // subsistema-sector-manzana-nivel-nivel-fraccion-toma
    public int Subsistema { get; set; }
    public int Sector { get; set; }
    public int Manzana { get; set; }
    public int Lote { get; set; }
    public int[] Nivel { get; set; } = new int[2];
    public int Fraccion { get; set; }
    public int Toma { get; set; }
    public string Original { get; set; }
    public string Correcta
    {
      get
      {
        return $"{Subsistema.ToString()}-{Sector.ToString().PadLeft(2, '0')}-{Manzana.ToString().PadLeft(4, '0')}-{Lote.ToString().PadLeft(4, '0')}-{Nivel[0].ToString().PadLeft(2, '0')}-{Nivel[1].ToString().PadLeft(2, '0')}-{Fraccion.ToString().PadLeft(2, '0')}-{Toma.ToString().PadLeft(2, '0')}";
      }
    }

    public static ClaveDeLocalizacion Inicializar(string clave)
    {
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
        throw new FormatException("El formato de la clave es incorrecto.");
      }

      return nuevaClave;
    }
  }
}