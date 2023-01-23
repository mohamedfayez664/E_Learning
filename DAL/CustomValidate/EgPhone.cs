using System.ComponentModel.DataAnnotations;

namespace DAL.CustomValidate
{
    public class EgPhone : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {


            // [RegularExpression("(01)[0125][0-9]{8}")]
            if (value.ToString().Contains("P"))
            {
                return true;
            }
            else
            {
                return false;
            }
            return base.IsValid(value);
        }
    }
}
