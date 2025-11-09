using System;
using System.Collections.Generic;

namespace Portal.Api.Models;

public partial class Kategori
{
    public int KategoriId { get; set; }

    public string Tanim { get; set; } = null!;
}
