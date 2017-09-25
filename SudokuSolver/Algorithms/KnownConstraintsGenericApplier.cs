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
	/// Decreases state space by already known constrains (definite cells)
	/// </summary>	
	public static class KnownConstraintsGenericApplier
	{	
		/// <summary>
		/// returns cells that form constraint on the reference cell (row, col)
		/// </summary>
		/// <returns>May contain also the refence cell itself</returns>
		public delegate IEnumerable<StateSpace> ConstraintCellsSelector(Matrix m, int row, int col);
		
		/// <summary>
		/// Applies constraints on all cells based on specific constraint cell selection mechanism
		/// </summary>
		/// <param name="constraintCellsSelector">returns cells that form constraint on the reference cell based on its coordinate in matrix</param>
		public static void Apply(Matrix m, ConstraintCellsSelector constraintCellsSelector )
		{
			SquareArray.Sudoku.
				IterateTwo((row, col) =>
				           {
				           		var restrictionCells = constraintCellsSelector(m, row, col);
                        		
                        		var currentCell = m[row, col];
                        		foreach(var testCell in restrictionCells)
                        		{
                        			if(testCell == currentCell)
                        				continue;
                        			if(testCell.GetValidStates().Count() == 1) //cell value is definite
										m[row, col].SetInvalid(testCell.GetValidStates().First());
                        		}	
				           });			
		}
		
	}
}
