using System.ComponentModel.DataAnnotations;

namespace _3_HW_Films_MVC.Annotations
{
    public class RestrictedMoviesAttribute: ValidationAttribute
    {
        private static string[] restrictedMovies;

        public RestrictedMoviesAttribute(string[] books)
        {
            restrictedMovies = books;
        }

        public override bool IsValid(object? value)
        {
            if (value != null)
            {
                string? strval = value.ToString();
                for (int i = 0; i < restrictedMovies.Length; i++)
                {
                    if (strval == restrictedMovies[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
