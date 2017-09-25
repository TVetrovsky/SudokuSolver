/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 24.09.2017
 * Time: 22:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace SudokuSolver
{
	/// <summary>
	/// Description of SolutionExecutor.
	/// </summary>
	public class SolutionExecutor
	{
		Matrix m;
		
		public SolutionExecutor(Matrix m)
		{
			this.m = m;			
		}
		
		public void Execute(Action<Matrix> solver)
		{
			bool change = true;
			m.ChangeObserver = () => change = true;
			
			while(change)
			{
				change = false;
				solver(m);
			}
		}
	}
}
