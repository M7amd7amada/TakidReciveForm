namespace TakidReciveForm.Domain.Data;

public static class PagedResultExtension
{
    public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
    {
        var result = new PagedResult<T>
        {
            CurrentPage = page,
            PageSize = pageSize,
            RowCount = query.Count()
        };

        var pageCount = (double)result.RowCount / pageSize;
        result.PageCount = (int)Math.Ceiling(pageCount);

        var skip = (page - 1) * pageSize;
        skip = Math.Max(0, skip); // Ensure skip is not negative

        result.Results = query.Skip(skip).Take(pageSize).ToList();

        return result;
    }
}