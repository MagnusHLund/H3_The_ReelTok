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
    }
}
