using System;
using System.Collections.Generic;
using System.Linq;
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


        public static Ticket CreateTicketFrom(string orderInput)
        {
            var split = orderInput.Split(',');
        
            var timeOfDay = split[0].Trim().ToLower();
            var dishTypes = split.Skip(1).Select(GetDishTypeFor).ToList();
           
            return new Ticket(timeOfDay, dishTypes);
        }

        static DishType GetDishTypeFor(string dishCode)
        {
            int result;
            if (Int32.TryParse(dishCode, out result))
                return DishType.GetByCode(result);

            return DishType.Unknown;
        }
    }
}