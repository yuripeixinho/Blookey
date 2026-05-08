using Blookey.Domain.Common;
using Blookey.Infrastructure.Integrations.Assas.Dtos;
using MediatR;

namespace Blookey.Application.Features.Identity.Commands.Onboarding;

public sealed record CompleteProfileCommand : IRequest<Result<CreateSubAccountResponse>>;