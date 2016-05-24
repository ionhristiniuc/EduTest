using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EduTestContract.Models;
using EduTestData.Model;

namespace EduTestService.Repositories
{
    public class QuestionsRepository : IQuestionsRepository
    {
        public Task<IEnumerable<QuestionBaseModel>> GetQuestions(int page, int perPage)
        {
            using (var dbModel = new EduTestEntities())
            {
                throw new NotImplementedException();
            }
        }

        public Task<QuestionBaseModel> GetQuestion(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddQuestion(QuestionBaseModel question)
        {
            using (var dbModel = new EduTestEntities())
            {
                throw new NotImplementedException();
                //var dbQuestion = Mapper.Map<QuestionBase>()
            }
        }
    }
}
