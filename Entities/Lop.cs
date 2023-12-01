using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Webquanlybaithi.Entities;

public partial class Lop
{
    public int Id { get; set; }

    public string? Ten { get; set; }

    public string? Ma { get; set; }

    public int? KhoaDk { get; set; }
    [JsonIgnore]
    public virtual ICollection<FilesUp> FilesUps { get; set; } = new List<FilesUp>();

    public virtual KhoaDk? KhoaDkNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<Taikhoan> Taikhoans { get; set; } = new List<Taikhoan>();
}
