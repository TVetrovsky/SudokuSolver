/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 24.09.2017
 * Time: 21:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;

namespace SudokuSolver
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public static class MatrixConsolePrinter
	{
		public static void Print(Matrix m)
		{
			SquareArray.Sudoku.IterateTwo((i, j) =>
			                              {
			                              	var st = m[i, j].GetValidStates().ToArray();
			                              	Console.Write(st.Length + "   ");
			                              	if(j==8)
			                              		Console.WriteLine();
			                              });
		}
	}
}
