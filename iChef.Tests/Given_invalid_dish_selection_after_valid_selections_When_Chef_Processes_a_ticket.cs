using System.Linq;
using Machine.Specifications;
using iChef.Domain;
using iChef.Tests.TestData;

namespace iChef.Tests
{
    public class Given_invalid_dish_selection_after_valid_selections_When_Chef_Processes_a_ticket
    {
        private Establish context = () =>
            {
                _ticket = new Ticket("morning", new[] { DishType.Entree, DishType.Side, DishType.Unknown, DishType.Drink });
                _sut = ChefFactory.CreateChef();
            };

        private Because of = () =>
            {
                _order = _sut.ProcessOrder(_ticket);
            };


        It should_process_till_the_error = () =>
            {
                _order.ShouldNotBeNull();
                _order.Items.ShouldNotBeNull();
                _order.Items.Count().ShouldEqual(3);
                _order.Items.ElementAt(0).DishName.ShouldNotEqual("Coffee");
                _order.Items.ElementAt(1).DishName.ShouldNotEqual("Coffee");
                _order.Items.ElementAt(2).DishName.ShouldEqual("Error");
            };

        static Chef _sut;
        static Ticket _ticket;
        static Order _order;
    }
}