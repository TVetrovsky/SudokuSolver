/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 24.09.2017
 * Time: 21:49
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
	public class KnownConstraintsGenericApplierTests
	{
		[Test]
		public void Apply_RemovesByRow()
		{
			var m = new Matrix();
			m[1,2].CollapseTo(5);
			
			KnownConstraintsGenericApplier.Apply(m, (mmm, row, col) => Matrix.CellSelectors.SelectRowCells(mmm, row));
						
			SquareArray.Sudoku.IterateTwo((row, col) =>
			                              {
			                              	if(row != 1)
			                              		Assert.IsTrue(m[row, col].GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9}));
			                              	else
			                              		if(col!=2)
			                              			Assert.IsTrue(m[row, col].GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 6, 7, 8, 9}));
			                              });
		}

		[Test]
		public void Apply_RemovesByCol()
		{
			var m = new Matrix();
			m[1,2].CollapseTo(5);
			
			KnownConstraintsGenericApplier.Apply(m, (mmm, row, col) => Matrix.CellSelectors.SelectColCells(mmm, col));
			
			SquareArray.Sudoku.IterateTwo((row, col) =>
			                              {
			                              	if(col != 2)
			                              		Assert.IsTrue(m[row, col].GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9}));
			                              	else
			                              		if(row != 1)
			                              			Assert.IsTrue(m[row, col].GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 6, 7, 8, 9}));
			                              });
		}

		[Test]
		public void Apply_RemovesBySubSquare()
		{
			var m = new Matrix();
			m[1,2].CollapseTo(5);
			
			KnownConstraintsGenericApplier.Apply(m, Matrix.CellSelectors.SelectSquareCells);
		
			SquareArray.Sudoku.IterateTwo((row, col) =>
			                              {
			                              	if(row<3 && col<3)
			                              	{
			                              		if( !(row==1 && col==2))
			                              			Assert.IsTrue(m[row, col].GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 6, 7, 8, 9}));
			                              	}
			                              	else
			                              		Assert.IsTrue(m[row, col].GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9}));
			                              });
		}
		
		
	}
}
