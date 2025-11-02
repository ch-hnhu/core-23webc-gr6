using System;
using System.Collections.Generic;

namespace core_23webc_gr6.Models.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
