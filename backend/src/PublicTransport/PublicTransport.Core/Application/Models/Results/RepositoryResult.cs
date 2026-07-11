using PublicTransport.Core.Application.Models.Results.Codes;

namespace PublicTransport.Core.Application.Models.Results;

public record RepositoryResult(RepositoryResultCode ResultCode);

public sealed record RepositoryResult<T>(RepositoryResultCode ResultCode, T Data) : RepositoryResult(ResultCode);