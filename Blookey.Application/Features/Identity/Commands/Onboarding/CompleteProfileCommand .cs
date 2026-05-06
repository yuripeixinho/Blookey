using Blookey.Domain.Common;
using MediatR;

namespace Blookey.Application.Features.Identity.Commands.Onboarding;

// CompleteProfileCommand.cs
public sealed record CompleteProfileCommand() : IRequest<Result<string>>;