namespace iChef.Domain
{
    public class OrderItem
    {
        readonly DishType _dishType;
        readonly Dish _dish;

        public OrderItem(DishType dishType, Dish dish)
        {
            _dishType = dishType;
            _dish = dish;
            Quantity = 1;
        }
        public int Quantity { get; set; }

        public string DishName
        {
            get { return _dish.DishName; }
        }

        public int FoodMakingOrder
        {
            get { return _dishType.FoodMakingOrder; }
        }
    }
}