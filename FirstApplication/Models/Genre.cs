namespace FirstApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Genre
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Genre()
        {
            Games = new HashSet<GameGenre>();
        }

        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public string GenreId { get; set; }
        
        [Required]
        [StringLength(250)]
        [Display(Name = "Genre Name")]
        public string Name { get; set; }

        [Display(Name = "Create Date")]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Edit Date")]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public DateTime EditDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Game")]
        [InverseProperty("Genre")]
        public virtual ICollection<GameGenre> Games { get; set; }

        public override string ToString()
        {
            return String.Format("{0}", Name);
        }
    }
}
