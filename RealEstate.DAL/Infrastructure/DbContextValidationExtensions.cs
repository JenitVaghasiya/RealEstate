namespace RealEstate
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public static class DbContextValidationExtensions
    {
        public static void ValidateCreatedAndUpdatedEntities(this DbContext context)
        {
            var createdAndUpdatedEntities = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added
                    || e.State == EntityState.Modified)
                .Select(e => e.Entity);

            foreach (var entity in createdAndUpdatedEntities)
            {
                // Validate attributes first
                var validationContext = new ValidationContext(entity, null, null);
                var results = new List<ValidationResult>();

                bool valid = Validator.TryValidateObject(entity, validationContext, results, true);
                if (!valid)
                {
                    throw new InvalidOperationException(results.First().ErrorMessage);
                }

                // Then carry out any custom validation
                if (entity is IValidatable validatable)
                {
                    validatable.Validate();
                }
            }
        }
    }
}
