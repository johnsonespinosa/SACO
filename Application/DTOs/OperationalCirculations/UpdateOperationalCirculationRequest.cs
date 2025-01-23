namespace Application.DTOs.OperationalCirculations;

public class UpdateOperationalCirculationRequest : CreateOperationalCirculationRequest
{
    public Guid Id { get; set; }
}