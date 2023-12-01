using Microsoft.EntityFrameworkCore;
using Webquanlybaithi.Entities;
using Webquanlybaithi.Models;

namespace Webquanlybaithi.Respositories
{
    public class FilesRespositories : IFilesRespositories
    {
        private readonly WebQuanlybaithiContext _context;

        public FilesRespositories(WebQuanlybaithiContext context) {
            _context = context;
        }

        public async Task<string> delete(int id)
        {
            var fileToDel = await _context.FilesUps.FindAsync(id);
            if (fileToDel == null)
            {
                return null;
            }
            deleteFile(fileToDel.FileUp);
            _context.FilesUps.Remove(fileToDel);
            await _context.SaveChangesAsync();
            return "Xóa Thành công";

        }

        public async Task<string> Fix(FilesUpModel model)
        {
            var objToFix = await _context.FilesUps.FindAsync(model.Id);
            if (objToFix == null)
            {
                return null;
            }
            objToFix.Makhoa = model.Makhoa;
            objToFix.IdkhoaDk = model.IdkhoaDk;
            objToFix.Lop = model.Lop;
            objToFix.Mahocphan = model.Mahocphan;
            objToFix.Makhoa = model.Makhoa;
            var fileName = "";
            if (model.ImageFile != null)
            {
                deleteFile(objToFix.FileUp);
                fileName = saveFile(model.ImageFile);
                objToFix.FileUp = fileName;

            }
            
            await _context.SaveChangesAsync();
            return "Sửa thành công" + fileName;
        }

        public async Task<List<FilesUpModelsEnough>> get(string ma)
        {
            return await _context.FilesUps
                            .Where( file => file.Idgiaovien == ma )
                            .Select( files =>  new FilesUpModelsEnough
                            {
                                Id = files.Id,
                                giaovien = _context.Taikhoans
                                            .Where( tk => tk.Username == files.Idgiaovien )
                                            .Select( tk => tk.Tendangnhap)
                                            .FirstOrDefault(),
                                Khoa = files.MakhoaNavigation.Ten,
                                Khoa_DK = files.IdkhoaDkNavigation.Loai,
                                Lop = files.LopNavigation.Ten, 
                                Mon = files.MahocphanNavigation.Tenhocphan,
                                FilesUp = files.FileUp,
                                thoigian = files.Thoigian,

                            })
                            .ToListAsync();
                            
        }

        public async Task<string> post(FilesUpModel model)
        {
            // Lưu tệp vào thư mục uploads
            var fileName = saveFile(model.ImageFile);
            //Lưu thông tin vào cơ sở dữ liệu
            var fileRecord = new FilesUp
            {
                Makhoa = model.Makhoa,
                IdkhoaDk = model.IdkhoaDk,
                Lop = model.Lop,
                Mahocphan = model.Mahocphan,
                Thoigian = model.Thoigian,
                FileUp = fileName,
                Idgiaovien = model.Idgiaovien
            };

            _context.FilesUps.Add(fileRecord);
            await _context.SaveChangesAsync();
            return "File uploaded successfully";
        }
        private string saveFile(IFormFile file)
        {
            var uploadFolders = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadFolders, uniqueFileName);
            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return uniqueFileName;
        }
        private void deleteFile(string file)
        {
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
        }
    }
}
