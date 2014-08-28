using System;
using System.Collections;
using System.Collections.Generic;

namespace ATL
{
	/// <summary>
	/// Describes a file format
	/// </summary>
	public class Format : IEnumerable
	{
		// Name of the format
		protected String fName;
		// ID of the format
		protected int fID;
		// List of file extensions proper to this format
		protected IList<String> extList;
		// true if the format is readable by ATL
		protected bool fReadable;

        public Format() { }

		public Format(String iName)
		{
            init(iName);
		}

        protected void init(String iName)
        {
            fName = iName;
			fReadable = true;
			extList = new List<String>();
        }

        protected void copyFrom(Format iFormat)
        {
            this.fName = iFormat.fName;
            this.fID = iFormat.fID;
            this.fReadable = iFormat.fReadable;
            this.extList = new List<String>(iFormat.extList);
        }

		public String Name
		{
			get { return fName; }
			set { fName = value; }
		}

		public int ID
		{
			get { return fID; }
			set { fID = value; }
		}

		public bool Readable
		{
			get { return fReadable; }
			set { fReadable = value; }
		}

		#region Code for IEnumerable implementation

		// NB : Same principle as in Collection		

		public IEnumerator GetEnumerator() 
		{
			return extList.GetEnumerator();
		}

		#endregion

		// Adds the extension ext to the extensions list of this Format
		public void AddExtension(String ext)
		{
			if ( !extList.Contains(ext.ToUpper()) )
				extList.Add(ext.ToUpper());
		}

		// Tests if the extension ext is a valid extension of the current Format
		public bool IsValidExtension(String ext)
		{
			return extList.Contains(ext.ToUpper());
		}
	}
}
