using Webquanlybaithi.Entities;

namespace Webquanlybaithi.Respositories
{
    public interface ITaiKhoanRespositories
    {
        public Task<Taikhoan> register(Taikhoan tk);
        public Task<string> get(Taikhoan tk);
        public Task<string> getUser(Taikhoan tk);
    }
}
