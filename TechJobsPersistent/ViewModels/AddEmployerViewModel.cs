using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddEmployerViewModel
    {
        [Required(ErrorMessage ="Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Locatrion is required.")]
        public string Location { get; set; }

        public AddEmployerViewModel(List<Employer> employers)
        {

            foreach (var item in employers)
            {
                this.Name = item.Name;
                this.Location = item.Location;
            }

        }

        public AddEmployerViewModel()
        {

        }
    }
}
