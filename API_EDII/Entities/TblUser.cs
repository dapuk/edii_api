using System;
using System.Collections.Generic;

namespace API_EDII.Entities;

public partial class TblUser
{
    public int Userid { get; set; }

    public string Namalengkap { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Status { get; set; } = null!;
}
