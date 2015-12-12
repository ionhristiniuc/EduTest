using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EduTestContract.Models;
using EduTestData.Model;

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

        public async Task<int> AddChapter(int moduleId, ChapterModel chapterModel)
        {
            using (var dbContext = new EduTestEntities())
            {
                var chapter = new Chapter()
                {
                    Name = chapterModel.Name,
                    ModuleId = moduleId
                };
                dbContext.Chapters.Add(chapter);

                if (await dbContext.SaveChangesAsync() == 0)
                    throw new Exception("ChapterRepository.AddChapter: Could not add user to db");

                return chapter.Id;
            }   
        }

        public Task UpdateChapter(int id, ChapterModel chapter)
        {
            throw new NotImplementedException();
        }

        public async void RemoveChapter(int id)
        {
            using (var dbContext = new EduTestEntities())
            {
                var chapter = await dbContext.Chapters.FirstOrDefaultAsync(u => u.Id == id);

                if (chapter == null)
                    throw new ObjectNotFoundException("ChaptersRepository.RemoveChapter: Chapter not found");

                dbContext.Chapters.Remove(chapter);
                if (await dbContext.SaveChangesAsync() == 0)
                    throw new Exception("ChaptersRepository.RemoveChapter: Could not remove chapter from db");
            }
        }

        public int GetCourseId(int chapterId)
        {
            using (var dbContext = new EduTestEntities())
            {
                return dbContext.Chapters.First(c => c.Id == chapterId).Module.CourseId;
            }
        }

        public async Task<bool> ExistsChapter(int id)
        {
            using (var dbContext = new EduTestEntities())
            {
                return await dbContext.Chapters.AnyAsync(u => u.Id == id);
            }
        }
    }
}