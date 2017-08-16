namespace FirstApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Game : BaseModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game()
        {
            Genres = new HashSet<GameGenre>();
        }
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public string GameId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Game Name")]
        public string Name { get; set; }
        [Display(Name = "Multiplay")]
        public bool IsMultiplayer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        [Display(Name = "Genres")]
        [InverseProperty("Game")]
        public virtual ICollection<GameGenre> Genres { get; set; } = new HashSet<GameGenre>();

        [Display(Name="Ratings")]
        [InverseProperty("Game")]
        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();

        [NotMapped]
        public decimal OverallRating
        {
            get
            {
                if(Ratings.Count() > 0)
                {
                    return (Ratings.Average(x => x.Rank));
                } else
                {
                    return (9);
                }
                
            }
        }

        public override string ToString()
        {
            return String.Format("{0}", Name);
        }
    }
}
