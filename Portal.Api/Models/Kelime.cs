using System;
using System.Collections.Generic;

namespace Portal.Api.Models;

public partial class Kelime
{
    public int KelimeId { get; set; }

    public string Tanim { get; set; } = null!;

    public virtual ICollection<KelimeKategori> KelimeKategoris { get; set; } = new List<KelimeKategori>();
}
