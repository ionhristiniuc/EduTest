using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestClient.Services
{
    public interface IChaptersService
    {
        Task<bool> AddChapter(int moduleId, ChapterModel chapter);
        Task<bool> DeleteChapter(int id);
    }
}
