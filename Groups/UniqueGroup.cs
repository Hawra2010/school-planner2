using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TimeTablePlanner.Collections
{
	public class UniqueGroup<T> : Collection<T>
	{
		public UniqueGroup()
		{
		}

		public UniqueGroup(IList<T> list) : base(list)
		{
		}

		protected override void ClearItems()
		{
			base.ClearItems();
		}

		protected override void InsertItem(int index, T item)
		{
			if (IndexOf(item) > -1)
			{
				throw new Exception("The given entry is already present in the collection, duplicates are not allowed");
			}
			else
			{
				base.InsertItem(index, item);
			}
		}

		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		protected override void SetItem(int index, T item)
		{
			throw new NotSupportedException();
		}
	}
}
