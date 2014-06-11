using System.Collections.Generic;
using iChef.Domain;

namespace iChef.Tests.TestData
{
    public static class ChefFactory
    {
        public static Chef CreateChef()
        {
            var menuItems = new List<MenuItem> {new MenuItem(DishType.Entree,new Dish("Eggs")), new MenuItem( DishType.Side,new Dish("Toast")), new MenuItem(DishType.Drink,new Dish("Coffee"),2)};
            var timeOfDayMenu = new Dictionary<string, IEnumerable<MenuItem>> {{"morning", menuItems}};
            return new Chef(new Menu(timeOfDayMenu));
        }
    }
}