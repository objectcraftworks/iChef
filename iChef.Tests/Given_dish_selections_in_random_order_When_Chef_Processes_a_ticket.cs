using System.Linq;
using Machine.Specifications;
using iChef.Domain;
using iChef.Tests.TestData;

namespace iChef.Tests
{
    public class Given_dish_selections_in_random_order_When_Chef_Processes_a_ticket
    {
        private Establish context = () =>
            {
                _ticket = new Ticket("morning", new[] { DishType.Side, DishType.Entree, DishType.Drink});
                _sut = ChefFactory.CreateChef();
            };

        private Because of = () =>
            {
                _order = _sut.ProcessOrder(_ticket);
            };


        It should_serve_the_dishes_in_a_particular_order = () =>
            {
                _order.ShouldNotBeNull();
                _order.Items.ShouldNotBeNull();
                _order.Items.ElementAt(0).DishName.ShouldEqual("Eggs");
                _order.Items.ElementAt(1).DishName.ShouldEqual("Toast");
                _order.Items.ElementAt(2).DishName.ShouldEqual("Coffee");
            };

        static Chef _sut;
        static Ticket _ticket;
        static Order _order;
    }
}