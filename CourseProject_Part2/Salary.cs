﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mims_CourseProject_Part2
{
    [Serializable]
    public class Salary : Employee
    {
        // attributes
        private double annualSalary;

        public Salary() : base()
        {
            annualSalary = 0.0;
        }

        public Salary(string firstName, string lastName, string ssn, DateTime hireDate, Benefits benefits, double annualSalary) : base(firstName, lastName, ssn, hireDate, benefits)
        {
            this.annualSalary = annualSalary;
        }

        public override double CalculatePay()
        {
            return annualSalary / 26.0;
        }

        public override string ToString()
        {
            return base.ToString() + ", annualSalary=" + annualSalary.ToString("C2");
        }

        public double AnnualSalary
        {
            get { return annualSalary; }
            set { annualSalary = value; }
        }
    }
}
