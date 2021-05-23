﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3Training24032021.Shared
{
    public class CustomDomainValidation: ValidationAttribute
    {
        public string AllowedDomain { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] strings = value.ToString().Split("@");
            if(strings[1].ToUpper() == AllowedDomain.ToUpper())
            {
                return null;
            }

            return new ValidationResult(ErrorMessage,
                       new[] { validationContext.MemberName });



        }

    }
}
