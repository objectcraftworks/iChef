using System.Linq;
namespace iChef.Domain
{
    public class DishType
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int FoodMakingOrder { get; set; }
        
        public DishType(int code, string name, int foodMakingOrder)
        {
            Code = code;
            Name = name;
            FoodMakingOrder = foodMakingOrder;
        }
        public static DishType Entree = new DishType(1,"Entree",1);
        public static DishType Side= new DishType(2,"Side",2);
        public static DishType Drink= new DishType(3,"Drink",3);
        public static DishType Desert= new DishType(4,"Desert",4);
        public static DishType Unknown= new DishType(5,"Error",5);

        public static DishType GetByCode(int code)
        {
            var dishTypeList = new[] {Entree, Side, Drink, Desert};
            var dishType = dishTypeList.FirstOrDefault(d => d.Code == code);
            return dishType ?? Unknown;
        }
    }
}