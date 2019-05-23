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
        return $"{Subsistema.ToString()}-{Sector.ToString().PadLeft(2, '0')}-{Manzana.ToString().PadLeft(4, '0')}-{Lote.ToString().PadLeft(4, '0')}-{Nivel[0].ToString().PadLeft(2, '0')}-{Nivel[1].ToString().PadLeft(2,'0')}-{Fraccion.ToString().PadLeft(2,'0')}-{Toma.ToString().PadLeft(2,'0')}";
      }
    }
  }
}