using System;
using System.Collections.Generic;
using System.Text;
using T8_Builder_DesignPatterns.Models;

namespace T8_Builder_DesignPatterns.Builders
{
    public class BurgerBuilder
    {
        private Burger _burger;

        public BurgerBuilder ResetBurger()
        {
            _burger = new Burger();
            return this;
        }

        public BurgerBuilder WithBun()
        {
            _burger.Ingredients.Add("Bun");
            return this;
        }

        public BurgerBuilder WithLettuce()
        {
            _burger.Ingredients.Add("Lettuce");
            return this;
        }

        public BurgerBuilder WithCheese()
        {
            _burger.Ingredients.Add("Cheese");
            return this;
        }

        public BurgerBuilder WithPickles()
        {
            _burger.Ingredients.Add("Pickles");
            return this;
        }

        public BurgerBuilder WithBeef()
        {
            _burger.Ingredients.Add("Beef");
            return this;
        }

        public BurgerBuilder WithChicken()
        {
            _burger.Ingredients.Add("Chicken");
            return this;
        }

        public Burger Build()
        {
            return _burger;
        }
    }
}
