namespace HospitalApp.Models
{
    // Randevu.cs

public class Randevu
{
    public int RandevuId { get; set; }
    public string? HastaAdi { get; set; }
    public int DoktorId { get; set; }
    public string? HastaTC { get; set; }
    public string? HastaTelefonNo { get; set; }
    public DateTime RandevuTarihi { get; set; }

    // Navigation properties
    public Doktor? Doktor { get; set; }
}

}