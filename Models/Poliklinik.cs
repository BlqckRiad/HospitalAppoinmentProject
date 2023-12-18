using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Models
{
    public class Poliklinik
{
    [Key]
    public int PolId { get; set; }
    public string? PolName { get; set; }

        // Bu alan, Entity Framework Core için birincil anahtarı ifade eder.
        public List<Doktor> Doktorlar { get; set; } = new List<Doktor>();
    }

}