using System;
using System.Collections.Generic;

namespace core_23webc_gr6.Models.Entities;

public partial class WebSetting
{
    public int SettingId { get; set; }

    public string? SiteName { get; set; }

    public string? SiteDescription { get; set; }

    public string? Logo { get; set; }

    public string? Favicon { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Facebook { get; set; }

    public string? Youtube { get; set; }

    public string? LinkedIn { get; set; }

    public string? Twitter { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
