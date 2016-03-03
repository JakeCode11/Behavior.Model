using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathCog2.UserModeling;
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
            MathTutorEngine.Instance.Select(1);
            MpTutorInterpreter mti = MathTutorEngine.Instance.CurrentTutorInterp;
            Console.WriteLine(mti.CurrentProblemTutor.PL.CurrentProblem.OriginalProblem);
            Console.WriteLine();
            MathTutorEngine.Instance.Select(2);
            mti = MathTutorEngine.Instance.CurrentTutorInterp;
            Console.WriteLine(mti.CurrentProblemTutor.PL.CurrentProblem.OriginalProblem);
            Console.WriteLine();
            MathTutorEngine.Instance.Select(5);
            mti = MathTutorEngine.Instance.CurrentTutorInterp;
            Console.WriteLine(mti.CurrentProblemTutor.PL.CurrentProblem.OriginalProblem);
            Console.WriteLine();
            Assert.True(MathTutorEngine.Instance.TutoredProblems.Count == 3);
        }
    }
}
