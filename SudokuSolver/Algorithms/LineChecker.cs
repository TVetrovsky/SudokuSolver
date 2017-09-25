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
	/// Description of LineChecker.
	/// </summary>
	public static class LineChecker
	{		
		private static IEnumerable<StateSpace> SelectRowCells(Matrix m, int row)
		{
			return Enumerable.Range(0, Matrix.MAX).Select((i) => m[row, i]);			
		}
		
		private static IEnumerable<StateSpace> SelectColCells(Matrix m, int col)
		{
			return Enumerable.Range(0, Matrix.MAX).Select((i) => m[i, col]);
		}
		
		private static IEnumerable<StateSpace> SelectSquareCells(Matrix m, int row, int col)
		{
			int baseRow = row / 3;
			int baseCol = col / 3;
		
			baseRow *= 3;
			baseCol *= 3;
		
			return new StateSpace[]{
				m[baseRow, baseCol], m[baseRow, baseCol+1], m[baseRow, baseCol+2],  
				m[baseRow+1, baseCol], m[baseRow+1, baseCol+1], m[baseRow+1, baseCol+2],
				m[baseRow+2, baseCol], m[baseRow+2, baseCol+1], m[baseRow+2, baseCol+2]};			
		}


		
		public static void CheckLines(Matrix m)
		{
			SquareArray.Sudoku.
				IterateTwo((row, col) =>
				          SquareArray.Sudoku.
				          	IterateSingle(i =>
				                        {
				                        		var restrictionCells = SelectRowCells(m, row)
				                        			.Union(SelectColCells(m, col))
				                        			.Union(SelectSquareCells(m, row, col));
				                        		
				                        		var currentCell = m[row, col];
				                        		foreach(var testCell in restrictionCells)
				                        		{
				                        			if(testCell != currentCell && testCell.GetValidStates().Count() == 1)
														m[row, col].SetInvalid(testCell.GetValidStates().First());
				                        		}				                        									
										})
				         );			
		}
		
	}
}
