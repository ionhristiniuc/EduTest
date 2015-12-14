using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTestContract.Models
{
    public class VariantQuestionModel : QuestionBaseModel
    {
        public IEnumerable<VariantModel> Variants { get; set; }
    }
}
