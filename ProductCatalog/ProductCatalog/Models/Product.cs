using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Models {
    public class Product {
        [Key]
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Produktname muss angegeben werden!")]
        public string Name { get; set; }

        [JsonProperty("description")]
        [Display(Name = "Beschreibung")]
        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }

        [JsonProperty("unitPrice")]
        [Display(Name = "Preis (€)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Produktpreis muss angegeben werden!")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }


        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }

            var p = (Product)obj;

            return Id == p.Id && string.Equals(Name, p.Name) && string.Equals(Description, p.Description) && UnitPrice == p.UnitPrice;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public static bool operator ==(Product p1, Product p2) {
            return p1?.Id == p2?.Id;
        }
        public static bool operator !=(Product p1, Product p2) {
            return p1?.Id != p2?.Id;
        }
    }
}
