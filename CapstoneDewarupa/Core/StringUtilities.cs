using System.Text;
using System.Text.RegularExpressions;

namespace CapstoneDewarupa.Core
{
    public static class StringUtilities
    {
        public static string Truncate(string input, int length)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            // Menghapus semua tag HTML
            string cleanedInput = Regex.Replace(input, "<.*?>", string.Empty);

            // Jika panjang string lebih pendek dari length, return string asli
            if (cleanedInput.Length <= length)
            {
                return cleanedInput;
            }

            // Truncate string dan tambahkan "..."
            return cleanedInput.Substring(0, length) + "...";
        }
        public static string GenerateSlug(string input)
        {
            // Convert to lower case
            input = input.ToLowerInvariant();

            // Normalize the input string to decompose combined characters into their base characters + diacritics
            input = input.Normalize(NormalizationForm.FormD);

            // Remove diacritics (accents) from letters
            input = Regex.Replace(input, @"\p{Mn}", "");

            // Remove all non-alphanumeric characters except spaces
            input = Regex.Replace(input, @"[^\w\s-]", "");

            // Replace multiple spaces with a single space
            input = Regex.Replace(input, @"\s+", " ").Trim();

            // Replace spaces with hyphens
            input = input.Replace(" ", "-");

            // Remove any leading or trailing hyphens (in case there were leading/trailing spaces)
            input = input.Trim('-');

            return input;
        }
    }
}
