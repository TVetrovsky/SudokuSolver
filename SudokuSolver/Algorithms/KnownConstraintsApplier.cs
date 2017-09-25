/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 24.09.2017
 * Time: 17:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Algorithms
{
	/// <summary>
	/// Applies constraints create by already known definite values in rows, columns and the sub-square(3x3)
	/// </summary>
	public static class KnownConstraintsApplier
	{		
		public static void Apply(Matrix m)
		{			
			KnownConstraintsGenericApplier.Apply(m, 
				(matrix, row, col) => Matrix.CellSelectors.SelectRowCells(matrix, row)
						.Union(Matrix.CellSelectors.SelectColCells(matrix, col))
						.Union(Matrix.CellSelectors.SelectSquareCells(matrix, row, col))
				);
		}		
	}
}
