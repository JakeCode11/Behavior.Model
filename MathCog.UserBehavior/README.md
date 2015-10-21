MathCog.UserBehavior is the front-end of both NLP module + Cognitive Reasoning Module.

Any UI components should use this as the back-end API.

In tutoring mode, if there is no user behavior data, the system should 
prompt the user to change to demonstration mode.

In the front-end, if there is no behavior data, convert to demonstration
mode automatically.

=======================================================================

Drag Input:

All drag input are knowledge input both in the sketch and drag mode. 

In tutoring mode, the drag input of query fire the tutoring session. 
Otherwise, the system does not start to tutor students.

========================================================================

Sketch Input:

In demonstration mode(worked example), the input will be separted as two 
groups: knowledge input and user input. The mode switch happens when user
queried one knowledge output. After such query, all input will be treated
as user inputs.

In tutoring mode, all the input are user input.

========================================================================

Interaction Simulation Test Cases