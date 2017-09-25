/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 24.09.2017
 * Time: 16:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace SudokuSolver
{		
	public class Matrix
	{
		private readonly StateSpace[,] matrix = new StateSpace[MAX,MAX];
		private Action changeObserver = () => {};
		public Action ChangeObserver
		{
			set {
				if(value == null)
					throw new ArgumentNullException("value");
				   
				changeObserver = value;
			}
		}
		
		public const int MAX = 9;
		
		public Matrix()
		{			
			// disable once ConvertClosureToMethodGroup
			SquareArray.Sudoku.IterateTwo((int r, int c) => {matrix[r, c] = new StateSpace(() => changeObserver());});
		}
		
		public Matrix(int[,] matrix)
		{
			SquareArray.Sudoku.IterateTwo((int r, int c) => 
				{
                  	// disable once ConvertClosureToMethodGroup
                  	this.matrix[r, c] = new StateSpace(() => changeObserver());
                	if(matrix[r, c] > 0 )
                		this.matrix[r, c].ColapseTo(matrix[r, c]);
				});
		}
		
		public StateSpace this[int r, int c]
		{
			get{ return matrix[r, c];}
		}
		
	}
}
