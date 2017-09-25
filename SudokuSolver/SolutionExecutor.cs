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
		
		/// <summary>
		/// Executes specified strategy until solved, no change or incorrect solution
		/// </summary>
		/// <param name="solver"></param>
		/// <returns>null - valid but not solved, true - solved, false - invalid solution</returns>
		public bool? Execute(Action<Matrix> solver)
		{
			bool change = true;
			bool valid = true;
			m.ChangeObserver = () => change = true;
			m.InvalidStateObserver = () => valid = false;
			
			while(change && valid)
			{
				change = false;
				solver(m);
			}
			
			if(!valid) // unable to solve
				return false; 
			
			return m.IsSolved;			
		}
	}
}
