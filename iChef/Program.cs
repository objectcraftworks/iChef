using System;
using System.Collections.Generic;
using System.Linq;
using iChef.Domain;

namespace iChef
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var chef = CreateChef();
                Console.WriteLine("Please Enter to end the program.");
                while (true)
                {
                    Console.Write("Input:");
                    var orderInput = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(orderInput))
                        break;
                    var ticket = Chef.CreateTicketFrom(orderInput);
                    var order = chef.ProcessOrder(ticket);
                    Console.WriteLine("Output: {0}",
                                      string.Join(",", order.Items.Select(GetDisplayOrderItem)));
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("Unexpected Error occurred {0}", exp.Message);
            }
        }

        static string GetDisplayOrderItem(OrderItem item)
        {
            return string.Format("{0}{1}", item.DishName.ToLower(),
                                 item.Quantity > 1 ? string.Format("(x{0})", item.Quantity) : "");
        }

        public static Chef CreateChef()
        {
            var nightItems= new List<MenuItem> { new MenuItem(DishType.Entree, new Dish("Steak")),
                                                 new MenuItem(DishType.Side, new Dish("Potato"),allowedItems:2), 
                                                 new MenuItem(DishType.Drink, new Dish("Wine")),
                                                 new MenuItem(DishType.Desert, new Dish("Cake")) };

            var morningItems= new List<MenuItem> { new MenuItem(DishType.Entree, new Dish("Eggs")),
                                                 new MenuItem(DishType.Side, new Dish("Toast")), 
                                                 new MenuItem(DishType.Drink, new Dish("Coffee"), allowedItems:2) };

            var timeOfDayMenu = new Dictionary<string, IEnumerable<MenuItem>> { { "morning", morningItems},{"night",nightItems} };
            return new Chef(new Menu(timeOfDayMenu));
        }
    }
}
