using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EduTestContract.Models;

namespace EduTestService.Repositories
{
    public class ChaptersRepository : IChaptersRepository
    {
        public IEnumerable<ChapterModel> GetChapters(int moduleId)
        {
            throw new NotImplementedException();
        }

        public Task<ChapterModel> GetChapter(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddChapter(int moduleId, ChapterModel chapter)
        {
            throw new NotImplementedException();
        }

        public Task UpdateChapter(int id, ChapterModel chapter)
        {
            throw new NotImplementedException();
        }

        public void RemoveChapter(int id)
        {
            throw new NotImplementedException();
        }

        public int GetCourseId(int chapterId)
        {
            throw new NotImplementedException();
        }
    }
}