using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeledocTest.Models
{
    public class Founder
    {
        public Founder()
        {
            Clients = new List<Client>();
        }

        public int Id { get; set; }

        [Display(Name = "ИНН")]
        [Required]
        [StringLength(12)]
        public string Inn { get; set; }

        [Display(Name = "Имя")]
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        [StringLength(50)]
        public string SecondName { get; set; }

        [Display(Name = "Имя")]
        [StringLength(150)]
        public string FullName { get; set; }

        [Display(Name = "Дата добавления")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Дата обновления")]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "Учрежденные организации")]
        public virtual ICollection<Client> Clients { get; set; }

        public void SetFullName()
        {
            string fullName = LastName + " " + FirstName;
            if (SecondName != null)
            {
                fullName = fullName + " " + SecondName;
            }
            FullName = fullName;
        }
    }
}
