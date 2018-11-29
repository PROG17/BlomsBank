using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndreasBank.Models
{
    public class TransferViewModel
    {
        [Display(Name = "Från kontonummer")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Kontonumret får bara innehålla siffror.")]
        public int FromAccountId { get; set; }

        [Display(Name = "Till kontonummer")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Kontonumret får bara innehålla siffror.")]
        public int ToAccountId { get; set; }

        [Display(Name = "Summa")]
        public decimal Amount { get; set; }

        [Display(Name = "Disponibelt på frånkontot")]
        public string FromActualSumString { get; set; } = string.Empty;

        [Display(Name = "Disponibelt på tillKontot")]
        public string ToActualSumString { get; set; } = string.Empty;
    }
}
