/*
 * Created by SharpDevelop.
 * User: tomas
 * Date: 24.09.2017
 * Time: 19:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolver
{
	/// <summary>
	/// StateSpace - stores which values could be in a cell
	/// Only one value means that we are alreadz certain about cell value (definite value).
	/// </summary>
	public class StateSpace
	{
		BitArray bits;
		Action changeObserver;
		
		public StateSpace(Action changeObserver = null)
		{
			bits = new BitArray(Matrix.MAX,true);
			this.changeObserver = changeObserver;
		}
		
		private static IEnumerable<int> GetValidStates(BitArray bits)
		{
			for(int i = 0 ; i<bits.Length; i++)
				if(bits[i]) 
					yield return i+1;
		}
		
		public IEnumerable<int> GetValidStates()
		{
			lock(bits)
			{
				return GetValidStates(bits);
			}
		}
		
		/// <summary>
		/// Removes value from StateSpace
		/// </summary>
		public void SetInvalid(int val)
		{
			lock(bits)
			{
				if(bits[val-1] && changeObserver != null)
					changeObserver();
				
				bits.Set(val-1, false);
			}
		}

		/// <summary>
		/// Collapse StateState to just one definite values  
		/// </summary>
		public void CollapseTo(int val)
		{
			var newBits = new BitArray(Matrix.MAX, false);
			newBits.Set(val-1, true);
			
			lock(bits)
			{
				if(changeObserver != null && !GetValidStates(newBits).SequenceEqual(GetValidStates(bits)))
				   changeObserver();
				
				bits.And(newBits);
			}
		}
		
		/// <summary>
		/// Apply intersection of two StateSpaces to the current instance.
		/// </summary>
		public void Intersect(StateSpace s2)
		{
			lock(bits)
			{
				if(changeObserver != null && !GetValidStates().SequenceEqual(GetValidStates(s2.bits)))
				   changeObserver();
	
				bits.And(s2.bits);
			}
		}
		
		/// <summary>
		/// Prints valid value for this StateSpace (for debugging) 
		/// </summary>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach(var i in GetValidStates(bits)) sb.Append(i);
			return sb.ToString();
		}			
	}
}
