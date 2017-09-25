/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 24.09.2017
 * Time: 20:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
	public class SquareArray
	{		
		readonly int size;
		
		public static SquareArray Sudoku = new SquareArray(Matrix.MAX);
		public static SquareArray Three = new SquareArray(3);
		
		public SquareArray(int size)
		{
			this.size = size;
		}
		
		public void IterateTwo(Action<int, int> action)
		{
			for(int i = 0; i < size; i++)
				for(int j = 0 ; j < size; j++)
					action(i, j);
		}
		
		public void IterateSingle(Action<int> action)
		{			
			for(int j = 0 ; j < size; j++)
				action(j);
		}
	}
}
