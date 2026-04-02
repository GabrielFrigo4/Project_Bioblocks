using System.Collections.Generic;
using QuestionSystem;

namespace QuestionSystem
{
    public interface IQuestionDatabase
    {
        bool IsDatabaseInDevelopment();
        List<Question> GetQuestions();
        QuestionSet GetQuestionSetType();
        string GetDatabankName();
        string GetDisplayName();
    }
}


