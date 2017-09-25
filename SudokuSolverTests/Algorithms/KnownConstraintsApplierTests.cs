/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 25.09.2017
 * Time: 10:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.Framework;
using SudokuSolver;
using SudokuSolver.Algorithms;
using System.Linq;

namespace SudokuSolverTests.Algorithms
{
	[TestFixture]
	public class KnownConstraintsApplierTests
	{
		[Test]
		public void Apply_RemovesRowCellAndSquareConstraints()
		{
			var m = new Matrix();
			m[1,2].CollapseTo(5);
			
			KnownConstraintsApplier.Apply(m);

			SquareArray.Sudoku.IterateTwo((row, col) =>
			                              {
			                              	if(row<3 && col<3 || row == 1 || col == 2)
			                              	{
			                              		if( !(row==1 && col==2))
			                              			Assert.IsTrue(m[row, col].GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 6, 7, 8, 9}), 
			                              			              String.Format("row: {0}, col: {1}, validStates:{2}", row, col, String.Join("", m[row, col].GetValidStates())));
			                              	}
			                              	else
			                              		Assert.IsTrue(m[row, col].GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9}), 
			                              			              String.Format("row: {0}, col: {1}, validStates:{2}", row, col, String.Join("", m[row, col].GetValidStates())));
			                              });

		}
	}
}
