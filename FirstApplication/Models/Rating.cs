namespace FirstApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    

    public partial class Rating
    {
        public string RatingId { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AspNetUser User { get; set; }

        [Required]
        [StringLength(128)]
        public string GameId { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
        public decimal Rank { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime EditDate { get; set; }
    }
}
