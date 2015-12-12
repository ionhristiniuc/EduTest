using System.Collections.Generic;
using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestService.Repositories
{
    public interface IChaptersRepository
    {
        IEnumerable<ChapterModel> GetChapters(int moduleId);
        Task<ChapterModel> GetChapter(int id);
        Task<int> AddChapter(int moduleId, ChapterModel chapter);
        Task UpdateChapter(int id, ChapterModel chapter);
        void RemoveChapter(int id);
        int GetCourseId(int chapterId);
    }
}
