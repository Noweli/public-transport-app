namespace PublicTransport.Core.Application.Models.Results.Codes;

public enum RepositoryResultCode
{
    Created,
    Found,
    Removed,
    NotFound,
    Cancelled,
    DbException
}