using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.Utils
{
    internal static class CategoryValidationUtils
    {
        internal static bool IsValidCategoryIndex(int categoryNumber)
        {
            return Enum.IsDefined(typeof(CategoryType), categoryNumber);
        }

        internal static bool IsValidCategoryType(string category)
        {
            return byte.TryParse(category, out byte categoryType) && IsValidCategoryIndex(categoryType);
        }
    }
}