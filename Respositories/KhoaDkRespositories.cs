using Microsoft.EntityFrameworkCore;
using Webquanlybaithi.Entities;

namespace Webquanlybaithi.Respositories
{
    public class KhoaDkRespositories : IKhoaDKRespositories
    {
        private readonly WebQuanlybaithiContext _context;
        public KhoaDkRespositories(WebQuanlybaithiContext context)
        {
            _context = context;
        }

        public async Task<List<KhoaDk>> getAll()
        {
            return await _context.KhoaDks
                 .Include(dk => dk.MaNavigation)
                 .Select(dk => new KhoaDk
                     {
                        Id = dk.Id,
                        Loai = dk.Loai,
                        Ma = dk.Ma,
                        MaNavigation = new Khoa
                        {
                            Ma = dk.MaNavigation.Ma,
                            Ten = dk.MaNavigation.Ten,
                        }
                     }
                 )
                 .ToListAsync();
        }

        public async Task<int?> getIdByLoai(int loai, string ma)
        {
            var Khoadk = await _context.KhoaDks.Where(dk => dk.Loai == loai && dk.Ma == ma).FirstOrDefaultAsync();
            return Khoadk.Id;
        }

        public async Task<List<KhoaDk>> getkHOAbyMa(string ma)
        {
            return await _context.KhoaDks.Where( kh => kh.Ma == ma).ToListAsync();
        }
        public async Task<string> del(int ma)
        {
            var modelToDel = await _context.KhoaDks.FindAsync(ma);
            _context.KhoaDks.Remove(modelToDel);
            await _context.SaveChangesAsync();
            return "Xóa thành công !";
        }
        public async Task<string> post(KhoaDk model)
        {
            _context.KhoaDks.Add(model);
            await _context.SaveChangesAsync();
            return "Thêm thành công !!";
        }

        public async Task<string> put(KhoaDk model)
        {
            var modelToFix = await _context.KhoaDks.FindAsync(model.Id);
            if (modelToFix == null)
            {
                return "Dữ liệu không tồn tại !!";
            }
            modelToFix.Loai = model.Loai;
            modelToFix.Ma = model.Ma;
            await _context.SaveChangesAsync();
            return "Sửa thành công !";
        }
    }
}
 