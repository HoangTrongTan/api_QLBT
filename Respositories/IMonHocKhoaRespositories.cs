using Webquanlybaithi.Entities;

namespace Webquanlybaithi.Respositories
{
    public interface IMonHocKhoaRespositories
    {
        public Task<List<Hocphan>> all();
        public Task<List<Hocphan>> findName(string name);
        public Task<string> post(Hocphan model);

        public Task<string> put(Hocphan model);

        public Task<string> del(string ma);
    }
}
