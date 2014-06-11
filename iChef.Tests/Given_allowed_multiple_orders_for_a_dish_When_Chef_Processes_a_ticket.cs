using Machine.Specifications;
using iChef.Domain;
using iChef.Tests.TestData;
using System.Linq;
namespace iChef.Tests
{
    public class Given_allowed_multiple_orders_for_a_dish_When_Chef_Processes_a_ticket
    {
        private Establish context = () =>
            {
                _ticket = new Ticket("morning", new[] {DishType.Drink, DishType.Drink});
                _sut =  ChefFactory.CreateChef();
            };

        private Because of = () =>
            {
                _order = _sut.ProcessOrder(_ticket);
            };

        It should_allow_multiple_orders_of_item = () =>
            {
                _order.Items.ElementAt(0).Quantity.ShouldEqual(2);
            };

               
        static Chef _sut;
        static Ticket _ticket;
        static Order _order;
    }
}