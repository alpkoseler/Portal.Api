using System;
using System.Collections.Generic;

namespace Portal.Api.Models;

public partial class Kelime
{
    public int KelimeId { get; set; }

    public string Tanim { get; set; } = null!;

    public int DilId { get; set; }
}
