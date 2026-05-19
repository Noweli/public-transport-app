using PublicTransport.Core.Application.Models.Results.Codes;

namespace PublicTransport.Core.Application.Models.Results;

public record RepositoryResult(RepositoryResultCode ResultCode)
{
    public bool Succeeded => ResultCode == RepositoryResultCode.Success;
}

public sealed record RepositoryResult<T>(RepositoryResultCode ResultCode, T Data) : RepositoryResult(ResultCode);