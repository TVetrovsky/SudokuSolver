/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 24.09.2017
 * Time: 16:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Collections.Generic;

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
		
		#region Cell Celectors
		public class CellSelectors
		{
			public static IEnumerable<StateSpace> SelectRowCells(Matrix m, int row)
			{
				return Enumerable.Range(0, Matrix.MAX).Select((i) => m[row, i]);			
			}
			
			public static IEnumerable<StateSpace> SelectColCells(Matrix m, int col)
			{
				return Enumerable.Range(0, Matrix.MAX).Select((i) => m[i, col]);
			}
			
			public static IEnumerable<StateSpace> SelectSquareCells(Matrix m, int row, int col)
			{
				int baseRow = row / 3;
				int baseCol = col / 3;
			
				baseRow *= 3;
				baseCol *= 3;
			
				return new StateSpace[]{
					m[baseRow, baseCol], m[baseRow, baseCol+1], m[baseRow, baseCol+2],  
					m[baseRow+1, baseCol], m[baseRow+1, baseCol+1], m[baseRow+1, baseCol+2],
					m[baseRow+2, baseCol], m[baseRow+2, baseCol+1], m[baseRow+2, baseCol+2]};			
			}
		}
		#endregion		
		
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
                		this.matrix[r, c].CollapseTo(matrix[r, c]);
				});
		}
		
		public StateSpace this[int r, int c]
		{
			get{ return matrix[r, c];}
		}
		
	}
}
