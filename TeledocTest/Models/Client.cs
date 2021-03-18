using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeledocTest.Models
{
    public class Client
    {
        public Client()
        {
            Founders = new List<Founder>();
        }

        public int Id { get; set; }

        [Display(Name = "ИНН")]
        [Required]
        [StringLength(12)]
        public string Inn { get; set; }

        [Display(Name = "Название")]
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Display(Name = "Тип организации")]
        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Display(Name = "Дата добавления")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Дата обновления")]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "Учредители")]
        public virtual ICollection<Founder> Founders { get; set; }
    }
}
