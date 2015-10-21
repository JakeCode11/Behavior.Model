/*******************************************************************************
 * Math Interactive Tutoring System
 * <p>
 * Copyright (C) 2015 Bo Kang, Joseph J. LaViola Jr.
 * <p>
 * This program is free software; you can redistribute it and/or modify it under
 * the terms of the GNU General Public License as published by the Free Software
 * Foundation; either version 2 of the License, or any later version.
 * <p>
 * This program is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 * FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
 * details.
 * <p>
 * You should have received a copy of the GNU General Public License along with
 * this program; if not, write to the Free Software Foundation, Inc., 51
 * Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
 ******************************************************************************/

namespace MathCog.UserModeling
{
    public class AGKnowledgeHints
    {
        #region Knowledge and Knowledge Property Hint

        public const string LineGeneralFormHint = "The general form of a line is Ax + By + C = 0.";
        public const string LineCoefficientHint = "As the general form of a line is Ax + By + C = 0, you can get three coefficients respectively.";
        public const string LineSlopeHint = "For a general form line Ax + By + c = 0, its slope is m = -A/B";

        public const string FindlineYInterceptPoint = "Find the Y-Intercept of the line.";

        public const string FitYInterceptIntoPointSlopeForm =
            "Substitute the value of Y-Intercept Point into point-slope form.";

        public const string LineYInterceptHints = "For a general form line Ax + By + c = 0, its Y intercept is I = -C/B";

        public const string CircleStandardFormHint = "The standard form of a circle is (x-a)^2 + (y-b)^2 = c^2.";
        public const string CircleRadiusHint = "For a standard form circle (x-a)^2 + (y-b)^2 = c^2, its radius is c.";
        public const string CircleCentralPtHint = "For a standard form circle (x-a)^2 + (y-b)^2 = c^2, its center is CP(a,b).";

        public const string EllipseStandarFormHint = "The standard form of a ellipse is (x-x0)/a^2 + (y-y0)/b^2 = 1";
        public const string EllipseCenterHint = "For a standard form ellipse  (x-x0)/a^2 + (y-y0)/b^2 = 1, its center is CP(a,b).";
        public const string EllipseRadiusHint = "For a standard form ellipse  (x-x0)/a^2 + (y-y0)/b^2 = 1, its semimajor is a, semiminor is b";
        public const string EllipseFociHint = "For a standard form ellipse (x-x0)/a^2 + (y-y0)/b^2 = 1, its foci is c^2 = a^2 - b^2";
        public const string EllipseFociPoint = "For a standard form ellipse with foci c, FP1 is (-c+a,b), FP2 is (c+a,b)";

        public const string ImplicitLineHint = "Implicit Line Form: aX + bY + c = 0";
        public const string ExplicitLineHint = "Explicit Line Form: Y = aX + b";
        public const string ParametricLineHint = "Parametric Line Form: X = x0 + a * T, Y = y0 + b * T";

        public const string ImplicitCircleHint = "Implicit Circle Form: (X-a)^2 + (Y-b)^2 = r^2";
        public const string ParametricCircleHint = "Parametric Circle Form: X = r * cos(t), Y = r * sin(t)";

        public const string ImplicitEllipseHint = "Implicit Ellipse Form: (X-h)^2/a^2 + (Y-k)^2/b^2 = 1";
        public const string ParametricEllipseHint = "Parametric Ellipse Form: X = a * cos(t) + h, Y = b * sin(t) + k";

        #endregion

        #region Algebra Manipulation Hint

        public const string FindIntercepts = "Find X-Intercept and Y-Intercept from Line Standard form.";

        public const string MoveTermsFromRightToLeft = "Move Terms from the Right Of Equatin to the Left.";
        public const string CommutativeLaw = "Find and Group Terms with the different Coefficients and same variable.";
        public const string MergeLaw = "Merge Terms with different Coefficients and same variable.";

