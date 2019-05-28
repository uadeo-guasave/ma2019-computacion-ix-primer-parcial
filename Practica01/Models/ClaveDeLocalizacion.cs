using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practica01.Models
{
  public class ClaveDeLocalizacion
  {
    // subsistema-sector-manzana-nivel-nivel-fraccion-toma
    public int Id { get; set; }
    [Required]
    public int Subsistema { get; set; }
    [Required]
    public int Sector { get; set; }
    [Required]
    public int Manzana { get; set; }
    [Required]
    public int Lote { get; set; }
    [Required]
    public int Nivel1 { get; set; }
    [Required]
    public int Nivel2 { get; set; }
    [Required]
    public int Fraccion { get; set; }
    [Required]
    public int Toma { get; set; }
    [Required]
    public string Original { get; set; }
    [NotMapped]
    public string Correcta
    {
      get
      {
        return $"{Subsistema.ToString()}-{Sector.ToString().PadLeft(2, '0')}-{Manzana.ToString().PadLeft(4, '0')}-{Lote.ToString().PadLeft(4, '0')}-{Nivel1.ToString().PadLeft(2, '0')}-{Nivel2.ToString().PadLeft(2, '0')}-{Fraccion.ToString().PadLeft(2, '0')}-{Toma.ToString().PadLeft(2, '0')}";
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
        throw new FormatException("El formato de la clave es incorrecto.");
      }

      return nuevaClave;
    }
  }
}