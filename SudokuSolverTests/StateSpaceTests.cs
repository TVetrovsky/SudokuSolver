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
			var sp = new StateSpace(null);
			
			Assert.IsTrue(sp.GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9}));
		}
		
		[Test]
		public void SetInvalid_RemovesValidState1()
		{
			var sp = new StateSpace(null);
			sp.SetInvalid(1);
			
			Assert.IsTrue(sp.GetValidStates().SequenceEqual(new int[]{2, 3, 4, 5, 6, 7, 8, 9}));
		}

		[Test]
		public void SetInvalid_RemovesValidState9()
		{
			var sp = new StateSpace(null);
			sp.SetInvalid(9);
			
			Assert.IsTrue(sp.GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 5, 6, 7, 8}));
		}
		
		[Test]
		public void Intersect_WithDefaultSpace_ReturnsJustIntersection()
		{
			var sp1 = new StateSpace(null);
			var sp2 = new StateSpace(null);			
			sp2.SetInvalid(5);
			sp2.SetInvalid(7);
			
			sp1.Intersect(sp2);
			
			Assert.IsTrue(sp1.GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 6, 8, 9}));
			Assert.IsFalse(sp1.GetValidStates().SequenceEqual(new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9}));
		}
	}
}
