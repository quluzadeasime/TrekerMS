using System.Text;

namespace TREKER.MVC.Areas.Admin.Functions
{
    public class ViewFunctions
    {
        public static string InsertSpacesBeforeUppercase(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            result.Append(input[0]);

            for (int i = 1; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]))
                {
                    result.Append(' ');
                }
                result.Append(input[i]);
            }

            return result.ToString();
        }

        public static string GetStarRating(float starRating)
        {
            int fullStars = (int)starRating;
            bool hasHalfStar = (starRating - fullStars) >= 0.1;
            int emptyStars = 5 - fullStars - (hasHalfStar ? 1 : 0);

            string starsHtml = "";
            for (int i = 0; i < fullStars; i++)
            {
                starsHtml += "<i class='fa fa-star'></i>";
            }
            if (hasHalfStar)
            {
                starsHtml += "<i class='fa fa-star-half'></i>";
            }
            for (int i = 0; i < emptyStars; i++)
            {
                starsHtml += "<i class='fa fa-star empty'></i>";
            }
            return starsHtml;
        }
    }
}
