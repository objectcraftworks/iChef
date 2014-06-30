using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace iChef.Domain
{
    public class Order
    {
        static readonly OrderItem ErrorItem = new OrderItem(DishType.Unknown, new Dish("Error"),1);
        IEnumerable<OrderItem> _items;
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
                    yield return ErrorItem;
            }
        }
       
        public bool HasError { get; set; }

        public void AddItems(IEnumerable<OrderItem> orderItems)
        {
            _items = orderItems;
        }
    }
}