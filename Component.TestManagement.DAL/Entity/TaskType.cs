using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestManagement.DAL.Entity
{
    public enum TaskType
    {
        MultipleChoice = 1,
        OpenBrackets,
        General
    }

    public enum AnswerType
    {
        Open,
        Quiz,
        MultipleQuiz,
    }
}
