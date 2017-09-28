/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 25.09.2017
 * Time: 11:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;


namespace SudokuSolver.Algorithms
{
	/// <summary>
	/// Description of BackTracking.
	/// </summary>
	public static class StupidBackTrackingWithTPL
	{
		/// <summary>
		/// Applies backtracking until solution is found or figured out that matrix is invalid
		/// </summary>
		/// <returns>returns Matrix if it was solved, otherwise null</returns>
		public static Matrix ApplyAll(Matrix m, CancellationTokenSource cts=null, string context = "")
		{
			Debug.WriteLine(context);
			
			if(cts == null)
				cts = new CancellationTokenSource();
						
			Matrix result = null;
			
			var pivot = SelectPivot(m);			
			var pivotPossibleValues = m[pivot.Item1, pivot.Item2].GetValidStates();
						
			Parallel.ForEach(pivotPossibleValues, possibleValueOfPivot => 
			{		
				if(cts.IsCancellationRequested)
					return;
				
				var copy = new Matrix(m);
				copy[pivot.Item1, pivot.Item2].CollapseTo(possibleValueOfPivot);
				
				var state = copy.GetState();
				if(state == Matrix.StateEnum.Invalid)
					return;
				if(state == Matrix.StateEnum.Solved)
				{					
					result = copy;
					cts.Cancel();
					return;
				} 
								
				var newContext = context + String.Format("Pivot={0},{1}; TestValue={2} | ", pivot.Item1, pivot.Item2, possibleValueOfPivot);
				Matrix tempResult = ApplyAll(copy, cts, newContext);
				if(tempResult != null)
				{					
					result = tempResult;
					cts.Cancel();					
				}
			});
			
			return result;
		}

		static Tuple<int, int> SelectPivot(Matrix m)
		{
			for(int i = 0; i<Matrix.MAX; i++)
				for(int j = 0; j < Matrix.MAX; j++)
					if (m[i, j].GetValidStates().Skip(1).Any())
						return new Tuple<int,int> (i, j);			
			
			throw new Exception("Matrix is solved or in invalid state.");
		}
	}
}
