namespace iChef.Domain
{
    public class Dish
    {
        public string DishName { get; private set; }
        public Dish(string dishName)
        {
            DishName = dishName;
        }
    }
}