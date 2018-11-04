using System;
using System.Collections.Generic;

namespace Digify
{
	public abstract class BaseDataStore <T> where T : class
	{
		// public abstract T All();

		public abstract void Clear();
		
		public abstract void Add(T t);

		public abstract void Remove(T t);

		public abstract long Size();

	}
}