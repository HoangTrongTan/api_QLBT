using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Webquanlybaithi.Entities;

public partial class KhoaDk
{
    public int Id { get; set; }

    public int? Loai { get; set; }

    public string? Ma { get; set; }

    [JsonIgnore]
    public virtual ICollection<FilesUp>? FilesUps { get; set; } = new List<FilesUp>();
    [JsonIgnore]
    public virtual ICollection<Lop>? Lops { get; set; } = new List<Lop>();

    [JsonPropertyName("Khoa")]
    public virtual Khoa? MaNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<Taikhoan>? Taikhoans { get; set; } = new List<Taikhoan>();
}
