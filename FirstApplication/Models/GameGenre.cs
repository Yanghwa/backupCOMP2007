namespace FirstApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GameGenre
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public string GameGenreId { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Genre Name")]
        public string GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Game Name")]
        public string GameId { get; set; }

        public virtual Game Game { get; set; }

        [Display(Name = "Create Date")]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Edit Date")]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public DateTime EditDate { get; set; } = DateTime.UtcNow;

    }
}
