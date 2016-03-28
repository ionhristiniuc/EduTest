using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestClient.Services
{
    public interface IQuestionsService
    {
        Task<bool> AddQuestion(int topicId, QuestionBaseModel question);
        Task<bool> DeleteQuestion(int id);
    }
}
