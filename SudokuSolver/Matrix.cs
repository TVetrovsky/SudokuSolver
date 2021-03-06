﻿/*
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
using System.Collections;

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

		private Action invalidStateObserver = () => {};
		public Action InvalidStateObserver
		{
			set {
				if(value == null)
					throw new ArgumentNullException("value");
				   
				invalidStateObserver = value;
			}
		}

		
		public const int MAX = 9;
		
		#region Cell Celectors
		public static class CellSelectors
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
			SquareArray.Sudoku.IterateTwo((int r, int c) => 
			                              {
			                              	matrix[r, c] = new StateSpace(
			                              		() => changeObserver(),
			                              		() => invalidStateObserver());
			                              });
		}
		
		public Matrix(Matrix origin)
		{			
			// disable once ConvertClosureToMethodGroup
			SquareArray.Sudoku.IterateTwo((int r, int c) => 
			                              {
			                              	matrix[r, c] = new StateSpace(
			                              		origin.matrix[r,c], 
			                              		() => changeObserver(),
			                              		() => invalidStateObserver());
			                              	});
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
		
		public enum StateEnum
		{
			Invalid = -1,
			Valid = 0,
			Solved = 1
		}
		
		public StateEnum GetState()
		{
			// checks whether StateSpace is collapsed and the definite number is used just once against "storage"
			bool undefiniteValueExists = false;
			
			Func<BitArray, StateSpace, bool> checker = (storage, cell) =>
			{
				var possibleValues = cell.GetValidStates().ToArray();
				if(possibleValues.Length == 1) // cell value is already definite
				{
					if(storage[possibleValues[0]-1]) // the value already was there -> rules violation
						return false; 
					storage[possibleValues[0]-1] = true;						
				}
				else
					undefiniteValueExists = true;
				
				return true;					
			};

			// check rows and columns
			for(int i = 0 ; i<MAX; i++)
			{
				var col = new BitArray(MAX, false);
				var row = new BitArray(MAX, false);
				
				for(int j = 0; j < MAX; j++)
				{
					if(!checker(row, matrix[i, j]))
					   return StateEnum.Invalid;
					if(!checker(col, matrix[j, i]))
					   return StateEnum.Invalid;
				}
			}
			
			bool result = true;
			// check subsquares
			SquareArray.Three.IterateTwo(
				(base_i, base_j) =>
				{
					if(result)
					{
						var storage = new BitArray(MAX, false);
						
				    	SquareArray.Three.IterateTwo(
							(i, j) =>
							{
								if(!checker(storage, matrix[base_i*3 + i, base_j*3 + j]))
									result = false;									
							});
					}
				});
			
			if(!result)
				return StateEnum.Invalid;
			if(undefiniteValueExists)
				return StateEnum.Valid;
			return StateEnum.Solved;
		}		
	}
}
