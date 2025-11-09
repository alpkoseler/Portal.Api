using System;
using System.Collections.Generic;

namespace Portal.Api.Models;

public partial class KelimeKategori
{
    public int KelimeKategoriId { get; set; }

    public int KelimeId { get; set; }

    public int KategoriId { get; set; }

    public virtual Kategori Kategori { get; set; } = null!;

    public virtual Kelime Kelime { get; set; } = null!;
}
