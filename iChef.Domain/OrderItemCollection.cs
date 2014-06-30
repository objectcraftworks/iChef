using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace iChef.Domain
{
    public class OrderItemCollection : ICollection<OrderItem>
    {
        readonly Dictionary<DishType,OrderItem> _dishTypeOrderItemMap = new Dictionary<DishType, OrderItem>(); 
        public IEnumerator<OrderItem> GetEnumerator()
        {
            return _dishTypeOrderItemMap.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(OrderItem item)
        {   
            if (!CanAddItem(item))
            {
                throw new Exception();
            }
            if (!_dishTypeOrderItemMap.ContainsKey(item.DishType))
                _dishTypeOrderItemMap.Add(item.DishType, item);
            else
                _dishTypeOrderItemMap[item.DishType].Quantity++;
        }

        private bool CanAddItem(OrderItem item)
        {
            return _dishTypeOrderItemMap.Count(i => i.Key == item.DishType) < item.AllowedItems;
        }
        public void Clear()
        {
            _dishTypeOrderItemMap.Clear();
        }

        public bool Contains(OrderItem item)
        {
            return _dishTypeOrderItemMap.ContainsValue(item);
        }

        public void CopyTo(OrderItem[] array, int arrayIndex)
        {
            _dishTypeOrderItemMap.Values.CopyTo(array,arrayIndex);
        }

        public bool Remove(OrderItem item)
        {
            return _dishTypeOrderItemMap.Remove(item.DishType);
        }

        public int Count
        {
            get { return _dishTypeOrderItemMap.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
    }
}