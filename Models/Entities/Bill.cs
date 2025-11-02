using System;
using System.Collections.Generic;

namespace core_23webc_gr6.Models.Entities;

public partial class Bill
{
    public int BillId { get; set; }

    public int UserId { get; set; }

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public string ShippingMethod { get; set; } = null!;

    public int DiscountPercentage { get; set; }

    public decimal TotalAmount { get; set; }

    public bool Status { get; set; }

    public string DeliveryStatus { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

    public virtual User User { get; set; } = null!;
}
