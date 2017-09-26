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
	public static class BackTracking
	{
		public static bool ApplyAll(ref Matrix m, string context = "")
		{
			Debug.WriteLine(context);
			
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
			for(int i = 0; i<Matrix.MAX; i++)
				for(int j = 0; j < Matrix.MAX; j++)
					if (m[i, j].GetValidStates().Skip(1).Any())
						return new Tuple<int,int> (i, j);			
			
			throw new Exception("Matrix is solved or in invalid state.");
		}
	}
}
