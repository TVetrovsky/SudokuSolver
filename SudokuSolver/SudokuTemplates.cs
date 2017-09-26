/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 26.09.2017
 * Time: 13:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace SudokuSolver
{
	/// <summary>
	/// Description of SudokuTemplates.
	/// </summary>
	public static class SudokuTemplates
	{
		public static int[,] template = new int[,]{
				{},
				{},
				{},
				{},
				{},
				{},
				{},
				{},
				{}
			};

		public static int[,] Simple = new int[,]{
				{0,0,0,0,1,0,0,0,0},
				{7,0,1,0,2,6,8,0,5},
				{3,0,6,0,7,0,0,9,0},
				{0,0,0,7,8,0,2,4,9},
				{4,0,0,0,0,0,0,0,1},
				{1,9,7,0,4,2,0,0,0},
				{0,6,0,0,9,0,4,0,2},
				{2,0,3,4,5,0,9,0,8},
				{0,0,0,0,6,0,0,0,0}
			};
			
		public static int[,] Hard = new int[,]{
				{0,5,3,0,2,0,0,0,0},
				{0,0,0,3,0,0,7,0,0},
				{4,0,0,9,0,0,0,3,6},
				{2,6,0,0,0,0,0,0,0},
				{0,0,7,1,0,8,2,0,0},
				{0,0,0,0,0,0,0,9,8},
				{7,3,0,0,0,2,0,0,4},
				{0,0,5,0,0,6,0,0,0},
				{0,0,0,0,1,0,5,6,0}
			};
		
	}
}
