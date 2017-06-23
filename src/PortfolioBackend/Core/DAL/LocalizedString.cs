using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Core.DAL
{
    public class LocalizedString
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(1024)]
        [Required]
        public string Key { get; set; }
        public int CultureId { get; set; }
        [Column(TypeName = "text")]
        public string DefaultValue { get; set; }
        [DataType("text")]
        public string Value { get; set; }
    }
}
