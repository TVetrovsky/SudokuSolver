/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 24.09.2017
 * Time: 17:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;

namespace SudokuSolver.Algorithms
{
	/// <summary>
	/// Applies constraints create by already known definite values in rows, columns and the sub-square(3x3)
	/// </summary>
	public static class KnownConstraintsApplier
	{		
		/// <summary>
		/// Applies one walkthroug of constrints 
		/// </summary>
		public static void Apply(Matrix m)
		{			
			KnownConstraintsGenericApplier.Apply(m, 
				(matrix, row, col) => Matrix.CellSelectors.SelectRowCells(matrix, row)
						.Union(Matrix.CellSelectors.SelectColCells(matrix, col))
						.Union(Matrix.CellSelectors.SelectSquareCells(matrix, row, col))
				);
		}		
		
		/// <summary>
		/// Applies constrints until solution is invalid or all constraints were applied 
		/// </summary>
		/// <returns>true - matrix is still valid (=solved or not yet solved)</returns>
		public static bool ApplyAll(Matrix m)
		{			
			bool change = true;
			bool valid = true;
			m.ChangeObserver = () => change = true;
			m.InvalidStateObserver = () => valid = false;
			int i = 0;
			
			while(change && valid)
			{
				change = false;
				Apply(m);
				i++;
			}
			
			if(!valid) // unable to solve
				return false; 
			
			return true;						
		}
	}
}
