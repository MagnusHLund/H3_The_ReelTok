using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.Mappers
{
    internal static class CategoryMapper
    {
        /// <summary>
        /// Because the CategoryType enum is responsible for adding the category rows in the database,
        /// We will get the correct CategoryId in the database, by the count of the Enum value.
        /// </summary>
        /// <param name="categoryType">The category to get the database categoryId of</param>
        /// <returns></returns>
        internal static uint ConvertCategoryTypeToCategoryId(CategoryType categoryType)
        {
            CategoryType[] enumValues = (CategoryType[])Enum.GetValues(typeof(CategoryType));
            uint categoryId = (uint)Array.IndexOf(enumValues, categoryType) + 1;

            return categoryId;
        }

        /// <summary>
        /// Converts a CategoryId to the corresponding CategoryType enum value.
        /// </summary>
        /// <param name="categoryId">The CategoryId to convert</param>
        /// <returns>The corresponding CategoryType enum value</returns>
        internal static CategoryType ConvertCategoryIdToCategoryType(uint categoryId)
        {
            CategoryType[] enumValues = (CategoryType[])Enum.GetValues(typeof(CategoryType));

            if (categoryId < 1 || categoryId > enumValues.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(categoryId), "Invalid CategoryId");
            }

            return enumValues[categoryId - 1];
        }
    }
}
