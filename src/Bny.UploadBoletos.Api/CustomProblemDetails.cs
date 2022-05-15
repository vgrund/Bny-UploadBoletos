using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Bny.UploadBoletos.Api
{
    public class CustomProblemDetails: ProblemDetails
    {
        public CustomProblemDetails(ValidationResult validationResult, int statusCode)
        {
            Errors = new List<string>(validationResult.Errors.Count);
            Status = statusCode;
            AddErrors(validationResult);
        }

        private void AddErrors(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Errors.Add(error.ErrorMessage);
            }
        }

        public ICollection<string> Errors { get; private set; }
    }
}
