using System;
using System.Collections.Generic;

namespace core_23webc_gr6.Models.Entities;

public partial class Review
{
    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public string? Content { get; set; }

    public byte Rating { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
