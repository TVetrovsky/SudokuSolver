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
			var m = new Matrix(SudokuTemplates.Simple);
			
			if(Algorithms.StupidBackTracking.ApplyAll(ref m))
				Console.WriteLine("Solved: ");
			else
				Console.WriteLine("Not possible to continue from: ");
			
			MatrixConsolePrinter.Print(m);
						
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}