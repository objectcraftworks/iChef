using System;
using System.Collections.Generic;
using Machine.Specifications;
using iChef.Domain;
using iChef.Tests.TestData;

namespace iChef.Tests
{
    public class Given_multiple_orders_for_a_dish_that_can_be_ordered_atmost_one_When_Chef_Processes_a_ticket
    {
        private Establish context = () =>
            {
                _ticket = new Ticket("morning", new[] { DishType.Entree, DishType.Entree });
                _sut = ChefFactory.CreateChef();
            };

        private Because of = () =>
            {
                _order = _sut.ProcessOrder(_ticket);
            };


        It should_be_error = () =>
            {
                _order.ShouldNotBeNull();
                _order.HasError.ShouldEqual(true);
            };


        static Chef _sut;
        static Ticket _ticket;
        static Order _order;
    }
}