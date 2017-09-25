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
	public class LineCheckerTests
	{
		[Test]
		public void CheckLines_RemovesByRow()
		{
			var m = new Matrix();
			m[1,2].ColapseTo(5);
			
			LineChecker.CheckLines(m);
			
			MatrixConsolePrinter.Print(m);
			
			SquareArray.Sudoku.IterateTwo((row, col) =>
			                              {
			                              	if(row != 1 && col != 2)
			                              		Assert.IsTrue(m[row, col].GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9}));
			                              	else
			                              		if(!(row == 1 && col==2))
			                              			Assert.IsTrue(m[row, col].GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 6, 7, 8, 9}));
			                              });
		}
	}
}
