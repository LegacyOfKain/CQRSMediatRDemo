using System.Threading.Tasks;

namespace CQRSMediatRDemo.Validation
{
    public interface IValidationHandler { };
    public interface IValidationHandler<T> : IValidationHandler
    {
        Task<ValidationResult> Validate(T Request);
    }
}
