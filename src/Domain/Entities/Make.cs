using System.Collections.Generic;

namespace CarsManager.Domain.Entities
{
    public class Make
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Model> Models { get; private set; } = new HashSet<Model>();
    }
}
