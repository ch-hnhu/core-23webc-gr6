using System;
using System.Collections.Generic;

namespace core_23webc_gr6.Models.Entities;

public partial class Contact
{
    public int ContactId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Subject { get; set; }

    public string Content { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }
}
