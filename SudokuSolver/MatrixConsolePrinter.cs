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
			for(int i = 0; i < Matrix.MAX; i++)
			{
				for(int triple = 0 ; triple <3; triple++)
				{
					for(int j = 0; j < Matrix.MAX; j++)
					{
						var states = m[i, j].GetValidStates().Skip(triple*3).Take(3).ToArray();
						
						Console.Write(String.Join("", states).PadRight(3));
						if((j % 3) == 2)
							Console.Write(" * ");
						else
							Console.Write(" | ");
					}
					Console.WriteLine();
				}
				if((i % 3) == 2)
				   	Console.WriteLine(String.Empty.PadRight(6*9, '*'));
				else
					Console.WriteLine(String.Empty.PadRight(6*9, '-'));
			}
		}
	}
}
