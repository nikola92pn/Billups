using Billups.Api.Models;
using Billups.Application.Dtos;

namespace Billups.Api.Mappers;

public static class ChoiceMapper
{
    public static ChoiceResponse ToResponse(this ChoiceDto choice)
        => new(choice.Id, choice.Move.ToString());
}