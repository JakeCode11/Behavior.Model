namespace GeometryConcept
{
    using System.Collections.Generic;

    /// <summary>
    /// Concept Modeling
    /// </summary>
    public class ConceptCrawler
    {
        #region Singleton

        private static ConceptCrawler _instance;

        public static ConceptCrawler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConceptCrawler();
                }
                return _instance;
            }
        }

        private ConceptCrawler()
        {
            dict = new Dictionary<string, string>();
            InitDictionary();
        }

        private Dictionary<string, string> dict;

        private void InitDictionary()
        {
            dict.Add(Concept_YCoord, Explain_YCoord);
            dict.Add(Concept_LineSlope, Explain_LineSlope);
            dict.Add(Concept_LineStandardForm, Explain_LineStandardForm);
            dict.Add(Concept_LineIntercept, Explain_LineIntercept);
            dict.Add(Concept_LineSlopeInterceptForm, Explain_LineSlopeInterceptForm);

            dict.Add(Concept_Distance, Explain_Distance);
            dict.Add(Concept_Perpendicular, Explain_Perpendicular);
            dict.Add(Concept_Parallel, Explain_Parallel);
            dict.Add(Concept_Midpoint, Explain_MidePoint);
        }

        #endregion

        public object RetrieveConceptExplain(string concept)
        {
            if (dict.ContainsKey(concept)) return dict[concept];
            return null;
        }

        #region Unary Concept

        public static string Concept_YCoord = "y-coordinate";
        public static string Explain_YCoord = "For a point (x,y), the y-coordinate for this point is y.";


        public static string Concept_LineSlope = "Line Slope";
        public static string Explain_LineSlope = "The slope of a line is a number that measures its steepness, usually denoted by the letter m.\nIt is the change in y for a unit change in x along the line.";

        public static string Concept_LineIntercept = "Line y-intercept";
        public static string Explain_LineIntercept = "The y-intercept of this line is the value of y at the point where the line crosses the y axis.\nIt is usually denoted by the letter k.";

        public static string Concept_LineStandardForm = "Line General Form";
        public static string Explain_LineStandardForm = "The general algebraic form of a line is ax+by+c=0, where a>0.";

        public static string Concept_LineSlopeInterceptForm = "Line Slope Intercept Form";
        public static string Explain_LineSlopeInterceptForm = "The slope intercept algebraic form of a line is y = mx + k,\nwhere m is the slope of the line, k is the y-intercept for the line.";

        public static string LineGeneralForm = "Ax+By+C=0";

        public static string LinePointSlopeForm = "y-y1=m(x-x1)";

        public static string LineSlopeInterceptForm = "y=mx+b";

        public static string ll =
            "The slope-intercept form of line equation is y = ax +b where a is the slope of the line and b is the intercept of the line."; 

        public static string LineStandardForm = "Ax+By=C";

        public static string LineSlope = "In the standard line form ax+by+c=0, slope is s = - a/b. To calculate Slope, substitute two points into the slope equation y1-y0/(x1-x0)";

        public static string LineIntercept = "";

        public static string CircleRadius = "";

        public static string CircleCentralPoint = "";

        public static string CircleStandform = "(x-a)^2+(y-b)^2=r^2";

        public static string CircleGeneralForm = "x^2+y^2+Dx+Ey+F=0 (D^2+E^2-4F>0)";

        #endregion

        #region Binary Relation Concept

        public static string Concept_Distance = "Distance";

        public static string Explain_Distance =
            "Distance is a numerical measurement of how far apart objects are. The Distance Formula is a variant of the Pythagorean Theorem that you used back in geometry.";

        //"Distance is a numerical description of how far apart two objects are. Using Pythagorean Theorem derives the distance formula between two points: D^2=(X1-X0)^2+(Y1-Y0)^2.";

        public static string Concept_Perpendicular = "Perpendicular";
        public static string Explain_Perpendicular = "When the two lines are perpendicular, the product of the slopes is -1.";

        public static string Concept_Parallel = "Parallel";
        public static string Explain_Parallel = "Lines are parallel if they are always the same distance apart, and will never meet.\nParallel lines have the same slope: m1=m2.";

        public static string Concept_Midpoint = "MidPoint";
        public static string Explain_MidePoint = "A point on a line segment that divides it into two equal parts.\nThe Midpoint Formula is ((x1+x2)/2,(y1+y2)/2)";

        public static string Bisector = "";

        public static string ClosestDistanceBetweenPointAndLine = "d = |Ax0+By0+C|/Math.Sqrt(A^2+B^2)";

        public static string Intersection = "";

        public static string Angle = "";

        #endregion
    }
}
