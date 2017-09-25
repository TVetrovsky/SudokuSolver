/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 24.09.2017
 * Time: 23:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.Framework;
using SudokuSolver;

namespace SudokuSolverTests
{
	[TestFixture]
	public class SolutionExecutorTests
	{
		[Test]
		public void Execute_ExecuteTillChangesAreMade()
		{
			int i = 0;
			Matrix m = new Matrix();
			Action<Matrix> solver = (mmm) => {i++; if(i<5) mmm[i,0].ColapseTo(i);};
			var se = new SolutionExecutor(m);
			
			se.Execute(solver);
			
			Assert.AreEqual(5, i);
		}
	}
}
