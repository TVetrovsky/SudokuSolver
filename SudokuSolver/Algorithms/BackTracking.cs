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

namespace SudokuSolver.Algorithms
{
	/// <summary>
	/// Description of BackTracking.
	/// </summary>
	public static class BackTracking
	{
		public static bool ApplyAll(ref Matrix m, string context = "")
		{
			Console.WriteLine(context);
			
			if(!KnownConstraintsApplier.ApplyAll(m))
				return false; // during application of constraint we got invalid matrix
			
			if(m.IsSolved)
				return true;
			
			var pivot = SelectPivot(m);			
			var possibleValues = m[pivot.Item1, pivot.Item2].GetValidStates();
			
			foreach(var possibleValue in possibleValues)
			{
				var copy = new Matrix(m);
				copy[pivot.Item1, pivot.Item2].CollapseTo(possibleValue);
				var newContext = context + String.Format("Pivot={0},{1}; TestValue={2} | ", pivot.Item1, pivot.Item2, possibleValue);
				if(ApplyAll(ref copy, newContext))
				{
					m = copy;
					return true;
				}
			}
			
			return false;
		}

		static Tuple<int, int> SelectPivot(Matrix m)
		{
			var min = m[0,0].GetValidStates().ToArray();
			int r = 0, c = 0;
			SquareArray.Sudoku.IterateTwo(
				(i, j) =>
				{
					var cur = m[i, j].GetValidStates().ToArray();					
					if(cur.Length > 1 && cur.Length <= min.Length)
					{
						min = cur;
						r = i;
						c = j;
					}
				});
			
			return new Tuple<int,int> (r, c);
		}
	}
}
