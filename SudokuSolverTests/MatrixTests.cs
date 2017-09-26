/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 24.09.2017
 * Time: 16:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.Framework;
using System.Linq;
using SudokuSolver;

namespace SudokuSolverTests
{
	[TestFixture]
	public class MatrixTests
	{
		#region Get/Set cells
		[Test]
		[TestCase(0,0, 1)]
		[TestCase(8,8, 9)]
		[TestCase(8,8, 5)]
		public void GetSetPoint_Works(int x, int y, int val)
		{
			var m = new Matrix();
			
			m[x,y].CollapseTo(val);
			
			Assert.IsTrue(m[x, y].GetValidStates().SequenceEqual(new int[] {val}));
		}
		
		[Test]
		[TestCase(-1, 0)]
		[TestCase(0, -1)]
		[TestCase(9,8)]
		[TestCase(8,9)]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void GetSetPoint_Throws_WithWrongIndex(int x, int y)
		{
			var m = new Matrix();
			
			m[x,y].CollapseTo(5);
		}
		#endregion

		#region Observer calls
		[Test]
		public void Colapse_CallsObserver()
		{
			bool observerCalled = false;
			Matrix m = new Matrix();
			m.ChangeObserver = () => observerCalled = true;
			
			m[0,0].CollapseTo(1);
			
			Assert.IsTrue(observerCalled);
		}

		[Test]
		public void Colapse_DoesNotCallObserverIfNoChange()
		{
			bool observerCalled = false;
			Matrix m = new Matrix();
			m[0,0].CollapseTo(1);		
			
			m.ChangeObserver = () => observerCalled = true;
			
			// check observer is not called 
			m[0,0].CollapseTo(1);
			
			Assert.IsFalse(observerCalled);
		}		
		#endregion
		
		#region IsSolved
		[Test]
		public void StateIsValid_WhenCellIsUncertain()
		{
			var m = new Matrix();
			
			Assert.AreEqual(Matrix.StateEnum.Valid, m.GetState());
		}

		[Test]
		public void StateIsSolved_WithValidSudokuSolution()
		{
			var sudoku = new int[,]{
				{2,4,8,3,9,5,7,1,6},
				{5,7,1,6,2,8,3,4,9},
				{9,3,6,7,4,1,5,8,2},
				{6,8,2,5,3,9,1,7,4},
				{3,5,9,1,7,4,6,2,8},
				{7,1,4,8,6,2,9,5,3},
				{8,6,3,4,1,7,2,9,5},
				{1,9,5,2,8,6,4,3,7},
				{4,2,7,9,5,3,8,6,1}
			};

			var m = new Matrix(sudoku);
			
			Assert.AreEqual(Matrix.StateEnum.Solved, m.GetState());
		}		
		
		[Test]
		public void StateIsInvalid_WithInvalid1()
		{
			var sudoku = new int[,]{
				{2,2,8,3,9,5,7,1,6},//here 2
				{5,7,1,6,2,8,3,4,9},
				{9,3,6,7,4,1,5,8,2},
				{6,8,2,5,3,9,1,7,4},
				{3,5,9,1,7,4,6,2,8},
				{7,1,4,8,6,2,9,5,3},
				{8,6,3,4,1,7,2,9,5},
				{1,9,5,2,8,6,4,3,7},
				{4,2,7,9,5,3,8,6,1}
			};

			var m = new Matrix(sudoku);
			
			Assert.AreEqual(Matrix.StateEnum.Invalid, m.GetState());
		}		


		[Test]
		public void StateIsInvalid_WithInvalid2()
		{
			var sudoku = new int[,]{
				{2,4,8,3,9,5,7,1,6},
				{2,7,1,6,2,8,3,4,9},//here 2
				{9,3,6,7,4,1,5,8,2},
				{6,8,2,5,3,9,1,7,4},
				{3,5,9,1,7,4,6,2,8},
				{7,1,4,8,6,2,9,5,3},
				{8,6,3,4,1,7,2,9,5},
				{1,9,5,2,8,6,4,3,7},
				{4,2,7,9,5,3,8,6,1}
			};

			var m = new Matrix(sudoku);
			
			Assert.AreEqual(Matrix.StateEnum.Invalid, m.GetState());
		}
		#endregion
		
	}
}
