/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 24.09.2017
 * Time: 19:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.Framework;
using SudokuSolver;
using System.Linq;

namespace SudokuSolverTests
{
	[TestFixture]
	public class StateSpaceTests
	{
	
		[Test]
		public void GetValidStates_DefaultContainsAllStates()
		{
			var sp = new StateSpace();
			
			Assert.IsTrue(sp.GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9}));
		}
		
		[Test]
		public void SetInvalid_RemovesValidState1()
		{
			var sp = new StateSpace();
			sp.SetInvalid(1);
			
			Assert.IsTrue(sp.GetValidStates().SequenceEqual(new int[]{2, 3, 4, 5, 6, 7, 8, 9}));
		}

		[Test]
		public void SetInvalid_RemovesValidState9()
		{
			var sp = new StateSpace();
			sp.SetInvalid(9);
			
			Assert.IsTrue(sp.GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 5, 6, 7, 8}));
		}
		
		[Test]
		[TestCase(-1)]
		[TestCase(0)]
		[TestCase(10)]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Collapse_Throws_WithWrongValue(int val)
		{
			var sp = new StateSpace();
			
			sp.CollapseTo(val);
		}
	}
}
