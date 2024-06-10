namespace Application.Services
{
    public interface IPatientService
    {
        Task<bool> IsPatientAvailableAsync(int patientId);
    }
}
