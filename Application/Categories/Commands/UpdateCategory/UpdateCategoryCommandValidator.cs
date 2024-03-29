﻿using Application.Categories.Commands.EditCategory;
using FluentValidation;

namespace Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(255)
                .NotEmpty();
        }
    }
}
