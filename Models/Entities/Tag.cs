using System;
using System.Collections.Generic;

namespace core_23webc_gr6.Models.Entities;

public partial class Tag
{
    public int TagId { get; set; }

    public string TagName { get; set; } = null!;

    public string? Description { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
