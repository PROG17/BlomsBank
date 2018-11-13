using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndreasBank.Models
{
    public class IndexViewModel
    {
        [Display(Name = "Kontonummer")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Kontonumret får bara innehålla siffror.")]
        public int AccountId { get; set; }
        [Display(Name = "Summa")]
        public decimal Amount { get; set; }
        [Display(Name = "Disponibelt")]
        public string ActualSumString { get; set; } = string.Empty;
    }
}
