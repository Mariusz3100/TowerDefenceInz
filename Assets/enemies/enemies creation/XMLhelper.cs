using System;
using System.Xml;
using UnityEngine;
using System.IO;
using System.Collections;

public class XMLhelper
{
	
	private XmlNode xmlNode;

	public XmlNode XmlNode {
		get {
			return this.xmlNode;
		}
		set {
			xmlNode = value;
		}
	}	
	
	public XmlNode[] getAllChildrenNamed(String name)
	{
		return XMLhelper.getAllChildrenNamed(name,xmlNode);
	}
	
	public static XmlNode[] getAllChildrenNamed(String name,XmlNode xmlNode)
	{
		XmlNode current=xmlNode.FirstChild;
		ArrayList arrayOfFoundChildren=new ArrayList();
		
		for(int i=0;i<xmlNode.ChildNodes.Count;i++, current=current.NextSibling)
		{
			if(current.Name==name)
			{
				arrayOfFoundChildren.Add(current);
			}
		
		}
		
		XmlNode[] ret=new XmlNode[arrayOfFoundChildren.Count];
		
		for(int i=0;i<arrayOfFoundChildren.Count;i++)
			ret[i]=(XmlNode)arrayOfFoundChildren[i];
		
		return ret;
		
	}
	
	
	
	public XMLhelper (XmlNode x)
	{
	xmlNode=x;
	}
}


