using Webquanlybaithi.Entities;
using Webquanlybaithi.Models;

namespace Webquanlybaithi.Respositories
{
    public interface IFilesRespositories
    {
        public Task<List<FilesUpModelsEnough>> get(string ma);
        public Task<string> post(FilesUpModel file);

        public Task<string> delete(int id);

        public Task<string> Fix(FilesUpModel model);
    }
}