        public const string FactorCircle0 = "Normalize Circle's Coefficients.";
        public const string FactorizeCircle1 = "Prepare to add the needed value to create the perfect square trinomial.";
        public const string FactorizeCircle2 = "Merge Constant Terms.";
        public const string FactorizeCircle3 = "Factor the perfect square trinomial.";
        public const string FactorizeCircle4 = "Move Constant Coefficient to the right of equation.";
        public const string FactorizeCircle5 = "Take the square root of each side and solve.";

        public const string FactorizeEllipse1 = "Normalize the Ellipse Constant Coefficient.";
        public const string FactorizeEllipse2 = "Calculate the Ellipse Constant Coefficient.";

        #endregion
    }

    public class AGStrategyHint
    {
        public const string InputTextDrag = "You can drag this word onto the below working space.";
        public const string ConceptQuestionDrag =
            "Drag this word onto the below working space to show the solving strategy.";

        //problem 1, question 1
        public const string QueryLineStandardFormStrategyFromGeometry = "The general of line form is ax+by+c=0.";
        //problem 1, question 2
        public const string LineSlopeStrategy = "The slope of line Ax + By + C = 0 is – A/B";


        public const string DistanceBetweenTwoPoints = "The distance bwtween two points (x0,y0) and (x1,y1) is " +
                                                       "\n d^2 = (x0-x1)^2 + (y0-y1)^2";

        //Problem 4
        public const string DistanceBetweenPointAndLine =
            "The distance between a point (X0,Y0) and a line ax+by+c = 0 is" +
            "\n (|aX0 + bY0 + c| divide by Math.Sqrt(a^2 + b^2) )";



        public const string QueryLinePointSlopeFormStrategy = "A line with the point-slope form is like Y-Y0 = m(X-X0), " +
                                                              "\nwhere m is the slope, and (X0,Y0) is a point on the line.";
        public const string AlgebraicTransformation = "Transform this non-general form equation to a general form equation";

    }

    public class AGAppliedRule
    {
        public const string ConceptUnderstanding = "Extract Properties From Math Concepts.";
        public const string LineSlopeFromABC = "Get Line Slope from A, B, C of general line form.";
        public const string CalculateYIntercept = "Calculate the Y-Intercept of a line.";
        public const string FitYInterceptIntoPointSlopeForm = "Substitute Y-Intercept value into the Point-Slope form.";
        public const string AlgebraSubsitituion = "Algebra Substitution";
        public const string AlgebraMovingTerms = "Move Algebraic Terms";
        public const string AlgebraCommutativeLaw = "Apply Commutative Law";
        public const string AlgebraMergeLikeTerms = "Combine Like Terms";
        public const string AlgebraElimination = "Algebra Elimination";
        public const string FitABCToGeneralForm = "Substitute A,B,C into the line general form.";

        public const string FindIntercepts = "Let y=0 and x=0 respectively.";

    }

    public class AGWarning
    {
        public const string NoSelectUserInput = "You cannot select your own solving step to verify it in the current demonstration mode. Change to tutor mode instead.";

        public const string QueryFailed = "The system does not understand your input. Please input problem first or provide the system more your intention.";
        public const string DeleteInputKnowledge = "You have deleted a input.";
        public const string AnswerShowed = "AnalyticalInk shows you the answer for the current question.";

        public const string NoSelection = "You do not select a knowledge to ask. Select a knowledge by circle it.";

        public const string QueryProblem = "You currently ask a given problem, attemp to ask the question instead.";

        public const string DragKnowledge = "You already drag this content onto the sketch working space.";

        public const string DragFailedOnGeo1 = "You cannot drag this non-shape knowledge onto the geometry canvas. Instead drag it onto the algebraic canvas.";

        public const string DragFailedOnGeo2 =
            "You cannot drag this shape pattern onto the geometry canvas, you can drag it onto the algebraic side";

        public const string FailedToAnswer =
            "The system failed to solve this question due to lack of input or mismatch between input and question.";

        public const string NoContentOnWhat = "You need to draw a question mark upon or near the expression to ask what it is.";
        public const string NoContentOnWhy = "You need to select a problem or a question to ask why.";
        public const string NoContentOnHow = "You need to select a problem or a question to ask how to do it.";
        public const string NoContentOnHowNext = "You need to draw the down arrow gesture under one expression.";

