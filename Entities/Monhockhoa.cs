using System;
using System.Collections.Generic;

namespace Webquanlybaithi.Entities;

public partial class Monhockhoa
{
    public int Id { get; set; }

    public string? Mahocphan { get; set; }

    public string? Khoa { get; set; }

    public virtual Khoa? KhoaNavigation { get; set; }

    public virtual Hocphan? MahocphanNavigation { get; set; }
}
