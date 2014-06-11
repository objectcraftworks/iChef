using System.Collections.Generic;

namespace iChef.Domain
{
    public class Ticket
    {
        public string TimeOfDay { get; set; }
        public IEnumerable<DishType> Dishes { get; set; }
        public Ticket(string timeOfDay, IEnumerable<DishType> dishes)
        {
            TimeOfDay = timeOfDay;
            Dishes = dishes;
        }
    }
}