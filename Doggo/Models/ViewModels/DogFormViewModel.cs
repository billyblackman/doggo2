using System.Collections.Generic;

namespace Doggo.Models.ViewModels
{
    public class DogFormViewModel
    {
        public Dog Dog { get; set; }
        public List<Owner> Owners { get; set; }
    }
}
