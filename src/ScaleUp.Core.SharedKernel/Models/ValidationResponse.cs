namespace ScaleUp.Core.SharedKernel.Models;

public sealed class ValidationResponse
{
    public bool IsValid => _errorMessages.Count == 0;
    public IReadOnlyList<string> ErrorMessages => _errorMessages;

    private readonly List<string> _errorMessages = [];

    public void AddError(string message)
    {
        _errorMessages.Add(message);
    }
}