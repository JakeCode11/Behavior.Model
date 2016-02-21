using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UserModeling.Test
{
    [TestFixture]
    public class APITest
    {
        [Test]
        public void Test1()
        {
            //Solve three problems: 1, 2, 5
            MathTutor.Instance.Select(1);
            MpTutorInterpreter mti = MathTutor.Instance.CurrentTutorInterp;
            Console.WriteLine(mti.CurrentProblemTutor.PL.CurrentProblem.OriginalProblem);
            Console.WriteLine();
            MathTutor.Instance.Select(2);
            mti = MathTutor.Instance.CurrentTutorInterp;
            Console.WriteLine(mti.CurrentProblemTutor.PL.CurrentProblem.OriginalProblem);
            Console.WriteLine();
            MathTutor.Instance.Select(5);
            mti = MathTutor.Instance.CurrentTutorInterp;
            Console.WriteLine(mti.CurrentProblemTutor.PL.CurrentProblem.OriginalProblem);
            Console.WriteLine();
            Assert.True(MathTutor.Instance.TutoredProblems.Count == 3);
        }
    }
}