        public const string DownArrowUnderStrategy = "AnalyticalInk gives you the strategy to solve this problem. " +
                                                     "\nIf you know how to solve problem, further write next step below the strategy" +
                                                     "\nIf not, write a down-arrow below the strategy, AnalyticalInk will help you.";

        //public const string SelectQuestion = "What is the question? Input the question.";       
        public const string NoQueryOnTheKnowledge = "Your query question does not fit any problem.";

        public const string InvalidAskContent = "Your asking is neither the problem nor the question.";

        public const string FurtherDownArrowHint = "You can further draw a down arrow to let the system continue to help you.";
        public const string DoHowBeforeDownArrorwHint = "Circle-question this problem or question before letting system do derivation.";

        public const string NoProblems = "Few problem is detected. Input the problem first.";
    }

    public class AGDemonstrationMessage
    {
        //Step 1: Knowledge Input
        public const string InputKnowledge = "You input a knowledge into the system. If it is not the same as input, you can query it by circle it.";

        public const string InputQuery =
            "You input a question to ask the system. The system solved it. You can query the corresponding answer by circle it";

        //Step2: Answer Selection
        public const string SelectAnswer = "You have selected the answer. You can further query its intermediate steps by drawing question-mark on the canvas.";

        //Step3: Answer Query
        public const string QueryAnswer = "You query the intermediate step to reach that answer. Further derive the next step by using the hint control.";

        //Step4: Answer Query Step
        public const string QueryIntermediate = "You queried another step. ";

        //Step5: Answer Query End
        public const string QueryEnd = "You reach the answer for this question. No Further Hint Available.";

        public const string QueryEndByTutor = "You reach the answer for this question. No Further Hint Available.";

        public const string DeSelectAnswer = "You have de-selected the answer.";

        public const string CloseReasoning = "You close the reasoning process to get this answer.";

        public const string CloseAStep = "You close a derived step.";
    }

    public class AGTutorMessage
    {
        //Step 1: Knowledge Input
        public const string InputKnowledge = "You input a knowledge into the system. If this is the question, you can circle this question to enter the tutor session.";

        public const string InputQuery = "You input a question to ask the system. The system provides you the strategy to solve it. You are currently in the tutor session!";
       
        //Step 3: Intermediate Step Hint Query
        public const string QueryIntermediate  = "You ask for a hint toward the system. You can ask for this step answer or write your own.";

        //Step 4.1: Intermediate Step Answer Query
        public const string GetIntermediateAnswer = "You get this current step answer. You can further ask for a hint or write your own solving step into the system.";

        //Step 4.2: Intermediate Step Writing
        public const string WriteAStep = "You write your own solving step into the system. You can continue solving it. " +
                                         "Or you can verify your solving steps by drawing a question-mark. \n" +
                                         "Or you can drag words from the problem or click next button on the hint control to ask for the next step help.";

        public const string DeleteOwnStep = "You delete your own solving step.";

        //Step 4.2.1 Step Verification
        public const string SelectWritingStep = "First verify it and then select it if your step is wrong.";
        public const string DeSelectWritingStep = "You deselect your own solving step.";

        public const string DeSelectQuestion = "You have deselected a question, and left tutor session!";

        //Step 4.2.2 Step Verification Result 
        public const string SolvedProblem = "Congratulations!! You solved this problem.";

        public const string SolvingPartialProblem =
            "You have found out one answer for this problem; Other answers need to be solved or further questions need to be answered in this problem.";

        public const string VerifyCorrect = "You are on the right track to solve this problem.";

        public const string VerifyWrong = "You are on the wrong track to solve this problem."; //Current system cannot diagnose why your step is wrong.";

        public const string Verify_PatternNotRecognized = "The system failed to match your step as a valid knowledge to solve this question.";
    }

    public static class AGGeometry
    {
        public const string SelectFigure = "You have selected a figure on the geometry canvas. You can further label it.";
        public const string DeSelectFigure = "You have de-selected a figure on the geometry canvas";
    }
}
