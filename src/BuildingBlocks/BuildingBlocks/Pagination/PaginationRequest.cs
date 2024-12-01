namespace BuildingBlocks.Pagination;

public record PaginationRequest(int Page = 0, int PageSize = 10);