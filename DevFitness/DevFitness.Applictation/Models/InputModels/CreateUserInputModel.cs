﻿using System;

namespace DevFitness.Applictation.Models.InputModels
{
    public class CreateUserInputModel
    {
        public string FullName { get;  set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public DateTime BirthDate { get;  set; }
    }
}
