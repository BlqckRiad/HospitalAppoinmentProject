using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApp.Models
{
    // Doktor.cs

public class Doktor
{
    public int DoktorId { get; set; }
    public string? DoktorAd { get; set; }
    public int DalId { get; set; }

        [ForeignKey("Poliklinik")]
        public int PolId { get; set; }

    // Navigation properties
    public Dal? Dal { get; set; }
    public Poliklinik? Poliklinik { get; set; }
}

}