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

namespace SudokuSolver.Algorithms
{
	/// <summary>
	/// Description of BackTracking.
	/// </summary>
	public static class StupidBackTracking
	{
		/// <summary>
		/// Applies backtracking until solution is found or figured out that matrix is invalid
		/// </summary>
		/// <returns>true=solved, otherwise false</returns>
		public static bool ApplyAll(ref Matrix m, string context = "")
		{
			Debug.WriteLine(context);
			
			var pivot = SelectPivot(m);			
			var pivotPossibleValues = m[pivot.Item1, pivot.Item2].GetValidStates();
			
			foreach(var possibleValueOfPivot in pivotPossibleValues)
			{
				var copy = new Matrix(m);
				copy[pivot.Item1, pivot.Item2].CollapseTo(possibleValueOfPivot);
				
				var state = copy.GetState();
				if(state==Matrix.StateEnum.Solved)
				{
					m = copy;
					return true;
				}
				else if( state == Matrix.StateEnum.Invalid)
					continue;
								
				var newContext = context + String.Format("Pivot={0},{1}; TestValue={2} | ", pivot.Item1, pivot.Item2, possibleValueOfPivot);
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
			for(int i = 0; i<Matrix.MAX; i++)
				for(int j = 0; j < Matrix.MAX; j++)
					if (m[i, j].GetValidStates().Skip(1).Any())
						return new Tuple<int,int> (i, j);			
			
			throw new Exception("Matrix is solved or in invalid state.");
		}
	}
}
