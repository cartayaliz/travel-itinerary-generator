namespace Turist.Domain.Interfaces.Graph;

public interface ITransportWebService
{
    Task LoadAsync();

    List<int> ProposeAlternatives();
}