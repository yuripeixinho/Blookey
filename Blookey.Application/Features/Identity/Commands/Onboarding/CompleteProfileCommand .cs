using Blookey.Domain.Common;
using MediatR;

namespace Blookey.Application.Features.Identity.Commands.Onboarding;


public sealed record CompleteProfileResponse(string Message);

public sealed record CompleteProfileCommand : IRequest<Result<CompleteProfileResponse>>;