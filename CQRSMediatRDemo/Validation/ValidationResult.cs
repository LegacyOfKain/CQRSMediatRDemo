using System;

namespace CQRSMediatRDemo.Validation
{
    public record ValidationResult
    {
        public bool IsSuccessful { get; set; } = true;
        public string Error { get; init; }

        //public static ValidationResult Success() => new ValidationResult();
        // Use C# 9 new()
        public static ValidationResult Success() => new();
        public static ValidationResult Fail(string error) =>
            new() { IsSuccessful = false, Error = error };

    }
}