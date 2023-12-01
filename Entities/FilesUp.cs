using System;
using System.Collections.Generic;

namespace Webquanlybaithi.Entities;

public partial class FilesUp
{
    public int Id { get; set; }

    public string Makhoa { get; set; } = null!;

    public int? IdkhoaDk { get; set; }

    public int? Lop { get; set; }

    public string? Mahocphan { get; set; }

    public string? FileUp { get; set; }

    public int? Duyet { get; set; }

    public DateTime? Thoigian { get; set; }

    public string? Idgiaovien { get; set; }

    public virtual KhoaDk? IdkhoaDkNavigation { get; set; }

    public virtual Lop? LopNavigation { get; set; }

    public virtual Hocphan? MahocphanNavigation { get; set; }

    public virtual Khoa MakhoaNavigation { get; set; } = null!;
}
