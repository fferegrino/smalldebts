using System;
namespace Smalldebts.Core.UI.Models
{
	public class TodoItem
	{
		public string Id { get; set; }
		public string Text { get; set; }
		public bool Complete { get; set; }
		public TodoItem()
		{
		}
	}
}
