/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 24.09.2017
 * Time: 16:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace SudokuSolver
{
	class Program
	{
		public static void Main(string[] args)
		{
			var m = new Matrix(SudokuTemplates.Hard);
			var result = Algorithms.StupidBackTrackingWithTPL.ApplyAll(m);
			
			if(result != null)
				Console.WriteLine("Solved: ");
			else 
				Console.WriteLine("Not possible to solve.");
			
			MatrixConsolePrinter.Print(result);
						
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}