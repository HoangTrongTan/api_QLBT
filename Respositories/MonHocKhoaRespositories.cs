using Microsoft.EntityFrameworkCore;
using Webquanlybaithi.Entities;

namespace Webquanlybaithi.Respositories
{
    public class MonHocKhoaRespositories : IMonHocKhoaRespositories
    {
        private readonly WebQuanlybaithiContext _context;
        public MonHocKhoaRespositories(WebQuanlybaithiContext ctx)
        {
            _context = ctx;
        }
        public Task<List<Hocphan>> all()
        {
            return _context.Hocphans.ToListAsync();
        }

        public async Task<string> del(string ma)
        {
            var modelToDel = await _context.Hocphans.FindAsync(ma);
            _context.Hocphans.Remove(modelToDel);
            await _context.SaveChangesAsync();
            return "Xóa thành công !";
        }

        public async Task<List<Hocphan>> findName(string name)
        {
            return await _context.Hocphans
                .Where( hp => hp.Tenhocphan.Contains(name) )
                .ToListAsync();
        }

        public async Task<string> post(Hocphan model)
        {
            _context.Hocphans.Add(model);
            await _context.SaveChangesAsync();
            return "Thêm thành công !!";
        }

        public async Task<string> put(Hocphan model)
        {
            var modelToFix = await _context.Hocphans.FindAsync(model.Ma);
            if (modelToFix == null)
            {
                return "Dữ liệu không tồn tại !!";
            }
            modelToFix.Ma = model.Ma;
            modelToFix.Tenhocphan = model.Tenhocphan;
            await _context.SaveChangesAsync();
            return "Sửa thành công !";
        }
    }
}
