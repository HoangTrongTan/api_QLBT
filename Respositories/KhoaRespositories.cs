using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Webquanlybaithi.Entities;
using Webquanlybaithi.Responsitories;

namespace Webquanlybaithi.Respositories
{
    public class KhoaRespositories : IKhoaRespositories
    {
        private readonly WebQuanlybaithiContext _context;
        public KhoaRespositories(WebQuanlybaithiContext context)
        {
            _context = context;
        }

        public async Task<string> del(string ma)
        {
            var modelToDel = await _context.Khoas.FindAsync(ma);
            _context.Khoas.Remove(modelToDel);
            await _context.SaveChangesAsync();
            return "Xóa thành công !";
        }

        public async Task<List<Khoa>> getAll()
        {
           return await _context.Khoas!.ToListAsync();
        }

        public async Task<string> post(Khoa model)
        {
            _context.Khoas.Add(model);
            await _context.SaveChangesAsync();
            return "Thêm thành công !!";
        }

        public async Task<string> put(Khoa model)
        {
            var modelToFix = await _context.Khoas.FindAsync(model.Ma);
            if(modelToFix == null)
            {
                return "Dữ liệu không tồn tại !!";
            }
            modelToFix.Ma = model.Ma;
            modelToFix.Ten = model.Ten;
            await _context.SaveChangesAsync();
            return "Sửa thành công !";
        }
    }
}
