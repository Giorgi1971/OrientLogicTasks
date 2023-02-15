using System;
using System.Collections.Generic;
using System.Text;
using T8_Builder_DesignPatterns.Models;

namespace T8_Builder_DesignPatterns.Builders
{
    public class DrinkBuilder
    {
        private Drink _drink;

        public DrinkBuilder ResetDrink()
        {
            _drink = new Drink();
            return this;
        }

        public DrinkBuilder WithWather()
        {
            _drink.Cup.Add("Wather");
            return this;
        }

        public DrinkBuilder WithSpoonSuger()
        {
            _drink.Cup.Add("SpoonSuger");
            return this;
        }

        public DrinkBuilder WithChocolate()
        {
            _drink.Cup.Add("Shokolate");
            return this;
        }

        public DrinkBuilder WithNagebi()
        {
            _drink.Cup.Add("Nagebi");
            return this;
        }

        public DrinkBuilder WithKhotsakhuri()
        {
            _drink.Cup.Add("Kotskhuri");
            return this;
        }

        public Drink Build()
        {
            return _drink;
        }
    }
}