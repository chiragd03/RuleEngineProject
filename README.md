# RuleEngineProject
### This project was made in .NET Core 2.0 , thus making this platform independent.For the sake of simplicity , this project was written as a console application.

```
1. Briefly describe the conceptual approach you chose! What are the trade-offs?
```
   - Two threads run parallelly , one reading the rules text file and ther reading the json file
   - Then evaluation is done by picking one rule at a time and finding the corresponding input signal which does not satisfy that rule
   - Displaying the signals that fail the validation
   
   Trade Offs:
   - The rules file should be in a specific format where every single rule can contain at most three sections : 
       > [Operand] [Operator] [Value]
       ```
       eg: ATL1 NotMoreThan 240.00
           ATL2 NotEqual LOW 
           ATL3 NotInFuture
       ```
       where [Operator] can belong only to a set of predefined enum values : in this case 
       ```
        MoreThan,
        NotMoreThan,
        LessThan,
        NotLessThan,
        NotEqual,
        Equal,
        NotInFuture,
        NotInPast
      ```
```
2. What's the runtime performance? What is the complexity? Where are the bottlenecks? 
```
   - Complexity used for this code is : O(N*N*N) 
```
3. If you had more time, what improvements would you make, and in what order of priority?
```
   - Improve the lookup complexity for the signals
