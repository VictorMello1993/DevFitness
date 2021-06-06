﻿using DevFitness.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace DevFitness.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, decimal height, decimal weight, DateTime birthDate) : base()
        {
            FullName = fullName;
            Height = height;
            Weight = weight;
            BirthDate = birthDate;

            Meals = new List<Meal>();
        }

        public string FullName { get; private set; }
        public decimal Height { get; private set; }
        public decimal Weight { get; private set; }
        public DateTime BirthDate { get; private set; }

        public IEnumerable<Meal> Meals { get; private set; }

        public void Update(decimal height, decimal weight)
        {
            if (!Active)
            {
                throw new UserIsAlreadyInactiveException();
            }

            Height = height;
            Weight = weight;
        }

        public override void Deactivate()
        {
            if (!Active)
            {
                throw new UserIsAlreadyInactiveException();
            }

            base.Deactivate();
        }
    }
}