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

		[Test]
		[TestCase(-1)]
		[TestCase(0)]
		[TestCase(10)]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void GetSetPoint_Throws_WithWrongValue(int val)
		{
			var m = new Matrix();
			
			m[0,0].CollapseTo(val);
		}

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
	}
}
