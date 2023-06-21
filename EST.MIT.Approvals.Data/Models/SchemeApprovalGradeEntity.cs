﻿using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data.Models;

[ExcludeFromCodeCoverage]
public class SchemeApprovalGradeEntity : BaseEntity
{
    public decimal ApprovalLimit { get; init; } = default!;

    public bool IsUnlimited { get; init; } = default!;


    public int SchemeGradeId { get; init; }

    public SchemeGradeEntity SchemeGrade { get; set; } = default!;
}
