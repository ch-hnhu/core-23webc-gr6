using System;
using System.Collections.Generic;

namespace core_23webc_gr6.Models.Entities;

public partial class BillDetail
{
    public int BillDetailId { get; set; }

    public int BillId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public int? DiscountPercentage { get; set; }

    public decimal? SubTotal { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
