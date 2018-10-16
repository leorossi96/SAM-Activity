using System;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class rotateCamera{
		public float X;
		public float Y;

		public rotateCamera(){
		}

		public rotateCamera (float X, float Y){
			this.X = X;
			this.Y = Y;
		}

		public Dictionary<string, Object> toDictionary(){
			Dictionary <string, Object> result = new Dictionary<string, Object> ();
			result ["X"] = X;
			result ["Y"] = Y;

			return result;
		}
	}
}