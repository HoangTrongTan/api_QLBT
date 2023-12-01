using Microsoft.EntityFrameworkCore;
using Webquanlybaithi.Entities;

namespace Webquanlybaithi.Respositories
{
    public class LopRespositories : ILopRespositories
    {
        private readonly WebQuanlybaithiContext _context;
        public LopRespositories(WebQuanlybaithiContext ctx)
        {
            _context = ctx;
        }
        public async Task<List<Lop>> all()
        {
            return await _context.Lops.ToListAsync();
        }

        public async Task<List<Lop>> getByMa(int khoadk)
        {
            return await _context.Lops!.Where( lop => lop.KhoaDk == khoadk).ToListAsync();
        }

        public async Task<int> getIdByName(int dk, string name)
        {
            var objLop = await _context.Lops
                                        .Where( lop => lop.KhoaDk == dk && lop.Ten.Contains(name) ).FirstOrDefaultAsync();
            return objLop.Id;
        }

        public async Task<string> del(int ma)
        {
            var modelToDel = await _context.Lops.FindAsync(ma);
            _context.Lops.Remove(modelToDel);
            await _context.SaveChangesAsync();
            return "Xóa thành công !";
        }
        public async Task<string> post(Lop model)
        {
            _context.Lops.Add(model);
            await _context.SaveChangesAsync();
            return "Thêm thành công !!";
        }

        public async Task<string> put(Lop model)
        {
            var modelToFix = await _context.Lops.FindAsync(model.Id);
            if (modelToFix == null)
            {
                return "Dữ liệu không tồn tại !!";
            }
            modelToFix.Ten = model.Ten;
            modelToFix.Ma = model.Ma;
            modelToFix.KhoaDk = model.KhoaDk;
            await _context.SaveChangesAsync();
            return "Sửa thành công !";
        }

        public async Task<List<Lop>> getLopWithKhoaDk()
        {
            var lopList = await _context.Lops
                                        .Include(lop => lop.KhoaDkNavigation)
                                        .Select(lop => new Lop
                                            {
                                                Id = lop.Id,
                                                Ten = lop.Ten,
                                                Ma = lop.Ma,
                                                KhoaDk = lop.KhoaDk,
                                                KhoaDkNavigation = new KhoaDk
                                                {
                                                    Id = lop.KhoaDkNavigation.Id,
                                                    Loai = lop.KhoaDkNavigation.Loai,
                                                    Ma = lop.KhoaDkNavigation.Ma,
                                                    MaNavigation = new Khoa
                                                    {
                                                        Ten = lop.KhoaDkNavigation.MaNavigation.Ten,
                                                    }
                                                },
                                                
                                            }
                                        )
                                        .ToListAsync();
            return lopList;
        }
    }
}
