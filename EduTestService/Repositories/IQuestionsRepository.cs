using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestService.Repositories
{
    public interface IQuestionsRepository
    {        
        Task<IEnumerable<QuestionBaseModel>> GetQuestions(int page, int perPage);
        Task<QuestionBaseModel> GetQuestion(int id);
        Task<int> AddQuestion(QuestionBaseModel question);
    }
}
