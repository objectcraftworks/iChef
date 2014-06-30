namespace iChef.Domain
{
    public class OrderItem
    {
        readonly DishType _dishType;
        readonly Dish _dish;
        readonly int _allowedItems;

        public OrderItem(DishType dishType, Dish dish, int allowedItems)
        {
            _dishType = dishType;
            _dish = dish;
            _allowedItems = allowedItems;
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

        public DishType DishType
        {
            get { return _dishType; }
        }

        public int AllowedItems
        {
            get { return _allowedItems; }
        }
    }
}