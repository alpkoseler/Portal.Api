using System;
using System.Collections.Generic;

namespace Portal.Api.Models;

public partial class Kategori
{
    public int KategoriId { get; set; }

    public string Tanim { get; set; } = null!;

    public virtual ICollection<KelimeKategori> KelimeKategoris { get; set; } = new List<KelimeKategori>();
}
