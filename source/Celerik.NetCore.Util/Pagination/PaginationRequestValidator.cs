﻿using System;
using System.Linq;
using FluentValidation;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Custom validator for the PaginationRequest payload object.
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload object.</typeparam>
    public class PaginationRequestValidator<TPayload> : AbstractValidator<TPayload>
        where TPayload : PaginationRequest
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public PaginationRequestValidator()
        {
            RuleFor(payload => payload.PageNumber)
                .GreaterThan(0);

            RuleFor(payload => payload.PageSize)
                .GreaterThan(0);

            RuleFor(payload => payload.SortDirection)
                .Must(payload =>
                    payload.ToLowerInvariant() == SortDirectionType.Asc.GetDescription().ToLowerInvariant() ||
                    payload.ToLowerInvariant() == SortDirectionType.Desc.GetDescription().ToLowerInvariant())
                .When(payload => !string.IsNullOrEmpty(payload.SortDirection))
                .WithMessage(UtilResources.Get("PaginationRequestValidator.Constructor.InvalidSortDirection"));
        }
    }

    /// <summary>
    /// Custom validator for the PaginationRequest<TEntity> payload object.
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload object.</typeparam>
    /// <typeparam name="TEntity">The type of the entity against we validate
    /// wich columns are valid for the "SortKey" property.</typeparam>
    public class PaginationRequestValidator<TPayload, TEntity> : PaginationRequestValidator<TPayload>
        where TPayload : PaginationRequest
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public PaginationRequestValidator()
        {
            var propNames = typeof(TEntity).GetProperties().Select(prop => prop.Name.ToLowerInvariant());

            RuleFor(payload => payload.SortKey)
                .Must(payload => propNames.Contains(payload.ToLowerInvariant()))
                .When(payload => !string.IsNullOrEmpty(payload.SortKey))
                .WithMessage(
                    UtilResources.Get(
                        "PaginationRequestValidator.Constructor.InvalidSortKey",
                        string.Join(',', propNames)
                    )
                );
        }
    }
}
