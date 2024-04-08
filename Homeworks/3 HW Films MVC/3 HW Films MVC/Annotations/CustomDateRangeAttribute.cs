using System.ComponentModel.DataAnnotations;

namespace _3_HW_Films_MVC.Annotations
{
    public class CustomDateRangeAttribute : ValidationAttribute
    {
        private readonly string _minDate;
        private readonly string _maxDate;

        public CustomDateRangeAttribute(string minDate)
        {
            _minDate = minDate;
            _maxDate = DateTime.Now.ToString("yyyy-MM-dd");
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date;
                if (DateTime.TryParse(value.ToString(), out date))
                {
                    if (date >= DateTime.Parse(_minDate) && date <= DateTime.Parse(_maxDate))
                    {
                        return ValidationResult.Success;
                    }
                }

                return new ValidationResult(ErrorMessage ?? $"Значение должно быть датой между {_minDate} и {_maxDate}");
            }
            return ValidationResult.Success; 
        }
    }
}
