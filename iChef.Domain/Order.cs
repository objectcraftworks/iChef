using System.Collections.Generic;
using System.Linq;
namespace iChef.Domain
{
    public class Order
    {
        static OrderItem _errorItem = new OrderItem(DishType.Unknown, new Dish("Error"));
        readonly List<OrderItem> _items = new List<OrderItem>();
        
        public IEnumerable<OrderItem> Items
        {
            get
            {
                foreach (var item in _items.OrderBy(i => i.FoodMakingOrder))
                {
                    yield return item;
                }
                if (HasError)
                    // Client could call directly HasEror, or Order returns all the items treating the special condition as a dish type
                    yield return _errorItem;
            }
        }
       
        public bool HasError { get; set; }
        public void AddItem(OrderItem orderItem)
        {
            _items.Add(orderItem);
        }
    }
}